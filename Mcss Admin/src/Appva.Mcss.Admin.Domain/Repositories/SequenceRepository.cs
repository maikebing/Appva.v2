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

    public interface ISequenceRepository : IRepository<Sequence>
    {
        #region Fields

        #endregion
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class SequenceRepository : Repository<Sequence>, ISequenceRepository
    {
        #region Variables.

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="SequenceRepository"/> class.
        /// </summary>
        public SequenceRepository(IPersistenceContext persistenceContext)
            :base(persistenceContext)
        { 
        }

        #endregion

        #region IUpdateRepository members.

        /// <inheritdoc />
        //public void Update(Sequence entity)
        //{
        //    entity.UpdatedAt = DateTime.Now;
        //    this.persistenceContext.Update<Sequence>(entity);
        //}

        #endregion
    }
}