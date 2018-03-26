using System.Runtime.Serialization;
using CrimeMap.Common;

namespace CrimeMap.ApplicationServices.Users.Messages {

	[DataContract]
	public class UserRequest : BaseRequest {

		[DataMember]
		public string Username { get; set; }

	}

}
