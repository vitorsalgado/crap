using System;
using System.Runtime.Serialization;

namespace CrimeMap.Core.Domain {

	[Serializable]
	public class DomainException : Exception {

		public DomainException() {

		}

		public DomainException(string message)
			: base(message) {
		}

		public DomainException(string messageFormat, params object[] args)
			: base(string.Format(messageFormat, args)) {
		}

		protected DomainException(SerializationInfo
			info, StreamingContext context)
			: base(info, context) {
		}

		public DomainException(string message, Exception innerException)
			: base(message, innerException) {
		}
	}
}
