// <copyright file="PageableQueryParams.cs" company="Appva AB">
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
    public sealed class PageableQueryParams<TResponse> : IRequest<TResponse>
    {
        /// <summary>
        /// The search query.
        /// </summary>
        public String SearchQuery
        {
            get;
            set;
        }

        /// <summary>
        /// The page number.
        /// </summary>
        public long Page
        {
            get;
            set;
        }

        /// <summary>
        /// The sorting by property.
        /// </summary>
        public string OrderBy
        {
            get;
            set;
        }

        /// <summary>
        /// The order - either asc or desc.
        /// </summary>
        public string Order
        {
            get;
            set;
        }
    }
}