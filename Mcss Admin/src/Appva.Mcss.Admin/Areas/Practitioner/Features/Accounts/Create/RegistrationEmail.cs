// <copyright file="RegistrationEmail.cs" company="Appva AB">
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
    public sealed class RegistrationEmail
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
        /// The user name.
        /// </summary>
        public string UserName
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

        /// <summary>
        /// The full tenant link.
        /// </summary>
        public string TenantLink
        {
            get;
            set;
        }
    }
}