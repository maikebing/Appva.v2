// <copyright file="NotUniqueException.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:$emailAddress$">$developer$</a>
// </author>
namespace Appva.Mcss.Admin.Application.Exception
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class NotUniqueException : Exception
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="NotUniqueException"/> class.
        /// </summary>
        public NotUniqueException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotUniqueException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public NotUniqueException(string message)
            :base(message)
        {
        }

        #endregion
    }
}