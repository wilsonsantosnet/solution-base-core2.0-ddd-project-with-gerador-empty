using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain.CustomExceptions
{
    public class CustomValidationException : Exception
    {
        public IList<string> Errors { get; private set; }

        public CustomValidationException(IList<string> erros)
        {
            this.Errors = erros;
        }

        public CustomValidationException(IEnumerable<string> erros)
            : base(erros.FirstOrDefault())
        {

            this.Errors = new List<string>();
            foreach (var item in erros)
                this.Errors.Add(item);
        }
        public CustomValidationException(string message)
            : base(message)
        {
            this.Errors = new List<string> { message };
        }
    }



}
