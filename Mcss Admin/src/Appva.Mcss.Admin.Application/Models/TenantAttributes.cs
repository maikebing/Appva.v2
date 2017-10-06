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

        /// <summary>
        /// Gets or sets the adress.
        /// </summary>
        /// <value>
        /// The adress.
        /// </value>
        public string Adress
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        public string City
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the zip.
        /// </summary>
        /// <value>
        /// The zip.
        /// </value>
        public string Zip
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the phone.
        /// </summary>
        /// <value>
        /// The phone.
        /// </value>
        public string Phone
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the workplace code.
        /// </summary>
        /// <value>
        /// The workplace code.
        /// </value>
        public string WorkplaceCode
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the workplace.
        /// </summary>
        /// <value>
        /// The workplace.
        /// </value>
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