using System.Web.Http;

namespace CarExp.Api.Controllers.V1
{
	public abstract class AbstractApiController : ApiController
	{
		//protected IHttpActionResult BadRequestResponse(string message) {
		//	var details = GetModelStateErrors(ModelState);
		//	var error = new ErrorResponse(1, message, "", details);

		//	var response = CreateSimpleErrorResponse(HttpStatusCode.BadRequest, message);
		//	response.Error = error;
		//	response.Message = message;

		//	return Request.CreateResponse(HttpStatusCode.BadRequest, response);
		//}

		//private static IEnumerable<ValidationMessage> GetModelStateErrors(ModelStateDictionary modelState) {
		//	return modelState.Values
		//		.SelectMany(x => x.Errors.Select(c => new ValidationMessage(c.ErrorMessage)));
		//}

		//private static ErrorResponse CreateSimpleErrorResponse(HttpStatusCode statusCode, string message) {
		//	var response = new ErrorResponse();
		//	response.Success = false;
		//	response.Message = message;
		//	response.Code = (int)statusCode;

		//	return response;
		//}
	}
}