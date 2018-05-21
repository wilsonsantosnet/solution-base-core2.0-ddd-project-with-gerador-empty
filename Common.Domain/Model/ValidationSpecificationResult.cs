using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Domain.Model
{
    public class ValidationSpecificationResult
    {

        public IEnumerable<string> Errors { get; set; }

        public bool IsValid { get; set; }

        public string Message { get; set; }

        public ValidationSpecificationResult()
        {
            this.IsValid = true;
        }
    }
}
