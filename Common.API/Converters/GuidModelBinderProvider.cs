using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace Common.API
{
    public class GuidModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (!context.Metadata.IsComplexType && context.Metadata.ModelType == typeof(Guid))
                return new GuidBinder();

            if (!context.Metadata.IsComplexType && context.Metadata.ModelType == typeof(Guid?))
                return new GuidBinder();

            return null;
        }
    }

    internal class GuidBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            var value = valueProviderResult.FirstValue;

            var parsed =  Guid.TryParse(value, out var valueAsGuid);

            var result = ModelBindingResult.Success(valueAsGuid);
            if (!parsed)
            {
                result = ModelBindingResult.Failed();
                bindingContext.ModelState.AddModelError(bindingContext.ModelName, "Invalid Guid");
            }

            bindingContext.Result = result;

            return Task.FromResult(0);
        }
    }


}
