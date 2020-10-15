
using Common.Domain;
using Common.Domain.Base;
using Common.Domain.Model;
using Microsoft.Extensions.Options;
using System;

namespace Common.Payment.Moip
{
    public class BillingAgreements : PaymentBase, IBillingAgreements
    {
        /// <summary>
        /// https://dev.wirecard.com.br/v1.5/reference#assinaturas
        /// </summary>
        private string _billing_resource;
        public BillingAgreements(IRequest request, ConfigPaymentBase config) : base(request, config)
        {
            this._billing_resource = "assinaturas/v1";
        }
        public BillingAgreements(IRequest request, IOptions<ConfigPaymentBase> config) : this(request, config.Value)
        {

        }

        public dynamic Create(dynamic data, string resouceAux = null)
        {
            var result = this._request.Post<dynamic, dynamic>($"{this._billing_resource}/subscriptions{resouceAux}", data);
            return result;
        }



        public dynamic Execute(string code)
        {
            var result = this._request.Post<dynamic, dynamic>($"{this._billing_resource}/{code}/agreement-execute", new
            {

            });
            return result;
        }

        public dynamic GetDetails(string code)
        {
            var result = this._request.Get<dynamic>($"{this._billing_resource}/subscriptions/{code}");
            return result;
        }

        public dynamic GetTransactions(string code)
        {
            return this.GetTransactions(code, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.AddMonths(12).ToString("yyyy-MM-dd"));
        }

        public dynamic GetTransactions(string code, string startDate, string endDate)
        {
            var result = this._request.Get<dynamic>($"{this._billing_resource}/subscriptions/{code}/invoices");
            return result;
        }

        public dynamic Cancel(string code)
        {
            var result = this._request.Post<dynamic, dynamic>($"{this._billing_resource}/subscriptions/{code}/cancel", new
            {
                note = "Canceling the profile."
            });
            return result;
        }

        public dynamic SignaturePlanCreditCard(Customer customer, CreditCard credit_card, string planId, string description, decimal amount)
        {
            return this.Create(new
            {
                code = Guid.NewGuid().ToString(),
                amount = amount.ToString().Replace(".", "").Replace(",", ""),
                plan = new
                {
                    code = planId
                },
                customer = new
                {
                    code = customer.CustomerId,
                    email = customer.Email,
                    fullname = $"{customer.FirstName} {customer.LastName}",
                    cpf = customer.DocumentNumber,
                    phone_number = customer.PhoneNumber,
                    phone_area_code = customer.PhoneDDD,
                    birthdate_day = customer.BirthDate.Day,
                    birthdate_month = customer.BirthDate.Month,
                    birthdate_year = customer.BirthDate.Year,
                    address = new
                    {
                        street = customer.Street,
                        number = customer.StreetNumber,
                        complement = "--",
                        district = customer.Neighborhood,
                        city = customer.City,
                        state = customer.State,
                        country = customer.Country,
                        zipcode = customer.ZipCode.Replace("-", "")
                    },
                    billing_info = new
                    {
                        credit_card = new
                        {

                            holder_name = $"{credit_card.FirstName} {credit_card.LastName}",
                            number = $"{credit_card.Number}",
                            expiration_month = credit_card.ExpireMonth,
                            expiration_year = credit_card.ExpireYear.Length > 2 ? credit_card.ExpireYear.Substring(2, 2) : credit_card.ExpireYear

                        }
                    }
                }
            }, "?new_customer=true");
        }

        public dynamic SignaturePlanBankSlip(Customer customer, string planId, string description, decimal amount)
        {
            return this.Create(new
            {
                code = Guid.NewGuid().ToString(),
                amount = amount.ToString().Replace(".", "").Replace(",", ""),
                payment_method = "BOLETO",
                plan = new
                {
                    code = planId
                },
                customer = new
                {
                    code = customer.CustomerId,
                    email = customer.Email,
                    fullname = customer.FirstName,
                    cpf = customer.LastName,
                    phone_number = customer.PhoneNumber,
                    phone_area_code = customer.PhoneDDD,
                    birthdate_day = customer.Email,
                    birthdate_month = customer.Email,
                    birthdate_year = customer.Email,
                    address = new
                    {
                        street = customer.Street,
                        number = customer.StreetNumber,
                        complement = "--",
                        district = customer.Neighborhood,
                        city = customer.City,
                        state = customer.State,
                        country = customer.Country,
                        zipcode = customer.ZipCode
                    },
                }
            });
        }

        public dynamic SignaturePlan(string planId, string description)
        {
            throw new NotImplementedException();
        }
     
    }
}