using CrimeMap.Core.Domain;
using CrimeMap.Core.Events;

namespace CrimeMap.Core.Storage {

	public interface IEventSerializer {

		byte[] SerializeEvent(IEvent @event);

		IEvent DeserializeEvent(byte[] data);

		Snapshot DeserializeSnapshot(byte[] data);

	}

}
