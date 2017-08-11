// <copyright file="TenaRepository.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Domain.Repositories
{
    #region Imports.

    using System.Collections.Generic;
    using Appva.Persistence;
    using Appva.Mcss.Admin.Domain.Entities;

    #endregion

    public interface ITenaRepository : IRepository
    {
        /// <summary>
        /// If the provided external id is unique.
        /// </summary>
        /// <param name="externalId">The external tena id.</param>
        /// <returns>Returns a <see cref="bool"/>.</returns>
        bool HasUniqueExternalId(string externalId);
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class TenaRepository : ITenaRepository
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext persistence;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="TenaRepository"/> class.
        /// </summary>
        /// <param name="context">The <see cref="IPersistenceContext"/>.</param>
        public TenaRepository(IPersistenceContext context)
        {
            this.persistence = context;
        }

        #endregion

        #region ITenarepository members.

        /// <inheritdoc />
        public bool HasUniqueExternalId(string externalId)
        {
            return this.persistence.QueryOver<Patient>()
                .Where(x => x.TenaId == externalId)
                    .RowCount() == 0;
        }

        #endregion
    }
}
