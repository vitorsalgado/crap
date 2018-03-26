using System.Runtime.Serialization;

namespace PogStore.Cms.Core.Framework
{
	[DataContract]
	public class ValidationMessage
	{
		[DataMember]
		public string Message { get; private set; }

		[DataMember]
		public string Description { get; private set; }

		public ValidationMessage(string message)
			: this(message, string.Empty)
		{
		}

		public ValidationMessage(string message, string description)
		{
			this.Message = message;
			this.Description = description;
		}
	}
}