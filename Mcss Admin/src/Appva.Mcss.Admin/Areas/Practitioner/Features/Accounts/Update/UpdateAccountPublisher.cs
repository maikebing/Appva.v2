// <copyright file="UpdateAccountPublisher.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Modles.Handlers
{
    #region Imports.

    using Appva.Core.Extensions;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class UpdateAccountPublisher : RequestHandler<UpdateAccount, bool>
    {
        #region Private fields

        /// <summary>
        /// The <see cref="IAccountService"/> implementation
        /// </summary>
        private readonly IAccountService accounts;

        /// <summary>
        /// The <see cref="ISettingsService"/> implementation
        /// </summary>
        private readonly ISettingsService settings;

        /// <summary>
        /// The <see cref="ITaxonomyService"/> implementation
        /// </summary>
        private readonly ITaxonomyService taxonomies;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateAccountPublisher"/> class.
        /// </summary>
        public UpdateAccountPublisher(IAccountService accounts, ISettingsService settings, ITaxonomyService taxonomies)
        {
            this.accounts = accounts;
            this.settings = settings;
            this.taxonomies = taxonomies;
        }

        #endregion

        #region RequestHandler overrides

        public override bool Handle(UpdateAccount message)
        {
            var account = this.accounts.Find(message.Id);
            if (message.Taxon.IsEmpty())
            {
                message.Taxon = taxonomies.Roots(TaxonomicSchema.Organization).SingleOrDefault().Id.ToString();
            }
            this.accounts.Update(
                account,
                message.FirstName,
                message.LastName,
                message.Email,
                message.DevicePassword,
                message.PersonalIdentityNumber,
                this.taxonomies.Get(message.Taxon.ToGuid()),
                message.HsaId);
            return true;
        }

        #endregion
    }
}