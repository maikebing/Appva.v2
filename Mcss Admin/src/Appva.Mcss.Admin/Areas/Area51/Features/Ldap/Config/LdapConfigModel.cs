// <copyright file="LdapConfigModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Area51.Models
{
    #region Imports.

    using Appva.Cqrs;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class LdapConfigModel : IRequest<bool>
    {
        #region Properties.

        /// <summary>
        /// If the connection is enabled
        /// </summary>
        public bool LdapConnectionIsEnabled
        {
            get;
            set;
        }

        /// <summary>
        /// The connection string
        /// </summary>
        [Required]
        public string LdapConnectionString
        {
            get;
            set;
        }

        /// <summary>
        /// The ldap credentials username
        /// </summary>
        public string LdapUsername
        {
            get;
            set;
        }

        /// <summary>
        /// The ldap credentials password
        /// </summary>
        public string LdapPassword
        {
            get;
            set;
        }

        /// <summary>
        /// The field for unique identifier
        /// </summary>
        [Required]
        public string UniqueIdentiferField
        {
            get;
            set;
        }

        /// <summary>
        /// The field for first name
        /// </summary>
        public string FirstNameField
        {
            get;
            set;
        }

        /// <summary>
        /// The field for last name
        /// </summary>
        public string LastNameField
        {
            get;
            set;
        }

        /// <summary>
        /// The field for mail
        /// </summary>
        public string MailField
        {
            get;
            set;
        }

        /// <summary>
        /// The field for HSA
        /// </summary>
        public string HsaField
        {
            get;
            set;
        }

        /// <summary>
        /// The field for Pin
        /// </summary>
        public string PinField
        {
            get;
            set;
        }

        /// <summary>
        /// The field for username
        /// </summary>
        public string UsernameField 
        { 
            get;
            set; 
        }

        #endregion

        
    }
}