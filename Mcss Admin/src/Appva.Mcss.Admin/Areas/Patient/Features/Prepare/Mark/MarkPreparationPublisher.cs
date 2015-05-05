// <copyright file="MarkPreparationPublisher.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Web.ViewModels;
    using Appva.Core.Extensions;
    using Appva.Core.Utilities;
    using Appva.Mcss.Admin.Commands;
    using Appva.Mcss.Web.Mappers;
    using Appva.Persistence;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Infrastructure;
using Appva.Mcss.Web.Controllers;
using NHibernate.Criterion;
using NHibernate.Transform;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class MarkPreparationPublisher : RequestHandler<MarkPreparation, string>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IPatientService"/>.
        /// </summary>
        private readonly IPatientService patientService;

        /// <summary>
        /// The <see cref="IPatientTransformer"/>.
        /// </summary>
        private readonly IPatientTransformer transformer;

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext persistence;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="MarkPreparationPublisher"/> class.
        /// </summary>
        public MarkPreparationPublisher(IPatientService patientService, IPatientTransformer transformer, IPersistenceContext persistence)
        {
            this.patientService = patientService;
            this.transformer = transformer;
            this.persistence = persistence;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override string Handle(MarkPreparation message)
        {
            var tasks = this.persistence.QueryOver<PreparedTask>()
                    .Where(x => x.PreparedSequence.Id == message.PreparedSequenceId)
                    .And(x => x.Date == message.Date)
                    .List();
            if (message.UnMark)
            {
                foreach (var task in tasks)
                {
                    this.persistence.Delete(task);
                }
                return string.Empty;
            }
            if (tasks.Count == 0)
            {
                var prepareSequence = this.persistence.Get<PreparedSequence>(message.PreparedSequenceId);
                this.persistence.Save(new PreparedTask
                {
                    Date = message.Date,
                    PreparedBy = null/*Identity()*/,
                    PreparedSequence = prepareSequence,
                    Schedule = prepareSequence.Schedule
                });
                return "Identity().FullName"/*Identity().FullName*/;
            }
            return tasks.First().PreparedBy.FullName;
        }

        #endregion
    }
}