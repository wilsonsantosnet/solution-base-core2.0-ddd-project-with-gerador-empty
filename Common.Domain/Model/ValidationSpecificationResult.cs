using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Domain.Model
{
    public class ValidationSpecificationResult
    {

        public IEnumerable<string> Errors { get; set; }

        public bool IsValid { get; set; }

        public string Message { get; set; }

        public ValidationSpecificationResult()
        {
            this.IsValid = true;
        }

        public bool IsDefaultValue()
        {

            return this.IsValid && Errors.IsNotAny();
        }

    }

    public static class ValidationSpecificationResultExtensions
    {

        public static ValidationSpecificationResult Merge(this ValidationSpecificationResult source, ValidationSpecificationResult others)
        {
             if (others.IsNull())
                return source;
            
            if (source.IsNotNull())
            {
                source.IsValid = others.Errors.IsAny() ? false : source.IsValid;
                var erros = new List<string>();
                if (source.Errors.IsAny()) erros.AddRange(source.Errors);
                if (others.Errors.IsAny()) erros.AddRange(others.Errors);
                source.Errors = erros;
                return source;
            }
            source = others;
            return source;
        }

    }
}
