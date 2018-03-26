namespace PogStore.Cms.Core.Cqrs.Command
{
	public interface ICommandHandler<TCommand> where TCommand : ICommand
	{
		void Execute(TCommand command);
	}
}