using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Domain.Model
{
    public class CreditCard
    {

        public string Number { get; set; }
        public string Cvv2 { get; set; }
        public string ExpireMonth { get; set; }
        public string ExpireYear { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Type { get; set; }

    }

}
