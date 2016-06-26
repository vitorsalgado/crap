using System;

namespace PogStore.Cms.Core.Cqrs.Command
{
	public class CommandDispatcher : ICommandDispatcher
	{
		private readonly ICommandHandlerResolver _commandHandlerResolver;

		public CommandDispatcher(ICommandHandlerResolver commandHandlerResolver)
		{
			_commandHandlerResolver = commandHandlerResolver;
		}

		public void Dispatch<TParameter>(TParameter command) where TParameter : ICommand
		{
			if (command == null)
			{
				throw new ArgumentNullException("command");
			}

			var commandHandler = _commandHandlerResolver.Resolve<TParameter>();

			if (commandHandler == null)
			{
				throw new CommandHandlerNotRegisteredException(command.ToString());
			}

			commandHandler.Execute(command);
		}
	}
}