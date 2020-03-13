using Common.Domain.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;

namespace Common.Mail
{
    public class Email : IEmail
    {

        private string smtpServer;
        private int smtpPortNumber;
        private string smtpPassword;
        private string smtpUser;
        private string textFormat;
        private readonly List<MailboxAddress> addressFrom;
        private readonly List<MailboxAddress> addressTo;


        public Email()
        {
            this.smtpPortNumber = 587;
            this.textFormat = TextFormat.Html.ToString();
            this.addressFrom = new List<MailboxAddress>();
            this.addressTo = new List<MailboxAddress>();
        }

        public void Config(string smtpServer, string smtpUser, string smtpPassword, int smtpPortNumber = 587, string textFormat = "HTML")
        {

            this.smtpServer = smtpServer;
            this.smtpUser = smtpUser;
            this.smtpPassword = smtpPassword;
            this.smtpPortNumber = smtpPortNumber;
            this.textFormat = textFormat;
        }

        public void AddAddressFrom(string name, string email)
        {
            this.addressFrom.Add(new MailboxAddress(name, email));
        }

        public void AddAddressTo(string name, string email)
        {
            this.addressTo.Add(new MailboxAddress(name, email));
        }

        public void Send(String subject, String content)
        {
            try
            {

                var mimeMessage = new MimeMessage();
                mimeMessage.From.AddRange(this.addressFrom);
                mimeMessage.To.AddRange(this.addressTo);

                mimeMessage.Subject = subject;

                mimeMessage.Body = new TextPart(this.textFormat)
                {
                    Text = content
                };

                using (var client = new SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    client.Connect(this.smtpServer, this.smtpPortNumber, SecureSocketOptions.SslOnConnect);
                    client.Authenticate(this.smtpUser, this.smtpPassword);
                    client.Send(mimeMessage);
                    client.Disconnect(true);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
