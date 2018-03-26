using System.Runtime.Serialization;

namespace CrimeMap.Common {

	[DataContract]
	public enum AckType {

		[EnumMember]
		FAILURE = 0,

		[EnumMember]
		SUCCESS = 1,
	}
}
