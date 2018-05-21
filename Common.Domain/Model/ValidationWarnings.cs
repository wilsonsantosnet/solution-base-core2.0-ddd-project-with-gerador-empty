using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Domain.Model
{
    public class ValidationWarning
    {
        public IEnumerable<string> Warnings { get; set; }
        public string Message { get; set; }
        public string VerifyBehavior { get; set; }
    }
}
