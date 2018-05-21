
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
    public class BillingPlans : PaymentBase
    {

        private string _billing_resource;

        public BillingPlans(IRequest request, ConfigPaymentBase config, string planId = null) : base(request, config)
        {
            if (!string.IsNullOrEmpty(planId))
                this._billing_resource = $"payments/billing-plans/{planId}";
            else
                this._billing_resource = "payments/billing-plans";

        }
        public BillingPlans(IRequest request, IOptions<ConfigPaymentBase> config, string planId = null)
            : this(request, config.Value) { }

        public dynamic Create(dynamic data)
        {
            var result = this._request.Post<dynamic, dynamic>(this._billing_resource, data);
            return result;
        }

        public dynamic Details()
        {
            var result = this._request.Get<dynamic>(this._billing_resource);
            return result;
        }

        public dynamic Active(dynamic data)
        {
            var result = this._request.Path<dynamic, dynamic>(this._billing_resource, data);
            return result;
        }


    }
}