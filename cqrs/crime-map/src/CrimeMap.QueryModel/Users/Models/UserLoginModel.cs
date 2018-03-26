using System;
using System.Runtime.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CrimeMap.QueryModel.Users.Models {

	[DataContract]
	public class UserLoginModel {

		[BsonId]
		public ObjectId Id { get; set; }

		[DataMember]
		public Guid UserId { get; set; }

		[DataMember]
		public string Name { get; set; }

		[DataMember]
		public string Email { get; set; }

		[DataMember]
		public string EncryptedPassword { get; set; }

		[DataMember]
		public string Salt { get; set; }

	}
}
