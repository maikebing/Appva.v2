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
    using System.Diagnostics.CodeAnalysis;
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

        #region ITestDataPopulator Members.

        /// <inheritdoc />
        public abstract void Populate();

        #endregion

        #region Protected Methods.

        /// <summary>
        /// Saves an entity and returns the entity ID.
        /// </summary>
        /// <typeparam name="T">The entity type</typeparam>
        /// <param name="entity">The entity to be saved</param>
        /// <returns>The entity ID</returns>
        protected Guid Save<T>(T entity) where T : class
        {
            return (Guid)this.context.Save<T>(entity);
        }

        /// <summary>
        /// Save a collection of entities.
        /// </summary>
        /// <typeparam name="T">The entity type</typeparam>
        /// <param name="entities">The collection of entities to be saved</param>
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
        /// <typeparam name="T">The entity type</typeparam>
        /// <param name="entity">The entity to be updated</param>
        protected void Update<T>(T entity) where T : class
        {
            this.context.Update<T>(entity);
        }

        #endregion
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
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