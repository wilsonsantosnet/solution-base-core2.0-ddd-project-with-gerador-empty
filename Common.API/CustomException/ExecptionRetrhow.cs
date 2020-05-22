using Microsoft.AspNetCore.Mvc;
using System;

namespace Common.API
{
    public class ExceptionRetrhow : Exception
    {
        public ObjectResult ObjectResult { get; protected set; }

        public ExceptionRetrhow(ObjectResult objectResult, string message, Exception innerException) 
            : base(message, innerException)
        {
            this.ObjectResult = objectResult;
        }

    }
}
