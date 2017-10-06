// <copyright file="TenantAttributes.cs" company="Appva AB">
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
    public sealed class TenantAttributes
    {
        #region Properties.

        public string Adress
        {
            get;
            set;
        }

        public string City
        {
            get;
            set;
        }

        public string Zip
        {
            get;
            set;
        }

        public string Phone
        {
            get;
            set;
        }
        public string WorkplaceCode
        {
            get;
            set;
        }

        public string Workplace
        {
            get;
            set;
        }

        #endregion

        #region Static members

        /// <summary>
        /// Defaults this instance.
        /// </summary>
        /// <returns></returns>
        public static TenantAttributes Default()
        {
            return new TenantAttributes
            {
                Adress = "Nordenskiöldsgatan 14",
                City = "Göteborg",
                Zip = "413 09"
            };
        }

        #endregion

       
    }
}