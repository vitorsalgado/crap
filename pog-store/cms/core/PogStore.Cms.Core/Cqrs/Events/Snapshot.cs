using System;

namespace PogStore.Cms.Core.Cqrs.Events
{
	public abstract class Snapshot
	{
		public Guid Id { get; set; }

		public int Version { get; set; }
	}
}