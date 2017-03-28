// <copyright file="MobileTelephoneNumber.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Domain
{
    #region Imports.

    using System.Text.RegularExpressions;
    using Appva.Core;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class MobileTelephoneNumber : ContactPoint, IValidator
    {
        #region Variables.

        /// <summary>
        /// Mobile telephone number validation format.
        /// </summary>
        private readonly Regex IsValidMobileTelephoneFormat = new Regex(@"/^(?:(?:\(?(?:00|\+)([1-4]\d\d|[1-9]\d?)\)?)?[\-\.\ \\\/]?)?((?:\(?\d{1,}\)?[\-\.\ \\\/]?){0,})(?:[\-\.\ \\\/]?(?:#|ext\.?|extension|x)[\-\.\ \\\/]?(\d+))?$/i");

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="MobileTelephoneNumber"/> class.
        /// </summary>
        /// <param name="value">The mobile telephone number</param>
        public MobileTelephoneNumber(string value)
            : base(value)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MobileTelephoneNumber"/> class.
        /// </summary>
        /// <remarks>
        /// An NHibernate visible no-argument constructor.
        /// </remarks>
        protected MobileTelephoneNumber()
        {
        }

        #endregion

        #region IValidator Members.

        /// <inheritdoc />
        public bool IsValid()
        {
            return IsValidMobileTelephoneFormat.IsMatch(this.Value);
        }

        #endregion
    }
}