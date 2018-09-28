using Common.Domain.Base;
using Common.Domain.Interfaces;
using Common.Domain.Serialization;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using System;

namespace Common.Cache
{
    public class RedisComponent : ICache
    {
        private IDistributedCache _cache;
        private ConfigSettingsBase _configSettingsBase { get; set; }
        public RedisComponent(IDistributedCache _cache, IOptions<ConfigSettingsBase> configSettingsBase)
        {
            this._cache = _cache;
            this._configSettingsBase = configSettingsBase.Value;
        }

        public bool Add(string key, object value)
        {
            this._cache.Set(key, value.ToBytes());
            return true;
        }

        public bool Add(string key, object value, TimeSpan expire)
        {
            this._cache.Set(key, value.ToBytes(), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expire
            });

            return true;

        }

        public bool ExistsKey(string key)
        {
            var result = this._cache.Get(key);
            if (result.IsAny())
                return true;

            return false;
        }


        public object Get(string key)
        {
            var result = this._cache.Get(key);
            var resultObject = result.ToObject();
            return resultObject;
        }


        public bool Remove(string key)
        {
            this._cache.Remove(key);
            return true;
        }



        public bool Update(string key, object value)
        {
            this._cache.Set(key, value.ToBytes());
            return true;
        }

        public bool Update(string key, object value, TimeSpan expire)
        {
            this._cache.Set(key, value.ToBytes(), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expire
            });

            return true;
        }

        public bool Enabled()
        {
            return !this._configSettingsBase.DisabledCache;
        }
      
    }
}
