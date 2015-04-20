using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace Appva.Mvc.Html.DataAnnotations
{

    /// <summary>
    /// If the <see cref="Target"/> date is greater than this.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class DateGreaterThanAttribute : ValidationAttribute, IClientValidatable {

        #region Public Fields.

        /// <summary>
        /// The target property to check. 
        /// </summary>
        public string Target { get; set; }

        #endregion

        #region Constructor.

        /// <summary>
        /// Default constructor.
        /// </summary>
        public DateGreaterThanAttribute() : base() { }

        #endregion

        #region Overridden Functions.
        
        protected override ValidationResult IsValid(object value, ValidationContext validationContext) {
            Type i = typeof(DateTime);
            var target = validationContext.ObjectType.GetProperty(Target);
            if (target == null) {
                return new ValidationResult(string.Format("Unknown property {0}", target));
            }
            var targetValue = target.GetValue(validationContext.ObjectInstance, null);
            if (value != null && targetValue != null) {
                DateTime currentDate;
                DateTime targetDate;
                DateTime.TryParse(value.ToString(), out currentDate);
                DateTime.TryParse(targetValue.ToString(), out targetDate);
                if (currentDate <= targetDate) {
                    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
                }
            }
            return ValidationResult.Success;
        }

        #endregion

        #region Public Functions.

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(
            ModelMetadata metadata,
            ControllerContext context
        ) {
            var rule = new ModelClientValidationRule { ErrorMessage = ErrorMessage, ValidationType = "dategreaterthan" };
            rule.ValidationParameters["target"] = Target;
            yield return rule;
        }

        #endregion

    }

}