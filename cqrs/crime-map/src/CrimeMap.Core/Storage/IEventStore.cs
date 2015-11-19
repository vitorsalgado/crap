using System;
using CrimeMap.Core.Domain;
using CrimeMap.Core.Events;

namespace CrimeMap.Core.Storage {

	public interface IEventStore {

		EventStream GetEvents(Guid id, long minVersion, long maxVersion);

		void Store(IEventProvider eventProvider);

		Snapshot GetSnapshot(Guid id);

		void SaveSnapshot(Snapshot snapshot);

	}

}
