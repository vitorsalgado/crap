using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;

namespace CrimeMap.Common {

	[DataContract(Namespace = "http://www.crimemap.com.br/messages")]
	public class BaseResponse {

		private List<ValidationMessage> _validationMessages;

		public BaseResponse(string correlationId)
			: this() {
			CorrelationId = correlationId;
		}

		public BaseResponse() {
			_validationMessages = new List<ValidationMessage>();
			Ack = AckType.SUCCESS;
		}

		[DataMember]
		public string CorrelationId { get; protected set; }

		[DataMember]
		public AckType Ack { get; set; }

		[DataMember]
		public string Message { get; set; }

		[DataMember]
		public List<ValidationMessage> ValidationMessages {
			get {
				return _validationMessages;
			}
		}

		[DataMember]
		public string Version {
			get {
				return string.Format("{0}.{1}",
					CurrentAssemblyName.Version.Major,
					CurrentAssemblyName.Version.Minor);
			}
		}

		[DataMember]
		public string Build {
			get {
				return CurrentAssemblyName.Version.Build.ToString();
			}
		}

		public string GetFormatedValidation() {
			var result = new StringBuilder();

			foreach (var message in ValidationMessages) {
				result.Append(message.Message).AppendLine();
			}

			return result.ToString();
		}

		public void SetFailureResponse(string message) {
			Ack = AckType.FAILURE;
			Message = message;
		}

		[IgnoreDataMember]
		private static AssemblyName CurrentAssemblyName {
			get {
				return Assembly.GetExecutingAssembly().GetName();
			}
		}
	}
}
