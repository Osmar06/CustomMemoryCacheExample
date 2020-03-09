using System.Collections.Concurrent;

namespace CustomMemoryCacheExample.Cache
{
    public class MemoryCache
    {
        #region Private Fields

        private static ConcurrentDictionary<string, object> cache;

        #endregion Private Fields

        #region Private Constructors

        static MemoryCache()
        {
            cache = new ConcurrentDictionary<string, object>();
        }

        #endregion Private Constructors

        #region Public Methods

        public static void Add<TValue>(string key, TValue value)
        {
            var keyFormatted = ResolveKey<TValue>(key);
            if (Exist(keyFormatted))
                return;

            cache.TryAdd(keyFormatted, value);
        }

        public static void AddOrUpdate<TValue>(string key, TValue value)
            => cache.AddOrUpdate(ResolveKey<TValue>(key), value, (key, oldValue) => value);

        public static void Delete<TValue>(string key) => cache.TryRemove(ResolveKey<TValue>(key), out _);

        public static bool Exist(string key) => cache.ContainsKey(key);

        public static TValue Get<TValue>(string key)
            => !cache.TryGetValue(ResolveKey<TValue>(key), out object value) ? default : (TValue)value;

        #endregion Public Methods

        #region Private Methods

        private static string ResolveKey<TValue>(string key)
            => $"{typeof(TValue)}_{key}";

        #endregion Private Methods
    }
}