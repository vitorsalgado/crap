using System.Collections.Generic;
using CrimeMap.Core.Events;
using MassTransit;

namespace CrimeMap.EventHandlers.Infrastructure {

	public class MassTransitEventPublisher : IEventPublisher {

		private IServiceBus _serviceBus;

		public MassTransitEventPublisher(IServiceBus serviceBus) {
			_serviceBus = serviceBus;
		}

		public void Publish<T>(T @event)
			where T : class, IEvent {
			_serviceBus.Publish(@event, @event.GetType());
		}

		public void Publish(IEnumerable<IEvent> eventCollection) {
			foreach (var @event in eventCollection) {
				this.Publish(@event);
			}
		}

	}

}
