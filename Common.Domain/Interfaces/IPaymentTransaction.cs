using Common.Domain.Model;
using System.Collections.Generic;

namespace Common.Domain
{
    public interface IPaymentTransaction
    {
        dynamic ExecutePaymentCreditCard(string email, CreditCard credit_card, decimal total, string description, string contry, string currency);
        dynamic ExecutePaymentPayPal(string email, decimal total, string description, string contry, string currency);
        dynamic Transaction(dynamic data);
        dynamic GetPayments(object data);
        dynamic GetPaymentsDetails(string transactionId);
        dynamic ExecutePayment(string transactionId, string payerId);
    }
}
