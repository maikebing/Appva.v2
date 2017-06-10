// <copyright file="GetCredentialsHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:$emailAddress$">$developer$</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Practitioner.Features.Accounts.GetCredentials
{
    #region Imports.

    using Appva.Core.IO;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Areas.Practitioner.Models;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Domain.Models;
    using Appva.Mvc.Localization;
    using Appva.Office;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class GetCredentialsHandler : RequestHandler<UserCredentialsFile, FileContentResult>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IAccountService"/>.
        /// </summary>
        private readonly IAccountService accountService;

        /// <summary>
        /// The <see cref="IHtmlLocalizer"/>.
        /// </summary>
        private readonly IHtmlLocalizer localizer;

        /// <summary>
        /// The <see cref="ITaxonFilterSessionHandler"/>.
        /// </summary>
        private readonly ITaxonFilterSessionHandler taxonFilter;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="GetCredentialsHandler"/> class.
        /// </summary>
        public GetCredentialsHandler(
            IAccountService accountService, 
            IHtmlLocalizer localizer,
            ITaxonFilterSessionHandler taxonFilter)
        {
            this.accountService = accountService;
            this.localizer      = localizer;
            this.taxonFilter    = taxonFilter;
        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override FileContentResult Handle(UserCredentialsFile message)
        {
            var path = this.taxonFilter.GetCurrentFilter().Path;
            var accounts = this.accountService.List()
                .Where(x => x.IsActive == true && x.IsPaused == false)
                .Where(x => x.Taxon.Path.Contains(path) || path.Contains(x.Taxon.Path)).ToList();

            //this.auditing.Read("skapade excellista för perioden {0} t o m {1}.", message.StartDate, message.EndDate);

            var templatePath = PathResolver.ResolveAppRelativePath("Templates\\user-credentials-template.xlsx");
            var bytes = ExcelWriter.CreateNew<Account, Credentials>(
                templatePath,
                x => new Credentials
                {
                    UserFullName = x.FullName,
                    UserId       = x.PersonalIdentityNumber != null ? x.PersonalIdentityNumber.Value : string.Empty,
                    Password     = x.DevicePassword
                },
                accounts);
            return new FileContentResult(bytes, "application/vnd.ms-excel")
            {
                FileDownloadName = this.localizer["Users-{0}.xlsx", this.taxonFilter.GetCurrentFilter().Address.Replace(" ", "-"), DateTime.Now.ToFileTimeUtc()].ToString()
            };
        }

        #endregion
    }

    public class Credentials
    {
        #region Properties.

        /// <summary>
        /// The user  human full name
        /// </summary>
        public string UserFullName
        {
            get;
            set;
        }

        /// <summary>
        /// The user-id
        /// </summary>
        public string UserId
        {
            get;
            set;
        }

        /// <summary>
        /// The device password
        /// </summary>
        public string Password
        {
            get;
            set;
        }

        #endregion
    }
}