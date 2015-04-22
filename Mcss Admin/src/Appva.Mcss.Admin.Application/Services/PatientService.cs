// <copyright file="PatientService.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Services
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Persistence;
    using NHibernate.Criterion;

    #endregion

    public interface IPatientService : IService
    {
        /// <summary>
        /// Returns a single account by id.
        /// </summary>
        /// <param name="id"></param>
        Patient Get(Guid id);

        IList<Patient> FindByTaxon(Taxon taxon, bool deceased = true);

        /// <summary>
        /// Updates the property HasUnatendedTasks.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="hasUnattendedTasks"></param>
        void UpdateUnattendantTasks(Patient patient, bool hasUnattendedTasks);

    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class PatientService : IPatientService
    {
        #region Variables.

        private readonly IPersistenceContext context;
        private readonly ILogService logService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientService"/> class.
        /// </summary>
        public PatientService(IPersistenceContext context)
        {
            this.context = context;
        }

        #endregion

        #region 

        /// <inheritdoc />
        public Patient Get(Guid id)
        {
            return this.context.Get<Patient>(id);
        }

        /// <inheritdoc />
        public IList<Patient> FindByTaxon(Taxon taxon, bool deceased = true)
        {
            return this.context.QueryOver<Patient>()
                .Where(x => x.IsActive == true)
                .And(x => x.Deceased == deceased)
                .JoinQueryOver<Taxon>(x => x.Taxon)
                .Where(Restrictions.On<Taxon>(x => x.Path)
                .IsLike(taxon.Id.ToString(), MatchMode.Anywhere))
                .List();
        }

        /// <inheritdoc />
        public void UpdateUnattendantTasks(Patient patient, bool hasUnattendedTasks)
        {
            if (patient == null)
            {
                return;
            }
            patient.HasUnattendedTasks = hasUnattendedTasks;
            this.context.Update(patient);
        }

        #endregion
    }
}