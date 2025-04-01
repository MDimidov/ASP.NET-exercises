using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;

namespace Practice.ModelBinders
{
    public class DecimalModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            ValueProviderResult valueProviderResult = bindingContext
                .ValueProvider
                .GetValue(bindingContext.ModelName);

            string? stringValue = valueProviderResult.FirstValue?.Trim();

            if (valueProviderResult != ValueProviderResult.None &&
                !string.IsNullOrWhiteSpace(stringValue))
            {
                decimal decimalValue = 0m;
                bool binderSucceed = false;

                try
                {
                    stringValue = stringValue.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                    stringValue = stringValue.Replace(",", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);

                    decimalValue = Convert.ToDecimal(stringValue);

                    binderSucceed = true;
                }
                catch (FormatException ef)
                {
                    bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, ef, bindingContext.ModelMetadata);
                }

                if (binderSucceed)
                {
                    bindingContext.Result = ModelBindingResult.Success(decimalValue);
                }
            }

            return Task.CompletedTask;
        }
    }
}
