using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using CrimaMap.ApplicationServices.Users;
using CrimeMap.Commands.Users;

namespace CrimeMap.Api.Controllers.v1 {

	public class UserController : AbstractApiController {

		private IUserApplicationService _userApplicationService;

		public UserController(IUserApplicationService userApplicationService) {
			_userApplicationService = userApplicationService;
		}

		[HttpPost]
		public async Task<HttpResponseMessage> Register(RegisterUserCommand command) {
			await _userApplicationService.Register(command);
			return OkResponse(command.Identifier);
		}

		[HttpPost]
		public async Task<HttpResponseMessage> RegisterExternal(RegisterExternalUserCommand command) {
			await _userApplicationService.RegisterExternalUserIfNeeded(command);
			return OkResponse(command.Identifier);
		}

	}
}
