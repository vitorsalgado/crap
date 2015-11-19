using System;
using System.Runtime.Serialization;

namespace CrimeMap.DataTransferObjects.Users {

	[DataContract]
	public class UserLoginDto {

		[DataMember]
		public Guid UserId { get; set; }

		[DataMember]
		public string Name { get; set; }

		[DataMember]
		public string Email { get; set; }

	}
}
