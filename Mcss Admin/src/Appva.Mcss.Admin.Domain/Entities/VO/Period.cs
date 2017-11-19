// <copyright file="Period.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Domain
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    #endregion

    /// <summary>
    /// A time period defined by a start and end date/time. 
    /// <para>
    /// A period specifies a range of times. If the start element is missing, the start 
    /// of the period is not known. If the 
    /// end element is missing, it means that the period is ongoing.
    /// </para>
    /// </summary>
    public sealed class Period : ValueObject<Period>
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="Period"/> class.
        /// </summary>
        /// <param name="start">The start of the period</param>
        /// <param name="end">The end of the period</param>
        public Period(DateTime start, DateTime? end)
        {
            if (end.HasValue && end.Value > start)
            {
                new ArgumentException("Start date must be lesser than end date");
            }
            this.Start = start;
            this.End   = end ?? DateTime.MaxValue;
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="Period" /> class from being created.
        /// </summary>
        /// <remarks>For Json deserialization</remarks>
        [JsonConstructor]
        private Period()
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The start of the period. The boundary is inclusive.
        /// </summary>
        [JsonProperty]
        public DateTime Start
        {
            get;
            private set;
        }

        /// <summary>
        /// The end of the period. If the end of the period is missing, it means that the 
        /// period is ongoing.
        /// </summary>
        [JsonProperty]
        public DateTime End
        {
            get;
            private set;
        }

        #endregion

        #region Public Static Functions.

        /// <summary>
        /// Creates a new <c>Period</c>.
        /// </summary>
        /// <param name="start">The start of the period</param>
        /// <param name="end">The end of the period, defaults to null (ongoing)</param>
        /// <returns>A new <c>Period</c> instance</returns>
        public static Period New(DateTime start, DateTime? end = null)
        {
            return new Period(start, end);
        }

        #endregion

        #region Abstract Implementation.
        
        /// <inheritdoc />
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return this.Start;
            yield return this.End;
        }

        #endregion
    }
}