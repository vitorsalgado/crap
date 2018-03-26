using System.Runtime.Serialization;

namespace PogStore.Cms.Core.Framework
{
	[DataContract]
	public abstract class BaseRequest
	{
		[DataMember]
		public string Identifier { get; set; }
	}
}