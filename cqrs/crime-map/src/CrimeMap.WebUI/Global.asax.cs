using CrimeMap.WebUI.Infrastrucure.Mvc;

namespace CrimeMap.WebUI {

	public class MvcApplication : System.Web.HttpApplication {

		protected void Application_Start() {
			MvcConfig.Config();
		}
	}
}
