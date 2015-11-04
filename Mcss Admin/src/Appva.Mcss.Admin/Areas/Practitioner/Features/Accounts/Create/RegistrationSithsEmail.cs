// <copyright file="RegistrationSithsEmail.cs" company="Appva AB">
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
    public sealed class RegistrationSithsEmail
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
        /// The full tenant link.
        /// </summary>
        public string TenantLink
        {
            get;
            set;
        }
    }
}