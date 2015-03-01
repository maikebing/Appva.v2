// <copyright file="IntegerExtensions.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Cryptography.Extensions
{
    /// <summary>
    /// Extension helpers for ints.
    /// </summary>
    internal static class IntegerExtensions
    {
        /// <summary>
        /// 32 bit rotate left.
        /// </summary>
        /// <param name="x">The unsigned int to do work on</param>
        /// <param name="r">The byte</param>
        /// <returns>An unsigned int</returns>
        public static uint RotateLeft(this uint x, byte r)
        {
            return (x << r) | (x >> (32 - r));
        }

        /// <summary>
        /// 64 bit rotate left.
        /// </summary>
        /// <param name="x">The unsigned long to do work on</param>
        /// <param name="r">The byte</param>
        /// <returns>An unsigned long</returns>
        public static ulong RotateLeft(this ulong x, byte r)
        {
            return (x << r) | (x >> (64 - r));
        }

        /// <summary>
        /// 32 bit final mix for Murmur hash.
        /// </summary>
        /// <param name="h">The unsigned int to do work on</param>
        /// <returns>An unsigned int</returns>
        public static uint FMix(this uint h)
        {
            h = (h ^ (h >> 16)) * 0x85ebca6b;
            h = (h ^ (h >> 13)) * 0xc2b2ae35;
            return h ^ (h >> 16);
        }

        /// <summary>
        /// 64 bit final mix for Murmur hash.
        /// </summary>
        /// <param name="h">The unsigned long to do work on</param>
        /// <returns>An unsigned long</returns>
        public static ulong FMix(this ulong h)
        {
            h = (h ^ (h >> 33)) * 0xff51afd7ed558ccd;
            h = (h ^ (h >> 33)) * 0xc4ceb9fe1a85ec53;
            return h ^ (h >> 33);
        }
    }
}
