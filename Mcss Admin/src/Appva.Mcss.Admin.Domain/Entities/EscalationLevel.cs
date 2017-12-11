// <copyright file="EscalationLevel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
namespace Appva.Mcss.Admin.Domain.Entities
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class EscalationLevel : AggregateRoot
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="EscalationLevel"/> class.
        /// </summary>
        public EscalationLevel()
        {
        }

        #endregion

        #region Properties.

        public virtual string Name
        {
            get;
            set;
        }
        public virtual string Description
        {
            get;
            set;
        }
        public virtual int Minutes
        {
            get;
            set;
        }
        public virtual int Weight
        {
            get;
            set;
        }
        public virtual IList<EscalationAlert> Items
        {
            get;
            set;
        } 

        #endregion
    }
}