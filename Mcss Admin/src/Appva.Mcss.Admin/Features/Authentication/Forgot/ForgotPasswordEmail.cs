// <copyright file="ForgotPasswordEmail.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Models
{
    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ForgotPasswordEmail
    {
        /// <summary>
        /// The full human name.
        /// </summary>
        public string HumanName
        {
            get;
            set;
        }

        /// <summary>
        /// The full token link.
        /// </summary>
        public string TokenLink
        {
            get;
            set;
        }
    }
}