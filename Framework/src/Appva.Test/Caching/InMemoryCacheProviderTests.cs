// <copyright file="InMemoryCacheProviderTests.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Test.Caching
{
    #region Import.

    using System.Threading.Tasks;
    using Appva.Caching.Policies;
    using Appva.Caching.Providers;
    using Xunit;

    #endregion

    /// <summary>
    /// Test suite for <see cref="InMemoryCacheProvider"/>.
    /// </summary>
    public sealed class InMemoryCacheProviderTests
    {
        #region Variables.

        /// <summary>
        /// Zero (0).
        /// </summary>
        private const int Zero = 0;

        /// <summary>
        /// One (1).
        /// </summary>
        private const int One = 1;

        /// <summary>
        /// One thousand (1000).
        /// </summary>
        private const int OneThousand = 1000;

        /// <summary>
        /// The cache key.
        /// </summary>
        private const string Key = "foo";

        #endregion

        #region Tests.

        /// <summary>
        /// Test: Runs <c>InMemoryCacheProvider.Get</c> 1 000 times in parallel.
        /// Expected Result: The item is fetched 1 000 times. 
        /// </summary>
        [Fact]
        public void GetCachedItemOneThousandItems_ShouldNotBeModified()
        {
            var cache = new InMemoryCacheProvider(new LeastFrequentlyUsedPolicy());
            cache.Add(Key, Zero);
            Parallel.For(
                Zero,
                OneThousand, 
                x =>
                {
                    var actual = cache.Get<int>(Key);
                    Assert.Equal(Zero, actual);
                });
        }
        
        /// <summary>
        /// Test: Runs <c>InMemoryCacheProvider.AddOrUpdate</c> 1 000 times in parallel.
        /// Expected Result: 1 000 items successfully cached. 
        /// </summary>
        [Fact]
        public void AddOrUpdateOneThousandItems_ShouldContainsOneThousand()
        {
            var cache = new InMemoryCacheProvider(new LeastFrequentlyUsedPolicy());
            Parallel.For(
                Zero, 
                OneThousand, 
                x =>
                {
                    cache.AddOrUpdate(Key + x, x);
                });
            Assert.Equal(OneThousand, cache.Count());
        }

        /// <summary>
        /// Test: Runs <c>InMemoryCacheProvider.AddOrUpdate</c> 1 000 times for the same 
        /// cache key in parallel.
        /// Expected Result: 1 item successfully cached.
        /// </summary>
        [Fact]
        public void AddOrUpdateUsingTheSameKeyOneThousandTimes_ShouldOnlyContainOne()
        {
            var cache = new InMemoryCacheProvider(new LeastFrequentlyUsedPolicy());
            Parallel.For(
                Zero,
                OneThousand, 
                x =>
                {
                    cache.AddOrUpdate(Key, x);
                });
            Assert.Equal(One, cache.Count());
        }

        /// <summary>
        /// Test: Runs <c>InMemoryCacheProvider.AddOrUpdate</c> and then immediately 
        /// <c>InMemoryCacheProvider.Remove</c> 1 000 times for the same cache key in 
        /// parallel.
        /// Expected Result: 0 items in cache.
        /// </summary>
        [Fact]
        public void AddAndRemoveTheSameKeyOneThousandTimes_ShouldBeEmpty()
        {
            var cache = new InMemoryCacheProvider(new LeastFrequentlyUsedPolicy());
            Parallel.For(
                Zero,
                OneThousand,
                x =>
                {
                    cache.AddOrUpdate(Key, x);
                    cache.Remove(Key);
                });
            Assert.Equal(Zero, cache.Count());
        }

        #endregion
    }
}