
using System;
using Common.Domain;
using System.Collections.Specialized;
using System.Xml.Linq;
using System.Linq;
using System.Dynamic;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using Common.Domain.Base;
using Common.Domain.Model;

namespace Common.Payment.PayPal
{
    public class BillingAgreements : PaymentBase, IBillingAgreements
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

        public dynamic SignaturePlan(string planId, string description)
        {
            return this.Create(new
            {
                name = description,
                description = description,
                start_date = DateTime.UtcNow.AddDays(1).ToString("yyyy-MM-ddTHH:mm:ssZ"),
                payer = new
                {
                    payment_method = "paypal",
                },
                plan = new
                {
                    id = planId
                },
                override_merchant_preferences = new
                {
                    return_url = this._return_url,
                    cancel_url = this._cancel_url,
                    auto_bill_amount = "YES",
                    initial_fail_amount_action = "CONTINUE",
                    max_fail_attempts = "11"
                }
            });
        }

        public dynamic Execute(string transactionId)
        {
            var result = this._request.Post<dynamic, dynamic>($"{this._billing_resource}/{transactionId}/agreement-execute", new
            {

            });
            return result;
        }

        public dynamic GetDetails(string transactionId)
        {
            var result = this._request.Get<dynamic>($"{this._billing_resource}/{transactionId}");
            return result;
        }

        public dynamic GetTransactions(string transactionId)
        {
            return this.GetTransactions(transactionId, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.AddMonths(12).ToString("yyyy-MM-dd"));
        }

        public dynamic GetTransactions(string transactionId, string startDate, string endDate)
        {
            var result = this._request.Get<dynamic>($"{this._billing_resource}/{transactionId}/transactions", new
            {
                start_date = startDate,
                end_date = endDate
            }.ToDictionary());
            return result;
        }

        public dynamic Cancel(string transactionId)
        {
            var result = this._request.Post<dynamic, dynamic>($"{this._billing_resource}/{transactionId}/cancel", new
            {
                note = "Canceling the profile."
            });
            return result;
        }

        public dynamic SignaturePlanCreditCard(Customer customer, CreditCard credit_card, string planId, string description, decimal amount)
        {
            var result = this.Create(new
            {
                name = description,
                description = description,
                start_date = DateTime.Now.ToString("yyyy-MM-ddTmm:ss:tt:Z"),
                payer = new
                {
                    payment_method = "credit_card",
                    payer_info = new
                    {
                        email = customer.Email
                    },
                    funding_instruments = new List<dynamic> {
                        new {
                            credit_card = new
                                {
                                    billing_address = new
                                    {
                                        city = customer.City,
                                        country_code = customer.Country,
                                        line1 = $"{customer.Street}, {customer.StreetNumber}",
                                        postal_code = customer.ZipCode,
                                        state = customer.State
                                    },
                                cvv2 = credit_card.Cvv,
                                expire_month = credit_card.ExpireMonth,
                                expire_year = credit_card.ExpireYear,
                                first_name = credit_card.FirstName,
                                last_name = credit_card.LastName,
                                number = credit_card.Number,
                                type = credit_card.Banner
                            }
                        }
                    }
                },
                plan = new
                {
                    id = planId
                },
                shipping_address = new
                {
                    line1 = customer.Street,
                    line2 = customer.StreetNumber,
                    city = customer.City,
                    state = customer.State,
                    postal_code = customer.ZipCode,
                    country_code = customer.Country
                }
            });

            return result;
        }

        public dynamic SignaturePlanBankSlip(Customer customer, CreditCard credit_card, string planId, string description,  decimal amount)
        {
            throw new NotImplementedException();
        }
    }
}