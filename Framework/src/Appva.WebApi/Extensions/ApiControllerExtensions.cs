// <copyright file="ApiControllerExtensions.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.WebApi.Extensions
{
    #region Imports.

    using System.Web.Http;
    using Results;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public static class ApiControllerExtensions
    {
        /// <summary>
        /// Returns a HTTP Status 200 paged result with Location headers and
        /// X-Total-Count headers.
        /// </summary>
        /// <typeparam name="T">The content type</typeparam>
        /// <param name="controller">The controller</param>
        /// <param name="routeName">The route name</param>
        /// <param name="content">The content</param>
        /// <param name="page">The page</param>
        /// <param name="perPage">The total results per page</param>
        /// <param name="totalResult">The total result count</param>
        /// <returns>A <see cref="OkPagedResult{T}"/></returns>
        public static OkPagedResult<T> Paged<T>(this ApiController controller, string routeName, T content, long page, long perPage, long totalResult) where T : class
        {
            return new OkPagedResult<T>(controller, content)
            {
                Page = page,
                PerPage = perPage,
                TotalResult = totalResult,
                RouteName = routeName
            };
        }

        /// <summary>
        /// Returns a HTTP status 200 with ETag header.
        /// </summary>
        /// <typeparam name="T">The content type</typeparam>
        /// <param name="controller">The controller</param>
        /// <param name="content">The content</param>
        /// <returns>A <see cref="OkOrNotModifiedResult{T}"/></returns>
        public static OkOrNotModifiedResult<T> OkOrNotModified<T>(this ApiController controller, T content) where T : class, IETag
        {
            return new OkOrNotModifiedResult<T>(controller, content);
        }

        /// <summary>
        /// Returns a HTTP status 422. 
        /// </summary>
        /// <typeparam name="T">The content type</typeparam>
        /// <param name="controller">The controller</param>
        /// <param name="content">The content</param>
        /// <returns>A <see cref="UnprocessableEntityResult{T}"/></returns>
        public static UnprocessableEntityResult<T> UnprocessableEntity<T>(this ApiController controller, T content)
        {
            return new UnprocessableEntityResult<T>(controller, content);
        }
    }
}