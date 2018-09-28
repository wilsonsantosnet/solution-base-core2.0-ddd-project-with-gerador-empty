using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Domain.Interfaces
{
    public interface IEmail
    {
        void Config(string smtpServer, string smtpUser, string smtpPassword, int smtpPortNumber = 587, string textFormat = "Html");
        void AddAddressFrom(string name, string email);
        void AddAddressTo(string name, string email);
        void Send(String subject, String content);


    }
}
