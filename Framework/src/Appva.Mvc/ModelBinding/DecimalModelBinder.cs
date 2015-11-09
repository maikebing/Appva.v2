// <copyright file="DecimalModelBinder.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.alvegard@appva.se">Richard Alvegard</a>
// </author>
namespace Appva.Mvc.ModelBinding
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// Binds decimal values separated with eithe "," or "." to the model
    /// </summary>
    public sealed class DecimalModelBinder : IModelBinder
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="DecimalModelBinder"/> class.
        /// </summary>
        public DecimalModelBinder()
        {
        }

        #endregion

        #region IModelBinder members

        /// <summary>
        /// Gets the input value, replaces the current separator with 
        /// the wanted separator and converts the value to a decimal
        /// </summary>
        /// <param name="controllerContext"></param>
        /// <param name="bindingContext"></param>
        /// <returns></returns>
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            ValueProviderResult valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            var wantedSeperator = NumberFormatInfo.CurrentInfo.NumberDecimalSeparator;
            var separator = (wantedSeperator == "," ? "." : ",");
            var value = valueResult.AttemptedValue.Replace(separator, wantedSeperator);

            ModelState modelState = new ModelState { Value = valueResult };
            
            object actualValue = null;
            try 
            {
                actualValue = Convert.ToDecimal(value, CultureInfo.CurrentCulture);
            }
            catch (FormatException e) 
            {
                modelState.Errors.Add(e);
            }

            bindingContext.ModelState.Add(bindingContext.ModelName, modelState);
            return actualValue;
        }

        #endregion
    }
}