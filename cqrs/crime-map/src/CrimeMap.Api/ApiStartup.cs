using System.Web.Http;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(CrimeMap.Api.ApiStartup))]

namespace CrimeMap.Api {
	
	public class ApiStartup {
		
		public void Configuration(IAppBuilder app) {
			HttpConfiguration httpConfiguration = new HttpConfiguration();
			WebApiConfig.Register(httpConfiguration);

			app.UseWebApi(httpConfiguration);
		}
	}
}
