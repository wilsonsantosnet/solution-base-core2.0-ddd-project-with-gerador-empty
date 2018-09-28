using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain.CustomExceptions
{
    public class CustomBadRequestException : Exception
    {
        public CustomBadRequestException(string message)
            : base(message)
        {

        }

    }

}
