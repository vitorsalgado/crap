using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using CrimeMap.WebUI.Infrastrucure.DI;
using Newtonsoft.Json;

namespace CrimeMap.WebUI.Infrastrucure.Mvc {

	public static class MvcConfig {

		public static void Config() {
			SetDependency();
			SetViewEngine();
			SetAreas();
			SetFilters();
			SetRoutes();
			SetBundles();
			SetJsonDefaults();
		}

		private static void SetJsonDefaults() {
			var jsonFormatter = JsonConvert.DefaultSettings = (() => {
				var settings = new JsonSerializerSettings();
				settings.ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor;

				return settings;
			});
		}

		private static void SetBundles() {
			BundleConfig.RegisterBundles(BundleTable.Bundles);
		}

		private static void SetRoutes() {
			RouteConfig.RegisterRoutes(RouteTable.Routes);
		}

		private static void SetFilters() {
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
		}

		private static void SetAreas() {
			AreaRegistration.RegisterAllAreas();
		}

		private static void SetViewEngine() {
			ViewEngines.Engines.Clear();
			ViewEngines.Engines.Add(new RazorViewEngine());
		}

		private static void SetDependency() {
			var dependencyResolver = DependencyResolverFactory.BuildForMvc();
			DependencyResolver.SetResolver(dependencyResolver);
		}

	}
}
