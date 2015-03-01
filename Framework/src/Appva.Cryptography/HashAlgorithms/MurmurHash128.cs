// <copyright file="MurmurHash128.cs" company="Appva AB">
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
    /// MurmurHash version 3, 128-bit variant for x64 processors.
    /// <externalLink>
    ///     <linkText>MurmurHash</linkText>
    ///     <linkUri>
    ///         http://en.wikipedia.org/wiki/MurmurHash
    ///     </linkUri>
    /// </externalLink>
    /// </summary>
    /// <remarks>
    /// The x86 and x64 versions do not produce the same results as the algorithms are 
    /// optimized for their respective platforms.
    /// </remarks>
    public sealed class MurmurHash128 : HashAlgorithm
    {
        #region Variables.

        /// <summary>
        /// The state.
        /// </summary>
        private MurmurHashState state;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="MurmurHash128"/> class.
        /// </summary>
        public MurmurHash128()
        {
            this.Initialize();
        }

        #endregion

        #region Public Properties.

        /// <summary>
        /// The seed.
        /// </summary>
        public uint Seed
        {
            get;
            set;
        }

        /// <inheritdoc />
        public override int HashSize
        {
            get
            {
                return 128;
            }
        }

        #endregion

        #region HashAlgorithm Overrides.

        /// <inheritdoc />
        public override void Initialize()
        {
            this.state.H1 = this.state.H2 = this.Seed;
            this.state.C1 = 0x87c37b91114253d5;
            this.state.C2 = 0x4cf5ad432745937f;
            this.state.Length = 0;
        }

        /// <inheritdoc />
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1305:FieldNamesMustNotUseHungarianNotation", Justification = "Reviewed.")]
        protected override void HashCore(byte[] array, int ibStart, int cbSize)
        {
            this.state.Length += cbSize;
            this.Body(array, ibStart, cbSize);
        }

        /// <inheritdoc />
        protected override byte[] HashFinal()
        {
            var len = (ulong) this.state.Length;
            this.state.H1 ^= len;
            this.state.H2 ^= len;
            this.state.H1 += this.state.H2;
            this.state.H2 += this.state.H1;
            this.state.H1 = this.state.H1.FMix();
            this.state.H2 = this.state.H2.FMix();
            this.state.H1 += this.state.H2;
            this.state.H2 += this.state.H1;
            var result = new byte[16];
            Array.Copy(BitConverter.GetBytes(this.state.H1), 0, result, 0, 8);
            Array.Copy(BitConverter.GetBytes(this.state.H2), 0, result, 8, 8);
            return result;
        }

        /// <summary>
        /// The 128-bit variants mix multiple blocks of key data in parallel
        /// </summary>
        /// <param name="data">The byte data</param>
        /// <param name="start">The start</param>
        /// <param name="length">The length</param>
        private void Body(byte[] data, int start, int length)
        {
            var remainder = length & 15;
            var alignedLength = start + (length - remainder);
            for (var i = start; i < alignedLength; i += 16)
            {
                this.state.H1 ^= (data.ToUInt64(i) * this.state.C1).RotateLeft(31) * this.state.C2;
                this.state.H1  = ((this.state.H1.RotateLeft(27) + this.state.H2) * 5) + 0x52dce729;
                this.state.H2 ^= (data.ToUInt64(i + 8) * this.state.C2).RotateLeft(33) * this.state.C1;
                this.state.H2  = ((this.state.H2.RotateLeft(31) + this.state.H1) * 5) + 0x38495ab5;
            }
            if (remainder > 0)
            {
                this.Tail(data, alignedLength, remainder);
            }
        }

        /// <summary>
        /// Process the remainder.
        /// </summary>
        /// <param name="tail">The remaining bytes</param>
        /// <param name="start">The length</param>
        /// <param name="remaining">The remainder</param>
        private void Tail(byte[] tail, int start, int remaining)
        {
            ulong k1 = 0, k2 = 0;
            switch (remaining)
            {
                case 15: 
                    k2 ^= (ulong) tail[start + 14] << 48; 
                    goto case 14;
                case 14: 
                    k2 ^= (ulong) tail[start + 13] << 40; 
                    goto case 13;
                case 13: 
                    k2 ^= (ulong) tail[start + 12] << 32; 
                    goto case 12;
                case 12: 
                    k2 ^= (ulong) tail[start + 11] << 24; 
                    goto case 11;
                case 11: 
                    k2 ^= (ulong) tail[start + 10] << 16; 
                    goto case 10;
                case 10: 
                    k2 ^= (ulong) tail[start + 9] << 8; 
                    goto case 9;
                case 9:  
                    k2 ^= (ulong) tail[start + 8] << 0; 
                    goto case 8;
                case 8:  
                    k1 ^= (ulong) tail[start + 7] << 56; 
                    goto case 7;
                case 7:  
                    k1 ^= (ulong) tail[start + 6] << 48; 
                    goto case 6;
                case 6:  
                    k1 ^= (ulong) tail[start + 5] << 40; 
                    goto case 5;
                case 5:  
                    k1 ^= (ulong) tail[start + 4] << 32; 
                    goto case 4;
                case 4:  
                    k1 ^= (ulong) tail[start + 3] << 24; 
                    goto case 3;
                case 3:  
                    k1 ^= (ulong) tail[start + 2] << 16; 
                    goto case 2;
                case 2:  
                    k1 ^= (ulong) tail[start + 1] << 8; 
                    goto case 1;
                case 1:  
                    k1 ^= (ulong) tail[start] << 0; 
                    break;
            }
            this.state.H2 ^= (k2 * this.state.C2).RotateLeft(33) * this.state.C1;
            this.state.H1 ^= (k1 * this.state.C1).RotateLeft(31) * this.state.C2;
        }

        #endregion

        #region MurmurHash State.

        /// <summary>
        /// The state.
        /// </summary>
        public struct MurmurHashState
        {
            /// <summary>
            /// Constant 1.
            /// </summary>
            public ulong C1;

            /// <summary>
            /// Constant 2.
            /// </summary>
            public ulong C2;

            /// <summary>
            /// Hash state 1.
            /// </summary>
            public ulong H1;

            /// <summary>
            /// Hash state 2.
            /// </summary>
            public ulong H2;

            /// <summary>
            /// The length.
            /// </summary>
            public int Length;
        }

        #endregion
    }
}
