// <copyright file="CachePolicy.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Caching.Policies
{
    #region Imports.

    using System;
    using System.Runtime.Serialization;

    #endregion

    /// <summary>
    /// Abstract base implementation of <see cref="ICachePolicy"/>.
    /// </summary>
    public abstract class CachePolicy : ICachePolicy
    {
        #region Private Variables.

        /// <summary>
        /// The sample size to use.
        /// </summary>
        private static readonly int DefaultSampleSize = 30;

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

        #region Implementation.

        /// <inheritdoc />
        public ICacheItem FindEvictionCandidate(ICacheItem[] sampledElements)
        {
            if (sampledElements.Length == 1)
            {
                return sampledElements[0];
            }
            ICacheItem lowestElement = null;
            foreach (var element in sampledElements)
            {
                if (element == null)
                {
                    continue;
                }
                if (lowestElement == null)
                {
                    lowestElement = element;
                } 
                else if (this.Compare(lowestElement, element))
                {
                    lowestElement = element;
                }
            }
            return lowestElement;
        }

        /// <inheritdoc />
        public abstract bool Compare(ICacheItem element1, ICacheItem element2);

        #endregion
    }
}