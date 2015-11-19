using System;
using System.Net;
using System.Web.Mvc;

namespace CrimeMap.WebUI.Infrastrucure.Filters {

	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
	public sealed class CrimeAuthorizeAttribute : AuthorizeAttribute {

		protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext) {

			var httpContext = filterContext.HttpContext;
			var request = httpContext.Request;
			var response = httpContext.Response;

			if (request.IsAjaxRequest()) {
				response.SuppressFormsAuthenticationRedirect = true;
				response.StatusCode = (int)HttpStatusCode.Unauthorized;
				response.End();
			}

			base.HandleUnauthorizedRequest(filterContext);
		}
	}
}
