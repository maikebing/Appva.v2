// <copyright file="KeySize.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Cryptography.X509
{
    #region Imports.

    using Validation;

    #endregion

    /// <summary>
    /// The encryption bit size.
    /// </summary>
    public sealed class KeySize
    {
        #region Variables.
        
        /// <summary>
        /// The key size in bits.
        /// </summary>
        private readonly int size;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="KeySize"/> class.
        /// </summary>
        /// <param name="size">The size in bits</param>
        private KeySize(int size)
        {
            Requires.ValidState(size > 0, "Key size must not be zero or a negative value");
            this.size = size;
        }

        #endregion

        #region Properties.

        /// <summary>
        /// 1024 bit size.
        /// </summary>
        public static KeySize Bit1024
        {
            get
            {
                return new KeySize(1024);
            }
        }

        /// <summary>
        /// 2048 bit size.
        /// </summary>
        public static KeySize Bit2048
        {
            get
            {
                return new KeySize(2048);
            }
        }

        /// <summary>
        /// 4096 bit size.
        /// </summary>
        public static KeySize Bit4096
        {
            get
            {
                return new KeySize(4096);
            }
        }

        /// <summary>
        /// Gets the bit size.
        /// </summary>
        public int Value
        {
            get
            {
                return this.size;
            }
        }

        #endregion
    }
}
