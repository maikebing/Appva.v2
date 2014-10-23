// <copyright file="Upgrade.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Models
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Appva.Cqrs;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public class Upgrade : IRequest<Upgrade>
    {
        /// <summary>
        /// Whether upgrade or not.
        /// </summary>
        public bool IsUpgraded
        {
            get;
            set;
        }

        /// <summary>
        /// Whether upgrade was successful or not.
        /// </summary>
        public bool IsException
        {
            get;
            set;
        }

        /// <summary>
        /// An exception message if any.
        /// </summary>
        public string ExceptionMessage
        {
            get;
            set;
        }
    }
}