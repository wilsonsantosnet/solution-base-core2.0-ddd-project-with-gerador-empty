using Common.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Validation
{
    public interface IWarningSpecification<T>
    {
        WarningSpecification<T> WithRules(Func<KeyValuePair<string, Rule<T>>, bool> predicate);
        WarningSpecificationResult Validate(T entity);
    }
    public class WarningSpecification<T> : Dictionary<string, Rule<T>> , IWarningSpecification<T>
    {

        public WarningSpecification<T> WithRules(Func<KeyValuePair<string, Rule<T>>, bool> predicate)
        {
            var validationsFilter = new WarningSpecification<T>();
            foreach (var item in this.Where(predicate).ToDictionary(_ => _.Key, _ => _.Value))
                validationsFilter.Add(item.Key, item.Value);

            return validationsFilter;
        }

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
