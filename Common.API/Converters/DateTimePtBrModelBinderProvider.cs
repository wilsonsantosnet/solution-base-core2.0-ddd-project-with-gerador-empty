using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace Common.API
{
    public class DateTimePtBrModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (!context.Metadata.IsComplexType && context.Metadata.ModelType == typeof(DateTime))
                return new DateTimePtBrBinder();
                
            if (!context.Metadata.IsComplexType && context.Metadata.ModelType == typeof(DateTime?))
                return new DateTimePtBrBinder();

            return null;
        }
    }

    internal class DateTimePtBrBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            var value = valueProviderResult.FirstValue;

            //var parsed = DateTime.TryParse(value, CultureInfo.GetCultureInfo("pt-BR").DateTimeFormat, DateTimeStyles.None, out outDate);
            var parsed = DateTime.TryParse(value, new CultureInfo("pt-BR").DateTimeFormat, DateTimeStyles.None, out DateTime outDate);

            var result = ModelBindingResult.Success(outDate);
            if (!parsed)
            {
                result = ModelBindingResult.Failed();
                bindingContext.ModelState.AddModelError(bindingContext.ModelName, "Data inválida");
            }

            bindingContext.Result = result;

            return Task.FromResult(0);
        }
    }


}
