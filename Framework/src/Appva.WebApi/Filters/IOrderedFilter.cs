// <copyright file="IOrderedFilter.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
namespace Appva.WebApi.Filters
{
    /// <summary>
    /// Introduces the MVC counterpart "Order" property for the <see cref="ActionFilterAttribute"/>.
    /// Read more at <a href="http://stackoverflow.com/questions/21628467/order-of-execution-with-multiple-filters-in-web-api">Stackoverflow</a>
    /// </summary>
    public interface IOrderedFilter
    {
        /// <summary>
        /// The order of the filter.
        /// </summary>
        int Order 
        { 
            get; 
            set; 
        }
    }
}