// <copyright file="ObservationCreatedEvent.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
namespace Appva.Mcss.Admin.Domain.Entities
{
    #region Imports.

    using System;
    using Newtonsoft.Json;


    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class ObservationCreatedEvent : DomainEvent
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservationCreatedEvent"/> class.
        /// </summary>
        /// <param name="observation">The observation.</param>
        public ObservationCreatedEvent(Observation observation)
        {
            this.Patient     = observation.Patient.Id;
            this.CreatedAt   = observation.CreatedAt;
            this.IsActive    = observation.IsActive;
            this.Name        = observation.Name;
            this.Description = observation.Description;
            //this.Category    = observation.Category.Id;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservationCreatedEvent"/> class.
        /// </summary>
        /// <remarks>Required for deserialization.</remarks>
        [JsonConstructor]
        protected ObservationCreatedEvent()
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
        /// The observation category, see <see cref="Taxon"/>.
        /// </summary>
        [JsonProperty]
        public Guid Category
        {
            get;
            private set;
        }

        #endregion

        #region Public Static Builders.

        /// <summary>
        /// Creates a new instance of the <see cref="ObservationCreatedEvent"/> class.
        /// </summary>
        /// <param name="observation">The observation.</param>
        /// <returns>A new <see cref="ObservationCreatedEvent"/> instance.</returns>
        public static ObservationCreatedEvent New(Observation observation)
        {
            return new ObservationCreatedEvent(observation);
        }

        #endregion
    }
}