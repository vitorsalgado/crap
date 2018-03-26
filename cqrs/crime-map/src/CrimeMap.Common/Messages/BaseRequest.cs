using System;
using System.Runtime.Serialization;

namespace CrimeMap.Common {

	[DataContract]
	public abstract class BaseRequest {

		protected BaseRequest() {
			Identifier = Guid.NewGuid().ToString();
		}

		protected BaseRequest(string idetifier) {
			Identifier = Identifier;
		}

		protected BaseRequest(Guid identifier) : this(identifier.ToString()) { }

		[DataMember]
		public string Identifier { get; protected set; }

	}
}
