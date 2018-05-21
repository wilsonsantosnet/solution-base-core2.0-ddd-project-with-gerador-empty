using Common.Domain.CustomExceptions;
using Common.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dto
{
    public class ValidatorAnnotations<T> where T : class
    {
        private ValidationSpecificationResult result = new ValidationSpecificationResult();

        public ValidatorAnnotations()
        {
            result.Errors = new List<string>();
        }
        public IEnumerable<string> GetErros()
        {
            return result.Errors.Distinct().ToList();
        }

        public void Validate(T entity)
        {
            if (entity.IsNull())
                return;

            result = this.GetValidationAnnotationsResult(entity);
        }

        public void Validate(IEnumerable<T> entitys)
        {
            if (entitys.IsNull())
                return;

            foreach (var entity in entitys)
                Validate(entity);
        }

        private ValidationSpecificationResult GetValidationAnnotationsResult(T entity)
        {

            var errors = new List<string>();

            var prop = entity.GetType().GetProperties();

            foreach (var item in prop)
            {
                var attsCustom = item.GetCustomAttributes(typeof(RequiredAllowCustomAttribute), true);
                if (attsCustom.IsAny())
                    continue;

                var atts = item.GetCustomAttributes(typeof(ValidationAttribute), true);
                foreach (ValidationAttribute att in atts)
                {
                   

                    var propinfo = entity.GetType().GetTypeInfo().GetProperty(item.Name);
                    var propValue = propinfo.GetValue(entity);

                    if (!att.IsValid(propValue))
                        errors.Add(att.ErrorMessage);

                    if (propinfo.PropertyType == typeof(DateTime) && default(DateTime) == (DateTime)propValue)
                        errors.Add(att.ErrorMessage);

                    if (att.GetType() == typeof(RequiredAttribute))
                    {
                        if (propinfo.PropertyType == typeof(int))
                        {
                            if (default(int) == (int)propValue && atts.Where(_ => _.GetType() == typeof(RangeAttribute)).IsNotAny())
                                errors.Add(att.ErrorMessage);
                        }
                    }

                }

            }

            return new ValidationSpecificationResult { Errors = errors };
        }

    }

}
