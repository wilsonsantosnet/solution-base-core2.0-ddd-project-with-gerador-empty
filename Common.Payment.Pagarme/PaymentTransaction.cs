
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
using IdentityModel.Client;

namespace Common.Payment.Pagarme
{
    public class PaymentTransaction : PaymentBase, IPaymentTransaction
    {
        //https://docs.pagar.me/v2013-03-01/docs/overview-transacao

        private string _transaction_resource;

        public PaymentTransaction(IRequest request, ConfigPaymentBase config) : base(request, config)
        {

            this._transaction_resource = "transactions";
            this._request = request;
            this._request.SetAddress(this._endpoint);
        }

        public PaymentTransaction(IRequest request, IOptions<ConfigPaymentBase> config) : this(request, config.Value)
        {
        }


        public dynamic ExecuteCreditCard(Customer customer, CreditCard creditCard, decimal total, string description = null, string contry = null, string currency = null)
        {
            var card_hash = GetCardHash(customer, creditCard);

            return this.Transaction(new
            {
                amount = total,
                api_key = this._client_Id,
                card_hash = card_hash,
                customer = new {
                    address = new {
                        neighborhood = customer.Neighborhood,
                        street = customer.Street,
                        street_number = customer.StreetNumber,
                        zipcode = customer.ZipCode
                    },
                    document_number = customer.DocumentNumber,
                    email = customer.Email,
                    name = $"{customer.FirstName} {customer.LastName}",
                    phone = new {
                        ddd = customer.PhoneDDD,
                        number = customer.PhoneNumber
                    }
                }
            });
        }

        public dynamic ExecuteBankSlip(Customer customer, decimal total, string description = null, DateTime? expirationDate = null, string postbackUrl = null)
        {
            return this.Transaction(new
            {
                amount = total,
                api_key = this._client_Id,
                payment_method = "boleto",
                boleto_instructions = description,
                boleto_expiration_date = Convert.ToDateTime(expirationDate).ToString("yyyy-MM-ddTHH:mm:ss.0000Z"),
                //postback_url = postbackUrl,
                customer = new
                {
                    external_id = customer.customerId,
                    type = "individual",
                    name = $"{customer.FirstName} {customer.LastName}",
                    country = customer.Country,
                    email = customer.Email,
                    documents = new {
                        type = customer.DocumentType,
                        number = customer.DocumentNumber,
                    },
                    phone_numbers = customer.PhoneNumber,
                    address = new
                    {
                        neighborhood = customer.Neighborhood,
                        street = customer.Street,
                        street_number = customer.StreetNumber,
                        zipcode = customer.ZipCode
                    }
                },
                metadata = new
                {
                    internal_info = new
                    {
                        transaction_id = 999,
                        guid = "xxxxxxxxxxxxxxxx",
                        creation_date = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.0000Z")
                    },
                    customer_info = new
                    {
                        product_id = 888,
                        request_id = 887
                    },
                    addition_info = new
                    {
                        comments = ""
                    }
                }, 
            });
        }

        public dynamic Transaction(dynamic data, string resouceAux = null)
        {

            var result = this._request.Post<dynamic, dynamic>(this._transaction_resource, data);
            return result;
        }

        public dynamic GetPayments(object data)
        {
            var result = this._request.Get<dynamic>(this._transaction_resource, data.ToDictionary());
            return result;
        }

        public dynamic GetDetails(string transactionId)
        {
            var result = this._request.Get<dynamic>($"{this._transaction_resource}/{transactionId}");
            return result;
        }

        public dynamic Execute(string transactionId, string payerId)
        {
            var result = this._request.Post<dynamic, dynamic>($"{this._transaction_resource}/{transactionId}/execute", new
            {
                payer_id = payerId
            });
            return result;
        }

        private string GetCardHash(Customer customer, CreditCard creditCard)
        {
            var baseKeyParams = new { encryption_key = this._secret };
            var baseKey = this._request.Get<dynamic>(this._transaction_resource + "/card_hash_key", baseKeyParams.ToDictionary());
            return CardHash.Make(baseKey.id, baseKey.public_key, creditCard);
        }

        public dynamic ExecutePayment(string email, decimal total, string description, string contry, string currency)
        {
            throw new NotImplementedException();
        }
    }

}