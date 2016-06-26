using PogStore.Cms.Core.Cqrs.Events;
using System;

namespace PogStore.Cms.Core.Cqrs.Storage
{
	public interface IUnitOfWork : IDisposable
	{
		void Track(IEventProvider eventProvider);

		void Commit();

		void Rollback();
	}
}