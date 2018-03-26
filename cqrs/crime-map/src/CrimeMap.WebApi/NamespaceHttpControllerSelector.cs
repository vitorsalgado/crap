using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Http.Routing;

namespace CrimeMap.WebApi {

	public class NamespaceHttpControllerSelector : IHttpControllerSelector {

		private const string NAMESPACE_KEY = "namespace";
		private const string CONTROLLER_KEY = "controller";

		private readonly HttpConfiguration _configuration;
		private readonly Lazy<Dictionary<string, HttpControllerDescriptor>> _controllers;
		private readonly HashSet<string> _duplicates;

		public NamespaceHttpControllerSelector(HttpConfiguration config) {
			_configuration = config;
			_duplicates = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			_controllers = new Lazy<Dictionary<string, HttpControllerDescriptor>>(InitializeControllerDictionary);
		}

		public HttpControllerDescriptor SelectController(HttpRequestMessage request) {

			IHttpRouteData routeData = request.GetRouteData();

			if (routeData == null)
				throw new HttpResponseException(HttpStatusCode.NotFound);

			string namespaceName = GetRouteVariable<string>(routeData, NAMESPACE_KEY);

			if (namespaceName == null)
				throw new HttpResponseException(HttpStatusCode.NotFound);

			string controllerName = GetRouteVariable<string>(routeData, CONTROLLER_KEY);

			if (controllerName == null)
				throw new HttpResponseException(HttpStatusCode.NotFound);

			string key = String.Format(CultureInfo.InvariantCulture, "{0}.{1}", namespaceName, controllerName);

			HttpControllerDescriptor controllerDescriptor;

			if (_controllers.Value.TryGetValue(key, out controllerDescriptor))
				return controllerDescriptor;
			else if (_duplicates.Contains(key))
				throw new HttpResponseException(
					request.CreateErrorResponse(HttpStatusCode.InternalServerError,
					"Multiple controllers were found that match this request."));
			else
				throw new HttpResponseException(HttpStatusCode.NotFound);
		}

		public IDictionary<string, HttpControllerDescriptor> GetControllerMapping() {
			return _controllers.Value;
		}

		private Dictionary<string, HttpControllerDescriptor> InitializeControllerDictionary() {

			var dictionary = new Dictionary<string, HttpControllerDescriptor>(StringComparer.OrdinalIgnoreCase);

			IAssembliesResolver assembliesResolver = _configuration.Services.GetAssembliesResolver();
			IHttpControllerTypeResolver controllersResolver = _configuration.Services.GetHttpControllerTypeResolver();

			ICollection<Type> controllerTypes = controllersResolver.GetControllerTypes(assembliesResolver);

			foreach (Type t in controllerTypes) {
				var segments = t.Namespace.Split(Type.Delimiter);
				var controllerName = t.Name.Remove(t.Name.Length - DefaultHttpControllerSelector.ControllerSuffix.Length);
				var key = String.Format(CultureInfo.InvariantCulture, "{0}.{1}", segments[segments.Length - 1], controllerName);

				if (dictionary.Keys.Contains(key))
					_duplicates.Add(key);
				else
					dictionary[key] = new HttpControllerDescriptor(_configuration, t.Name, t);
			}

			foreach (string s in _duplicates)
				dictionary.Remove(s);

			return dictionary;
		}

		private static T GetRouteVariable<T>(IHttpRouteData routeData, string name) {

			object result = null;

			if (routeData.Values.TryGetValue(name, out result))
				return (T)result;

			return default(T);
		}

	}
}
