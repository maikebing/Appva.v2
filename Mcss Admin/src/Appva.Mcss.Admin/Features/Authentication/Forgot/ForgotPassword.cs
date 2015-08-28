// <copyright file="ForgotPassword.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Models
{
    #region Imports.

    using System.ComponentModel.DataAnnotations;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Domain.Entities;
    using DataAnnotationsExtensions;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class ForgotPassword : IRequest<bool>
    {
        /// <summary>
        /// The E-mail address.
        /// </summary>
        [Required(ErrorMessage = "E-postadress måste anges")]
        [Email(ErrorMessage = "E-postadress måste anges i korrekt format, t. ex. namn.efternamn@foretag.se.")]
        public string Email
        {
            get;
            set;
        }

        /// <summary>
        /// The personal identity number.
        /// </summary>
        [Required(ErrorMessage = "Personnummer måste anges")]
        [Mvc.PersonalIdentityNumber(ErrorMessage = "Personnummer är ej korrekt")]
        public PersonalIdentityNumber PersonalIdentityNumber
        {
            get;
            set;
        }

        /// <summary>
        /// Returns a hidden E-mail address, e.g. test****@example.com.
        /// </summary>
        public string HiddenEmailAddress
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.Email))
                {
                    return string.Empty;
                }
                var localPart = this.Email.Substring(0, this.Email.IndexOf("@"));
                if (localPart.Length == 0)
                {
                    return string.Empty;
                }
                var replacePart = localPart.Substring((int) (localPart.Length / 2));
                return this.Email.Replace(replacePart, createAsterisks(replacePart.Length));
            }
        }

        /// <summary>
        /// Creates a number of asterisks.
        /// </summary>
        /// <param name="length">The length</param>
        /// <returns>A string of asterisks</returns>
        private string createAsterisks(int length)
        {
            var result = string.Empty;
            for (var i = 0; i < length; i++)
            {
                result += "*";
            }
            return result;
        }
    }
}