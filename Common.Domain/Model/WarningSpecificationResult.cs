using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Domain.Model
{
    public class WarningSpecificationResult
    {
        public WarningSpecificationResult()
        {
            this.IsValid = true;
        }
        public IEnumerable<string> Warnings { get; set; }

        public bool IsValid { get; set; }

        public string Message { get; set; }

        

    }
}
