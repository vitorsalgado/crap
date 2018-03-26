using PogStore.Cms.Core.Cqrs.Storage;
using System;

namespace PogStore.Cms.Core.Cqrs.Command
{
	public abstract class TransactionalCommandHandler<T> : ICommandHandler<T> where T : ICommand
	{
		protected readonly IUnitOfWork _unitOfWork;

		protected TransactionalCommandHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public void Execute(T command)
		{
			if (command == null)
			{
				throw new ArgumentNullException("command");
			}

			using (_unitOfWork)
			{
				this.Handle(command);
				_unitOfWork.Commit();
			}
		}

		protected abstract void Handle(T command);
	}
}