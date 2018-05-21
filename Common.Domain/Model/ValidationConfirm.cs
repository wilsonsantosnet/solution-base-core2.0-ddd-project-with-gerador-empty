using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Domain.Model
{
    public class ValidationConfirm
    {
        public ValidationConfirm(string message, string verifyBehavior = null)
        {
            this.Message = message;
            this.VerifyBehavior = verifyBehavior;
        }

        public string Message { get; set; }
        public string VerifyBehavior { get; set; }
    }
    
}
