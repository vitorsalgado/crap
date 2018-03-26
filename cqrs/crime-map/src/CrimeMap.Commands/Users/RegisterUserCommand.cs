using System;
using System.Runtime.Serialization;
using CrimeMap.Command;
using CrimeMap.Common;

namespace CrimeMap.Commands.Users {

	[DataContract]
	public class RegisterUserCommand : BaseRequest, ICommand {

		[DataMember]
		public Guid UserId { get; private set; }

		[DataMember]
		public string Name { get; private set; }

		[DataMember]
		public bool IsExternal { get; private set; }

		[DataMember]
		public string Email { get; private set; }

		[DataMember]
		public string Password { get; private set; }

		[DataMember]
		public string Provider { get; private set; }

		[DataMember]
		public string ProviderKey { get; private set; }

		public RegisterUserCommand(Guid userId, string name, string email, string password) {
			UserId = userId;
			Name = name;
			Email = email;
			Password = password;
			IsExternal = false;
		}

		public RegisterUserCommand(Guid userId, string name, string email, string provider, string providerKey) {
			UserId = userId;
			Name = name;
			Email = email;
			Provider = provider;
			ProviderKey = providerKey;
			IsExternal = true;
		}

		 RegisterUserCommand() { }
	}

}
