using System.Threading.Tasks;
using CrimeMap.Command;

namespace CrimeMap.CommandHandlers {

	public interface ICommandDispatcher {

		Task Dispatch<TCommand>(TCommand command) where TCommand : ICommand;

	}
}
