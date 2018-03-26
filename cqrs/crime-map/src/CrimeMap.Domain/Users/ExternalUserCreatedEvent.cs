using System;
using CrimeMap.Core.Events;

namespace CrimeMap.Domain.Model.Users {

	public class ExternalUserCreatedEvent : Event {

		public Guid Id { get; private set; }

		public string Name { get; private set; }

		public string Email { get; private set; }

		public string Provider { get; private set; }

		public string ProviderKey { get; private set; }

		public ExternalUserCreatedEvent(Guid id, string name, string email, string provider, string providerKey) {
			Id = id;
			Name = name;
			Email = email;
			Provider = provider;
			ProviderKey = providerKey;
		}

	}

}
