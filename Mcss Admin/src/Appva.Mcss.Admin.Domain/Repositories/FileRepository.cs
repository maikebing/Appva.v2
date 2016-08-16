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
    public interface IFileRepository : ISaveRepository<File>, IRepository
    {
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
        public void Save(File entity)
        {
            this.persistenceContext.Save<File>(entity);
        }

        #endregion
    }
}
