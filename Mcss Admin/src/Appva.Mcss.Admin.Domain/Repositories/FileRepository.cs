// <copyright file="FileRepository.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Domain.Repositories
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Persistence;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface IFileRepository : IRepository
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
        /// <returns></returns>
        IList<DataFile> GetUploadedFiles();
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class FileRepository : IFileRepository
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext persistence;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="FileRepository"/> class.
        /// </summary>
        /// <param name="persistence">The <see cref="IPersistenceContext"/>.</param>
        public FileRepository(IPersistenceContext persistence)
        {
            this.persistence = persistence;
        }

        #endregion

        #region IFileRepository members.

        /// <inheritdoc />
        public IList<DataFile> GetUploadedFiles()
        {
            return this.persistence.QueryOver<DataFile>()
                .Where(x => x.IsActive == true)
                    .List();
        }

        /// <inheritdoc />
        public DataFile Get(Guid id)
        {
            return this.persistence.QueryOver<DataFile>()
                .Where(x => x.Id == id)
                    .SingleOrDefault();
        }

        #endregion
    }
}