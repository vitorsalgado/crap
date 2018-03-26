using System;

namespace CrimeMap.Core.Events {
	
	public abstract class Snapshot {

		public Guid Id { get; set; }

		public int Version { get; set; }

	}

}
