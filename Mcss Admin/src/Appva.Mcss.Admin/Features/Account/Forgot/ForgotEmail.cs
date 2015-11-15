// <copyright file="ForgotEmail.cs" company="Appva AB">
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
    public sealed class ForgotEmail
    {
        /// <summary>
        /// The full name.
        /// </summary>
        public string Name
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