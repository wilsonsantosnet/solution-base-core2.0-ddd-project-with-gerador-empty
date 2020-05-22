using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Domain.Model
{
    public enum EInfoResult { 
    
        Success,
        Error,
        Wait
    }
    public class InfoResult
    {
        public EInfoResult Type { get; set; }
        public string GeneralInfo { get; set; }

    }
}
