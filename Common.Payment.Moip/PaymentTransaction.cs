
using Common.Domain;
using Common.Domain.Base;
using Common.Domain.Model;
using IdentityModel.Client;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace Common.Payment.Moip
{
    public class PaymentTransaction : PaymentBase, IPaymentTransaction
    {

        private string _transaction_resource;
        public PaymentTransaction(IRequest request, ConfigPaymentBase config) : base(request, config)
        {
            this._request = request;
            this._request.SetAddress(this._endpoint);
        }

        public PaymentTransaction(IRequest request, IOptions<ConfigPaymentBase> config) :
                    this(request, config.Value)
        { }

        public dynamic ExecuteCreditCard(Customer customer, CreditCard creditCard, decimal total, string description = null, string contry = null, string currency = null, int parcelas = 1)
        {
            var order = this.Transaction(new
            {
                ownId = Guid.NewGuid().ToString(),
                amount = new
                {
                    currency = currency
                },
                items = new List<dynamic>() {
                    new
                    {
                        product = description,
                        quantity = 1,
                        detail = description,
                        price = Math.Round(total * 100, 0)
                    }
                }.ToArray(),
                customer = new
                {
                    ownId = customer.CustomerId,
                    fullname = $"{customer.FirstName} {customer.LastName}",
                    email = customer.Email,
                    birthDate = customer.BirthDate.ToString("yyyy-MM-dd"),
                    taxDocument = new
                    {
                        type = "CPF",
                        number = customer.DocumentNumber
                    },
                    phone = new
                    {
                        countryCode = customer.PhoneDDI,
                        areaCode = customer.PhoneDDD,
                        number = customer.PhoneNumber
                    },
                    shippingAddress = new
                    {
                        street = customer.Street,
                        streetNumber = customer.StreetNumber,
                        complement = customer.Complement,
                        district = customer.Neighborhood,
                        city = customer.City,
                        state = customer.State,
                        country = customer.Country,
                        zipCode = customer.ZipCode
                    }
                }
            }, "v2/orders");

            if (creditCard.Hash.IsNullOrEmpty())
                return CompleteCreditCard(customer, creditCard, description, order, parcelas);

            return HashCreditCard(customer, creditCard, description, order, parcelas);


        }

        private dynamic CompleteCreditCard(Customer customer, CreditCard creditCard, string description, dynamic order, int parcelas)
        {
            return this.Transaction(new
            {
                installmentCount = parcelas,
                statementDescriptor = description.Substring(0, 11),
                fundingInstrument = new
                {
                    method = "CREDIT_CARD",
                    creditCard = new
                    {
                        expirationMonth = creditCard.ExpireMonth,
                        expirationYear = creditCard.ExpireYear,
                        number = creditCard.Number,
                        cvc = creditCard.Cvv,
                        holder = new
                        {
                            fullname = $"{customer.FirstName} {customer.LastName}",
                            birthDate = customer.BirthDate.ToString("yyyy-MM-dd"),
                            taxDocument = new
                            {
                                type = "CPF",
                                number = customer.DocumentNumber
                            },
                            phone = new
                            {
                                countryCode = customer.PhoneDDI,
                                areaCode = customer.PhoneDDD,
                                number = customer.PhoneNumber
                            }
                        }
                    }
                },
                device = new
                {
                    ip = creditCard.Device.Ip,
                    geolocation = new
                    {
                        latitude = creditCard.Device.Geolocation.Latitude,
                        longitude = creditCard.Device.Geolocation.Longitude
                    },
                    userAgent = creditCard.Device.UserAgent,
                    fingerprint = creditCard.Device.Fingerprint
                }
            }, $"v2/orders/{order.id.Value}/payments");
        }

        private dynamic HashCreditCard(Customer customer, CreditCard creditCard, string description, dynamic order, int parcelas)
        {
            return this.Transaction(new
            {
                installmentCount = parcelas,
                statementDescriptor = description.Substring(0, 11),
                fundingInstrument = new
                {
                    method = "CREDIT_CARD",
                    creditCard = new
                    {
                        hash = creditCard.Hash,
                        holder = new
                        {
                            fullname = $"{customer.FirstName} {customer.LastName}",
                            birthDate = customer.BirthDate.ToString("yyyy-MM-dd"),
                            taxDocument = new
                            {
                                type = "CPF",
                                number = customer.DocumentNumber
                            },
                            phone = new
                            {
                                countryCode = customer.PhoneDDI,
                                areaCode = customer.PhoneDDD,
                                number = customer.PhoneNumber
                            }
                        }
                    }
                },
                device = new
                {
                    ip = creditCard.Device.Ip,
                    geolocation = new
                    {
                        latitude = creditCard.Device.Geolocation.Latitude,
                        longitude = creditCard.Device.Geolocation.Longitude
                    },
                    userAgent = creditCard.Device.UserAgent,
                    fingerprint = creditCard.Device.Fingerprint
                }
            }, $"v2/orders/{order.id.Value}/payments");
        }

        public dynamic ExecuteBankSlip(Customer customer, decimal total, string description = null, DateTime? expirationDate = null, string postbackUrl = null, string logoUri = null)
        {
            var order = this.Transaction(new
            {
                ownId = Guid.NewGuid().ToString(),
                amount = new
                {
                    currency = "BRL"
                },
                items = new List<dynamic>() {
                    new
                    {
                        product = description,
                        quantity = 1,
                        detail = description,
                        price = Math.Round(total * 100, 0)
                    }
                }.ToArray(),
                customer = new
                {
                    ownId = customer.CustomerId,
                    fullname = $"{customer.FirstName} {customer.LastName}",
                    email = customer.Email,
                    birthDate = customer.BirthDate.ToString("yyyy-MM-dd"),
                    taxDocument = new
                    {
                        type = customer.DocumentNumber.Length == 11 ? "CPF" : "CNPJ" ,
                        number = customer.DocumentNumber
                    },
                    phone = new
                    {
                        countryCode = customer.PhoneDDI,
                        areaCode = customer.PhoneDDD,
                        number = customer.PhoneNumber
                    },
                    shippingAddress = new
                    {
                        street = customer.Street,
                        streetNumber = customer.StreetNumber,
                        complement = "--",
                        district = customer.Neighborhood,
                        city = customer.City,
                        state = customer.State,
                        country = customer.Country,
                        zipCode = customer.ZipCode
                    }
                }
            }, "v2/orders");

            return this.Transaction(new
            {
                statementDescriptor = description.Substring(0, 11),
                fundingInstrument = new
                {
                    method = "BOLETO",
                    boleto = new
                    {
                        expirationDate = expirationDate.Value.ToString("yyyy-MM-dd"),
                        instructionLines = new
                        {
                            first = "Atenção",
                            second = "fique atento à data de vencimento do boleto.",
                            third = "Pague em qualquer casa lotérica."
                        },
                        logoUri
                    }
                }

            }, $"v2/orders/{order.id.Value}/payments");




        }

        public dynamic Transaction(dynamic data, string resouceAux = null)
        {
            var result = this._request.Post<dynamic, dynamic>(resouceAux, data);
            return result;
        }

        public dynamic GetPayments(object data)
        {
            var result = this._request.Get<dynamic>(this._transaction_resource, data.ToDictionary());
            return result;
        }

        public dynamic GetDetails(string transactionId)
        {
            var result = this._request.Get<dynamic>($"v2/payments/{transactionId}");
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

        protected override string GetAccessToken()
        {
            var _client = new TokenClient(this._authority_endpoint, this._client_Id, this._secret);
            var token = _client.RequestAuthorizationCodeAsync(Guid.NewGuid().ToString(), this._return_url).Result;
            return token.AccessToken;
        }

        public dynamic ExecutePayment(string email, decimal total, string description, string contry, string currency)
        {
            throw new NotImplementedException();
        }
    }

}