using PogStore.Cms.Core.Cqrs.Events;
using PogStore.Cms.Core.Cqrs.Storage;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace PogStore.EventStore
{
	public class SqlServerEventStore : IEventStore
	{
		private static readonly string connectionStringName = ConfigurationManager.AppSettings.Get("pogstore.connectionStringName");
		private static readonly string connectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;

		private readonly IEventSerializer _eventSerializer;

		public SqlServerEventStore(IEventSerializer eventSerializer)
		{
			_eventSerializer = eventSerializer;
		}

		public EventStream GetEvents(Guid id, long minVersion, long maxVersion)
		{
			const string selectEventsByVersion = @"
				SET NOCOUNT ON; SELECT el.EventSourceId, el.Data, el.Version FROM EventLog el
				WHERE el.EventSourceId = @eventsourceId AND el.Version >= @minVersion AND el.Version <= @maxVersion ORDER BY el.Version";

			var events = new List<IEvent>();

			using (var connection = new SqlConnection(connectionString))
			{
				using (var command = connection.CreateCommand())
				{
					command.CommandText = selectEventsByVersion;
					command.Parameters.Add("@eventsourceId", SqlDbType.UniqueIdentifier).Value = id;
					command.Parameters.Add("@minVersion", SqlDbType.BigInt).Value = minVersion;
					command.Parameters.Add("@maxVersion", SqlDbType.BigInt).Value = maxVersion;

					connection.Open();
					var reader = command.ExecuteReader();

					while (reader.Read())
					{
						byte[] data = reader[1] as byte[];
						IEvent @event = _eventSerializer.DeserializeEvent(data);
						events.Add(@event);
					}
				}
			}

			if (!events.Any())
			{
				return EventStream.Empty();
			}

			var fromVersion = events.First().Version;
			var toVersion = events.Last().Version;

			return new EventStream(id, events, fromVersion, toVersion);
		}

		public void Store(IEventProvider eventProvider)
		{
			var eventStream = eventProvider.UncommitedEvents;

			if (!eventStream.Any())
			{
				return;
			}

			var version = GetVersion(eventProvider);

			if (version != eventProvider.Version)
			{
				throw new ConcurrencyException(eventProvider.Id, version);
			}

			foreach (var @event in eventStream)
			{
				this.SaveEvent(eventProvider, @event);
			}

			UpdateAggregateVersion(eventProvider);
		}

		public Snapshot GetSnapshot(Guid id)
		{
			const string selectSnapshot = @"
				SET NOCOUNT ON; SELECT TOP 1 s.Data FROM SnapShot s
				WHERE s.EventSourceId = @eventsourceId ORDER BY s.Version DESC;";

			using (var connection = new SqlConnection(connectionString))
			{
				using (var command = connection.CreateCommand())
				{
					command.CommandText = selectSnapshot;
					command.Parameters.Add("@eventsourceId", SqlDbType.UniqueIdentifier).Value = id;

					connection.Open();

					var data = (byte[])command.ExecuteScalar();

					if (data == null)
					{
						return null;
					}

					var snapshot = _eventSerializer.DeserializeSnapshot(data);

					return snapshot;
				}
			}
		}

		public void SaveSnapshot(Snapshot memento)
		{
			throw new NotImplementedException();
		}

		private void SaveEvent(IEventProvider eventProvider, IEvent @event)
		{
			const string saveEventCmd = @"
				INSERT INTO EventLog (Id, Name, Type, EventSourceId, Timestamp, Version, SourceVersion, Data)
				VALUES (@id, @name, @type, @eventsourceId, @timestamp, @version, @sourceVersion, @data);";

			using (var connection = new SqlConnection(connectionString))
			{
				using (var command = connection.CreateCommand())
				{
					command.CommandText = saveEventCmd;

					command.Parameters.Add("@id", SqlDbType.UniqueIdentifier).Value = @event.Identifier;
					command.Parameters.Add("@name", SqlDbType.VarChar).Value = @event.Name;
					command.Parameters.Add("@type", SqlDbType.VarChar).Value = @event.Type;
					command.Parameters.Add("@eventsourceId", SqlDbType.UniqueIdentifier).Value = eventProvider.Id;
					command.Parameters.Add("@timestamp", SqlDbType.DateTime).Value = @event.Timestamp;
					command.Parameters.Add("@version", SqlDbType.BigInt).Value = @event.Version;
					command.Parameters.Add("@sourceVersion", SqlDbType.VarChar).Value = @event.SourceVersion;
					command.Parameters.Add("@data", SqlDbType.VarBinary).Value = _eventSerializer.SerializeEvent(@event);

					connection.Open();
					command.ExecuteNonQuery();
				}
			}
		}

		private static int GetVersion(IEventProvider eventProvider)
		{
			const string selectOrInsertEventsource = @"
				SET NOCOUNT ON;
				IF NOT EXISTS(SELECT 1 FROM EventSource WHERE Id = @id) BEGIN INSERT INTO EventSource VALUES (@id, @name, @type, 0) END;
				SELECT Version FROM EventSource WHERE Id = @id";

			var aggregateType = eventProvider.GetType();

			using (var connection = new SqlConnection(connectionString))
			{
				using (var command = connection.CreateCommand())
				{
					command.CommandText = selectOrInsertEventsource;
					command.Parameters.Add("@id", SqlDbType.UniqueIdentifier).Value = eventProvider.Id;
					command.Parameters.Add("@name", SqlDbType.VarChar).Value = aggregateType.Name;
					command.Parameters.Add("@type", SqlDbType.VarChar).Value = aggregateType.FullName;

					connection.Open();
					var queryResult = command.ExecuteScalar();
					var version = Convert.ToInt32(queryResult);

					return version;
				}
			}
		}

		private static void UpdateAggregateVersion(IEventProvider eventProvider)
		{
			const string updateProviderVersion = @"UPDATE EventSource SET Version = @version WHERE Id = @id;";

			eventProvider.UpdateVersion();

			using (var connection = new SqlConnection(connectionString))
			{
				using (var command = connection.CreateCommand())
				{
					command.CommandText = updateProviderVersion;
					command.Parameters.Add("@id", SqlDbType.UniqueIdentifier).Value = eventProvider.Id;
					command.Parameters.Add("@version", SqlDbType.BigInt).Value = eventProvider.Version;

					connection.Open();
					command.ExecuteNonQuery();
				}
			}
		}
	}
}