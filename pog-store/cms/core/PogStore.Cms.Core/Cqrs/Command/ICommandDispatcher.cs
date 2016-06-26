namespace PogStore.Cms.Core.Cqrs.Command
{
	public interface ICommandDispatcher
	{
		void Dispatch<TCommand>(TCommand command) where TCommand : ICommand;
	}
}