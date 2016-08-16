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

        #endregion
        /// <summary>
        /// Initializes a new instance of the <see cref="FileService"/> class.
        /// </summary>
        /// <param name="repository"></param>
        public FileService(IFileRepository repository)
        {
            this.repository = repository;
        }

        /// <inheritdoc />
        public void SaveXLS(XLS xls)
        {
            this.repository.Save(xls);
        }
    }
}
