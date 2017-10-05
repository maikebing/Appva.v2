// <copyright file="HsaAttributes.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:$emailAddress$">$developer$</a>
// </author>
namespace Appva.Mcss.Admin.Application.Models
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class HsaAttributes
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="HsaAttributes"/> class.
        /// </summary>
        public HsaAttributes()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the prescriber code.
        /// </summary>
        /// <value>
        /// The prescriber code.
        /// </value>
        public string PrescriberCode
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the legitimation code.
        /// </summary>
        /// <value>
        /// The legitimation code.
        /// </value>
        public string LegitimationCode
        {
            get;
            set;
        }

        #endregion
    }
}