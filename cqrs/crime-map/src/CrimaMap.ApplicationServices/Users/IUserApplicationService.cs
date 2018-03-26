using System.Threading.Tasks;
using CrimeMap.ApplicationServices.Users.Messages;
using CrimeMap.Commands.Users;
using CrimeMap.Common;
using CrimeMap.Common.Messages;
using CrimeMap.DataTransferObjects.Users;

namespace CrimaMap.ApplicationServices.Users {
	
	public interface IUserApplicationService {

		Task<BaseResponse<UserLoginDto>> GetLoginInformation(LoginRequest request);

		Task<BaseResponse> Register(RegisterUserCommand command);

		Task<BaseResponse> RegisterExternalUserIfNeeded(RegisterExternalUserCommand command);

	}
}
