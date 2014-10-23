// <copyright file="PageableHelper.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Infrastructure
{
    #region Imports.

    using Models;
    using NHibernate;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public static class PageableHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TQuery"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="message"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public static Pageable<TResponse> ToPageable<TQuery, TResponse> (
            PageableQueryParams<Pageable<TResponse>> message, 
            IQueryOver<TQuery, TQuery> query)
        {
            var perPage = 20;
            var page = message.Page == 0 ? 1 : message.Page;
            var items = query.Skip((int) (page - 1) * perPage).Take(perPage).List<TResponse>();
            var totalCount = query.ToRowCountQuery().FutureValue<int>();
            return new Pageable<TResponse>
            {
                SearchQuery = message.SearchQuery,
                Page = page,
                Next = ((page + 1) > (totalCount.Value / perPage)) ? page + 1 : page,
                Prev = ((page - 1) > 0) ? page - 1 : page,
                TotalCount = totalCount.Value,
                PerPage = perPage,
                Items = items
            };
        }
    }
}