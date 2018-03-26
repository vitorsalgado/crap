using PogStore.Cms.Core.Infrastructure.Cache;
using System;

namespace PogStore.Cms.Infrastructure.Cache
{
	public class NullCacheManager : ICacheManager
	{
		public T Get<T>(string key)
		{
			return default(T);
		}

		public T Get<T>(string key, Func<T> fetchFunction)
		{
			return fetchFunction();
		}

		public T Get<T>(string key, int cacheDuration, Func<T> fetchFunction)
		{
			return fetchFunction();
		}

		public void Add(string key, object data, int cacheDuration)
		{
		}

		public bool Contains(string key)
		{
			return false;
		}

		public void Remove(string key)
		{
		}

		public void ClearAll()
		{
		}
	}
}