// <copyright file="LevelOfMeasurement.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johan.sall.larsson@appva.com">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Entities
{
    #region Imports.

    using System.ComponentModel;

    #endregion

    /// <summary>
    /// Represents the LOINC type of scale.
    /// </summary>
    public enum LevelOfMeasurement
    {
        /// <summary>
        /// Undetermined measurement scale due to not set, nulls etc.
        /// </summary>
        [Description("Und")]
        //[DisplayName("Undetermined")]
        Undetermined,

        /// <summary>
        /// The result of the test is a numeric value that relates to a continuous numeric 
        /// scale. Reported either as an integer, a ratio, a real number, or a range. The 
        /// test result value may optionally contain a relational operator from the set 
        /// {<=, <, >, >=}. 
        /// Valid values for a quantitative test are of the form “7”, “-7”, “7.4”, “-7.4”, 
        /// “7.8912”, “0.125”, “<10”, “<10.15”, “>12000”, 1-10, 1:256
        /// </summary>
        [Description("Qn")]
        //[DisplayName("Quantitative")]
        Quantitative,

        /// <summary>
        /// Ordered categorical responses, e.g., 1+, 2+, 3+; positive, negative; reactive, 
        /// indeterminate, nonreactive. 
        /// </summary>
        [Description("Ord")]
        //[DisplayName("Ordinal")]
        Ordinal,

        /// <summary>
        /// Nominal or categorical responses that do not have a natural ordering. (e.g., 
        /// names of bacteria, reported as answers, categories of appearance that do not 
        /// have a natural ordering, such as, yellow, clear, bloody
        /// </summary>
        [Description("Nom")]
        //[DisplayName("Nominal")]
        Nominal,

        /// <summary>
        /// Text narrative, such as the description of a microscopic part of a surgical 
        /// papule test.
        /// </summary>
        [Description("Nar")]
        //[DisplayName("Narrative")]
        Narrative
    }
}