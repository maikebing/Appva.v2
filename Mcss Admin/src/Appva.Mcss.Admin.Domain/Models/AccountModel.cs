// <copyright file="AccountModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Models
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class AccountModel
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountModel"/> class.
        /// </summary>
        public AccountModel()
        {
        }

        #endregion

        #region Members.

        /// <summary>
        /// The <see cref="Account"/> Id
        /// </summary>
        public Guid Id 
        {
            get; 
            set; 
        }

        /// <summary>
        /// User firstname
        /// </summary>
        public string FirstName 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// User lastname
        /// </summary>
        public string LastName 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// User fullname
        /// </summary>
        public string FullName 
        { 
            get;
            set;
        }

        /// <summary>
        /// Users perosnal identity number (e.g. Social security number)
        /// </summary>
        public string PersonalIdentityNumber 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// Users title
        /// </summary>
        public string Title 
        { 
            get; 
            set;
        }

        /// <summary>
        /// If the <see cref="Account"/> is active
        /// </summary>
        public bool IsActive 
        { 
            get;
            set;
        }

        /// <summary>
        /// If the <see cref="Account"/> is paused
        /// </summary>
        public bool IsPaused 
        { 
            get;
            set;
        }

        /// <summary>
        /// If the account has an expiring delegation
        /// </summary>
        public bool HasExpiringDelegation 
        { 
            get;
            set;
        }

        /// <summary>
        /// Days left until a delegation expire for the user. Negative value means account have expired delegation
        /// </summary>
        public int? DelegationDaysLeft
        {
            get;
            set;
        }

        #endregion
    }
}