// <copyright file="AccountTransformer.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Infrastructure
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Security.Identity;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Web;
    using Appva.Mcss.Web.ViewModels;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface IAccountTransformer
    {
        AccountViewModel ToAccount(Account account);
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class AccountTransformer : IAccountTransformer
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IIdentityService"/>.
        /// </summary>
        private readonly IIdentityService identityService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountTransformer"/> class.
        /// </summary>
        public AccountTransformer(IIdentityService identityService)
        {
            this.identityService = identityService;
        }

        #endregion

        #region IAccountTransformer Members.

        /// <inheritdoc />
        public AccountViewModel ToAccount(Account account)
        {
            var principalLocationPath = this.identityService.Principal.LocationPath();
            var IsEditableForCurrentPrincipal = account.Locations.OrderByDescending(x => x.Sort).First().Taxon.Path.StartsWith(principalLocationPath);
            return new AccountViewModel
            {
                Id                            = account.Id,
                Active                        = account.IsActive,
                FullName                      = account.FullName,
                UniqueIdentifier              = account.PersonalIdentityNumber,
                Title                         = account.Title,
                IsPaused                      = account.IsPaused,
                IsEditableForCurrentPrincipal = IsEditableForCurrentPrincipal
            };
        }

        #endregion
    }
}