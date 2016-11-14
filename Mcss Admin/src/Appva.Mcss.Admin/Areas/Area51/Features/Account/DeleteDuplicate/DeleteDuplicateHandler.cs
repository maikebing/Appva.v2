// <copyright file="DeleteDuplicateHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Area51.Features.Account.DeleteDuplicate
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Areas.Area51.Models;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using Appva.Persistence;
    using NHibernate.Criterion;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class DeleteDuplicateHandler : RequestHandler<DeleteDuplicate, Parameterless<List<DuplicatedAccount>>>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IAccountService"/>
        /// </summary>
        private readonly IAccountService accountService;

        /// <summary>
        /// The <see cref="IPersistenceContext"/>
        /// </summary>
        private readonly IPersistenceContext persistence;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteDuplicateHandler"/> class.
        /// </summary>
        public DeleteDuplicateHandler(IAccountService accountService, IPersistenceContext persistence)
        {
            this.accountService = accountService;
            this.persistence = persistence;
        }

        #endregion

        #region ReuquestHandler overrides

        /// <inheritdoc />
        public override Parameterless<List<DuplicatedAccount>> Handle(DeleteDuplicate message)
        {
            var removeAccount = this.accountService.Find(message.AccountToRemove);
            var newAccount    = this.accountService.Find(message.AccountToKeep);

            //// Update refs on entities
            // Delegation - CreatedBy
            var delegationsCreatedBy = this.persistence.QueryOver<Delegation>()
                .Where(x => x.CreatedBy.Id == removeAccount.Id)
                .List();
            foreach (var delegation in delegationsCreatedBy)
            {
                delegation.CreatedBy = newAccount;
                this.persistence.Save<Delegation>(delegation);
            }

            // Delegation - Account
            var delegationsAccount = this.persistence.QueryOver<Delegation>()
                .Where(x => x.Account.Id == removeAccount.Id)
                .List();
            foreach (var delegation in delegationsAccount)
            {
                delegation.Account = newAccount;
                this.persistence.Save<Delegation>(delegation);
            }

            // InventoryTransactionItem - Account
            var iti = this.persistence.QueryOver<InventoryTransactionItem>()
                .Where(x => x.Account.Id == removeAccount.Id)
                .List();
            foreach (var i in iti)
            {
                i.Account = newAccount;
                this.persistence.Save<InventoryTransactionItem>(i);
            }

            // KnowledgeTest - Account
            var knowledgeTests = this.persistence.QueryOver<KnowledgeTest>()
               .Where(x => x.Account.Id == removeAccount.Id)
               .List();
            foreach (var kt in knowledgeTests)
            {
                kt.Account = newAccount;
                this.persistence.Save<KnowledgeTest>(kt);
            }

            // Log - Account
            var logAccount = this.persistence.QueryOver<Log>()
              .Where(x => x.Account.Id == removeAccount.Id)
              .List();
            foreach (var log in logAccount)
            {
                log.Account = newAccount;
                this.persistence.Save<Log>(log);
            }

            // Log - Message
            var logMessage = this.persistence.QueryOver<Log>()
              .WhereRestrictionOn(x => x.Message).IsLike(removeAccount.Id.ToString(), MatchMode.Anywhere)
              .List();
            foreach (var log in logAccount)
            {
                log.Message = log.Message.Replace(removeAccount.Id.ToString(), newAccount.Id.ToString());
                this.persistence.Save<Log>(log);
            }

            // NotificationViewedBy - Account
            var nvb = this.persistence.QueryOver<NotificationViewedBy>()
              .Where(x => x.Account.Id == removeAccount.Id)
              .List();
            foreach (var n in nvb)
            {
                n.Account = newAccount;
                this.persistence.Save<NotificationViewedBy>(n);
            }

            // NotificationVisibleTo - AccountId
            // Hasent been set anywhere so skip this one for now

            // PreparedTask - PreparedBy
            var PreparedTask = this.persistence.QueryOver<PreparedTask>()
              .Where(x => x.PreparedBy.Id == removeAccount.Id)
              .List();
            foreach (var t in PreparedTask)
            {
                t.PreparedBy = newAccount;
                this.persistence.Save<PreparedTask>(t);
            }

            // Sequence - OrderedBy
            var sequenceOrderedBy = this.persistence.QueryOver<Sequence>()
              .Where(x => x.RefillInfo.OrderedBy.Id == removeAccount.Id)
              .List();
            foreach (var s in sequenceOrderedBy)
            {
                s.RefillInfo.OrderedBy = newAccount;
                this.persistence.Save<Sequence>(s);
            }

            // Sequence - RefillOrderedBy
            var sequenceRefillOrderedBy = this.persistence.QueryOver<Sequence>()
              .Where(x => x.RefillInfo.RefillOrderedBy.Id == removeAccount.Id)
              .List();
            foreach (var s in sequenceRefillOrderedBy)
            {
                s.RefillInfo.RefillOrderedBy = newAccount;
                this.persistence.Save<Sequence>(s);
            }

            // Task - CompletedBy
            var taskCompletedBy = this.persistence.QueryOver<Task>()
              .Where(x => x.CompletedBy.Id == removeAccount.Id)
              .List();
            foreach (var t in taskCompletedBy)
            {
                t.CompletedBy = newAccount;
                this.persistence.Save<Task>(t);
            }

            // Task - DelayHandledBy
            var taskDelayHandledBy = this.persistence.QueryOver<Task>()
              .Where(x => x.DelayHandledBy.Id == removeAccount.Id)
              .List();
            foreach (var t in taskDelayHandledBy)
            {
                t.DelayHandledBy = newAccount;
                this.persistence.Save<Task>(t);
            }

            // Task - QuittancedBy
            var taskQuittancedBy = this.persistence.QueryOver<Task>()
              .Where(x => x.QuittancedBy.Id == removeAccount.Id)
              .List();
            foreach (var t in taskQuittancedBy)
            {
                t.QuittancedBy = newAccount;
                this.persistence.Save<Task>(t);
            }
            var random = new Random();
            removeAccount.FirstName = "Arkiverat";
            removeAccount.LastName  = "Konto";
            removeAccount.FullName  = "Arkiverat Konto";
            removeAccount.HsaId     = string.Format("Account was duplicate of account {0}", newAccount.Id);
            removeAccount.PersonalIdentityNumber = new PersonalIdentityNumber(string.Format("18010101-{0}", random.Next(1000,9999)));
            removeAccount.AdminPassword = null;
            removeAccount.Salt      = null;
            removeAccount.Roles     = new List<Role>();
            removeAccount.IsActive  = false;


            return new Parameterless<List<DuplicatedAccount>>();
        }

        #endregion
    }
}