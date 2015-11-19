using System.Collections.Generic;

namespace CrimeMap.Core.Events {

	public interface IEventPublisher {

		void Publish<T>(T @event)
			where T : class, IEvent;

		void Publish(IEnumerable<IEvent> eventCollection);

	}

}
