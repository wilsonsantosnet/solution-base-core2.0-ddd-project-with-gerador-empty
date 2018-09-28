using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Configuration
{
    public class ConfigSettingsBase
    {

        public bool DisabledCache { get; set; }

        public string AuthorityEndPoint { get; set; }

    }
}
