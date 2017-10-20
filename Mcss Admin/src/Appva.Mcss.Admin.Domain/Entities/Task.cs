// <copyright file="Task.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
namespace Appva.Mcss.Admin.Domain.Entities
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class Task : AggregateRoot
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="Task"/> class.
        /// </summary>
        public Task()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Task"/> class.
        /// </summary>
        [Obsolete]
        public Task(Guid id)
        {
            this.Id = id;
        }

        #endregion

        #region Properties.

        /// <summary>
        /// A copy of the sequence name.
        /// </summary>
        public virtual string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Date and time (HH:mm) when a task is set to be completed.
        /// </summary>
        /// <remarks>Part of the Unique Identifier</remarks>
        public virtual DateTime Scheduled
        {
            get;
            set;
        }

        /// <summary>
        /// Whether the task has been completed/processed.
        /// </summary>
        public virtual bool IsCompleted
        {
            get;
            set;
        }

        /// <summary>
        /// The date and time when the task was completed.
        /// </summary>
        public virtual DateTime? CompletedDate
        {
            get;
            set;
        }

        /// <summary>
        /// Whether the task is ready (or open) to be processed.
        /// </summary>
        /// <remarks>Not stored in the database</remarks>
        public virtual bool IsReadyToExecute
        {
            get;
            set;
        }

        /// <summary>
        /// Indicates the range in minutes before any of the tasks are ready to be processed.
        /// </summary>
        public virtual int RangeInMinutesBefore
        {
            get;
            set;
        }

        /// <summary>
        /// Indicates the range in minutes after any of the tasks are ready to be processed.
        /// </summary>
        public virtual int RangeInMinutesAfter
        {
            get;
            set;
        }

        /// <summary>
        /// These tasks will always be ready to start, e.g. a task "give pain killers" can be given when
        /// necassary and on need basis.
        /// </summary>
        public virtual bool OnNeedBasis
        {
            get;
            set;
        }

        /// <summary>
        /// Status of the task, either 
        /// 1: Completed, 
        /// 2: Partially Completed, 
        /// 3: Not completed,
        /// 4. Unable to complete,
        /// 5. Patient responsible
        /// </summary>
        public virtual int Status
        {
            get;
            set;
        }

        /// <summary>
        /// Status of the task, available taxons defined in schedulesettings
        /// </summary>
        public virtual Taxon StatusTaxon
        {
            get;
            set;
        }

        /// <summary>
        /// If it is completed after the timeslot.
        /// </summary>
        public virtual bool Delayed
        {
            get;
            set;
        }

        /// <summary>
        /// If there is a delay then it must be handled.
        /// </summary>
        public virtual bool DelayHandled
        {
            get;
            set;
        }

        /// <summary>
        /// Basically handled and quittenced by a user.
        /// </summary>
        public virtual Account DelayHandledBy
        {
            get;
            set;
        }

        /// <summary>
        /// If the task has been Quittanced
        /// </summary>
        public virtual bool Quittanced
        {
            get;
            set;
        }

        /// <summary>
        /// User who quittanced
        /// </summary>
        public virtual Account QuittancedBy
        {
            get;
            set;
        }

        /// <summary>
        /// The Patient.
        /// </summary>
        public virtual Patient Patient
        {
            get;
            set;
        }

        /// <summary>
        /// The Schedule
        /// </summary>
        public virtual Schedule Schedule
        {
            get;
            set;
        }

        /// <summary>
        /// The Sequence.
        /// </summary>
        /// <remarks>Part of the Unique Identifier</remarks>
        public virtual Sequence Sequence
        {
            get;
            set;
        }

        /// <summary>
        /// Processing user account.
        /// </summary>
        public virtual Account CompletedBy
        {
            get;
            set;
        }

        /// <summary>
        /// A copy of the Patient taxonomical node for easy search access.
        /// </summary>
        public virtual Taxon Taxon
        {
            get;
            set;
        }

        /// <summary>
        /// The inventory levels, if a task includes inventory management.
        /// </summary>
        [Obsolete]
        public virtual InventoryOld Inventory
        {
            get;
            set;
        }

        /// <summary>
        /// FIXME: ONLY USED IN CLIENT FOR ON NEED BASIS. REMOVE THIS AND ADD A NEW VIEWMODEL AND MAPPER INSTEAD.
        /// </summary>
        public virtual DateTime? LastCompletion
        {
            get;
            set;
        }

        /// <summary>
        /// Keeps track on the current escalation level.
        /// </summary>
        public virtual int CurrentEscalationLevel
        {
            get;
            set;
        }

        /// <summary>
        /// If visible on the overview
        /// </summary>
        public virtual bool Overview
        {
            get;
            set;
        }

        /// <summary>
        /// Overrides CanRaiseAlerts in <see cref="ScheduleSettings"/> for Calendar-sequences
        /// </summary>
        public virtual bool CanRaiseAlert
        {
            get;
            set;
        }

        /// <summary>
        /// If true, then alerts from tasks wont be produced.
        /// </summary>
        public virtual bool PauseAnyAlerts
        {
            get;
            set;
        }

        /// <summary>
        /// If is all day or not.
        /// </summary>
        public virtual bool AllDay
        {
            get;
            set;
        }

        /// <summary>
        /// If this event is an abscence or not.
        /// </summary>
        public virtual bool Absent
        {
            get;
            set;
        }

        /// <summary>
        /// If task is event - StartDate and starttime
        /// </summary>
        public virtual DateTime? StartDate
        {
            get;
            set;
        }

        /// <summary>
        /// If task is event - EndDate and EndTime
        /// </summary>
        public virtual DateTime? EndDate
        {
            get;
            set;
        }

        /// <summary>
        /// Inventory transactions for this task
        /// </summary>
        public virtual IList<InventoryTransactionItem> InventoryTransactions
        {
            get;
            set;
        }

        /// <summary>
        /// taskobservationitemss for this task.
        /// it should only never be more than 1 item in this list.
        /// </summary>
        public virtual IList<TaskObservationItem> TaskObservationItems
        {
            get;
            set;
        }

        /// <summary>
        /// The collection of sub tasks.
        /// </summary>
        /// <remarks>Populated by NHibernate.</remarks>
        /*public virtual IList<SubTask> SubTasks
        {
            get;
            set;
        }*/

        #endregion
    }
}