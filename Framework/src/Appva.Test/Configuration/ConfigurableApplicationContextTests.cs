// <copyright file="ConfigurableApplicationContextTests.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Test.Configuration
{
    #region Import.

    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;
    using Appva.Core.Configuration;
    using Xunit;
    using Xunit.Extensions;

    #endregion

    /// <summary>
    /// Test suite for <see cref="ConfigurableApplicationContext"/>.
    /// </summary>
    public sealed class ConfigurableApplicationContextTests
    {
        #region Variables.

        /// <summary>
        /// Zero (0).
        /// </summary>
        private const int Zero = 0;

        /// <summary>
        /// One thousand (1000).
        /// </summary>
        private const int OneThousand = 1000;

        #endregion

        #region Tests.

        /// <summary>
        /// Test: Read and write a protected configuration file.
        /// Expected Result: The written and read configuration file are equal.
        /// </summary>
        /// <param name="location">The path to the resource</param>
        /// <param name="protect">If the resource is protected</param>
        [Theory]
        [InlineData("App_Data\\ConfigurableApplicationContext.json", false)]
        [InlineData("App_Data\\ConfigurableApplicationContextProtected.json", true)]
        public void ReadAndWriteConfiguration_ExpectThatWrittenAndReadAreEqual(string location, bool protect)
        {
            var expected = new ReadableResource
            {
                Id = new Guid("54c2ec2d-0238-424c-bc3b-787d4cd866a5"),
                Value = 0
            };
            ConfigurableApplicationContext.Write(expected).To(location).Protect(protect).Execute();
            var actual = ConfigurableApplicationContext.Read<ReadableResource>().From(location).Unprotect(protect).ToObject();
            Assert.Equal(expected.Id, actual.Id);
        }

        /// <summary>
        /// Test: Upadates can be done to the configurable dictionary in parallel.
        /// Expected Result: The value is updated to 1 000.
        /// </summary>
        [Fact(Skip = "Until the update lambda is fixed")]
        public void OverwriteConfigurationOneThousandTimes_ExpectThatConfigurationIsUpdated()
        {
            Parallel.For(
                Zero,
                OneThousand, 
                x =>
                {
                    ConfigurableApplicationContext.Add<ReadableResource>(new ReadableResource
                    {
                        Id = Guid.NewGuid(),
                        Value = x
                    });
                });
            var actual = ConfigurableApplicationContext.Get<ReadableResource>();
            Assert.NotNull(actual);
            Assert.Equal(OneThousand, actual.Value);
        }

        #endregion
    }

    /// <summary>
    /// A <see cref="IConfigurableResource"/> for test.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "In order to isolate test cases as much as possible.")]
    internal sealed class ReadableResource : IConfigurableResource
    {
        /// <summary>
        /// Gets and sets the id.
        /// </summary>
        public Guid Id
        {
            get;
            set;
        }

        /// <summary>
        /// Gets and sets the value.
        /// </summary>
        public long Value
        {
            get;
            set;
        }
    }
}