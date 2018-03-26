using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using CrimeMap.Core.Events;
using CrimeMap.Core.Storage;
using MySql.Data.MySqlClient;

namespace CrimeMap.EventStore {

	public class MySqlEventStore : IEventStore {

		private static readonly string connectionStringName = ConfigurationManager.AppSettings.Get("crimeMap.connectionStringName");
		private static readonly string connectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;

		private readonly IEventSerializer _eventSerializer;

		public MySqlEventStore(IEventSerializer eventSerializer) {
			_eventSerializer = eventSerializer;
		}

		public EventStream GetEvents(Guid id, long minVersion, long maxVersion) {

			const string selectEventsByVersion = @"
				SELECT el.EventSourceId, el.Data, el.Version FROM EventLog el 
				WHERE el.EventSourceId = @eventsourceId AND el.Version >= @minVersion AND el.Version <= @maxVersion ORDER BY el.Version";

			var events = new List<IEvent>();

			using (var connection = new MySqlConnection(connectionString)) {
				using (var command = connection.CreateCommand()) {

					command.CommandText = selectEventsByVersion;
					command.Parameters.AddWithValue("@eventsourceId", id);
					command.Parameters.Add("@minVersion", MySqlDbType.Int64).Value = minVersion;
					command.Parameters.Add("@maxVersion", MySqlDbType.Int64).Value = maxVersion;

					connection.Open();
					var reader = command.ExecuteReader();

					while (reader.Read()) {
						byte[] data = reader[1] as byte[];
						IEvent @event = _eventSerializer.DeserializeEvent(data);
						events.Add(@event);
					}

				}
			}

			if (!events.Any()) {
				return EventStream.Empty();
			}

			var fromVersion = events.First().Version;
			var toVersion = events.Last().Version;

			return new EventStream(id, events, fromVersion, toVersion);
		}

		public void Store(IEventProvider eventProvider) {
			var eventStream = eventProvider.UncommitedEvents;

			if (!eventStream.Any()) {
				return;
			}

			var version = GetVersion(eventProvider);

			if (version != eventProvider.Version) {
				throw new ConcurrencyException(eventProvider.Id, version);
			}

			foreach (var @event in eventStream) {
				this.SaveEvent(eventProvider, @event);
			}

			UpdateAggregateVersion(eventProvider);
		}

		public Snapshot GetSnapshot(Guid id) {

			const string selectSnapshot = @"
				SELECT s.Data FROM SnapShot s 
				WHERE s.EventSourceId = @eventsourceId ORDER BY s.Version DESC LIMIT 1;";

			using (var connection = new MySqlConnection(connectionString)) {
				using (var command = connection.CreateCommand()) {
					command.CommandText = selectSnapshot;
					command.Parameters.Add("@eventsourceId", MySqlDbType.Guid).Value = id;

					connection.Open();

					var data = (byte[])command.ExecuteScalar();

					if (data == null) {
						return null;
					}

					var snapshot = _eventSerializer.DeserializeSnapshot(data);

					return snapshot;
				}
			}
		}

		public void SaveSnapshot(Snapshot memento) {
			throw new NotImplementedException();
		}

		private void SaveEvent(IEventProvider eventProvider, IEvent @event) {

			const string saveEventCmd = @"
				INSERT INTO EventLog (Id, Name, Type, EventSourceId, Timestamp, Version, SourceVersion, Data, StoredOn) 
				VALUES (@id, @name, @type, @eventsourceId, @timestamp, @version, @sourceVersion, @data, @storedOn);";

			using (var connection = new MySqlConnection(connectionString)) {
				using (var command = connection.CreateCommand()) {
					command.CommandText = saveEventCmd;

					command.Parameters.Add("@id", MySqlDbType.Guid).Value = @event.Identifier;
					command.Parameters.Add("@name", MySqlDbType.VarChar).Value = @event.EventName;
					command.Parameters.Add("@type", MySqlDbType.VarChar).Value = @event.Type;
					command.Parameters.Add("@eventsourceId", MySqlDbType.Guid).Value = eventProvider.Id;
					command.Parameters.Add("@timestamp", MySqlDbType.DateTime).Value = @event.Timestamp;
					command.Parameters.Add("@version", MySqlDbType.Int64).Value = @event.Version;
					command.Parameters.Add("@sourceVersion", MySqlDbType.VarChar).Value = @event.SourceVersion;
					command.Parameters.Add("@data", MySqlDbType.LongBlob).Value = _eventSerializer.SerializeEvent(@event);
					command.Parameters.Add("@storedOn", MySqlDbType.DateTime).Value = DateTime.Now;

					connection.Open();
					command.ExecuteNonQuery();
				}
			}
		}

		private static int GetVersion(IEventProvider eventProvider) {

			const string selectOrInsertEventsource = @"
				INSERT IGNORE INTO EventSource VALUES (@id, @name, @type, 0); 
				SELECT Version FROM EventSource WHERE Id = @id";

			var aggregateType = eventProvider.GetType();

			using (var connection = new MySqlConnection(connectionString)) {
				using (var command = connection.CreateCommand()) {
					command.CommandText = selectOrInsertEventsource;
					command.Parameters.Add("@id", MySqlDbType.Guid).Value = eventProvider.Id;
					command.Parameters.Add("@name", MySqlDbType.VarChar).Value = aggregateType.Name;
					command.Parameters.Add("@type", MySqlDbType.VarChar).Value = aggregateType.FullName;

					connection.Open();
					var queryResult = command.ExecuteScalar();
					var version = Convert.ToInt32(queryResult);

					return version;
				}
			}
		}

		private static void UpdateAggregateVersion(IEventProvider eventProvider) {

			const string updateProviderVersion = @"UPDATE EventSource SET Version = @version WHERE Id = @id;";

			eventProvider.UpdateVersion();

			using (var connection = new MySqlConnection(connectionString)) {
				using (var command = connection.CreateCommand()) {
					command.CommandText = updateProviderVersion;
					command.Parameters.Add("@id", MySqlDbType.Guid).Value = eventProvider.Id;
					command.Parameters.Add("@version", MySqlDbType.Int64).Value = eventProvider.Version;

					connection.Open();
					command.ExecuteNonQuery();
				}
			}
		}

	}

}
