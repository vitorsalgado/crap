using System.Runtime.Serialization;
using CrimeMap.Common;

namespace CrimeMap.ApplicationServices.Users.Messages {

	[DataContract]
	public class LoginRequest : BaseRequest {

		[DataMember]
		public string Username { get; set; }

		[DataMember]
		public string Password { get; set; }

	}
}
