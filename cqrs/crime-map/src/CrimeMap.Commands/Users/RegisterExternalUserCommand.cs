using System;
using CrimeMap.Command;

namespace CrimeMap.Commands.Users {

	public class RegisterExternalUserCommand : AbstractCommand {

		public Guid UserId { get; private set; }

		public string Name { get; private set; }

		public string Email { get; private set; }

		public string Provider { get; private set; }

		public string ProviderKey { get; private set; }

		public RegisterExternalUserCommand(Guid userId, string name, string email, string provider, string providerkey) {
			UserId = userId;
			Name = name;
			Email = email;
			Provider = provider;
			ProviderKey = providerkey;
		}

	}
}
