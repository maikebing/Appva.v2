// <copyright file="TaxonomyRepository.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.ResourceServer.Domain.Repositories
{
    #region Imports.

    using System.Collections.Generic;
    using Appva.Core.Extensions;
    using Appva.Persistence;
    using Appva.Repository;
    using Mcss.Domain.Entities;

    #endregion

    /// <summary>
    /// The taxonomy repository.
    /// </summary>
    public interface ITaxonomyRepository : IRepository<Taxonomy>
    {
        /// <summary>
        /// Returns the <see cref="Taxonomy"/> by MachineName.
        /// </summary>
        /// <param name="machineName">The machineName</param>
        /// <returns>A <see cref="Taxonomy"/></returns>
        Taxonomy GetByMachineName(string machineName);

        /// <summary>
        /// Returns collection of <see cref="Taxonomy"/> by MachineNames
        /// </summary>
        /// <param name="machineNames">A collection of machine names</param>
        /// <returns>A collection of <see cref="Taxonomy"/></returns>
        IList<Taxonomy> GetByMachineName(List<string> machineNames);
    }

    /// <summary>
    /// Implementation of <see cref="ITaxonomyRepository"/>.
    /// </summary>
    public class TaxonomyRepository : Repository<Taxonomy>, ITaxonomyRepository
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="TaxonomyRepository"/> class.
        /// </summary>
        /// <param name="persistenceContext">The <see cref="IPersistenceContext"/></param>
        public TaxonomyRepository(IPersistenceContext persistenceContext)
            : base(persistenceContext)
        {
        }

        #endregion

        #region ITaxonomyRepository Members.

        /// <inheritdoc />
        public Taxonomy GetByMachineName(string machineName)
        {
            if (machineName.IsNotEmpty())
            {
                return Where(x => x.MachineName == machineName).SingleOrDefault();
            }
            return null;
        }

        /// <inheritdoc />
        public IList<Taxonomy> GetByMachineName(List<string> machineNames)
        {
            return Where(x => x.Active).WhereRestrictionOn(x => x.MachineName).IsIn(machineNames).List();
        }

        #endregion
    }
}