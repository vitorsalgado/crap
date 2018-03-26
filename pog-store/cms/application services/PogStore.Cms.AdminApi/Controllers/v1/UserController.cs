using PogStore.Cms.ApplicationServices.Users;
using PogStore.Cms.ApplicationServices.Users.Contracts;
using System.Web.Http;

namespace PogStore.Cms.AdminApi.Controllers.v1
{
	public class UserController : ApiController
	{
		private readonly IUserService _userService;

		public UserController(IUserService userService)
		{
			this._userService = userService;
		}

		public IHttpActionResult Get(int id)
		{
			return Ok();
		}

		public IHttpActionResult Post(CreateUserRequest request)
		{
			_userService.Create(request);
			return Ok();
		}
	}
}