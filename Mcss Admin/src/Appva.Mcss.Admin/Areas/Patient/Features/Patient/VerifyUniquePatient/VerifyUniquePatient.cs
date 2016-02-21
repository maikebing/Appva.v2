// <copyright file="VerifyUniquePatient.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Models
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Domain.Entities;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class VerifyUniquePatient : IRequest<bool>
    {
        /// <summary>
        /// The patient ID.
        /// </summary>
        public Guid? Id
        {
            get;
            set;
        }

        /// <summary>
        /// The personal identity number.
        /// </summary>
        public PersonalIdentityNumber PersonalIdentityNumber
        {
            get;
            set;
        }
    }
}