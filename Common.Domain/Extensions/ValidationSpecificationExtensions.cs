using Common.Domain.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Domain.Model
{
    public static class ValidationSpecificationExtensions
    {
        public static void ThrowCustomValidationException(this ValidationSpecificationResult result)
        {
            if (result.Errors.IsAny())
            {
                var errors = result.Errors.Distinct().ToList();
                throw new CustomValidationException(errors);

            }
        }

    }
}
