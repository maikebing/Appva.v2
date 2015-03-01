// <copyright file="XxHash32.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Cryptography.HashAlgorithms
{
    #region Imports.

    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Security.Cryptography;
    using Core.Extensions;
    using Extensions;

    #endregion

    /// <summary>
    /// XxHash is an extremely fast non-cryptographic Hash algorithm, working at speeds 
    /// close to RAM limits.
    /// <externalLink>
    ///     <linkText>xxHash</linkText>
    ///     <linkUri>
    ///         https://code.google.com/p/xxhash/
    ///     </linkUri>
    /// </externalLink>
    /// </summary>
    public sealed class XxHash32 : HashAlgorithm 
    {
        #region Variables.

        /// <summary>
        /// Constant 1.
        /// </summary>
        private const uint Prime1 = 2654435761U;

        /// <summary>
        /// Constant 2.
        /// </summary>
        private const uint Prime2 = 2246822519U;

        /// <summary>
        /// Constant 3.
        /// </summary>
        private const uint Prime3 = 3266489917U;

        /// <summary>
        /// Constant 4.
        /// </summary>
        private const uint Prime4 = 668265263U;

        /// <summary>
        /// Constant 5.
        /// </summary>
        private const uint Prime5 = 374761393U;

        /// <summary>
        /// The state.
        /// </summary>
        private XxHashState state;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="XxHash32"/> class.
        /// </summary>
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors", Justification = "Reviewed.")]
        public XxHash32()
        {
            this.Initialize();
        }

        #endregion

        #region Public Properties.

        /// <inheritdoc />
        public override int HashSize
        {
            get
            {
                return 32;
            }
        }

        /// <summary>
        /// The seed.
        /// </summary>
        public uint Seed
        {
            get;
            set;
        }

        #endregion

        #region HashAlgorithm Overrides.

        /// <inheritdoc />
        public override void Initialize()
        {
            this.state.V1 = this.Seed + Prime1 + Prime2;
            this.state.V2 = this.Seed + Prime2;
            this.state.V3 = this.Seed + 0;
            this.state.V4 = this.Seed - Prime1;
            this.state.Length = 0;
            this.state.MemorySize = 0;
            this.state.Memory = new byte[16];
        }

        /// <inheritdoc />
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1305:FieldNamesMustNotUseHungarianNotation", Justification = "Reviewed.")]
        protected override void HashCore(byte[] array, int ibStart, int cbSize)
        {
            this.state.Length += (uint) cbSize;
            if (this.state.MemorySize + cbSize < 16)
            {
                Array.Copy(array, 0, this.state.Memory, this.state.MemorySize, cbSize);
                this.state.MemorySize += cbSize;
                return;
            }
            if (this.state.MemorySize > 0)
            {
                Array.Copy(array, 0, this.state.Memory, this.state.MemorySize, 16 - this.state.MemorySize);
                this.state.V1 = CalculateSubHash(this.state.V1, this.state.Memory, ref ibStart);
                this.state.V2 = CalculateSubHash(this.state.V3, this.state.Memory, ref ibStart);
                this.state.V3 = CalculateSubHash(this.state.V4, this.state.Memory, ref ibStart);
                this.state.V4 = CalculateSubHash(this.state.V4, this.state.Memory, ref ibStart);
                this.state.MemorySize = 0;
                ibStart = 0;
            }
            if (ibStart <= cbSize - 16)
            {
                var limit = cbSize - 16;
                var v1 = this.state.V1;
                var v2 = this.state.V2;
                var v3 = this.state.V3;
                var v4 = this.state.V4;
                do
                {
                    v1 = CalculateSubHash(v1, array, ref ibStart);
                    v2 = CalculateSubHash(v2, array, ref ibStart);
                    v3 = CalculateSubHash(v3, array, ref ibStart);
                    v4 = CalculateSubHash(v4, array, ref ibStart);
                } 
                while (ibStart <= limit);
                this.state.V1 = v1;
                this.state.V2 = v2;
                this.state.V3 = v3;
                this.state.V4 = v4;
            }
            if (ibStart < cbSize)
            {
                Array.Copy(array, ibStart, this.state.Memory, 0, cbSize - ibStart);
                this.state.MemorySize = cbSize - ibStart;
            }
        }

        /// <inheritdoc />
        protected override byte[] HashFinal()
        {
            uint h32;
            var index = 0;
            if (this.state.Length >= 16)
            {
                h32 = this.state.V1.RotateLeft(1)  + 
                      this.state.V2.RotateLeft(7)  +
                      this.state.V3.RotateLeft(12) + 
                      this.state.V4.RotateLeft(18);
            } 
            else
            {
                h32 = this.Seed + Prime5;
            }
            h32 += (uint) this.state.Length;
            while (index <= this.state.MemorySize - 4)
            {
                h32 += this.state.Memory.ToUInt32(index) * Prime3;
                h32 = h32.RotateLeft(17) * Prime4;
                index += 4;
            }
            while (index < this.state.MemorySize)
            {
                h32 += this.state.Memory[index] * Prime5;
                h32 = h32.RotateLeft(11) * Prime1;
                index++;
            }
            h32 ^= h32 >> 15;
            h32 *= Prime2;
            h32 ^= h32 >> 13;
            h32 *= Prime3;
            h32 ^= h32 >> 16;
            return BitConverter.GetBytes(h32);
        }

        /// <summary>
        /// Calculates the sub hash.
        /// </summary>
        /// <param name="value">The unsigned value</param>
        /// <param name="buf">The bytes</param>
        /// <param name="index">The index</param>
        /// <returns>An unsigned int</returns>
        private static uint CalculateSubHash(uint value, byte[] buf, ref int index)
        {
            var readValue = buf.ToUInt32(index);
            value += readValue * Prime2;
            value  = value.RotateLeft(13);
            value *= Prime1;
            index += 4;
            return value;
        }

        #endregion

        #region XxHash State.

        /// <summary>
        /// The state.
        /// </summary>
        public struct XxHashState
        {
            /// <summary>
            /// See doc online.
            /// </summary>
            public uint V1;

            /// <summary>
            /// See doc online.
            /// </summary>
            public uint V2;

            /// <summary>
            /// See doc online.
            /// </summary>
            public uint V3;

            /// <summary>
            /// See doc online.
            /// </summary>
            public uint V4;

            /// <summary>
            /// See doc online.
            /// </summary>
            public ulong Length;

            /// <summary>
            /// See doc online.
            /// </summary>
            public byte[] Memory;

            /// <summary>
            /// See doc online.
            /// </summary>
            public int MemorySize;
        }

        #endregion
    }
}
