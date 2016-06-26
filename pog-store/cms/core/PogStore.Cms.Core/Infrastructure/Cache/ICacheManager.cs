using System;

namespace PogStore.Cms.Core.Infrastructure.Cache
{
	public interface ICacheManager
	{
		T Get<T>(string key);

		T Get<T>(string key, Func<T> fetchFunction);

		T Get<T>(string key, int cacheDurationInMinutes, Func<T> fetchFunction);

		void Add(string key, object data, int cacheDuration);

		bool Contains(string key);

		void Remove(string key);

		void ClearAll();
	}
}