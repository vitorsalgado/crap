using PogStore.Cms.ApplicationServices.Users.Contracts;
using PogStore.Cms.Core.Cqrs.Command;
using PogStore.Cms.Core.Framework.Messages;
using System;

namespace PogStore.Cms.ApplicationServices.Users.Implementations
{
	public class UserServiceImpl : IUserService
	{
		private readonly ICommandDispatcher _commandDispatcher;

		public UserServiceImpl(ICommandDispatcher commandDispatcher)
		{
			this._commandDispatcher = commandDispatcher;
		}

		public Response Create(CreateUserRequest request)
		{
			throw new NotImplementedException();
		}
	}
}