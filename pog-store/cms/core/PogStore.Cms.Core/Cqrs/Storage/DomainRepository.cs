using PogStore.Cms.Core.Cqrs.Domain;
using PogStore.Cms.Core.Cqrs.Events;
using System;

namespace PogStore.Cms.Core.Cqrs.Storage
{
	public class DomainRepository : IDomainRepository
	{
		private readonly IEventStore _eventStore;
		private readonly IUnitOfWork _unitOfWork;

		public DomainRepository(IEventStore eventStore, IUnitOfWork unitOfWork)
		{
			_eventStore = eventStore;
			_unitOfWork = unitOfWork;
		}

		public T Get<T>(Guid id) where T : AggregateRoot, new()
		{
			var snapshot = _eventStore.GetSnapshot(id);
			var aggregate = new T();

			long maxVersion = long.MaxValue;
			long minVersion = long.MinValue;

			if (snapshot != null)
			{
				minVersion = snapshot.Version + 1;

				if (aggregate is IOriginator)
				{
					((IOriginator)aggregate).SetSnapshot(snapshot);
				}
			}

			var eventStream = _eventStore.GetEvents(id, minVersion, maxVersion);

			if (eventStream.IsEmpty())
			{
				return null;
			}

			aggregate.LoadFromEventStream(eventStream);

			_unitOfWork.Track(aggregate);

			return aggregate;
		}

		public void Store(AggregateRoot aggregateRoot)
		{
			_unitOfWork.Track(aggregateRoot);
		}
	}
}