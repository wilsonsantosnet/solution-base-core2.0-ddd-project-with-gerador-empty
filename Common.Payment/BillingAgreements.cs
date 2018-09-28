
using System;
using Common.Domain;
using System.Collections.Specialized;
using System.Xml.Linq;
using System.Linq;
using System.Dynamic;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using Common.Domain.Base;

namespace Common.Payment
{
    public class BillingAgreements : PaymentBase
    {

        private string _billing_resource;
        public BillingAgreements(IRequest request, ConfigPaymentBase config) : base(request, config)
        {
            this._billing_resource = "payments/billing-agreements";
        }
        public BillingAgreements(IRequest request, IOptions<ConfigPaymentBase> config) : this(request, config.Value)
        {
            
        }

        public dynamic Create(dynamic data)
        {
            var result = this._request.Post<dynamic, dynamic>(this._billing_resource, data);
            return result;
        }


    }
}