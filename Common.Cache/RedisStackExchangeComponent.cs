using Common.Domain.Base;
using Common.Domain.Interfaces;
using Common.Domain.Serialization;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Text;

namespace Common.Cache
{
    public class RedisStackExchangeComponent : ICache
    {

        static RedisStackExchangeComponent()
        {
            _lazyConnection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(_configCache.Default));
        }
        private static Lazy<ConnectionMultiplexer> _lazyConnection;

        private static ConfigCacheConnectionStringBase _configCache;
        public static void Init(IOptions<ConfigCacheConnectionStringBase> configCache) {
            _configCache = configCache.Value;
        }


        public static ConnectionMultiplexer Connection => _lazyConnection.Value;

        public static IDatabase RedisCache => Connection.GetDatabase();

        private readonly IDatabase _cache;
        private ConfigSettingsBase _configSettingsBase { get; set; }


        public RedisStackExchangeComponent(IOptions<ConfigSettingsBase> configSettings, IOptions<ConfigCacheConnectionStringBase> configCache)
        {
            this._configSettingsBase = configSettings.Value;
            RedisStackExchangeComponent.Init(configCache);
            this._cache = RedisStackExchangeComponent.RedisCache;
        }

        public bool Add(string key, object value)
        {
            try
            {
                if (!this.Enabled())
                    return false;


                if (value.ToBytes().Length == 0)
                    return false;

                this._cache.StringSet(key, value.ToBytes());
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

                this._cache.StringSet(key, value.ToBytes(), expire);
            }
            catch { }
            return true;

        }

        public bool ExistsKey(string key)
        {
            if (!this.Enabled())
                return false;

            try
            {
                var result = this._cache.StringGet(key);
                if (result.HasValue)
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
                var result = this._cache.StringGet(key);
                var resultObject = Deserializer<T>(result);

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

                this._cache.KeyDelete(key);
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

                if (value.ToBytes().Length == 0)
                    return false;

                this._cache.StringSet(key, value.ToBytes());
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

                if (value.ToBytes().Length == 0)
                    return false;

                this._cache.StringSet(key, value.ToBytes(), expire);
            }
            catch { }
            return true;
        }

        public bool Enabled()
        {
            return this._configSettingsBase.EnabledCache;
        }

        
        private static T Deserializer<T>(byte[] value) where T : class
        {
            if (value.IsNull())
                return default(T);

            string resultJson = Encoding.UTF8.GetString(value);
            var resultObject = JsonConvert.DeserializeObject<T>(resultJson);
            return resultObject;
        }

    }
}
