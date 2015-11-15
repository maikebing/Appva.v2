// <copyright file="AdminModelBinder.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Infrastructure
{
    #region Imports.

    using System.Web.Mvc;
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
}