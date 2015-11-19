using System;
using CrimeMap.Core.Domain;
using CrimeMap.Core.Events;

namespace CrimeMap.Core.Storage {

	public interface IUnitOfWork : IDisposable {

		void Track(IEventProvider eventProvider);

		T Get<T>(Guid id) where T : AggregateRoot, new();

		void Add(AggregateRoot aggregateRoot);

		void Commit();

		void Rollback();

	}

}
