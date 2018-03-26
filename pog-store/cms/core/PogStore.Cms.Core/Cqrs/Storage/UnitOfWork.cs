using PogStore.Cms.Core.Cqrs.Events;
using System;
using System.Collections.Generic;
using System.Transactions;

namespace PogStore.Cms.Core.Cqrs.Storage
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly IEventStore _eventStore;

		private List<IEventProvider> _trackedEventProviders;
		private TransactionScope _transaction;

		private bool _isDisposed = false;

		public UnitOfWork(IEventStore eventStore)
		{
			_eventStore = eventStore;
			_trackedEventProviders = new List<IEventProvider>();
		}

		public void Track(IEventProvider eventProvider)
		{
			if (_trackedEventProviders.Exists(x => x.Id == eventProvider.Id))
			{
				return;
			}

			_trackedEventProviders.Add(eventProvider);
		}

		public void Commit()
		{
			try
			{
				_transaction = new TransactionScope();

				foreach (var eventProvider in _trackedEventProviders)
				{
					_eventStore.Store(eventProvider);
					eventProvider.ClearEvents();
				}

				_transaction.Complete();
				_trackedEventProviders.Clear();
			}
			catch
			{
				Rollback();
				throw;
			}
		}

		public void Rollback()
		{
			_trackedEventProviders.Clear();
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool isDisposing)
		{
			if (!this._isDisposed)
			{
				if (isDisposing)
				{
					if (_transaction != null)
						_transaction.Dispose();
				}
			}

			this._isDisposed = true;
		}
	}
}