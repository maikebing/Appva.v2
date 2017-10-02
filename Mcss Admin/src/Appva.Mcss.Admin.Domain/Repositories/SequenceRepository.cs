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
    using Appva.Persistence;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface ISequenceRepository : IRepository<Sequence>, IUpdateRepository<Sequence>
    {
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class SequenceRepository : Repository<Sequence>, ISequenceRepository
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="SequenceRepository"/> class.
        /// </summary>
        /// <param name="context">The <see cref="IPersistenceContext"/>.</param>
        public SequenceRepository(IPersistenceContext context)
            : base(context)
        {
        }

        #endregion

        /// <inheritdoc />
        public void Update(Sequence entity)
        {
            entity.UpdatedAt = DateTime.Now;
            this.Context.Update<Sequence>(entity);
        }
    }
}