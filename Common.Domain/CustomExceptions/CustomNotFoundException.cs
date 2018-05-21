using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain.CustomExceptions
{

    public class CustomNotFoundException : Exception
    {
        public CustomNotFoundException(string message)
            : base(message)
        {

        }

    }
}
