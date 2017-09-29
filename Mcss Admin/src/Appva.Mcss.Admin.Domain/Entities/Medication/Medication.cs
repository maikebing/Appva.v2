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

        public virtual DosageScheme DosageScheme
        {
            get;
            set;
        }


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

        public virtual IList<Medication> PreviousMedications
        {
            get;
            set;
        }

        #region Expidition

        public virtual DateTime? LastExpiditedAt
        {
            get;
            set;
        }

        public virtual int? LastExpiditedAmount
        {
            get;
            set;
        }

        public virtual string LastExpiditedNplPackId
        {
            get;
            set;
        }

        public virtual int? NumbersOfExpiditions
        {
            get;
            set;
        }

        public virtual int? RemainingExpiditions
        {
            get;
            set;
        }

        #endregion

        #region Medication runs out

        #region Properties.

        /// <summary>
        /// Gets or sets the discontinued at.
        /// Swedish: Utsättningsdatum
        /// </summary>
        /// <value>
        /// The discontinued at.
        /// </value>
        public virtual DateTime? DiscontinuedAt
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the type of the discontinued.
        /// Swedish: Utsättningstyp
        /// </summary>
        /// <value>
        /// The type of the discontinued.
        /// </value>
        public virtual string DiscontinuedType
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the discontinued comment.
        /// Swedish: Uttsättnings kommentar
        /// </summary>
        /// <value>
        /// The discontinued comment.
        /// </value>
        public virtual string DiscontinuedComment
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the canceled at.
        /// Swedish: Makuleringsdatum
        /// </summary>
        /// <value>
        /// The canceled at.
        /// </value>
        public virtual DateTime? CanceledAt
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the cancellation reason code.
        /// Swedish: Makuleringsorsakskod
        /// </summary>
        /// <value>
        /// The cancellation reason code.
        /// </value>
        public virtual int? CancellationReasonCode
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the cancellation reason.
        /// Swedish: Makuleringsorsak
        /// </summary>
        /// <value>
        /// The cancellation reason.
        /// </value>
        public virtual string CancellationReason
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the cancellation comment.
        /// Swedish: Makuleringskommentar
        /// </summary>
        /// <value>
        /// The cancellation comment.
        /// </value>
        public virtual string CancellationComment
        {
            get;
            set;
        }

        #endregion

        #region Computed properties

        public virtual bool IsCanceled
        {
            get { return this.CanceledAt.HasValue; }
        }

        public virtual DateTime? EndsAt
        {
            get {
                var endDates = new List<DateTime>();
                if (this.CanceledAt.HasValue)
                {
                    endDates.Add(this.CanceledAt.Value);
                }
                if (this.DiscontinuedAt.HasValue)
                {
                    endDates.Add(this.DiscontinuedAt.Value);
                }

                if (endDates.Count == 0)
                {
                    return null;
                }

                return endDates.FirstOrDefault();
            }
        }

        #endregion

        #endregion

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