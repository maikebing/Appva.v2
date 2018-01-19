// <copyright file="Sequence.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Entities
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Validation;
    using Appva.Domain;
    using NHibernate.Classic;
    using Appva.Core.Logging;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class Sequence : AggregateRoot, ILifecycle
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ILog"/> for sequence.
        /// </summary>
        private static readonly ILog Log = LogProvider.For<Sequence>();

        #endregion

        #region Constructors.

        public static Sequence New(Schedule schedule, string name, string description, Repeat repeat)
        {
            return new Sequence();
        }

        public Sequence(
            Schedule schedule, 
            string name, 
            string description, 
            Repeat repeat, 
            Taxon taxon = null, 
            Role role = null, 
            Inventory inventory = null, 
            Refillable refillable = null,
            bool overview = false, 
            bool canRaiseAlert = false, 
            bool pauseAnyAlerts = false, 
            bool absent = false, 
            bool reminder = false)
        {
            Requires.NotNull(schedule,   "schedule");
            Requires.NotNull(repeat,       "repeat");
            Requires.NotNullOrEmpty(name,    "name");
            this.Schedule       = schedule;
            this.Patient        = schedule.Patient;
            this.Name           = name;
            this.Description    = description;
            this.Repeat         = repeat;
            this.Taxon          = taxon;
            this.Role           = role;
            this.Inventory      = inventory;
            this.RefillInfo     = refillable;
            this.Overview       = overview;
            this.CanRaiseAlert  = canRaiseAlert;
            this.PauseAnyAlerts = pauseAnyAlerts;
            this.Absent         = absent;
            if (Log.IsTraceEnabled())
            {
                Log.Trace("Initialized a new instance of sequence with id: {0}, name: {1}", this.Id, this.Name);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Sequence"/> class.
        /// </summary>
        /// <remarks>
        /// An NHibernate visible no-argument constructor.
        /// </remarks>
        protected internal Sequence()
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// Name of the item, e.g. "Do something"
        /// </summary>
        public virtual string Name
        {
            get;
            set;
        }

        /// <summary>
        /// TODO: Change property name to Instruction, since its not a description of the sequence
        /// but rather an instruction for the end user of how to prepare or administer e.g. a drug.
        /// The desciption, such as instructions 
        /// </summary>
        public virtual string Description
        {
            get;
            set;
        }

        /// <summary>
        /// The <see cref="Repeat"/> object (rules for repititative events/tasks).
        /// </summary>
        public virtual Repeat Repeat
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
        /// The Patient
        /// </summary>
        public virtual Patient Patient
        {
            get;
            set;
        }

        /// <summary>
        /// The Delegation <see cref="Taxon"/> permission for any <see cref="Task"/>
        /// </summary>
        public virtual Taxon Taxon
        {
            get;
            set;
        }

        /// <summary>
        /// The Role <see cref="Taxon"/> permission for any <see cref="Task"/>
        /// </summary>
        public virtual Role Role
        {
            get;
            set;
        }

        /// <summary>
        /// If <see cref="Tasks"/> from this sequence should be visible on the overview
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
        /// If this event is an abscence or not.
        /// </summary>
        public virtual bool Absent
        {
            get;
            set;
        }

        /// <summary>
        /// Information about Refill and ordering status
        /// </summary>
        public virtual Refillable RefillInfo
        {
            get;
            internal protected set;
        }

        /// <summary>
        /// The inventory
        /// </summary>
        public virtual Inventory Inventory
        {
            get;
            set;
        }

        /// <summary>
        /// The article
        /// </summary>
        public virtual Article Article
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the medications connected to this sequence.
        /// </summary>
        /// <value>
        /// The medications.
        /// </value>
        public virtual IList<Medication> Medications
        {
            get;
            set;
        }

        #endregion

        #region ILifecycle Members.

        /// <inheritdoc />
        public virtual LifecycleVeto OnDelete(NHibernate.ISession s)
        {
            if (Log.IsTraceEnabled())
            {
                Log.Trace("Deleted sequence with id: {0}", this.Id);
            }
            return LifecycleVeto.NoVeto;
        }

        /// <inheritdoc />
        public virtual void OnLoad(NHibernate.ISession s, object id)
        {
            if (Log.IsTraceEnabled())
            {
                Log.Trace("Loaded sequence with id: {0}", this.Id);
            }
        }

        /// <inheritdoc />
        public virtual LifecycleVeto OnSave(NHibernate.ISession s)
        {
            if (Log.IsTraceEnabled())
            {
                Log.Trace("Saved sequence with id: {0}", this.Id);
            }
            return LifecycleVeto.NoVeto;
        }

        /// <inheritdoc />
        public virtual LifecycleVeto OnUpdate(NHibernate.ISession s)
        {
            if (Log.IsTraceEnabled())
            {
                Log.Trace("Updated sequence with id: {0}", this.Id);
            }
            return LifecycleVeto.NoVeto;
        }

        #endregion
    }
}