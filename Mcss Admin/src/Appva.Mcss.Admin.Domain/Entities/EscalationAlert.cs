// <copyright file="EscalationAlert.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Entities
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Common.Domain;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class EscalationAlert : AggregateRoot<EscalationAlert>
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="EscalationAlert"/> class.
        /// </summary>
        public EscalationAlert()
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// Whether or not the <see cref="EscalationAlert"/> is active.
        /// </summary>
        public virtual bool IsActive
        {
            get;
            protected set;
        }

        public virtual string Name
        {
            get;
            set;
        }
        public virtual string Email
        {
            get;
            set;
        }
        public virtual IList<EscalationLevel> EscalationLevels
        {
            get;
            set;
        }
        public virtual IList<Taxon> Taxons
        {
            get;
            set;
        }

        #endregion
    }
}