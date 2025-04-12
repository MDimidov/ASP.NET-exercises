using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;

namespace HouseRentingSystem.ModelBinders
{
    public class DecimalModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            ValueProviderResult valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (valueResult != ValueProviderResult.None
                && !string.IsNullOrWhiteSpace(valueResult.FirstValue))
            {
                decimal result = 0M;
                bool success = false;

                try
                {
                    string strValue = valueResult.FirstValue.Trim();

                    strValue = strValue.Replace(",", CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator);
                    strValue = strValue.Replace(".", CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator);

                    result = decimal.Parse(strValue);
                    //result = Convert.ToDecimal(strValue); 
                    success = true;
                }
                catch (FormatException fe)
                {
                    bindingContext.ModelState.AddModelError(bindingContext.ModelName, fe, bindingContext.ModelMetadata);
                }

                if (success)
                {
                    bindingContext.Result = ModelBindingResult.Success(result);
                }
            }

            return Task.CompletedTask;
        }
    }
}
