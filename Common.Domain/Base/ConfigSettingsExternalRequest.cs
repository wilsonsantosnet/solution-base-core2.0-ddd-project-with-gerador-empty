using System.Collections.Generic;

namespace Common.Domain.Base
{
    public class ConfigSettingsExternalRequest 
    {

        public string EndPointHangfire { get; set; }
        public string ClientIdHangfire { get; set; }
        public string SecretHangfire { get; set; }
        public string ScopeHangfire { get; set; }

    }
}
