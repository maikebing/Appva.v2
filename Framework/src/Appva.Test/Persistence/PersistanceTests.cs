// <copyright file="PersistanceTests.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Test.Persistence
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Appva.Core.Configuration;
    using Appva.Persistence;
    using Xunit;
    using System.Reflection;
    using Appva.Test.Tools;

    #endregion

    /// <summary>
    /// TODO: Add a sumamry for readability.
    /// </summary>
    public class PersistanceTests : InMemoryDatabase
    {
        /// <summary>
        /// TODO: Add a sumamry for readability.
        /// </summary>
        [Fact]
        public void Persistance_StoreAnEntity_ExpectStoredEntityIsEqual()
        {
            var expected = "foo";
            var id = (Guid) this.PersistenceContext.Save<Entity>(new Entity()
            {
                Name = expected
            });
            var result = this.PersistenceContext.Get<Entity>(id);
            Assert.Equal(id, result.Id);
            Assert.Equal(expected, result.Name);
        }
    }
}
