// <copyright file="MarkPreparationPublisher.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using System.Linq;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Auditing;
    using Appva.Mcss.Admin.Application.Security.Identity;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Persistence;
    
    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class MarkPreparationPublisher : RequestHandler<MarkPreparation, string>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IAuditService"/>.
        /// </summary>
        private readonly IAuditService auditing;

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext persistence;

        /// <summary>
        /// The <see cref="IIdentityService"/>.
        /// </summary>
        private readonly IIdentityService identity;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="MarkPreparationPublisher"/> class.
        /// </summary>
        public MarkPreparationPublisher(IAuditService auditing, IIdentityService identity, IPersistenceContext persistence)
        {
            this.auditing = auditing;
            this.identity = identity;
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
                    this.auditing.Delete(
                        "tog bort iordningsställande {0} [REF:{1}] {2:yyyy-MM-dd}{3}",
                        task.PreparedSequence.Name,
                        task.PreparedSequence.Id,
                        task.Date,
                        task.PreparedBy == null ? string.Empty : " iordningsställt av " + task.PreparedBy.FullName);
                    this.persistence.Delete(task);
                }
                return string.Empty;
            }
            var user = this.persistence.Get<Account>(this.identity.PrincipalId);
            if (tasks.Count == 0)
            {
                var prepareSequence = this.persistence.Get<PreparedSequence>(message.PreparedSequenceId);
                this.persistence.Save(new PreparedTask
                {
                    Date             = message.Date,
                    PreparedBy       = user,
                    PreparedSequence = prepareSequence,
                    Schedule         = prepareSequence.Schedule
                });
                this.auditing.Delete(
                    "iordningsställde {0} [REF:{1}] {2:yyyy-MM-dd}",
                    prepareSequence.Name,
                    prepareSequence.Id,
                    message.Date);
                return user.FullName;
            }
            return tasks.First().PreparedBy.FullName;
        }

        #endregion
    }
}