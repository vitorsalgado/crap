using PogStore.Cms.Core.Infrastructure.Cache;
using System;
using System.Runtime.Caching;

namespace PogStore.Cms.Infrastructure.Cache
{
	public class MemoryCacheManager : ICacheManager
	{
		private const int DEFAULT_CACHE_TIME = 60;
		private static ObjectCache Cache { get { return MemoryCache.Default; } }

		public T Get<T>(string key)
		{
			return (T)Cache[key];
		}

		public T Get<T>(string key, Func<T> fetchFunction)
		{
			return Get(key, DEFAULT_CACHE_TIME, fetchFunction);
		}

		public T Get<T>(string key, int cacheDurationInMinutes, Func<T> fetchFunction)
		{
			if (string.IsNullOrEmpty(key))
				throw new ArgumentNullException("key");

			if (fetchFunction == null)
				throw new ArgumentNullException("fetchFunction");

			if (Contains(key))
			{
				return Get<T>(key);
			}
			else
			{
				var result = fetchFunction();
				if (result != null)
					Add(key, result, cacheDurationInMinutes);

				return result;
			}
		}

		public void Add(string key, object data, int cacheDurationInMinutes)
		{
			if (data == null)
				return;

			var policy = new CacheItemPolicy();

			policy.AbsoluteExpiration = DateTime.Now + TimeSpan.FromMinutes(cacheDurationInMinutes);
			Cache.Add(new CacheItem(key, data), policy);
		}

		public bool Contains(string key)
		{
			return Cache.Contains(key);
		}

		public void Remove(string key)
		{
			Cache.Remove(key);
		}

		public void ClearAll()
		{
			foreach (var item in Cache)
				Cache.Remove(item.Key);
		}
	}
}