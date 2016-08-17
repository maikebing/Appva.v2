// <copyright file="FileRepository.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:kalle.jigfors@appva.se">Kalle Jigfors</a>
// </author>


namespace Appva.Mcss.Admin.Domain.Repositories.Contracts
{
    #region Imports.

    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Persistence;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface IFileRepository : ISaveRepository<Resource>, IRepository
    {
        /// <summary>
        /// Gets a file bi id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        T Find<T>(Guid id) where T : Resource;

        /// <summary>
        /// Generic save for all types of Resource
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="resource"></param>
        void Save<T>(T resource) where T : Resource;
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class FileRepository : IFileRepository
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IPersistenceContext"/> implementation.
        /// </summary>
        private readonly IPersistenceContext persistenceContext;

        #endregion

        #region Constructor.

        /// <summary>
        ///  Initializes a new instance of the <see cref="FileRepository"/> class.
        /// </summary>
        /// <param name="persistenceContext"></param>
        public FileRepository(IPersistenceContext persistenceContext)
        {
            this.persistenceContext = persistenceContext;
        }

        #endregion

        #region IFileRepository Members.

        /// <inheritdoc />
        public T Find<T>(Guid id) where T : Resource
        {
            return this.persistenceContext.Get<T>(id);
        }

        /// <inheritdoc />
        public void Save(Resource entity)
        {
            this.Save<Resource>(entity);
        }

        /// <inheritdoc />
        public void Save<T>(T entity) where T : Resource
        {
            this.persistenceContext.Save<T>(entity);
        }

        #endregion
    }
}
