// <copyright file="OrderedFilterProvider.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
namespace Appva.WebApi.Filters
{
    #region Imports.

    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    using System.Web.Http.Controllers;
    using System.Web.Http.Filters;

    #endregion

    /// <summary>
    /// Orders the controller and action filter to execute in specific order.
    /// Read more at <a href="http://stackoverflow.com/questions/21628467/order-of-execution-with-multiple-filters-in-web-api">Stackoverflow</a>
    /// </summary>
    /// <example>
    /// Add the following in the configuration:
    /// config.Services.Add(typeof(IFilterProvider), new OrderedFilterProvider());
    /// </example>
    public sealed class OrderedFilterProvider : IFilterProvider
    {
        #region IFilterProvider Members.

        /// <inheritdoc />
        public IEnumerable<FilterInfo> GetFilters(HttpConfiguration configuration, HttpActionDescriptor actionDescriptor)
        {
            var controllerFilters = this.OrderFilters(actionDescriptor.ControllerDescriptor.GetFilters(), FilterScope.Controller);
            var actionFilters = this.OrderFilters(actionDescriptor.GetFilters(), FilterScope.Action);
            return controllerFilters.Concat(actionFilters);
        }

        #endregion

        #region Private Functions.

        /// <summary>
        /// Orders the filters according to the Order property - in order
        /// to execute in a specific order.
        /// </summary>
        /// <param name="filters">The current filters</param>
        /// <param name="scope">The filter scope</param>
        /// <returns>An ordered colelction of <see cref="FilterInfo"/></returns>
        private IEnumerable<FilterInfo> OrderFilters(IEnumerable<IFilter> filters, FilterScope scope)
        {
            return filters.OfType<IOrderedFilter>()
                .OrderBy(x => x.Order)
                .Select(x => new FilterInfo(x as IFilter, scope));
        }

        #endregion
    }
}