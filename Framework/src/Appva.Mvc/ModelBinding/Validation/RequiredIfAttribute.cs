// <copyright file="RequiredIfAttribute.cs" company="Appva AB">
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

    #endregion

    /// <summary>
    /// If the <see cref="Target"/> value is null then it is required, otherwise not.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class RequiredIfAttribute : ValidationAttribute, IClientValidatable
    {
        #region Public Properties.

        /// <summary>
        /// The target property to check. 
        /// </summary>
        public string Target
        {
            get;
            set;
        }

        /// <summary>
        /// The value that must be equal.
        /// </summary>
        public object Value
        {
            get;
            set;
        }

        #endregion

        #region IClientValidatable Members.

        /// <inheritdoc />
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context) 
        {
            var rule = new ModelClientValidationRule { ErrorMessage = ErrorMessage, ValidationType = "requiredif" };
            rule.ValidationParameters["target"] = this.Target;
            rule.ValidationParameters["value"] = this.Value;
            yield return rule;
        }

        #endregion

        #region ValidationAttribute Overrides.

        /// <inheritdoc />
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var target = validationContext.ObjectType.GetProperty(this.Target);
            if (target == null)
            {
                return new ValidationResult(string.Format("Unknown property {0}", target));
            }
            var targetValue = target.GetValue(validationContext.ObjectInstance, null);
            if (targetValue == null && this.Value == null && !string.IsNullOrWhiteSpace(value as string))
            {
                return ValidationResult.Success;
            }
            if (targetValue.Equals(this.Value) && string.IsNullOrWhiteSpace(value as string))
            {
                new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }
            return ValidationResult.Success;
        }

        #endregion
    }
}