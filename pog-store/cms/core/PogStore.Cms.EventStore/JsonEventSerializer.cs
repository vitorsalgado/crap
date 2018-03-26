using Newtonsoft.Json;
using PogStore.Cms.Core.Cqrs.Events;
using PogStore.Cms.Core.Cqrs.Storage;
using System.Text;

namespace PogStore.EventStore
{
	public class JsonEventSerializer : IEventSerializer
	{
		private JsonSerializerSettings _settings;

		public JsonEventSerializer()
		{
			_settings = new JsonSerializerSettings();
			_settings.TypeNameHandling = TypeNameHandling.All;
		}

		public byte[] SerializeEvent(IEvent @event)
		{
			var jsonEvent = JsonConvert.SerializeObject(@event, _settings);
			return Encoding.ASCII.GetBytes(jsonEvent);
		}

		public IEvent DeserializeEvent(byte[] data)
		{
			var jsonString = Encoding.ASCII.GetString(data);
			return (IEvent)JsonConvert.DeserializeObject(jsonString, _settings);
		}

		public Snapshot DeserializeSnapshot(byte[] data)
		{
			var jsonString = Encoding.ASCII.GetString(data);
			return (Snapshot)JsonConvert.DeserializeObject(jsonString, _settings);
		}
	}
}