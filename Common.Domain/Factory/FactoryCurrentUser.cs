using Common.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Domain.Factory
{
    public class FactoryCurrentUser
    {
        private ICache _cache;
        public FactoryCurrentUser(ICache cache)
        {

        }

    }
}
