// <copyright file="ListLog.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Log.Models
{
    #region Imports.

    using Appva.Cqrs;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class ListLog : IRequest<ListLogModel>
    {
        #region Properties.

        /// <summary>
        /// The cursor
        /// </summary>
        public DateTime? Cursor
        {
            get;
            set;
        }

        /// <summary>
        /// The page
        /// </summary>
        public int Page
        {
            get;
            set;
        }

        /// <summary>
        /// The pagesize
        /// </summary>
        public int PageSize
        {
            get;
            set;
        }

        #endregion
    }
}