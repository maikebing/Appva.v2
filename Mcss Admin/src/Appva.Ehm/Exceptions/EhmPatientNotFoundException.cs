// <copyright file="EhmPatientNotFoundException.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:$emailAddress$">$developer$</a>
// </author>
namespace Appva.Ehm.Exceptions
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class EhmPatientNotFoundException : Exception
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="EhmPatientNotFoundException"/> class.
        /// </summary>
        public EhmPatientNotFoundException()
        {
        }

        #endregion
    }
}