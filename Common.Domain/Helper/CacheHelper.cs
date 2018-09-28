using Common.Domain.Interfaces;
using System.Collections.Generic;

namespace Common.Domain.Base
{
    public class CacheHelper
    {
        protected readonly ICache _cache;
        private readonly ConfigSettingsBase _configSettingsBase;
        protected string _tagNameCache;

        public CacheHelper(ICache _cache)
        {
            this._cache = _cache;
        }
        public virtual void ClearCache()
        {
            if (!this._cache.Enabled())
                return;
 
            if (this._cache.IsNotNull())
            {
                var tag = this._cache.Get(this._tagNameCache) as List<string>;
                if (tag.IsNull()) return;
                foreach (var item in tag)
                {
                    this._cache.Remove(item);
                }
                this._cache.Remove(this._tagNameCache);
            }

        }

        public virtual void SetTagNameCache(string tagNameCahed)
        {
            this._tagNameCache = tagNameCahed;
        }

        public virtual string GetTagNameCache()
        {
            return this._tagNameCache;
        }
    }
}
