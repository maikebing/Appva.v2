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
        #region Private Variables.

        /// <summary>
        /// The <see cref="IRoleService"/>.
        /// </summary>
        private readonly IRoleService roleService;

        /// <summary>
        /// The <see cref="ITaxonomyService"/>.
        /// </summary>
        private readonly ITaxonomyService taxonService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountTransformer"/> class.
        /// </summary>
        public AccountTransformer(IRoleService roleService, ITaxonomyService taxonService)
        {
            this.roleService = roleService;
            this.taxonService = taxonService;
        }

        #endregion

        #region IAccountTransformer Members.

        /// <inheritdoc />
        public AccountViewModel ToAccount(Account account)
        {
            //// FIXME: Is this ever used, or will be used?
            var superiors = this.roleService.MembersOfRole("_superioraccount");
            var taxon = this.taxonService.Find(account.Taxon.Id, TaxonomicSchema.Organization);
            var superiorList = superiors.Where(x => taxon.Path.Contains(x.Taxon.Path)).ToList();
            var superior = superiorList.Count() > 0 ? superiorList.First() : null;
            return new AccountViewModel()
            {
                Id = account.Id,
                Active = account.IsActive,
                FullName = account.FullName,
                UniqueIdentifier = account.PersonalIdentityNumber,
                Title = account.Title,
                Superior = superior != null ? superior.FullName : "Saknas",
                Account = account
            };
        }

        #endregion
    }
}