using System.Threading.Tasks;
using CrimeMap.Command;
using CrimeMap.Core.Storage;

namespace CrimeMap.CommandHandlers {

	public abstract class TransactionalCommandHandler<TCommand> : ICommandHandler<TCommand>
		where TCommand : ICommand {

		protected readonly IUnitOfWork _unitOfWork;

		protected TransactionalCommandHandler(IUnitOfWork unitOfWork) {
			_unitOfWork = unitOfWork;
		}

		public async Task Execute(TCommand command) {
			try{
				this.Handle(command);
				_unitOfWork.Commit();
			} catch {
				_unitOfWork.Rollback();
				throw;
			} finally {
				_unitOfWork.Dispose();
			}
		}

		protected abstract void Handle(TCommand command);
	}

}
