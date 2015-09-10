// <copyright file="Period.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Pdf.Prescriptions
{
    #region Imports.

    using System;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class Period
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Period"/> class.
        /// </summary>
        /// <param name="start">The start of the period with inclusive boundary</param>
        /// <param name="end">The end of the period with inclusive boundary</param>
        public Period(DateTimeOffset start, DateTimeOffset end)
        {
            this.Start = start;
            this.End = end;
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The valid from.
        /// </summary>
        public DateTimeOffset Start
        {
            get;
            private set;
        }

        /// <summary>
        /// The valid to.
        /// </summary>
        public DateTimeOffset End
        {
            get;
            private set;
        }

        #endregion

        #region Public Static Functions.

        /// <summary>
        /// Creates a new instance of the <see cref="Period"/> class.
        /// </summary>
        /// <param name="start">The start of the period with inclusive boundary</param>
        /// <param name="end">The end of the period with inclusive boundary</param>
        /// <returns>A new <see cref="Period"/> instance</returns>
        public static Period CreateNew(DateTimeOffset start, DateTimeOffset end)
        {
            return new Period(start, end);
        }

        #endregion
    }
}