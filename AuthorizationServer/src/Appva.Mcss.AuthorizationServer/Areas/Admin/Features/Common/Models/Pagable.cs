// <copyright file="Pagable.cs" company="Appva AB">
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

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public class Pageable<TModel>
    {
        /// <summary>
        /// The search query.
        /// </summary>
        public string SearchQuery
        {
            get;
            set;
        }

        /// <summary>
        /// The number of items to be returned per page.
        /// </summary>
        public int PerPage
        {
            get;
            set;
        }

        /// <summary>
        /// The current page number.
        /// </summary>
        public long Page
        {
            get;
            set;
        }

        /// <summary>
        /// The next page number.
        /// </summary>
        public long Next
        {
            get;
            set;
        }

        /// <summary>
        /// The previous page number.
        /// </summary>
        public long Prev
        {
            get;
            set;
        }

        /// <summary>
        ///  The total size of items.
        /// </summary>
        public long TotalCount
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public IList<TModel> Items
        {
            get;
            set;
        }
    }
}