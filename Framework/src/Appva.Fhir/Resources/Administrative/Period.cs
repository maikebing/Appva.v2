// <copyright file="Period.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Fhir.Resources.Administrative
{
    #region Imports.

    using System;
    using Validation;

    #endregion

    /// <summary>
    /// A time period defined by a start and end date/time. 
    /// <para>
    /// A period specifies a range of times. The context of use will specify whether the 
    /// entire range applies (e.g. "the patient was an inpatient of the hospital for 
    /// this time range") or one value from the period applies (e.g. "give to the 
    /// patient between 2 and 4 pm on 24-Jun 2013").
    /// </para>
    /// <para>
    /// If the start element is missing, the start of the period is not known. If the 
    /// end element is missing, it means that the period is ongoing.
    /// </para>
    /// <para>
    /// The end value includes any matching date/time. For example, the period 
    /// 2011-05-23 to 2011-05-27 includes all the times of 23rd May through to the end 
    /// of the 27th May.
    /// </para>
    /// <externalLink>
    ///     <linkText>1.14.0.9 Period</linkText>
    ///     <linkUri>
    ///         http://hl7.org/implement/standards/FHIR-Develop/datatypes.html#period
    ///     </linkUri>
    /// </externalLink>
    /// </summary>
    public sealed class Period : Element
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Period"/> class.
        /// </summary>
        /// <param name="start">The start of the period</param>
        /// <param name="end">The end of the period</param>
        public Period(DateTime? start, DateTime? end)
        {
            if (start.HasValue && end.HasValue)
            {
                Requires.ValidState(end > start, "Start date must be lower than end date");
            }
            this.Start = start;
            this.End = end;
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The start of the period. The boundary is inclusive.
        /// <externalLink>
        ///     <linkText>Start</linkText>
        ///     <linkUri>
        ///         http://hl7.org/implement/standards/FHIR-Develop/datatypes-definitions.html#Period.start
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        public DateTime? Start
        {
            get;
            private set;
        }

        /// <summary>
        /// The end of the period. If the end of the period is missing, it means that the 
        /// period is ongoing.
        /// <externalLink>
        ///     <linkText>End</linkText>
        ///     <linkUri>
        ///         http://hl7.org/implement/standards/FHIR-Develop/datatypes-definitions.html#Period.end
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        public DateTime? End
        {
            get;
            private set;
        }

        #endregion

        #region Static Functions.

        /// <summary>
        /// Creates a new <c>Period</c>.
        /// </summary>
        /// <param name="start">The start of the period</param>
        /// <param name="end">The end of the period, defaults to null (ongoing)</param>
        /// <returns>A new <c>Period</c> instance</returns>
        public static Period CreateNew(DateTime? start, DateTime? end = null)
        {
            return new Period(start, end);
        }

        #endregion
    }
}