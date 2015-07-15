// <copyright file="ArithmeticProjections.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.NHibernateUtils.Projections
{
    #region Imports.

    using NHibernate;
    using NHibernate.Criterion;
    using NHibernate.Dialect.Function;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public static class ArithmeticProjections
    {
        #region Public members

        /// <summary>
        /// Creates a <see cref="IProjection"/> which subtract given terms
        /// </summary>
        /// <param name="terms">The terms to subtract</param>
        /// <returns><see cref="IProjection"/></returns>
        public static IProjection Sub(params IProjection[] terms)
        {
            return ArithmeticOperation("-", terms);
        }

        /// <summary>
        /// Creates a <see cref="IProjection"/> where given terms is summed
        /// </summary>
        /// <param name="terms">The terms to sum</param>
        /// <returns><see cref="IProjection"/></returns>
        public static IProjection Add(params IProjection[] terms)
        {
            return ArithmeticOperation("+", terms);
        }
        #endregion

        /// <summary>
        /// Creates a <see cref="IProjection"/> where given terms is multiplied
        /// </summary>
        /// <param name="terms">The terms to multiple</param>
        /// <returns><see cref="IProjection"/></returns>
        public static IProjection Multiple(params IProjection[] terms)
        {
            return ArithmeticOperation("*", terms);
        }

        /// <summary>
        /// Creates a <see cref="IProjection"/> with terms to divide
        /// </summary>
        /// <param name="terms">The terms to divide</param>
        /// <returns><see cref="IProjection"/></returns>
        public static IProjection Deviation(params IProjection[] terms)
        {
            return ArithmeticOperation("/", terms);
        }

        #region Private members

        /// <summary>
        /// Creates a <see cref="IProjection"/> with given operator applied to terms
        /// </summary>
        /// <param name="op">The operator</param>
        /// <param name="terms">The terms</param>
        /// <returns><see cref="IProjection"/></returns>
        private static IProjection ArithmeticOperation(string op, params IProjection[] terms)
        {
            if (terms == null || terms.Count() < 1)
            {
                throw new ArgumentException("Operation must contain at least one term");
            }
            if (terms.Count() == 1)
            {
                return terms.First();
            }

            var function = new VarArgsSQLFunction("(", op, ")");

            return Projections.SqlFunction(function, NHibernateUtil.Int32, terms);
        }

        #endregion

    }
}