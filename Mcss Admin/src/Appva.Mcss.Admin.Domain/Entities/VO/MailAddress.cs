// <copyright file="MailAddress.cs" company="Appva AB">
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
    public class MailAddress : ContactPoint, IValidator
    {
        #region Variables.

        /// <summary>
        /// Mail validation format.
        /// </summary>
        private static readonly Regex IsValidMailFormat = new Regex(@"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$");

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="MailAddress"/> class.
        /// </summary>
        /// <param name="value">The mail address value.</param>
        public MailAddress(string value)
            : base(value)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MailAddress"/> class.
        /// </summary>
        /// <remarks>
        /// An NHibernate visible no-argument constructor.
        /// </remarks>
        protected MailAddress()
        {
        }

        #endregion

        #region IValidator Members.

        /// <inheritdoc />
        public bool IsValid()
        {
            return IsValidMailFormat.IsMatch(this.Value);
        }

        #endregion

        #region Public Static Builders.

        /// <summary>
        /// Creates a new instance of the <see cref="MailAddress"/> class.
        /// </summary>
        /// <param name="value">The mail address value.</param>
        /// <returns>A new <see cref="MailAddress"/> instance.</returns>
        public static MailAddress New(string value)
        {
            return new MailAddress(value);
        }

        #endregion
    }
}