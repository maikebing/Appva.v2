// <copyright file="FileService.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:kalle.jigfors@appva.se">Kalle Jigfors</a>
// </author>
namespace Appva.Mcss.Admin.Application.Services
{
    #region Imports.

    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Domain.Repositories.Contracts;
    using Security.Identity;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    #endregion
    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface IFileService : IService
    {
        void SaveXLS(XLS xls);
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class FileService : IFileService
    {
        #region Variables
        /// <summary>
        /// The <see cref="IFileRepository"/> implementation.
        /// </summary>
        private readonly IFileRepository repository;

        /// <summary>
        /// The <see cref="IIdentityService"/> implementation.
        /// </summary>
        private readonly IIdentityService identityService;
        private readonly IAccountService accountService;

        #endregion
        /// <summary>
        /// Initializes a new instance of the <see cref="FileService"/> class.
        /// </summary>
        /// <param name="repository"></param>
        public FileService(IFileRepository repository, IIdentityService identityService, IAccountService accountService)
        {
            this.repository = repository;
            this.identityService = identityService;
            this.accountService = accountService;
        }

        /// <inheritdoc />
        public void SaveXLS(XLS xls)
        {
            xls.UploadedBy = this.accountService.Load(this.identityService.PrincipalId);
            this.repository.Save(xls);
        }
    }
}
