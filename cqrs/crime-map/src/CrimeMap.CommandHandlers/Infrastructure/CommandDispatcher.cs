using System.Threading.Tasks;
using CrimeMap.Command;

namespace CrimeMap.CommandHandlers {

	public class CommandDispatcher : ICommandDispatcher {

		private readonly ICommandHandlerResolver _commandHandlerResolver;

		public CommandDispatcher(ICommandHandlerResolver commandHandlerResolver) {
			_commandHandlerResolver = commandHandlerResolver;
		}

		public async Task Dispatch<TCommand>(TCommand command) where TCommand : ICommand {
			var commandHandler = _commandHandlerResolver.Resolve<TCommand>();

			if (commandHandler == null) {
				throw new CommandHandlerNotRegisteredException(command.ToString());
			}

			await commandHandler.Execute(command);
		}

	}

}
