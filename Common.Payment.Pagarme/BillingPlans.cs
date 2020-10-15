
using System;
using Common.Domain;
using System.Collections.Specialized;
using System.Xml.Linq;
using System.Linq;
using System.Dynamic;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using Common.Domain.Base;

namespace Common.Payment.Pagarme
{
    public class BillingPlans : PaymentBase, IBillingPlans
    {

        private string _billing_resource;
        private string[] _frequency;

        public BillingPlans(IRequest request, ConfigPaymentBase config) : base(request, config)
        {
            this._billing_resource = "payments/billing-plans";
            this._frequency = new string[] { "MONTH", "YEAR" };
        }
        public BillingPlans(IRequest request, IOptions<ConfigPaymentBase> config, string planId = null)
            : this(request, config.Value) { }

        public dynamic Create(dynamic data)
        {
            var result = this._request.Post<dynamic, dynamic>(this._billing_resource, data);
            return result;
        }

        public dynamic Create(string name, string description, string currency, decimal value, int frequencyInterval,string frequency, int trial)
        {
            var trialPlan = default(dynamic);
            if (trial > 0)
            {
                trialPlan = new
                {
                    name = description,
                    type = "TRIAL",
                    frequency = "MONTH",
                    frequency_interval = trial,
                    amount = new
                    {
                        value = "0",
                        currency = currency
                    },
                    cycles = "1"
                };
            }

            var plan = new
            {
                name = name,
                description = description,
                type = "INFINITE",
                payment_definitions = new List<dynamic> {
                    new {
                        name= description,
                        type= "REGULAR",
                        frequency= frequency,
                        frequency_interval= frequencyInterval,
                        amount= new {
                            value= value,
                            currency= currency
                        },
                        cycles= "0"
                    },
                    trialPlan
                },
                
                merchant_preferences = new
                {
                    return_url = this._return_url,
                    cancel_url = this._cancel_url,
                    auto_bill_amount = "YES",
                    initial_fail_amount_action = "CONTINUE",
                    max_fail_attempts = "0"
                }
            };

            var resultPlan = this.Create(plan);
            if (resultPlan != null)
                this.Active(resultPlan.id.Value);

            return resultPlan;
        }

        public dynamic Details(string planId)
        {
            var result = this._request.Get<dynamic>($"{this._billing_resource}/{planId}");
            return result;
        }

        public dynamic Active(string planId)
        {
            var result = this._request.Path<dynamic, dynamic>($"{this._billing_resource}/{planId}", new List<dynamic>
            {
                new {
                  op = "replace",
                  path = "/",
                  value = new {
                    state = "ACTIVE",
                  }
                }
            });
            return result;
        }


    }
}