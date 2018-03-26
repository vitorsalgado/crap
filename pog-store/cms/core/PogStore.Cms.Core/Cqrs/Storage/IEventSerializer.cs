using PogStore.Cms.Core.Cqrs.Events;

namespace PogStore.Cms.Core.Cqrs.Storage
{
	public interface IEventSerializer
	{
		byte[] SerializeEvent(IEvent @event);

		IEvent DeserializeEvent(byte[] data);

		Snapshot DeserializeSnapshot(byte[] data);
	}
}