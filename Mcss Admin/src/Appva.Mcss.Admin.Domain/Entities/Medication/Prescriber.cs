// <copyright file="Prescriber.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:$emailAddress$">$developer$</a>
// </author>
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
    public class Prescriber
    {
        #region Properties.

        public virtual string GivenName
        {
            get;
            set;
        }

        public virtual string FamilyName
        {
            get;
            set;
        }

        public virtual string Code
        {
            get;
            set;
        }

        public virtual string WorkPlaceCode
        {
            get;
            set;
        }

        #endregion

        #region Computed properties.

        public virtual string FullName
        {
            get
            {
                return string.Format("{0} {1}", this.GivenName.Trim(), this.FamilyName.Trim());
            }
        }

        #endregion
    }
}