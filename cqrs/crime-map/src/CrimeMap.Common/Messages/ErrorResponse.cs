using System.Runtime.Serialization;
using System.Text;

namespace CrimeMap.Common {

	[DataContract]
	public class ErrorResponse : BaseResponse {

		public ErrorResponse() { }
		public ErrorResponse(string correlationId) : base(correlationId) { }

		[DataMember]
		public int Code { get; set; }

		[DataMember]
		public string Exception { get; set; }

		[DataMember]
		public string DetailsUrl { get; set; }

		[DataMember]
		public string Details { get; set; }

		public override string ToString() {
			StringBuilder error = new StringBuilder();

			error.AppendLine("Error Response")
				.Append("Code: ").AppendLine(Code.ToString())
				.Append("Details: ").AppendLine(Details)
				.Append("Details Url: ").AppendLine(DetailsUrl)
				.Append("Exception: ").AppendLine(Exception);

			return error.ToString();
		}

	}
}
