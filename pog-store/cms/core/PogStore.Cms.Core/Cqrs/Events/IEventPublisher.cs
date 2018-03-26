using System.Collections.Generic;

namespace PogStore.Cms.Core.Cqrs.Events
{
	public interface IEventPublisher
	{
		void Publish<T>(T @event)
			where T : class, IEvent;

		void Publish(IEnumerable<IEvent> eventCollection);

		void PublishNow<T>(T @event)
			where T : class, IEvent;
	}
}