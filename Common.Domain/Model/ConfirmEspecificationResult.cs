using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Domain.Model
{
    public class ConfirmEspecificationResult
    {

        public IEnumerable<ValidationConfirm> Confirms { get; set; }

        public bool IsValid { get; set; }

        public ConfirmEspecificationResult()
        {
            this.IsValid = true;
        }
    }

}
