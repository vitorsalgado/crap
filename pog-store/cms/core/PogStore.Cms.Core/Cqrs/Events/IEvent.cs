using System;

namespace PogStore.Cms.Core.Cqrs.Events
{
	public interface IEvent
	{
		Guid Identifier { get; }

		string Name { get; }

		string Type { get; }

		Guid EventSourceId { get; set; }

		DateTime Timestamp { get; }

		long Version { get; set; }

		string SourceVersion { get; }
	}
}