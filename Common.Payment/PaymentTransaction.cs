
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

namespace Common.Payment
{
    public class PaymentTransaction : PaymentBase, IPaymentTransaction
    {

        private string _transaction_resource;

        public PaymentTransaction(IRequest request, ConfigPaymentBase config) : base(request, config)
        {

            this._transaction_resource = "payments/payment";
            this._request = request;
            this._request.SetAddress(this._endpoint);

        }

        public PaymentTransaction(IRequest request, IOptions<ConfigPaymentBase> config) :
                    this(request, config.Value)
        { }

        public dynamic ExecutePaymentCreditCard(string email, CreditCard creditCard, decimal total, string description, string contry, string currency)
        {
            return this.Transaction(new
            {
                intent = "sale",
                payer = new
                {

                    payment_method = "credit_card",
                    payer_info = new
                    {
                        email = email,
                    },
                    funding_instruments = new List<dynamic> {
                        new {
                            credit_card = new {
                                number = creditCard.Number,
                                cvv2 = creditCard.Cvv2,
                                expire_month = creditCard.ExpireMonth,
                                expire_year = creditCard.ExpireYear,
                                first_name = creditCard.FirstName,
                                last_name =creditCard.LastName,
                                type = creditCard.Type,
                            }
                        }
                    }
                },
                transactions = new List<dynamic>
                {
                    new {
                        amount = new {
                            total= total,
                            currency= currency,
                            details = new {
                                subtotal= total
                            }
                        },
                        description = description,
                        invoice_number = DateTime.Now.ToString("yyMMddHHmms"),
                        item_list = new
                        {
                            items = new List<dynamic> {
                            new {
                                    name= description,
                                    description= description,
                                    quantity= "1",
                                    price= total,
                                    currency= currency
                                }
                            }
                        }
                    }
                }
            });
        }

        public dynamic ExecutePaymentPayPal(string email, decimal total, string description, string contry, string currency)
        {
            return this.Transaction(new
            {
                intent = "sale",
                payer = new
                {
                    payment_method = "paypal",
                },
               
                transactions = new List<dynamic>
                {
                    new {
                        amount = new {
                            total= total,
                            currency= currency,
                            details = new {
                                subtotal= total
                            }
                        },
                        description = description,
                        invoice_number = DateTime.Now.ToString("yyMMddHHmms"),
                        payment_options = new
                        {
                            allowed_payment_method = "IMMEDIATE_PAY"
                        },
                        item_list = new
                        {
                            items = new List<dynamic> {
                            new {
                                    name= description,
                                    description= description,
                                    quantity= "1",
                                    price= total,
                                    currency= currency
                                }
                            }
                        }
                    }
                },
                redirect_urls = new
                {
                    return_url = this._return_url,
                    cancel_url = this._cancel_url
                }
            });
        }

        public dynamic Transaction(dynamic data)
        {

            var result = this._request.Post<dynamic, dynamic>(this._transaction_resource, data);
            return result;
        }

        public dynamic GetPayments(object data)
        {
            var result = this._request.Get<dynamic>(this._transaction_resource, data.ToDictionary());
            return result;
        }

        public dynamic GetPaymentsDetails(string transactionId)
        {
            var result = this._request.Get<dynamic>($"{this._transaction_resource}/{transactionId}");
            return result;
        }

        public dynamic ExecutePayment(string transactionId, string payerId)
        {
            var result = this._request.Post<dynamic, dynamic>($"{this._transaction_resource}/{transactionId}/execute", new
            {
                payer_id = payerId
            });
            return result;
        }

    }
}