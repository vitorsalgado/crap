using System;
using CrimeMap.Core.Domain;
using CrimeMap.Core.Events;

namespace CrimeMap.Core.Storage {

	public class DomainRepository : IDomainRepository {

		private readonly IEventStore _eventStore;

		public DomainRepository(IEventStore eventStore) {
			_eventStore = eventStore;
		}

		public T Get<T>(Guid id) where T : AggregateRoot, new() {

			var snapshot = _eventStore.GetSnapshot(id);
			var aggregate = new T();

			long maxVersion = long.MaxValue;
			long minVersion = long.MinValue;

			if (snapshot != null) {
				minVersion = snapshot.Version + 1;

				if (aggregate is IOriginator) {
					((IOriginator)aggregate).SetSnapshot(snapshot);
				}
			}

			var eventStream = _eventStore.GetEvents(id, minVersion, maxVersion);

			if (eventStream.IsEmpty()) {
				return null;
			}

			aggregate.LoadFromEventStream(eventStream);

			return aggregate;
		}

		public void Store(AggregateRoot aggregateRoot) {
			_eventStore.Store(aggregateRoot);
		}

	}

}
