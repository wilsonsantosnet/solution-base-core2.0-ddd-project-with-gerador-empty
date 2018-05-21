using System.Collections.Generic;

namespace Common.Domain.Base
{
    public class ConfigSettingsBase
    {

        public bool DisabledCache { get; set; }

        public string AuthorityEndPoint { get; set; }

        public IEnumerable<string> ClientAuthorityEndPoint { get; set; }

        public IEnumerable<string> RedirectUris { get; set; }

        public string PostLogoutRedirectUris { get; set; }

        public string AutoRegister { get; set; }

        public string Salt { get; set; }
        
        public string FilePath { get; set; }
        
        public string ApiEndPoint { get; set; }


    }
}
