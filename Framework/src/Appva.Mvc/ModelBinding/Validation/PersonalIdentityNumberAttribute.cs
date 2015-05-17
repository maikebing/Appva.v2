// <copyright file="PersonalIdentityNumberAttribute.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
// ReSharper disable CheckNamespace
namespace Appva.Mvc
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using Appva.Core;

    #endregion

    /// <summary>
    /// Validates a Unique identifier, such as a swedish personnummer.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public sealed class PersonalIdentityNumberAttribute : ValidationAttribute, IClientValidatable
    {
        #region IClientValidatable Members.

        /// <inheritdoc />
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context) 
        {
            var rule = new ModelClientValidationRule { ErrorMessage = this.ErrorMessage, ValidationType = "uniqueidentifier" };
            yield return rule;
        }

        #endregion

        #region ValidationAttribute Overrides.

        /// <inheritdoc />
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (! (value is IPersonalIdentityNumber))
            {
                return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
            }
            var validator = value as IPersonalIdentityNumber;
            return validator.IsValid() ? ValidationResult.Success : new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
        }

        #endregion
    }
}