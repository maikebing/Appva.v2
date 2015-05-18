// <copyright file="DateLessThanOrEqualsAttribute.cs" company="Appva AB">
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
    /// If the <see cref="Target"/> date is less than this.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public sealed class DateLessThanOrEqualsAttribute : ValidationAttribute, IClientValidatable
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

        #endregion

        #region IClientValidatable Members.

        /// <inheritdoc />
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule { ErrorMessage = ErrorMessage, ValidationType = "datelessthan" };
            rule.ValidationParameters["target"] = this.Target;
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
            if (value != null && targetValue != null)
            {
                DateTime currentDate;
                DateTime targetDate;
                DateTime.TryParse(value.ToString(), out currentDate);
                DateTime.TryParse(targetValue.ToString(), out targetDate);
                if (currentDate > targetDate)
                {
                    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
                }
            }
            return ValidationResult.Success;
        }

        #endregion
    }
}