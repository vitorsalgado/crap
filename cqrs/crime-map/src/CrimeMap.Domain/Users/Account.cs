using System;

namespace CrimeMap.Domain.Model.Users {

	public class Account {

		public Account(string provider, string providerKey) {
			Provider = provider;
			ProviderKey = providerKey;
		}

		public string Provider { get; private set; }

		public string ProviderKey { get; private set; }

	}
}
