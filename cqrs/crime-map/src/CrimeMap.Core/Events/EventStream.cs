using System;
using System.Collections.Generic;

namespace CrimeMap.Core.Events {

	public class EventStream {

		public Guid EventSourceId { get; private set; }

		public IEnumerable<IEvent> Events { get; private set; }

		public long StartVersion { get; private set; }

		public long LastVersion { get; private set; }

		private bool _empty;

		public EventStream(Guid eventsourceId, IEnumerable<IEvent> events, long fromVersion, long toVersion) {
			EventSourceId = eventsourceId;
			Events = events;
			StartVersion = fromVersion;
			LastVersion = toVersion;
			_empty = false;
		}

		private EventStream() {
			Events = new List<IEvent>();
			_empty = true;
		}

		public static EventStream Empty() {
			return new EventStream();
		}

		public bool IsEmpty() {
			return _empty;
		}

	}

}
