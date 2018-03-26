using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PogStore.Cms.Core.Framework.Messages
{
	[DataContract]
	public class ErrorResponse : BaseResponse
	{
		[DataMember]
		public int Code { get; private set; }

		[DataMember]
		public string Exception { get; private set; }

		[DataMember]
		public string DetailsUrl { get; private set; }

		[DataMember]
		public IEnumerable<ValidationMessage> Details { get; private set; }

		public ErrorResponse(int code, string message, string exception, string detaisUrl, IEnumerable<ValidationMessage> details)
		{
			this.Code = code;
			this.Message = message;
			this.Exception = exception;
			this.DetailsUrl = detaisUrl;
			this.Details = details;

			this.Ack = AckType.FAILURE;
		}
	}
}