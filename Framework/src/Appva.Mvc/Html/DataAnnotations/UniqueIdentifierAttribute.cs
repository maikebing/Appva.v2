using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web;
using System.Web.Mvc;
using Appva.Core.Extensions;

namespace Appva.Mvc.Html.DataAnnotations
{

    /// <summary>
    /// Validates a Unique identifier, such as a swedish personnummer.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class UniqueIdentifierAttribute : ValidationAttribute, IClientValidatable {
        
        #region Constructor.

        /// <summary>
        /// Default constructor.
        /// </summary>
        public UniqueIdentifierAttribute() : base() { }

        #endregion

        #region Overridden Functions.

        protected override ValidationResult IsValid(object value, ValidationContext validationContext) {
            if (!UniqueIdentifierValidator.Validate(value)) {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }
            return ValidationResult.Success;
        }

        #endregion

        #region Public Functions.

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(
            ModelMetadata metadata,
            ControllerContext context
        ) {
            var rule = new ModelClientValidationRule { ErrorMessage = ErrorMessage, ValidationType = "uniqueidentifier" };
            yield return rule;
        }

        #endregion

    }

    /// <summary>
    /// The validator.
    /// </summary>
    public class UniqueIdentifierValidator {

        public static bool Validate(object str) {

            // TODO: Make sure we can parse more than swedish...
            // var twoLetterIsoLanguageName = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            // So make this as a factory instad.

            var ssn = str as string;
            if (ssn.IsEmpty()) {
                return false;
            }
            var isLength = ssn.Length.Equals(13);

            var contains = ssn.Contains("-");
            var stripped = ssn.Strip("-");
            var first = ssn.Strip("-").First(10).Is(Char.IsNumber);
            if (ssn.Length.Equals(13) &&
                ssn.Contains("-") &&
                ssn.Strip("-").First(10).Is(Char.IsDigit)
            ) {
                var length = ssn.Substring(0, 8);
                DateTime result;
                if (!DateTime.TryParseExact(ssn.Substring(0, 8), "yyyyMMdd", CultureInfo.CurrentCulture, DateTimeStyles.None, out result)) {
                    return false;
                };

                ssn = ssn.Strip("-").Substring(2);

                if (!ssn.Substring(ssn.Length - 1, 1).Is(Char.IsDigit)) {
                    return false;
                }

                // If the second to last digit is a letter, then it might be a temporary ssn.
                // Then we 
                if (ssn.Substring(ssn.Length - 2, 1).Is(Char.IsLetter)) {
                    return true;
                }

                int summary = 0;
                for (int i = 0; i < ssn.Length; i++) {
                    int number = int.Parse(ssn[i].ToString());
                    int partialsummary = 0;
                    if (i % 2 == 0) {
                        partialsummary = number * 2;
                        if (partialsummary > 9) {
                            partialsummary -= 9;
                        }
                    } else {
                        partialsummary = number;
                    }
                    summary += partialsummary;
                }

                return (summary % 10 == 0);
            } else {
                return false;
            }

        }

    }

}
