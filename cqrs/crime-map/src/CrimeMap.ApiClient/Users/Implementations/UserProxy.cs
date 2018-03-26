using System.Threading.Tasks;
using CrimeMap.ApiClient;
using CrimeMap.ApplicationServices.Users.Messages;
using CrimeMap.Commands.Users;
using CrimeMap.Common;
using CrimeMap.Common.Messages;
using CrimeMap.DataTransferObjects.Users;

namespace CrimaMap.ApiClient.Users {

	public class UserProxy : WebApiProxy, IUserProxy {

		public async Task<BaseResponse<UserLoginDto>> GetLoginInformation(LoginRequest request) {
			return await Fetch<LoginRequest, BaseResponse<UserLoginDto>>(request, "user/logininformation/");
		}

		public async Task<BaseResponse> Register(RegisterUserCommand command) {
			return await SendCommand(command, "user/register/");
		}

		public async Task<BaseResponse> RegisterExternalUserIfNeeded(RegisterExternalUserCommand command) {
			return await SendCommand(command, "user/registerexternal/");
		}

	}

}
