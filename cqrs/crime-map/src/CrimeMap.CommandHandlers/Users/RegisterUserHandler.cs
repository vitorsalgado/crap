using CrimeMap.Commands.Users;
using CrimeMap.Core.Storage;
using CrimeMap.Domain.Model.Users;
using CrimeMap.Encryption;

namespace CrimeMap.CommandHandlers.Users.CommandHandler {

	public class RegisterUserHandler : TransactionalCommandHandler<RegisterUserCommand> {

		private readonly IEncryptionService _encryptionService;

		public RegisterUserHandler(
			IEncryptionService encryptionService,
			IUnitOfWork unitOfWork)
			: base(unitOfWork) {

			_encryptionService = encryptionService;
		}

		protected override void Handle(RegisterUserCommand command) {

			User user = null;

			if (command.IsExternal) {
				user = new User(command.UserId, command.Name, command.Email, command.Provider, command.Provider);
			} else {
				var saltKey = _encryptionService.CreateSaltKey(5);
				var password = _encryptionService.CreatePasswordHash(command.Password, saltKey, string.Empty);

				user = new User(command.UserId, command.Name, command.Email, password, saltKey, "SHA1");
			}

			_unitOfWork.Add(user);
		}
	}
}
