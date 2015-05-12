// <copyright file="CreateAccountPublisher.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using Appva.Core.Extensions;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Domain.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class CreateAccountPublisher : RequestHandler<CreateAccountModel, bool>
    {
        #region Private fields.

        /// <summary>
        /// The <see cref="IAccountService"/>.
        /// </summary>
        private readonly IAccountService accounts;

        /// <summary>
        /// The <see cref="ISettingsService"/>.
        /// </summary>
        private readonly ISettingsService settings;

        /// <summary>
        /// The <see cref="ITaxonomyService"/>.
        /// </summary>
        private readonly ITaxonomyService taxonomies;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateAccountPublisher"/> class.
        /// </summary>
        public CreateAccountPublisher(IAccountService accounts, ISettingsService settings, ITaxonomyService taxonomies)
        {
            this.accounts = accounts;
            this.settings = settings;
            this.taxonomies = taxonomies;
        }

        #endregion

        #region RequestHandler overrides

        public override bool Handle(CreateAccountModel message)
        {
            if(this.settings.Find<bool>(ApplicationSettings.AutogeneratePasswordForMobileDevice, true))
            {
                var r = new Random();
                message.DevicePassword = string.Format("{0}{1}{2}{3}",r.Next(9),r.Next(9),r.Next(9),r.Next(9));
            }
            if (message.Taxon.IsEmpty())
            {
                message.Taxon = taxonomies.Roots(TaxonomicSchema.Organization).SingleOrDefault().Id.ToString();
            }

            return this.accounts.Create(
                message.FirstName,
                message.LastName,
                message.Email,
                message.DevicePassword,
                new PersonalIdentityNumber(message.PersonalIdentityNumber),
                this.taxonomies.Get(message.Taxon.ToGuid())).IsNotEmpty();
        }

        #endregion
    }
}