using System;
using System.Runtime.Serialization;
using System.Text;
using CrimeMap.Common;

namespace CrimeMap.ApiClient {

	[Serializable]
	public class ProxyException : Exception {

		private string _message;
		public ErrorResponse ErrorResponse { get; private set; }

		public override string Message {
			get {
				return _message;
			}
		}

		public ProxyException() {
		}

		public ProxyException(ErrorResponse errorResponse)
			: base() {
			ErrorResponse = errorResponse;

			StringBuilder message = new StringBuilder();

			message
				.AppendLine("An unhandled exception ocurred in the remote api.")
				.AppendLine(errorResponse.ToString());

			_message = message.ToString();
		}

		protected ProxyException(SerializationInfo
			info, StreamingContext context)
			: base(info, context) {
		}

		public ProxyException(string message, Exception innerException)
			: base(message, innerException) {
		}

	}
}
