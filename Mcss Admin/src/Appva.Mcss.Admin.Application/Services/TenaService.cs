// <copyright file="TenaService.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>


namespace Appva.Mcss.Admin.Application.Services
{
    #region Imports.

    using Appva.Mcss.Admin.Domain.Repositories;
    using Appva.Mcss.Admin.Domain.Entities;
    using System.Collections.Generic;
    using System;

    #endregion

    /// <summary>
    /// The <see cref="ITenaService"/>.
    /// </summary>
    public interface ITenaService : IService
    {


        /// <summary>
        /// Get TenaObservationPeriods List based on tenaId
        /// </summary>
        /// <param name="tenaId"></param>
        /// <returns></returns>
        IList<TenaObservationPeriod> GetTenaObservationPeriods(string tenaId);
    }

    /// <summary>
    /// The <see cref="TenaService"/> service.
    /// </summary>
    public sealed class TenaService : ITenaService
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ITenaRepository"/>.
        /// </summary>
        private readonly ITenaRepository repository;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="TenaService"/> class.
        /// </summary>
        /// <param name="repository">The <see cref="ITenaRepository"/>.</param>
        /// <param name="settingsService">The <see cref="ISettingsService"/>.</param>
        public TenaService(ITenaRepository repository)
        {
            this.repository = repository;
         }

        #endregion

        #region ITenaService members.

        public IList<TenaObservationPeriod> GetTenaObservationPeriods(string tenaId)
        {
            return this.repository.FindTenaObservationPeriods(tenaId);
        }
        #endregion
    }
}
