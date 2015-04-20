// <copyright file="ScheduleSettings.cs" company="Appva AB">
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
    using Appva.Common.Domain;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class ScheduleSettings : AggregateRoot<ScheduleSettings>
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ScheduleSettings"/> class.
        /// </summary>
        public ScheduleSettings()
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// Whether or not the <see cref="ScheduleSettings"/> is active.
        /// </summary>
        public virtual bool IsActive
        {
            get;
            protected set;
        }

        public virtual string Name
        {
            get;
            set;
        }
        public virtual string AlternativeName
        {
            get;
            set;
        }
        public virtual string MachineName
        {
            get;
            set;
        }
        public virtual bool IsPausable
        {
            get;
            set;
        }
        public virtual bool CanRaiseAlerts
        {
            get;
            set;
        }
        public virtual bool HasSetupDrugsPanel
        {
            get;
            set;
        }
        public virtual bool HasInventory
        {
            get;
            set;
        }
        public virtual bool CountInventory
        {
            get;
            set;
        }
        public virtual bool NurseConfirmDeviation
        {
            get;
            set;
        }
        public virtual bool SpecificNurseConfirmDeviation
        {
            get;
            set;
        }
        public virtual Taxon DelegationTaxon
        {
            get;
            set;
        }
        public virtual ScheduleSettings CombineWith
        {
            get;
            set;
        }
        public virtual ScheduleType ScheduleType
        {
            get;
            set;
        }
        public virtual bool Absence
        {
            get;
            set;
        }
        public virtual IList<Taxon> StatusTaxons
        {
            get;
            set;
        }
        public virtual string NurseConfirmDeviationMessage
        {
            get;
            set;
        }
        public virtual bool OrderRefill
        {
            get;
            set;
        }

        #endregion
    }

    public enum ScheduleType
    {
        Action = 0,
        Calendar = 1
    }
}