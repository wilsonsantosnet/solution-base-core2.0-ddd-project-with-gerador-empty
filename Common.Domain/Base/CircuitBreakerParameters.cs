using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Domain.Model
{
    public class CircuitBreakerParameters
    {
        public Exception Exception { get; set; }
        public string Process { get; set; }
    }

}
