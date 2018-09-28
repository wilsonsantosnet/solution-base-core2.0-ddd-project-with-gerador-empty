using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Domain.Base
{
    public class ConfigEmailBase
    {
		public string SmtpServer { get; set; }

		public string SmtpUser { get; set; }

		public string SmtpPassword { get; set; }

        public string FromEmail { get; set; }

        public string FromText { get; set; }

        public string SmtpPortNumber { get; set; }

		public string TextFormat { get; set; }
	}
}
