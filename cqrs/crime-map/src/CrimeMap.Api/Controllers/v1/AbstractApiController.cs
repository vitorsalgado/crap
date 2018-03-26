using System.Net;
using System.Net.Http;
using System.Web.Http;
using CrimeMap.Common;

namespace CrimeMap.Api.Controllers.v1 {

	public abstract class AbstractApiController : ApiController {

		protected HttpResponseMessage OkResponse(string commandIdentifier) {
			var response = new BaseResponse(commandIdentifier);
			return Request.CreateResponse(HttpStatusCode.OK, response);
		}

	}
}
