using System.Threading.Tasks;
using CrimeMap.ApplicationServices.Users.Messages;
using CrimeMap.CommandHandlers;
using CrimeMap.Commands.Users;
using CrimeMap.Common;
using CrimeMap.Common.Messages;
using CrimeMap.DataTransferObjects.Users;
using CrimeMap.QueryModel.Users;

namespace CrimaMap.ApplicationServices.Users {

	public class UserApplicationService : IUserApplicationService {

		private readonly ICommandDispatcher _commandDispatcher;
		private readonly IUserQueryService _userQueryService;

		public UserApplicationService(ICommandDispatcher commandDispatcher, IUserQueryService userQueryService) {
			_commandDispatcher = commandDispatcher;
			_userQueryService = userQueryService;
		}

		public async Task<BaseResponse<UserLoginDto>> GetLoginInformation(LoginRequest request) {
			var result = await _userQueryService.GetByLogin(request.Username, request.Password);

			var response = new BaseResponse<UserLoginDto>(request.Identifier);
			response.Data = result;

			return response;
		}

		public async Task<BaseResponse> Register(RegisterUserCommand command) {
			var response = new BaseResponse();
			var request = new UserRequest();

			request.Username = command.Email;

			var user = await _userQueryService.GetByUsername(command.Email);

			if (user != null) {
				response.Ack = AckType.FAILURE;
				response.Message = string.Format("There is already a user with the email {0}.", command.Email);

				return response;
			}

			await _commandDispatcher.Dispatch(command);

			return response;
		}

		public async Task<BaseResponse> RegisterExternalUserIfNeeded(RegisterExternalUserCommand command) {
			var response = new BaseResponse();
			var request = new UserRequest();

			request.Username = command.Email;

			var user = await _userQueryService.GetByUsername(command.Email);

			if (user != null) {
				response.Ack = AckType.FAILURE;
				response.Message = string.Format("There is already a user with the email {0}.", command.Email);

				return response;
			}

			await _commandDispatcher.Dispatch(command);

			return response;
		}

	}

}
