using System;
using System.Collections.Generic;
using CrimeMap.Core.Events;

namespace CrimeMap.Core.Domain {

	public abstract class AggregateRoot : IEventProvider {

		private ICollection<IEvent> _uncommitedEvents;
		private IDictionary<Type, Action<IEvent>> _eventRoutes;

		protected AggregateRoot() {
			_uncommitedEvents = new List<IEvent>();
			_eventRoutes = new Dictionary<Type, Action<IEvent>>();
		}

		public Guid Id { get; protected set; }

		public long Version { get; protected set; }

		public long CurrentVersion { get; protected set; }

		public IEnumerable<IEvent> UncommitedEvents {
			get { return _uncommitedEvents; }
		}

		public void ClearEvents() {
			_uncommitedEvents.Clear();
		}

		public void LoadFromEventStream(EventStream eventStream) {
			if (eventStream.IsEmpty()) {
				return;
			}

			foreach (var @event in eventStream.Events) {
				ApplyEvent(@event);
			}

			Version = eventStream.LastVersion;
			CurrentVersion = Version;
		}

		public void UpdateVersion() {
			this.Version = this.Version + this.CurrentVersion;
		}

		protected void RaiseEvent(IEvent @event) {
			CurrentVersion++;
			@event.Version = CurrentVersion;
			@event.EventSourceId = this.Id;

			ApplyEvent(@event);
			_uncommitedEvents.Add(@event);
		}

		protected void ApplyEvent(IEvent @event) {
			var eventType = @event.GetType();
			Action<IEvent> action;

			if (!_eventRoutes.TryGetValue(eventType, out action)) {
				throw new EventNotRegisteredException(@event, this._uncommitedEvents);
			}

			action(@event);
		}

		protected void RegisterEvent<T>(Action<T> eventRoute) where T : class, IEvent {
			_eventRoutes.Add(typeof(T), e => eventRoute(e as T));
		}

	}

}
