using System.Configuration;
using MongoDB.Driver;

namespace CrimeMap.MongoDb {

	public static class MongoDbConnector {

		private static readonly string connectionString = ConfigurationManager.ConnectionStrings["crimemap.mongodb"].ConnectionString;
		private static MongoClient _mongoClient;

		public static MongoDatabase Get() {

			if (_mongoClient == null) {
				_mongoClient = new MongoClient(connectionString);
			}

			var server = _mongoClient.GetServer();
			var database = server.GetDatabase("crimemap");

			return database;
		}

	}
}
