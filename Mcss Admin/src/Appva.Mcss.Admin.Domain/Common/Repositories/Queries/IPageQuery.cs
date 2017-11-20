// <copyright file="IPageQuery.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Domain
{
    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface IPageQuery : ICursoredPageQuery
    {
        /// <summary>
        /// The page number.
        /// </summary>
        int PageNumber
        {
            get;
        }

        /// <summary>
        /// The amount of items per page.
        /// </summary>
        int PageSize
        {
            get;
        }

        /// <summary>
        /// The skip size.
        /// </summary>
        int Skip
        {
            get;
        }

        /// <summary>
        /// Whether or not the first page, i.e. either 0 or 1.
        /// </summary>
        bool IsFirstPage 
        {
            get;
        }
    }
}