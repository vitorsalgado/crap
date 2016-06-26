namespace PogStore.Cms.Core.Cqrs.Command
{
	public interface ICommandHandlerResolver
	{
		ICommandHandler<TCommand> Resolve<TCommand>() where TCommand : ICommand;
	}
}