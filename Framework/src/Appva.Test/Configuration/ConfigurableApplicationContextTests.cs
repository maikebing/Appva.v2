// <copyright file="ConfigurableApplicationContextTests.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Test.Configuration
{
    #region Import.

    using System;
    using System.Threading.Tasks;
    using Appva.Core.Configuration;
    using Xunit;
    using Xunit.Extensions;

    #endregion

    /// <summary>
    /// <see cref="ConfigurableApplicationContext"/> read and write tests.
    /// </summary>
    /// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
    public sealed class ConfigurableApplicationContextTests
    {
        /// <summary>
        /// The configuration resource.
        /// </summary>
        private readonly ReadableResource resource = new ReadableResource()
        {
            Id = new Guid("54c2ec2d-0238-424c-bc3b-787d4cd866a5"),
            Value = 0
        };

        #region Tests.
        
        /// <summary>
        /// Test to read and write a protected configuration file.
        /// </summary>
        /// <param name="location">The path to the resource</param>
        /// <param name="protect">If the resource is protected</param>
        [Theory]
        [InlineData("App_Data\\ConfigurableApplicationContext.json", false)]
        [InlineData("App_Data\\ConfigurableApplicationContextProtected.json", true)]
        public void ConfigurableApplicationContext_ReadAndWriteConfigurableApplicationContext_ExpectIsEqual(string location, bool protect)
        {
            ConfigurableApplicationContext.Write<ReadableResource>(this.resource)
                .To(location: location).Protect(protect: protect).Execute();
            var resource = ConfigurableApplicationContext.Read<ReadableResource>()
                .From(location: location).Unprotect(unprotect: protect).ToObject();
            Assert.NotNull(resource);
            Assert.Equal(expected: this.resource.Id, actual: resource.Id);
        }

        /// <summary>
        /// Test to make sure changes can be done to the configurable dictionary.
        /// TODO: Fix this test. The update lambda in ConfigurableApplicationContext is incorrect.
        /// </summary>
        [Fact(Skip = "Until the TODO is fixed")]
        public void ConfigurableApplicationContext_AddMultipleResourcesInParallell_ExpectResourceIsUpdated()
        {
            long toExclusive = 1000;
            Parallel.For(
                0,
                toExclusive, 
                i =>
            {
                ConfigurableApplicationContext.Add<ReadableResource>(new ReadableResource()
                {
                    Id = Guid.NewGuid(),
                    Value = i
                });
            });
            var resource = ConfigurableApplicationContext.Get<ReadableResource>();
            Assert.NotNull(resource);
            Assert.Equal(expected: toExclusive, actual: resource.Value);
        }

        #endregion
    }
}