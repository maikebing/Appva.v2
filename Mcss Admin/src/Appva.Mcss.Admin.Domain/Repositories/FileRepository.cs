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
    using NHibernate.Criterion;

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
        /// <param name="isFilteredByImages">If true, only images will be fetched.</param>
        /// <returns>A collection of <see cref="DataFile"/>.</returns>
        IList<DataFile> GetUploadedFiles(bool? isFilteredByImages = null);
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
        public IList<DataFile> GetUploadedFiles(bool? isFilteredByImages = null)
        {
            var query = this.persistence.QueryOver<DataFile>()
                .Where(x => x.IsActive == true);

            if(isFilteredByImages.HasValue && isFilteredByImages.Value)
            {
                query = query.WhereRestrictionOn(x => x.ContentType).IsLike("image", MatchMode.Anywhere);
            }
            else if(isFilteredByImages.HasValue && isFilteredByImages.Value == false)
            {
                query = query.WhereRestrictionOn(x => x.ContentType).Not.IsInsensitiveLike("image", MatchMode.Anywhere);
            }

            return query.List();
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