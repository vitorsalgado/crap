using System.Web;
using System.Web.Optimization;

namespace CrimeMap.WebUI {

	public static class BundleConfig {

		public static void RegisterBundles(BundleCollection bundles) {

			RegisterCss(bundles);
			RegisterJs(bundles);

#if DEBUG
			BundleTable.EnableOptimizations = false;
#endif
		}

		public static void RegisterCss(BundleCollection bundles) {

			bundles.Add(new StyleBundle("~/css/map").Include(
				//"~/static/libs/bootstrap/dist/css/bootstrap.min.css",
				//"~/static/libs/bootstrap/dist/css/bootstrap-theme.min.css",
				"~/static/libs/custom-google-map/css/reset.css",
				"~/static/libs/custom-google-map/css/style.css",
				"~/static/libs/bootstrap.min.css",
				"~/static/css/main.css",
				"~/static/css/components.css"
			));

			bundles.Add(new StyleBundle("~/css/account").Include(
				"~/static/libs/bootstrap/dist/css/bootstrap.min.css",
				"~/static/libs/bootstrap/dist/css/bootstrap-theme.min.css"
			));

		}

		public static void RegisterJs(BundleCollection bundles) {

			bundles.Add(new ScriptBundle("~/js/core").Include(
				"~/static/libs/jquery/dist/jquery.min.js",
				"~/static/libs/bootstrap/dist/js/bootstrap.min.js",
				"~/static/js/login.js"
			));

			bundles.Add(new ScriptBundle("~/js/map").Include(
				"~/static/libs/google-maps-utils/infobox.js",
				"~/static/js/context.js",
				"~/static/js/location.js",
				"~/static/js/mapStyle.js",
				"~/static/js/map.js",
				"~/static/js/app.js"
			));

		}
	}
}
