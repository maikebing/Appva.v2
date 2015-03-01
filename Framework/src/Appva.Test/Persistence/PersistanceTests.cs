// <copyright file="PersistanceTests.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Test.Persistence
{
    #region Imports.

    using System;
    using System.Diagnostics.CodeAnalysis;
    using Tools;
    using Xunit;

    #endregion

    /// <summary>
    /// Test suite for <see cref="PersistenceContext"/>.
    /// </summary>
    public class PersistanceTests : InMemoryDatabase
    {
        #region Variables.

        /// <summary>
        /// The entity name.
        /// </summary>
        private const string Name = "foo";

        #endregion

        #region Tests.

        /// <summary>
        /// Test: Save an entity to the in memory database.
        /// Expected Result: The entity iproperly s stored.
        /// </summary>
        [Fact]
        public void SaveEntity_ExpectEntityIsSaved()
        {
            var id = (Guid) this.PersistenceContext.Save<Entity>(new Entity(Name));
            var result = this.PersistenceContext.Get<Entity>(id);
            Assert.Equal(id, result.Id);
            Assert.Equal(Name, result.Name);
        }

        #endregion
    }

    #region Entities.

    /// <summary>
    /// Anemic Entity test.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public class Entity
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Entity"/> class.
        /// </summary>
        /// <param name="name">The name</param>
        public Entity(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Entity"/> class.
        /// </summary>
        /// <remarks>Required by NHibernate</remarks>
        protected Entity()
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// Returns the id.
        /// </summary>
        public virtual Guid Id
        {
            get;
            protected set;
        }

        /// <summary>
        /// Returns the name.
        /// </summary>
        public virtual string Name
        {
            get;
            protected set;
        }

        #endregion
    }

    #endregion
}
