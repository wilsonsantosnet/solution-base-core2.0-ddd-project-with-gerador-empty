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
        private readonly IDistributedCache _cache;
        private ConfigSettingsBase _configSettingsBase { get; set; }

        public RedisComponent(IDistributedCache _cache, IOptions<ConfigSettingsBase> configSettingsBase)
        {
            this._cache = _cache;
            this._configSettingsBase = configSettingsBase.Value;
        }

        public bool Add(string key, object value)
        {
            try
            {
                if (!this.Enabled())
                    return false;


                this._cache.Set(key, value.ToBytes());
            }
            catch { }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expire">TimeSpan.FromMinutes(30)</param>
        /// <returns></returns>
        public bool Add(string key, object value, TimeSpan expire)
        {
            try
            {
                if (!this.Enabled())
                    return false;


                if (value.ToBytes().Length == 0)
                    return false;

                this._cache.Set(key, value.ToBytes(), new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = expire
                });
            }
            catch { }
            return true;

        }

        public bool ExistsKey(string key)
        {
            try
            {
                var result = this._cache.Get(key);
                if (result.IsAny())
                    return true;
            }
            catch { }
            return false;
        }


        public T Get<T>(string key) where T : class
        {
            if (!this.Enabled())
                return default(T);

            try
            {
                var result = this._cache.Get(key);
                var resultObject = result.ToType<T>();
                return resultObject;
            }
            catch (Exception ex)
            {
                return default(T);
            }

        }


        public bool Remove(string key)
        {
            try
            {
                if (!this.Enabled())
                    return false;

                this._cache.Remove(key);
            }
            catch { }
            return true;
        }



        public bool Update(string key, object value)
        {
            try
            {
                if (!this.Enabled())
                    return false;

                this._cache.Set(key, value.ToBytes());
            }
            catch { }
            return true;
        }

        public bool Update(string key, object value, TimeSpan expire)
        {
            try
            {
                if (!this.Enabled())
                    return false;

                this._cache.Set(key, value.ToBytes(), new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = expire
                });
            }
            catch { }
            return true;
        }

        public bool Enabled()
        {
            return this._configSettingsBase.EnabledCache;
        }

    }
}
