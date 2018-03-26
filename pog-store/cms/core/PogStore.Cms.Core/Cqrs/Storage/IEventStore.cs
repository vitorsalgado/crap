using PogStore.Cms.Core.Cqrs.Events;
using System;

namespace PogStore.Cms.Core.Cqrs.Storage
{
	public interface IEventStore
	{
		EventStream GetEvents(Guid id, long minVersion, long maxVersion);

		void Store(IEventProvider eventProvider);

		Snapshot GetSnapshot(Guid id);

		void SaveSnapshot(Snapshot snapshot);
	}
}