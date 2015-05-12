// <copyright file="InActivateAccountModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Practitioner.Features.Accounts.InActivate
{
    #region Imports.

    using Appva.Cqrs;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class InActivateAccountModel : IRequest<bool>
    {
        /// <summary>
        /// The account ID.
        /// </summary>
        public Guid AccountId
        {
            get;
            set;
        }
    }
}