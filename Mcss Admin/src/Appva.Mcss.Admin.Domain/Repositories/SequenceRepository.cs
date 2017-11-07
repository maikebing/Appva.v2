// <copyright file="SequenceRepository.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Repositories
{
    #region Imports.

    using Appva.Mcss.Admin.Domain.Entities;
using Appva.Mcss.Admin.Domain.Repositories.Contracts;
using Appva.Persistence;
    using Appva.Repository;
    using System;
using System.Collections.Generic;
using System.Linq;

    #endregion

    public interface ISequenceRepository : IIdentityRepository<Sequence>, 
        IUpdateRepository<Sequence>, 
        ISaveRepository<Sequence>, 
        IRepository
    {
        #region Fields

        Sequence Get(Guid id);

        #endregion
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class SequenceRepository : ISequenceRepository
    {
        #region Variables.

        private readonly IPersistenceContext persistenceContext;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="SequenceRepository"/> class.
        /// </summary>
        public SequenceRepository(IPersistenceContext persistenceContext)
        {
            this.persistenceContext = persistenceContext;
        }


        #endregion

        public Sequence Get(Guid id)
        {
            return this.persistenceContext.Get<Sequence>(id);
        }

        /// <inheritdoc />
        public Sequence Find(Guid id)
        {
            return this.persistenceContext.Get<Sequence>(id);
        }

        /// <inheritdoc />
        public void Save(Sequence entity)
        {
            this.persistenceContext.Save(entity);
        }

        /// <inheritdoc />
        public void Update(Sequence entity)
        {
            entity.UpdatedAt = DateTime.Now;
            this.persistenceContext.Update(entity);
        }
    }
}