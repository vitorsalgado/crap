using System.Web.Http;

namespace CrimeMap.Api.Controllers.v1 {

	public class TestController : AbstractApiController {

		public IHttpActionResult Get() {
			var json = new {
				message = "success"
			};

			return Ok(json);
		}

		public IHttpActionResult Get(int id) {
			var json = new {
				message = id.ToString()
			};

			return Ok(json);
		}

	}
}
