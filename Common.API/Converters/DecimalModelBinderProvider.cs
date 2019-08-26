using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace Common.API
{
    public class DecimalModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (!context.Metadata.IsComplexType && context.Metadata.ModelType == typeof(decimal))
                return new DecimalBinder();

            if (!context.Metadata.IsComplexType && context.Metadata.ModelType == typeof(decimal?))
                return new DecimalBinder();

            return null;
        }
    }

    internal class DecimalBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            var value = valueProviderResult.FirstValue;

            var parsed =  decimal.TryParse(value, out var valueAsDecimal);

            var result = ModelBindingResult.Success(valueAsDecimal);
            if (!parsed)
            {
                result = ModelBindingResult.Failed();
                bindingContext.ModelState.AddModelError(bindingContext.ModelName, "Invalid Decimal");
            }

            bindingContext.Result = result;

            return Task.FromResult(0);
        }
    }


}
