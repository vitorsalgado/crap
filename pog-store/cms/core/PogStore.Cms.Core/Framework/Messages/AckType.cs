using System.Runtime.Serialization;

namespace PogStore.Cms.Core.Framework
{
	[DataContract]
	public enum AckType
	{
		[EnumMember]
		SUCCESS = 0,

		[EnumMember]
		FAILURE = 1
	}
}