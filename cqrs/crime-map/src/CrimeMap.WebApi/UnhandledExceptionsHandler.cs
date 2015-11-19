using System;
using System.Web.Http.ExceptionHandling;
using System.Net;
using System.Net.Http;
using CrimeMap.Common;
using System.Web.Http.Results;

namespace CrimeMap.WebApi {

	public class UnhandledExceptionsHandler : ExceptionHandler {

		public override void Handle(ExceptionHandlerContext context) {

			if (context == null) {
				throw new ArgumentNullException("context");
			}

			var exception = context.Exception;
			var correlationId = context.Request.GetCorrelationId().ToString();

			var errorResponse = new ErrorResponse(correlationId);
			errorResponse.Message = exception.Message;
			errorResponse.Exception = exception.ToString();
			errorResponse.DetailsUrl = "/api/{api_version}/error/";
			errorResponse.Ack = AckType.FAILURE;

			var response = context.Request.CreateResponse(HttpStatusCode.InternalServerError, errorResponse);

			context.Result = new ResponseMessageResult(response);
		}

	}
}
