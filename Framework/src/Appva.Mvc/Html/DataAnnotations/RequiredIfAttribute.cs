using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Appva.Mvc.Html.DataAnnotations
{

    /// <summary>
    /// If the <see cref="Target"/> value is null then it is required, otherwise not.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class RequiredIfAttribute : ValidationAttribute, IClientValidatable {

        #region Public Fields.

        /// <summary>
        /// The target property to check. 
        /// </summary>
        public string Target { get; set; }
        
        /// <summary>
        /// The value that must be equal.
        /// </summary>
        public object Value { get; set; }

        #endregion

        #region Constructor.

        /// <summary>
        /// Default constructor.
        /// </summary>
        public RequiredIfAttribute() : base() { }

        #endregion

        #region Overridden Functions.

        protected override ValidationResult IsValid(object value, ValidationContext validationContext) {
            var target = validationContext.ObjectType.GetProperty(Target);
            if (target == null) {
                return new ValidationResult(string.Format("Unknown property {0}", target));
            }
            var targetValue = target.GetValue(validationContext.ObjectInstance, null);
            if (targetValue == null && Value == null && !string.IsNullOrWhiteSpace(value as string)) {
                return ValidationResult.Success;
            }
            if (targetValue.Equals(Value) && string.IsNullOrWhiteSpace(value as string)) {
                new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }
            return ValidationResult.Success; 
        }

        #endregion

        #region Public Functions.

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(
            ModelMetadata metadata,
            ControllerContext context
        ) {
            var rule = new ModelClientValidationRule { ErrorMessage = ErrorMessage, ValidationType = "requiredif" };
            rule.ValidationParameters["target"] = Target;
            rule.ValidationParameters["value"] = Value;
            yield return rule;
        }

        #endregion

    }

}