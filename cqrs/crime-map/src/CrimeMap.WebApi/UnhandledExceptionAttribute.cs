using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using CrimeMap.Common;

namespace CrimeMap.WebApi {

	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
	public sealed class UnhandledExceptionAttribute : ExceptionFilterAttribute {

		public override void OnException(HttpActionExecutedContext actionExecutedContext) {

			if (actionExecutedContext == null)
				throw new ArgumentNullException("actionExecutedContext");

			var exception = actionExecutedContext.Exception;
			var correlationId = actionExecutedContext.Request.GetCorrelationId().ToString();

			var errorResponse = new ErrorResponse(correlationId);
			errorResponse.Message = exception.Message;
			errorResponse.Exception = exception.ToString();
			errorResponse.DetailsUrl = "/api/{api_version}/error/";
			errorResponse.Ack = AckType.FAILURE;

			actionExecutedContext.Response =
				actionExecutedContext.Request.CreateResponse(HttpStatusCode.InternalServerError, errorResponse);
		}
	}
}
