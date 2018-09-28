using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain.CustomExceptions
{
    public class CustomNotAutorizedException : Exception
    {
        public CustomNotAutorizedException(string message)
            : base(message)
        {

        }

    }
}
