using System;
using System.Configuration;

namespace CrimeMap.WebUI.Infrastructure.Configuration {

	public class ConfigurationService : IConfigurationService {

		public string FacebookAppId {
			get {
				var facebookAppId = ConfigurationManager.AppSettings["facebookAppId"];

				if (string.IsNullOrEmpty(facebookAppId))
					throw new InvalidOperationException("facebookAppId not defined on web.config / app.config.");

				return facebookAppId;
			}
		}

		public string FacebookAppSecret {
			get {
				var facebookAppSecret = ConfigurationManager.AppSettings["facebookAppSecret"];

				if (string.IsNullOrEmpty(facebookAppSecret))
					throw new InvalidOperationException("facebookAppSecrete not defined on web.config / app.config.");

				return facebookAppSecret;
			}
		}

		public string PrivateKey {
			get {
				var privateKey = ConfigurationManager.AppSettings["encryption.key"];
				return privateKey;
			}
		}

		public Uri ApiUri {
			get {
				var url = ConfigurationManager.AppSettings["api.url"];
				var uri = new Uri(url);
				return uri;
			}
		}
	}

}
