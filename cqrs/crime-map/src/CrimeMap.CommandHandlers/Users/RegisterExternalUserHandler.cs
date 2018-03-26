using CrimeMap.Commands.Users;
using CrimeMap.Core.Storage;
using CrimeMap.Domain.Model.Users;

namespace CrimeMap.CommandHandlers.Users.CommandHandler {

	public class RegisterExternalUserHandler : TransactionalCommandHandler<RegisterExternalUserCommand> {

		public RegisterExternalUserHandler(IUnitOfWork unitOfWork)
			: base(unitOfWork) {
		}

		protected override void Handle(RegisterExternalUserCommand command) {
			var user = new User(command.UserId, command.Name, command.Email, command.Provider, command.ProviderKey);
			_unitOfWork.Add(user);
		}
	}

}
