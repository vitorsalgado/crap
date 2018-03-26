using System;
using System.Runtime.Serialization;

namespace CrimeMap.CommandHandlers {

	[Serializable]
	public class CommandHandlerNotRegisteredException : Exception {

		public CommandHandlerNotRegisteredException(string commandTypeName)
			: base(string.Format("Not handler for command \" {0} \" was found.", commandTypeName)) {

		}

		protected CommandHandlerNotRegisteredException(SerializationInfo info, StreamingContext context)
			: base(info, context) {
		}

	}

}
