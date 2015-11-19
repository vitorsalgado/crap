using System.Threading.Tasks;
using CrimeMap.Command;

namespace CrimeMap.CommandHandlers {

	public interface ICommandHandler<TCommand> 
		where TCommand : ICommand {

		Task Execute(TCommand command);

	}

}
