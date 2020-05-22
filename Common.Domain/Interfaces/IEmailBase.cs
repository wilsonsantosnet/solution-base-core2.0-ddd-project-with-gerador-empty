using Common.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Domain.Interfaces
{
    public interface IEmailBase
    {
        void AddBcc(string email, string name);
        void AddAddressFrom(string email, string name);
        void AddAddressReplyTo(string email, string name);
        void AddAddressTo(string email, string name);
        InfoResult Send(String subject, String content);


    }
}
