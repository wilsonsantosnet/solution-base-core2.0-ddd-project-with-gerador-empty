using Common.Domain.Base;
using Common.Domain.Interfaces;
using Common.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Domain.Interfaces
{
    public interface IEmailQueue : IEmailBase
    {
        void Config(ConfigEmailBase config);
        void Config(string endpoint, string key, string templateId);
        void AddParameters(string key, string value);
        InfoResult Send(String subject, String content, Func<CircuitBreakerParameters, bool> onError, Func<string, int> onCheckCountException);
    }
}
