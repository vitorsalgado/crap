using System.Web.Http;

namespace PogStore.Cms.AdminApi.Controllers.v1
{
	public class TestController : ApiController
	{
		public IHttpActionResult Get()
		{
			var json = new
			{
				message = "success"
			};

			return Ok(json);
		}

		public IHttpActionResult Get(int id)
		{
			var json = new
			{
				message = id.ToString()
			};

			return Ok(json);
		}
	}
}