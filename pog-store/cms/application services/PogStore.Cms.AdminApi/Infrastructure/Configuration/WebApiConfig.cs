using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PogStore.Cms.AdminApi.Infrastructure.Configuration;
using PogStore.Cms.AdminApi.Infrastructure.DI;
using PogStore.Cms.AdminApi.Infrastructure.Filters;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Http.ExceptionHandling;

namespace PogStore.Cms.AdminApi
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			SetErrorHandler(config);
			SetServices(config);
			SetFormatters();
			SetIoc(config);
			SetRoutes(config);
			SetJsonDefaults();

			config.EnsureInitialized();
		}

		private static void SetJsonDefaults()
		{
			var jsonFormatter = JsonConvert.DefaultSettings = (() =>
			{
				var settings = new JsonSerializerSettings();
				settings.ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor;

				return settings;
			});
		}

		private static void SetRoutes(HttpConfiguration config)
		{
			config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "{namespace}/{controller}"
			);

			config.Routes.MapHttpRoute(
				name: "DefaultApiWithId",
				routeTemplate: "{namespace}/{controller}/{id}",
				constraints: new { id = @"^\d+$" },
				defaults: new { id = RouteParameter.Optional }
			);

			config.Routes.MapHttpRoute(
				name: "ApiWithAction",
				routeTemplate: "{namespace}/{controller}/{action}"
			);
		}

		private static void SetIoc(HttpConfiguration config)
		{
			config.DependencyResolver = DependencyResolverFactory.BuildForWebApi2();
		}

		private static void SetFormatters()
		{
			var jsonFormatter = GlobalConfiguration.Configuration.Formatters.JsonFormatter;

			jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
			jsonFormatter.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
		}

		private static void SetServices(HttpConfiguration config)
		{
			config.Services.Replace(typeof(IHttpControllerSelector), new NamespaceHttpControllerSelector(config));
		}

		private static void SetErrorHandler(HttpConfiguration config)
		{
			config.Services.Replace(typeof(IExceptionHandler), new UnhandledExceptionFilterAttribute());
		}
	}
}