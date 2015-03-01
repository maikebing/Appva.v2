// <copyright file="EvicationPolicy.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Caching.Policies
{
    #region Imports.

    using System;
    using Logging;

    #endregion

    /// <summary>
    /// Abstract base implementation of <see cref="IEvicationPolicy"/>.
    /// </summary>
    public abstract class EvictionPolicy : IEvicationPolicy
    {
        #region Private Variables.

        /// <summary>
        /// The sample size to use.
        /// </summary>
        private const int DefaultSampleSize = 30;

        /// <summary>
        /// The <see cref="ILog"/> for <see cref="EvictionPolicy"/>.
        /// </summary>
        private static readonly ILog Log = LogProvider.For<EvictionPolicy>();

        /// <summary>
        /// To select random numbers.
        /// </summary>
        private static readonly Random Random = new Random();

        #endregion

        #region Public Static Functions.

        /// <summary>
        /// How many samples to take.
        /// </summary>
        /// <param name="populationSize">The size of the store</param>
        /// <returns>The smaller of the map size and the default sample size of 30</returns>
        public static int CalculateSampleSize(int populationSize)
        {
            return (populationSize < DefaultSampleSize) ? populationSize : DefaultSampleSize;
        }

        /// <summary>
        /// Generates a random sample from a population.
        /// </summary>
        /// <param name="populationSize">The size to draw from</param>
        /// <returns>A list of random offsets</returns>
        public static int[] GenerateRandomSample(int populationSize)
        {
            var sampleSize = CalculateSampleSize(populationSize);
            var offsets = new int[sampleSize];
            if (sampleSize != 0)
            {
                int maxOffset = populationSize / sampleSize;
                for (int i = 0; i < sampleSize; i++)
                {
                    offsets[i] = Random.Next(maxOffset);
                }
            }
            return offsets;
        }

        #endregion

        #region IEvicationPolicy Members.

        /// <inheritdoc />
        public ICacheItem FindCandidate(ICacheItem[] samples)
        {
            ICacheItem item = null;
            if (samples.Length == 1)
            {
                item = samples[0];
            }
            else
            {
                foreach (var sample in samples)
                {
                    if (sample == null)
                    {
                        continue;
                    }
                    if (item == null)
                    {
                        item = sample;
                    }
                    else if (this.Compare(item, sample))
                    {
                        item = sample;
                    }
                }
            }
            if (item != null)
            {
                Log.DebugFormat(
                    Debug.Messages.EvicationPolicyCandidateFound, 
                    item.Key, 
                    item.Value, 
                    item.CreatedAt, 
                    item.ModifiedAt, 
                    item.Hits);
            }
            else
            {
                Log.Debug(Debug.Messages.EvicationPolicyCandidateNotFound);
            }
            return item;
        }

        /// <inheritdoc />
        public abstract bool Compare(ICacheItem element1, ICacheItem element2);

        #endregion
    }
}
