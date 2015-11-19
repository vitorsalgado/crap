using CrimeMap.Command;

namespace CrimeMap.CommandHandlers {
	
	public interface ICommandHandlerResolver {

		ICommandHandler<TCommand> Resolve<TCommand>()
			where TCommand : ICommand;
	}

}
