using PogStore.Cms.Core.Framework.Messages;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;

namespace PogStore.Cms.AdminApi.Infrastructure.Filters
{
	public sealed class UnhandledExceptionFilterAttribute : ExceptionHandler
	{
		public override void Handle(ExceptionHandlerContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}

			var exception = context.Exception;

			var errorResponse = new ErrorResponse(
				500, exception.Message, exception.ToString(), "/api/{api_version}/error/", null);
			errorResponse.CorrelationId = context.Request.GetCorrelationId().ToString();

			context.Result = new ResponseMessageResult(
				context.Request.CreateResponse(HttpStatusCode.InternalServerError, errorResponse, "application/json"));
		}
	}
}