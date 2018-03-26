using System.Text;
using CrimeMap.Core.Events;
using CrimeMap.Core.Storage;
using Newtonsoft.Json;

namespace CrimeMap.EventStore {

	public class JsonEventSerializer : IEventSerializer {

		private JsonSerializerSettings _settings;

		public JsonEventSerializer() {
			_settings = new JsonSerializerSettings();
			_settings.TypeNameHandling = TypeNameHandling.All;
		}

		public byte[] SerializeEvent(IEvent @event) {
			string jsonEvent = JsonConvert.SerializeObject(@event, _settings);
			return Encoding.ASCII.GetBytes(jsonEvent);
		}

		public IEvent DeserializeEvent(byte[] data) {
			string jsonString = Encoding.ASCII.GetString(data);
			return (IEvent)JsonConvert.DeserializeObject(jsonString, _settings);
		}

		public Snapshot DeserializeSnapshot(byte[] data) {
			string jsonString = Encoding.ASCII.GetString(data);
			return (Snapshot)JsonConvert.DeserializeObject(jsonString, _settings);
		}

	}

}
