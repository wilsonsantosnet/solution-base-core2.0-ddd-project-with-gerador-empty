using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Domain.Model
{
    public class WarningSpecificationResult
    {
        public WarningSpecificationResult()
        {
            this.IsValid = true;
        }
        public IEnumerable<string> Warnings { get; set; }

        public bool IsValid { get; set; }

        public string Message { get; set; }

    }

    public static class WarningSpecificationResultExtensions
    {

        public static WarningSpecificationResult Merge(this WarningSpecificationResult source, WarningSpecificationResult others)
        {
            if (others.IsNull())
                return source;
            
            if (source.IsNotNull())
            {
                source.IsValid = others.Warnings.IsAny() ? false : source.IsValid;
                var warnings = new List<string>();
                if (source.Warnings.IsAny()) warnings.AddRange(source.Warnings);
                if (others.Warnings.IsAny()) warnings.AddRange(others.Warnings);
                source.Warnings = warnings;
                return source;
            }
            source = others;
            return source;
        }

    }

}
