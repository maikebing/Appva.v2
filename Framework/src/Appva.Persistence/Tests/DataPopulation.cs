// <copyright file="DataPopulation.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Persistence.Tests
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// Marker interface for data population.
    /// </summary>
    public interface IDataPopulation
    {
        /// <summary>
        /// Populates the test data.
        /// </summary>
        void Populate();
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public abstract class DataPopulation : IDataPopulation
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext context;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="DataPopulation"/> class.
        /// </summary>
        /// <param name="context">The <see cref="IPersistenceContext"/></param>
        protected DataPopulation(IPersistenceContext context)
        {
            this.context = context;
        }

        #endregion

        #region Protected Methods.

        /// <summary>
        /// Saves an entity.
        /// </summary>
        protected Guid Save<T>(T entity) where T : class
        {
            return (Guid) this.context.Save<T>(entity);
        }

        /// <summary>
        /// Saves an entity.
        /// </summary>
        protected void SaveAll<T>(IList<T> entities) where T : class
        {
            foreach (var entity in entities)
            {
                this.context.Save<T>(entity);
            }
        }

        /// <summary>
        /// Updates an entity.
        /// </summary>
        protected void Update<T>(T entity) where T : class
        {
            this.context.Update<T>(entity);
        }

        #endregion

        #region ITestDataPopulator Members.

        /// <inheritdoc />
        public abstract void Populate();

        #endregion
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class NoDataPopulation : DataPopulation, IDataPopulation
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="NoDataPopulation"/> class.
        /// </summary>
        /// <param name="context">The <see cref="IPersistenceContext"/></param>
        protected NoDataPopulation(IPersistenceContext context)
            : base(context)
        {
        }

        #endregion

        /// <inheritdoc />
        public override void Populate()
        {
            //// No op!
        }
    }
}