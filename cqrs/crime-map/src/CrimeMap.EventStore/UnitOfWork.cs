using System;
using System.Collections.Generic;
using CrimeMap.Core.Domain;
using CrimeMap.Core.Events;
using CrimeMap.Core.Storage;

namespace CrimeMap.EventStore {

	public class UnitOfWork : IUnitOfWork {

		private readonly IEventStore _eventStore;
		private readonly IDomainRepository _domainRepository;
		private readonly IEventPublisher _eventPublisher;

		private List<IEventProvider> _trackedEventProviders;
		private bool _isDisposed = false;

		public UnitOfWork(IEventStore eventStore, IDomainRepository domainRepository, IEventPublisher eventPublisher) {
			_eventStore = eventStore;
			_domainRepository = domainRepository;
			_eventPublisher = eventPublisher;
			_trackedEventProviders = new List<IEventProvider>();
		}

		public void Track(IEventProvider eventProvider) {
			if (_trackedEventProviders.Exists(x => x.Id == eventProvider.Id)) {
				return;
			}

			_trackedEventProviders.Add(eventProvider);
		}

		public T Get<T>(Guid id) where T : AggregateRoot, new() {
			var aggregate = _domainRepository.Get<T>(id);
			Track(aggregate);

			return aggregate;
		}

		public void Add(AggregateRoot aggregateRoot) {
			Track(aggregateRoot);
		}

		public void Commit() {
			try {

				foreach (var eventProvider in _trackedEventProviders) {
					_eventStore.Store(eventProvider);
					_eventPublisher.Publish(eventProvider.UncommitedEvents);

					eventProvider.ClearEvents();
				}

				_trackedEventProviders.Clear();

			} catch {
				Rollback();
				throw;
			}
		}

		public void Rollback() {
			_trackedEventProviders.Clear();
		}

		public void Dispose() {
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool isDisposing) {
			if (!this._isDisposed) {
				if (isDisposing) {
				}
			}

			this._isDisposed = true;
		}

	}

}
