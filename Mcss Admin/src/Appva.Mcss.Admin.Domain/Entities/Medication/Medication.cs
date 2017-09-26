// <copyright file="Medication.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:$emailAddress$">$developer$</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Entities
{
    #region Imports.

    using Appva.Common.Domain;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class Medication : AggregateRoot<Medication>
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Medication"/> class.
        /// </summary>
        public Medication()
        {
        }

        #endregion

        #region Properties.

        public virtual long OrdinationId
        {
            get;
            set;
        }

        public virtual int? Status
        {
            get;
            set;
        }

        public virtual OrdinationType Type
        {
            get;
            set;
        }

        public virtual DateTime OrdinationStartsAt
        {
            get;
            set;
        }

        public virtual DateTime OrdinationCreatedAt
        {
            get;
            set;
        }

        public virtual DateTime? OrdinationValidUntil
        {
            get;
            set;
        }

        public virtual DateTime? TreatmentStartsAt
        {
            get;
            set;
        }

        public virtual DateTime? TreatmentEndsAt
        {
            get;
            set;
        }

        public virtual string Purpose
        {
            get;
            set;
        }

        public virtual string DosageText1
        {
            get;
            set;
        }

        public virtual string DosageText2
        {
            get;
            set;
        }

        public virtual string DosageText
        {
            get
            {
                return string.Format("{0}. {1}", this.DosageText1, this.DosageText2); 
            }
        }

        //// TODO: Fix dosageSchemes


        public virtual Article Article 
        {
            get;
            set;
        }

        public virtual Prescriber Prescriber
        {
            get;
            set;
        }

        #endregion
    }

    public enum OrdinationType
    {
        NeedBased,
        Scheduled,
        Dispensed
    }

    public static class OrdinationTypeExtension
    {
        public static OrdinationType FromString(string s)
        {
            switch (s)
            {
                case "B":
                    return OrdinationType.NeedBased;
                case "S":
                    return OrdinationType.Scheduled;
                case "D":
                    return OrdinationType.Dispensed;
                default:
                    throw new ArgumentException(string.Format("Could not parse '{0}' to an ordination type", s));
            }
        }
    }
}