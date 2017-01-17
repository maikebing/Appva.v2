// <copyright file="TaxonFilterRestrictions.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.NHibernateUtils.Restrictions
{
    #region Imports.

    using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

    #endregion

    /// <summary>
    /// Restrictions for filtering organsiation-taxons
    /// </summary>
    public static class TaxonFilterRestrictions
    {
        #region Public Static Functions.

        /// <summary>
        /// Check is if the property is in the pipe of the filter. 
        /// Eg. checks if filter is in the path of the property, below or over 
        /// </summary>
        /// <typeparam name="T">The Entity</typeparam>
        /// <param name="property">The path property</param>
        /// <param name="filter">The filter path</param>
        /// <returns><code>AbstractCriterion</code></returns>
        public static AbstractCriterion Pipe<T>(Expression<Func<T, object>> property, string filter) where T : class
        {
            var paths = new List<string>();
            var path = filter;
            while (path.Contains("."))
            {
                paths.Add(path);
                path = path.Substring(0, path.LastIndexOf('.'));
            }
            paths.Add(path);

            return Restrictions.Disjunction()
                    .Add(Restrictions.Like(Projections.Property<T>(property), filter, MatchMode.Start))
                    .Add(Restrictions.In(Projections.Property<T>(property), paths));
        }

        /// <summary>
        /// Check is if the property is in the path of the filter. 
        /// Eg. Checks if the property path is below the filter 
        /// </summary>
        /// <typeparam name="T">The Entity</typeparam>
        /// <param name="property">The path property</param>
        /// <param name="filter">The filter path</param>
        /// <returns><code>AbstractCriterion</code><</returns>
        public static AbstractCriterion Path<T>(Expression<Func<T, object>> property, string filter) where T : class
        {
            return Restrictions.Like(Projections.Property<T>(property), filter, MatchMode.Start);
        }

        #endregion
    }
}