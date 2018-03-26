using System;

namespace CrimeMap.Core.Events {

	public interface IEvent {

		Guid Identifier { get; }

		string EventName { get; }

		string Type { get; }

		Guid EventSourceId { get; set; }

		DateTime Timestamp { get; }

		long Version { get; set; }

		string SourceVersion { get; }

	}

}
