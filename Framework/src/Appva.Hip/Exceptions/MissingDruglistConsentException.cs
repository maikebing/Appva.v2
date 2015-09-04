// <copyright file="MissingDruglistConsentException.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Hip.Exceptions
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class MissingDruglistConsentException : MissingConsentException
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="MissingDruglistConsentException"/> class.
        /// </summary>
        public MissingDruglistConsentException()
            :base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MissingDruglistConsentException"/> class.
        /// </summary>
        public MissingDruglistConsentException(string message)
            : base(message)
        {
        }

        #endregion
    }
}