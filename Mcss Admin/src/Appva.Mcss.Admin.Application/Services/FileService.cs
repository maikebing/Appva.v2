// <copyright file="FileService.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Application.Services
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Domain.Repositories;

    #endregion

    /// <summary>
    /// The <see cref="DataFile"/> service.
    /// </summary>
    public interface IFileService : IService
    {
        /// <summary>
        /// Get a file by its <see cref="Guid"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A <see cref="DataFile"/>.</returns>
        DataFile Get(Guid id);

        /// <summary>
        /// Gets a collection of uploaded files.
        /// </summary>
        /// <returns>A <see cref="IList{DataFile}"/> collection.</returns>
        IList<DataFile> GetUploadedFiles();
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class FileService : IFileService
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IFileRepository"/>.
        /// </summary>
        private readonly IFileRepository repository;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="FileService"/> class.
        /// </summary>
        /// <param name="repository">The <see cref="IFileRepository"/>.</param>
        public FileService(IFileRepository repository)
        {
            this.repository = repository;
        }

        #endregion

        #region IFileService members.

        /// <inheritdoc />
        public IList<DataFile> GetUploadedFiles()
        {
            return this.repository.GetUploadedFiles();
        }

        /// <inheritdoc />
        public DataFile Get(Guid id)
        {
            return this.repository.Get(id);
        }

        #endregion
    }
}