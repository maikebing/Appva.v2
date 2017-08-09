// <copyright file="TenaRepository.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>

namespace Appva.Mcss.Admin.Domain.Repositories
{
    #region Imports
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Appva.Repository;
    using Appva.Persistence;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Domain.Models;
    #endregion

    public interface ITenaRepository :  
        IRepository
    {
        IList<TenaObservationPeriod> FindTenaObservationPeriods(string tenaid);
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class TenaRepository : ITenaRepository
    {
        #region Variables

        /// <summary>
        /// The <see cref="IPersistenceContext"/>
        /// </summary>
        private readonly IPersistenceContext context;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="TenaRepository"/> class.
        /// </summary>
        public TenaRepository(IPersistenceContext context)
        {
            this.context = context;
        }

        #endregion

        #region ITenarepository members
        public IList<TenaObservationPeriod> FindTenaObservationPeriods(string tenaId)
        {
            var tenaObservationPeriods = this.context.QueryOver<TenaObservationPeriod>()
                .Where(i => i.TenaId == tenaId)
                .List();
            return tenaObservationPeriods;
        }
        #endregion
    }
}
