using System;

namespace CrimeMap.WebUI.Infrastructure.Configuration {

	public interface IConfigurationService {

		string FacebookAppId { get; }

		string FacebookAppSecret { get; }

		string PrivateKey { get; }

		Uri ApiUri { get; }

	}

}
