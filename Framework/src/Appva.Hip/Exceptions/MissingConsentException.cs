// <copyright file="MissingConsentException.cs" company="Appva AB">
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
    public class MissingConsentException : Exception
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="MissingConsentException"/> class.
        /// </summary>
        public MissingConsentException() 
            :base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MissingConsentException"/> class.
        /// </summary>
        /// <param name="message"></param>
        public MissingConsentException(string message)
            :base(message)
        {
        }

        #endregion
    }
}