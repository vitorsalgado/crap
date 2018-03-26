using PogStore.Cms.Core.Cqrs.Domain;
using System;

namespace PogStore.Cms.Core.Cqrs.Storage
{
	public interface IDomainRepository
	{
		T Get<T>(Guid id) where T : AggregateRoot, new();

		void Store(AggregateRoot aggregateRoot);
	}
}