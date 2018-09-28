using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Domain.Base
{
    public class ConfigPaymentBase
    {
        public string Endpoint { get; set; }
        public string AuthorityEndpoint { get; set; }
        public string ClientId { get; set; }
        public string Secret { get; set; }
        public string ReturnUrl { get; set; }
        public string CancelUrl { get; set; }


    }
}
