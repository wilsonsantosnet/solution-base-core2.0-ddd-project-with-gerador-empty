using Common.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Validation
{
    public class WarningSpecification<T> : Dictionary<string, Rule<T>>
    {

        public WarningSpecificationResult Validate(T entity)
        {
            var isValid = true;
            var warnings = new List<string>();
            foreach (var item in this)
            {
                if (!item.Value.GetSpecification().IsSatisfiedBy(entity))
                {
                    isValid = false;
                    warnings.Add(item.Value.GetMessage());
                }
            }

            return new WarningSpecificationResult {

                Warnings = warnings,
                IsValid = isValid,
            };
        }

    }
}
