using System.Runtime.Serialization;

namespace CrimeMap.Common.Messages {

	[DataContract]
	public class BaseResponse<T> : BaseResponse {

		public BaseResponse(string correlationId)
			: base() {
			CorrelationId = correlationId;
		}

		[DataMember]
		public T Data { get; set; }

	}

}
