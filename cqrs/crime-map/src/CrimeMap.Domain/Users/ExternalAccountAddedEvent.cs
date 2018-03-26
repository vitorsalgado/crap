using CrimeMap.Core.Events;

namespace CrimeMap.Domain.Model.Users {
	
	public class ExternalAccountAddedEvent : Event {

		public string Provider { get; private set; }

		public string ProviderKey { get; private set; }

		public ExternalAccountAddedEvent(string provider, string providerKey) {
			Provider = provider;
			ProviderKey = providerKey;
		}

	}

}
