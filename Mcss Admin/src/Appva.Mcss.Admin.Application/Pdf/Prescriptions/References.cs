// <copyright file="References.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Pdf.Prescriptions
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class References
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="References"/> class.
        /// </summary>
        /// <param name="number">The reference number</param>
        /// <param name="description">The reference description</param>
        public References(int number, string description)
        {
            this.Number = number;
            this.Description = description;
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The number.
        /// </summary>
        public int Number
        {
            get;
            private set;
        }

        /// <summary>
        /// The description.
        /// </summary>
        public string Description
        {
            get;
            private set;
        }

        #endregion

        #region Public Static Functions.

        /// <summary>
        /// Creates a new instance of the <see cref="References"/> class.
        /// </summary>
        /// <param name="number">The reference number</param>
        /// <param name="description">The reference description</param>
        /// <returns>A new <see cref="References"/> instance</returns>
        public static References CreateNew(int number, string description)
        {
            return new References(number, description);
        }

        #endregion
    }
}