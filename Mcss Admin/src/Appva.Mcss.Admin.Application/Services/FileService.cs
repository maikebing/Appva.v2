// <copyright file="FileService.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:kalle.jigfors@appva.se">Kalle Jigfors</a>
// </author>
namespace Appva.Mcss.Admin.Application.Services
{
    #region Imports.

    using Appva.Mcss.Admin.Application.Auditing;
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
        /// <summary>
        /// Gets a file by its id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        T Find<T>(Guid id);

        /// <summary>
        /// Stores an uploaded file
        /// </summary>
        /// <param name="xls"></param>
        void UploadFile<T>(T xls) where T : Resource;
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

        /// <summary>
        /// The <see cref="IAccountService"/> implementation.
        /// </summary>
        private readonly IAccountService accountService;

        /// <summary>
        /// The <see cref="IAuditService"/> implementation.
        /// </summary>
        private readonly IAuditService audit;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="FileService"/> class.
        /// </summary>
        /// <param name="repository"></param>
        public FileService(
            IFileRepository repository, 
            IIdentityService identityService,
            IAuditService auditservice ,
            IAccountService accountService)
        {
            this.repository         = repository;
            this.identityService    = identityService;
            this.accountService     = accountService;
            this.audit              = auditservice; 
        }

        #endregion

        #region IFileService members.

        /// <inheritdoc />
        public T Find<T>(Guid id) where T : Resource
        {
            var file = this.repository.Find<T>(id);
            if (file != null)
            {
                this.audit.Read("hämtade filen {0} (ref. {1})", file.Name, file.Id);
            }
            return file;
        }

        /// <inheritdoc />
        public void UploadFile<T>(T file) where T : Resource
        {
            file.UploadedBy = this.accountService.Load(this.identityService.PrincipalId);
            this.repository.Save<T>(file);
            this.audit.Create("uploaded file {0} (ref. {1})", file.Name, file.Id);
        }

        #endregion
    }
}
