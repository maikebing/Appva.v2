// <copyright file="AdminModelBinder.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Infrastructure
{
    #region Imports.

    using System.Collections.Generic;
    using System.Web.Mvc;
    using Appva.Domain;
    using Appva.Mcss.Admin.Domain.Entities;

    #endregion

    /// <summary>
    /// A <see cref="PersonalIdentityNumber"/> model binder.
    /// </summary>
    public class PersonalIdentityNumberModelBinder : IModelBinder
    {
        #region IModelBinder Members.

        /// <inheritdoc />
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var result = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            bindingContext.ModelState.Add(bindingContext.ModelName, new ModelState
            {
                Value = result
            });
            return new PersonalIdentityNumber(result.AttemptedValue);
        }

        #endregion
    }

    public class DateModelBinder : IModelBinder
    {
        #region IModelBinder Members.

        /// <inheritdoc />
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var result = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            bindingContext.ModelState.Add(bindingContext.ModelName, new ModelState
            {
                Value = result
            });
            Date date;
            if (Date.TryParse(result.AttemptedValue, out date))
            {
                return date;
            }
            return null;
        }

        #endregion
    }

    public class OnModelBinder : IModelBinder
    {
        private static readonly IList<string> ValuesOfTruthness = new List<string> { "on", "true", "1", "true,false" /* mvc when true */ };

        #region IModelBinder Members.

        /// <inheritdoc />
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var result = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            bindingContext.ModelState.Add(bindingContext.ModelName, new ModelState
            {
                Value = result
            });
            if (string.IsNullOrWhiteSpace(result.AttemptedValue))
            {
                return false;
            }
            if (ValuesOfTruthness.Contains(result.AttemptedValue.ToLowerInvariant()))
            {
                return true;
            }
            
            return false;
        }

        #endregion
    }
}