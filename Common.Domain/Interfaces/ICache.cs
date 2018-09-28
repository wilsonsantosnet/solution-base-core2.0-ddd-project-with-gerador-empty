using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain.Interfaces
{
    public interface ICache
    {
        bool Add(string key, object value);
        bool Add(string key, object value, TimeSpan expire);

        bool Update(string key, object value);
        bool Update(string key, object value, TimeSpan expire);

        bool Remove(string key);

        object Get(string key);

        bool ExistsKey(string key);

        bool Enabled();

    }
}
