using Common.Domain;
using Common.Domain.Base;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace Common.Payment.Moip
{
    /// <summary>
    /// https://dev.wirecard.com.br/v1.5/reference#criar-plano
    /// </summary>
    public class BillingPlans : PaymentBase, IBillingPlans
    {

        private string _billing_resource;

        public BillingPlans(IRequest request, ConfigPaymentBase config) : base(request, config)
        {
            this._billing_resource = "assinaturas/v1";

        }
        public BillingPlans(IRequest request, IOptions<ConfigPaymentBase> config, string planId = null)
            : this(request, config.Value) { }

        public dynamic Create(dynamic data)
        {
            var result = this._request.Post<dynamic, dynamic>($"{this._billing_resource}/plans", data);
            return result;
        }

        public dynamic Create(string name, string description, decimal value, int frequencyInterval, string frequency, int trial, decimal setupFee, bool holdSetupFee, bool active, int cicles)
        {
            return GetResult(name, description, value, frequencyInterval, frequency, trial, setupFee, holdSetupFee, active, cicles);
        }

        private dynamic GetResult(string name, string description, decimal value, int frequencyInterval, string frequency, int trial, decimal setupFee, bool holdSetupFee, bool active, int cicles)
        {
            var code = Guid.NewGuid().ToString();
            var result = this.Create(new
            {
                code,
                name,
                description,
                amount = Convert.ToInt32(Math.Round(value * 100, 0)),
                setup_fee = Convert.ToInt32(Math.Round(setupFee * 100, 0)),
                status = active ? "ACTIVE" : "INACTIVE",
                interval = new
                {
                    length = frequencyInterval,
                    unit = frequency
                },
                max_qty = 1,
                billing_cycles = cicles,
                trial = new
                {
                    days = trial,
                    enabled = trial > 0,
                    holdSetupFee
                },
                payment_method = "ALL",
            });
            return code;
        }

        public dynamic Details(string planId)
        {
            var result = this._request.Get<dynamic>($"{this._billing_resource}/plans/{planId}");
            return result;
        }

        public IEnumerable<dynamic> All()
        {
            var result = this._request.Get<dynamic>($"{this._billing_resource}/plans/");
            return result;
        }

        public dynamic Active(string planId)
        {
            var result = this._request.Get<dynamic>($"{this._billing_resource}/plans/{planId}/activate");
            return result;
        }



    }
}