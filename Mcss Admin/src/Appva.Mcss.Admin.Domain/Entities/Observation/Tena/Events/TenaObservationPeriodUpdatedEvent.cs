// <copyright file="TenaObservationPeriodUpdatedEvent.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:$emailAddress$">$developer$</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Entities
{
    #region Imports.

    using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class TenaObservationPeriodUpdatedEvent : DomainEvent
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="TenaObservationPeriodUpdatedEvent"/> class.
        /// </summary>
        /// <param name="observation">The Tena period</param>
        public TenaObservationPeriodUpdatedEvent(TenaObservationPeriod tenaPeriod)
        {
            this.Patient     = tenaPeriod.Patient.Id;
            this.CreatedAt   = tenaPeriod.CreatedAt;
            this.IsActive    = tenaPeriod.IsActive;
            this.Name        = tenaPeriod.Name;
            this.Description = tenaPeriod.Description;
            this.StartDate   = tenaPeriod.StartDate;
            this.EndDate     = tenaPeriod.EndDate;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TenaObservationPeriodUpdatedEvent"/> class.
        /// </summary>
        /// <remarks>Required for deserialization.</remarks>
        [JsonConstructor]
        public TenaObservationPeriodUpdatedEvent()
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The observation patient reference.
        /// </summary>
        [JsonProperty]
        public Guid Patient
        {
            get;
            private set;
        }

        /// <inheritdoc />
        [JsonProperty]
        public DateTime CreatedAt
        {
            get;
            private set;
        }

        /// <summary>
        /// The observation active.
        /// </summary>
        [JsonProperty]
        public bool IsActive
        {
            get;
            private set;
        }

        /// <summary>
        /// The observation name.
        /// </summary>
        [JsonProperty]
        public string Name
        {
            get;
            private set;
        }

        /// <summary>
        /// The observation description.
        /// </summary>
        [JsonProperty]
        public string Description
        {
            get;
            private set;
        }

        /// <summary>
        /// The period start date.
        /// </summary>
        [JsonProperty]
        public DateTime StartDate
        {
            get;
            private set;
        }

        /// <summary>
        /// The period end date.
        /// </summary>
        [JsonProperty]
        public DateTime EndDate
        {
            get;
            private set;
        }

        #endregion

        #region Public Static Builders.

        /// <summary>
        /// Creates a new instance of the <see cref="TenaObservationPeriodUpdatedEvent"/> class.
        /// </summary>
        /// <param name="tenaPeriod">The tena period.</param>
        /// <returns>A new <see cref="ObservationCreatedEvent"/> instance.</returns>
        public static TenaObservationPeriodUpdatedEvent New(TenaObservationPeriod tenaPeriod)
        {
            return new TenaObservationPeriodUpdatedEvent(tenaPeriod);
        }

        #endregion
    }
}