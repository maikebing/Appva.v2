// <copyright file="DateProjections.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.alvegard@appva.se">Richard Alvegard</a>
// </author>
namespace Appva.NHibernateUtils.Projections
{
    #region Imports.

    using System;
    using System.Linq.Expressions;
    using NHibernate;
    using NHibernate.Criterion;
    using NHibernate.Dialect.Function;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public static class DateProjections
    {
        #region Variables.

        /// <summary>
        /// The format for DateDiff
        /// </summary>
        private const string DateDiffFormat = "datediff({0}, ?1, ?2)";

        #endregion

        #region Public Static Functions.

        /// <summary>
        /// Creates a <see cref="IProjection"/> which calculates the time between given 
        /// timestamps
        /// </summary>
        /// <typeparam name="T">The entity</typeparam>
        /// <param name="datePart">
        /// The time-entity which the difff should be returned. E.g. "Minutes", "Hours", 
        /// "Days" etc.
        /// </param>
        /// <param name="startDate">The first timestamp</param>
        /// <param name="endDate">The second timestamp</param>
        /// <returns><see cref="IProjection"/></returns>
        public static IProjection DateDiff<T>(string datePart, Expression<Func<T, object>> startDate, Expression<Func<T, object>> endDate) where T : class
        {
            var functionTemplate = string.Format(DateDiffFormat, datePart);
            return Projections.SqlFunction(
                new SQLFunctionTemplate(NHibernateUtil.Int32, functionTemplate),
                NHibernateUtil.Int32,
                Projections.Property<T>(startDate),
                Projections.Property<T>(endDate));
        }

        #endregion
    }
}