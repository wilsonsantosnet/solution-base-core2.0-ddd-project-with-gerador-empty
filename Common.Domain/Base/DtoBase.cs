using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Domain.Base
{
    public abstract class DtoBase
    {
        public string ConfirmBehavior { get; set; }
        public string AttributeBehavior { get; set; }
        public string Cachekey { get; set; }
        public double CacheExpirationMinutes { get; set; }

    }
}
