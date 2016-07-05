// <copyright file="PdfPrintService.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Pdf
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using Appva.Mcss.Admin.Application.Auditing;
    using Appva.Mcss.Admin.Application.Pdf.Prescriptions;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Core.Extensions;
    using Domain.Entities;
    using NHibernate.Transform;
    using Persistence;
    using Services;

    #endregion

    /// <summary>
    /// The pdf print service.
    /// </summary>
    public interface IPdfPrintService : IService
    {
        /// <summary>
        /// Creates the by schedule.
        /// </summary>
        /// <param name="title">The pdf title.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <param name="patientId">The patient ID</param>
        /// <param name="scheduleId">The schedule ID.</param>
        /// <param name="showNeedBased">If set to <c>true</c> [show need based].</param>
        /// <param name="showStandardSequences">If set to <c>true</c> [show standard sequences].</param>
        /// <returns>A byte array</returns>
        byte[] CreateBySchedule(string title, DateTime start, DateTime end, Guid patientId, Guid scheduleId, bool showNeedBased = true, bool showStandardSequences = true);

        /// <summary>
        /// Creates the by tasks.
        /// </summary>
        /// <param name="title">The pdf title.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <param name="patientId">The patient ID</param>
        /// <param name="scheduleSettingsId">The schedule settings ID.</param>
        /// <param name="showNeedBased">If set to <c>true</c> [show need based].</param>
        /// <param name="showStandardSequences">If set to <c>true</c> [show standard sequences].</param>
        /// <returns>A byte array</returns>
        byte[] CreateByTasks(string title, DateTime start, DateTime end, Guid patientId, Guid? scheduleSettingsId, bool showNeedBased = true, bool showStandardSequences = true);

        /// <summary>
        /// Creates the signed tasks table.
        /// </summary>
        /// <param name="title">The pdf title.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <param name="patientId">The patient ID</param>
        /// <param name="scheduleSettingsId">The schedule settings ID.</param>
        /// <param name="showNeedBased">If set to <c>true</c> [show need based].</param>
        /// <param name="showStandardSequences">If set to <c>true</c> [show standard sequences].</param>
        /// <returns>A byte array</returns>
        byte[] CreateSignedTasksTable(string title, DateTime start, DateTime end, Guid patientId, Guid? scheduleSettingsId, bool showNeedBased = true, bool showStandardSequences = true);

        /// <summary>
        /// Creates the prepared sequence schedule.
        /// </summary>
        /// <param name="title">The pdf title.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <param name="patientId">The patient ID</param>
        /// <param name="scheduleId">The schedule ID.</param>
        /// <returns>A byte array</returns>
        byte[] CreateSignedPreparedTasks(string title, DateTime start, DateTime end, Guid patientId, Guid scheduleId);
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class PdfPrintService : AbstractTaskService, IPdfPrintService
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ISettingsService"/>.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// The <see cref="ITaxonomyService"/>.
        /// </summary>
        private readonly ITaxonomyService taxonomicService;

        /// <summary>
        /// The <see cref="IAuditService"/>.
        /// </summary>
        private readonly IAuditService auditing;

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext context;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfPrintService"/> class.
        /// </summary>
        /// <param name="auditing">The <see cref="IAuditService"/></param>
        /// <param name="taxonomicService">The <see cref="ITaxonomyService"/></param>
        /// <param name="settingsService">The <see cref="ISettingsService"/></param>
        /// <param name="context">The <see cref="IPersistenceContext"/></param>
        public PdfPrintService(IAuditService auditing, ITaxonomyService taxonomicService, ISettingsService settingsService, IPersistenceContext context)
        {
            this.auditing         = auditing;
            this.taxonomicService = taxonomicService;
            this.settingsService  = settingsService;
            this.context          = context;
        }

        #endregion

        /// <inheritdoc />
        public byte[] CreateBySchedule(string title, DateTime start, DateTime end, Guid patientId, Guid scheduleId, bool showNeedBased = true, bool showStandardSequences = true)
        {
            var patient  = this.context.Get<Patient>(patientId);
            var schedule = this.context.Get<Schedule>(scheduleId);
            var query = this.context.QueryOver<Sequence>()
                .Where(x => x.IsActive == true)
                .And(x => x.Schedule.Id == schedule.Id);
            //// FIXME: check the sql if both are false, might be and [OnNeedBasis] == 0 AND [OnNeedBasis] == 1 which makes it 0 results.
            //// If false then remove all needs based sequences.
            if (! showNeedBased)
            {
                /// if its not included we filter it away.
                query.And(x => x.OnNeedBasis == false);
            }
            //// if false then remove 
            if (! showStandardSequences)
            {
                /// just filter on need based.
                query.And(x => x.OnNeedBasis == true);
            }
            var sequences = query.List();
            var statusTaxons = schedule.ScheduleSettings.StatusTaxons;
            if (statusTaxons.Count == 0)
            {
                //// statusTaxons = this.taxonomicService.Roots(TaxonomicSchema.SignStatus.);
                statusTaxons = this.context.QueryOver<Taxon>().Where(x => x.IsActive && x.IsRoot)
                    .JoinQueryOver<Taxonomy>(x => x.Taxonomy)
                    .Where(x => x.MachineName == "SST").List();
            }
            this.auditing.Read(
                patient,
                "skapade utskrift av signeringslista {0} för boende {1} (REF: {2}).",
                schedule.ScheduleSettings.Name,
                patient.FullName,
                patient.Id);
            var lookAndFeel = this.settingsService.PdfLookAndFeelConfiguration();
            var showInstructionsOnSeparatePage = this.settingsService.Find(ApplicationSettings.PdfShowInstructionsOnSeparatePage);
            var result = this.FindSequencesWithinTimeSpan(schedule.ScheduleSettings.Name, start, end, patient, sequences, statusTaxons, showInstructionsOnSeparatePage);
            return PdfScheduleDocument.CreateNew(title, lookAndFeel).Process(result.Item1, result.Item2).Save();
        }

        /// <inheritdoc />
        public byte[] CreateByTasks(string title, DateTime start, DateTime end, Guid patientId, Guid? scheduleSettingsId, bool showNeedBased = true, bool showStandardSequences = true)
        {
            var patient = this.context.Get<Patient>(patientId);
            var query = this.context.QueryOver<Task>()
                .Where(x => x.IsActive)
                  .And(x => x.Patient.Id == patient.Id)
                  .And(x => x.Scheduled >= start && x.Scheduled <= end.LastInstantOfDay())
                .Fetch(x => x.StatusTaxon).Eager
                .TransformUsing(new DistinctRootEntityResultTransformer());
            if (scheduleSettingsId.HasValue)
            {
                query.JoinQueryOver<Schedule>(x => x.Schedule)
                    .JoinQueryOver<ScheduleSettings>(x => x.ScheduleSettings)
                    .Where(x => x.Id == scheduleSettingsId);
            }
            if (! showNeedBased)
            {
                query.AndNot(x => x.OnNeedBasis);
            }
            if (! showStandardSequences)
            {
                query.And(x => x.OnNeedBasis);
            }
            var scheduleSettings = this.context.Get<ScheduleSettings>(scheduleSettingsId);
            var statusTaxons = scheduleSettings.StatusTaxons;
            if (statusTaxons.Count == 0)
            {
                //// this.taxonomicService.List(TaxonomicSchema.SignStatus);
                statusTaxons = this.context.QueryOver<Taxon>().Where(x => x.IsActive && x.IsRoot)
                    .JoinQueryOver<Taxonomy>(x => x.Taxonomy)
                    .Where(x => x.MachineName == "SST").List();
            }
            this.auditing.Read(
                patient,
                "skapade utskrift av signeringslista {0} för boende {1} (REF: {2}).",
                scheduleSettings.Name,
                patient.FullName,
                patient.Id);
            var lookAndFeel = this.settingsService.PdfLookAndFeelConfiguration();
            var result      = this.FindTasksWithinTimeSpan(scheduleSettings.Name, start, end, patient, query.List(), statusTaxons);
            return PdfScheduleDocument.CreateNew(title, lookAndFeel).Process(result, null).Save();
        }

        /// <inheritdoc />
        public byte[] CreateSignedTasksTable(string title, DateTime start, DateTime end, Guid patientId, Guid? scheduleSettingsId, bool showNeedBased = true, bool showStandardSequences = true)
        {
            var patient = this.context.Get<Patient>(patientId);
            var query = this.context.QueryOver<Task>()
                .Where(x => x.IsActive)
                  .And(x => x.Patient.Id == patient.Id)
                  .And(x => x.Scheduled >= start && x.Scheduled <= end.LastInstantOfDay())
                .Fetch(x => x.StatusTaxon).Eager
                .TransformUsing(new DistinctRootEntityResultTransformer());
            if (scheduleSettingsId.HasValue)
            {
                query.JoinQueryOver<Schedule>(x => x.Schedule)
                    .JoinQueryOver<ScheduleSettings>(x => x.ScheduleSettings)
                    .Where(x => x.Id == scheduleSettingsId);
            }
            if (! showNeedBased)
            {
                query.AndNot(x => x.OnNeedBasis);
            }
            if (! showStandardSequences)
            {
                query.And(x => x.OnNeedBasis);
            }
            var scheduleSettings = this.context.Get<ScheduleSettings>(scheduleSettingsId);
            this.auditing.Read(
                patient,
                "skapade utskrift av signeringslista {0} för boende {1} (REF: {2}).",
                scheduleSettings.Name,
                patient.FullName,
                patient.Id);
            var lookAndFeel = this.settingsService.PdfLookAndFeelConfiguration();
            return PdfTableDocument.CreateNew(title, lookAndFeel).Process(
                scheduleSettings.Name,
                Period.CreateNew(start, end),
                new PatientInformation(patient.FullName, patient.PersonalIdentityNumber),
                query.List()).Save();
        }

        /// <inheritdoc />
        public byte[] CreateSignedPreparedTasks(string title, DateTime start, DateTime end, Guid patientId, Guid scheduleId)
        {
            var patient = this.context.Get<Patient>(patientId);
            var schedule = this.context.Get<Schedule>(scheduleId);
            var tasks = this.context.QueryOver<PreparedTask>()
                .Where(x => x.Schedule.Id == schedule.Id)
                .And(x => x.Date >= start && x.Date <= end.LastInstantOfDay())
                .List();
            this.auditing.Read(
                patient,
                "skapade utskrift av signeringslista {0} för boende {1} (REF: {2}).",
                schedule.ScheduleSettings.Name,
                patient.FullName,
                patient.Id);
            var lookAndFeel = this.settingsService.PdfLookAndFeelConfiguration();
            var result = this.ConvertPreparedTasks("Iordningsställande: " + schedule.ScheduleSettings.Name, start, end, patient, tasks);
            return PdfScheduleDocument.CreateNew(
                title, 
                lookAndFeel,
                false,
                new List<string>
                {
                    "LÄKEMEDEL",
                    "DAG"
                }).Process(result, null).Save();
        }
    }
}