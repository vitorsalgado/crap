using PogStore.Cms.Core.Cqrs.Command;
using System.Net.Http;

namespace PogStore.Cms.AdminApi.CommandResolver
{
	public class WebApiCommandResolver : ICommandHandlerResolver
	{
		private readonly HttpRequestMessage _requestMessage;

		public WebApiCommandResolver(HttpRequestMessage requestMessage)
		{
			_requestMessage = requestMessage;
		}

		public ICommandHandler<TCommand> Resolve<TCommand>() where TCommand : ICommand
		{
			using (var dependecyScope = _requestMessage.GetDependencyScope())
			{
				var component = dependecyScope.GetService(typeof(ICommandHandler<TCommand>));
				return (ICommandHandler<TCommand>)component;
			}
		}
	}
}