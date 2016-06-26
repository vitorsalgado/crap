using System;
using System.Collections.Generic;

namespace PogStore.Cms.Core.Cqrs.Events
{
	public interface IEventProvider
	{
		Guid Id { get; }

		long Version { get; }

		IEnumerable<IEvent> UncommitedEvents { get; }

		void ClearEvents();

		void LoadFromEventStream(EventStream eventStream);

		void UpdateVersion();
	}
}