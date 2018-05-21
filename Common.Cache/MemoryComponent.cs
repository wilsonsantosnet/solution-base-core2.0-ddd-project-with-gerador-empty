using Common.Domain.Base;
using Common.Domain.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace Common.Cache
{
    public class MemoryComponent : ICache
    {
        private static IDictionary<string, object> _memoryCache;
        private ConfigSettingsBase _configSettingsBase { get; set; }
        public MemoryComponent(IOptions<ConfigSettingsBase> configSettingsBase)
        {
            this._configSettingsBase = configSettingsBase.Value;
        }

        public bool Add(string key, object value)
        {
            _memoryCache.Add(key, value);
            return true;
        }

        public bool Add(string key, object value, TimeSpan expire)
        {
            _memoryCache.Add(key, value);
            return true;

        }

        public bool ExistsKey(string key)
        {
            return _memoryCache.ContainsKey(key);
        }

        public object Get(string key)
        {
            return _memoryCache[key];
        }

        public bool Remove(string key)
        {
            return true;
        }

        public bool Update(string key, object value)
        {
            return true;
        }

        public bool Update(string key, object value, TimeSpan expire)
        {
            return true;
        }

        public bool Enabled()
        {
            return !this._configSettingsBase.DisabledCache;
        }


    }
}
