using System;
using CrimeMap.Core.Domain;

namespace CrimeMap.Core.Storage {

	public interface IDomainRepository {

		T Get<T>(Guid id) where T : AggregateRoot, new();

		void Store(AggregateRoot aggregateRoot);

	}

}
