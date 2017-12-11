﻿// <copyright file="TenaIdentifiUnit.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.alvegard@appva.com">Richard Alvegard</a>
// </author>
namespace Appva.Mcss.Domain.Unit
{
    #region Imports.

    using System;

    #endregion

    /// <summary>
    /// A Tena Identifi <see cref="IUnit"/> implementation.
    /// </summary>
    public sealed class TenaIdentifiUnit : AbstractUnit<TenaIdentifiScale>
    {
        #region Variables.

        /// <summary>
        /// The unit name identifier / name.
        /// </summary>
        public const string Id = "tena_identifi";

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="TenaIdentifiUnit"/> class.
        /// </summary>
        /// <param name="value">
        /// A string representation of a TENA Identifi value set code, see
        /// <see cref="TenaIdentifiScale"/>.
        /// </param>
        public TenaIdentifiUnit(string value)
            : base(Id, Parse(value))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TenaIdentifiUnit"/> class.
        /// </summary>
        /// <param name="value">The <see cref="TenaIdentifiScale"/> enumeration.</param>
        public TenaIdentifiUnit(TenaIdentifiScale value)
            : base(Id, value)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TenaIdentifiUnit"/> class.
        /// </summary>
        /// <remarks>
        /// For internal serialization.
        /// </remarks>
        private TenaIdentifiUnit()
            : base(Id)
        {
        }

        #endregion

        #region Public Static Functions.

        /// <summary>
        /// Converts a string representation of a TENA Identifi code to a 
        /// <see cref="TenaIdentifiScale"/> enum.
        /// </summary>
        /// <param name="str">
        /// A string representation of a TENA Identifi value set code.
        /// </param>
        /// <returns>
        /// A <see cref="TenaIdentifiScale"/> which accurately represents the 
        /// <typeparamref name="str"/>.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// <typeparamref name="str"/> is null.
        /// </exception>
        /// <exception cref="System.ArgumentException">
        /// <typeparamref name="str"/> is either an empty string ("") or only contains white 
        /// space.
        /// </exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// <typeparamref name="str"/> is not a valid TENA identifi enumeration.
        /// </exception>
        public static TenaIdentifiScale Parse(string str)
        {
            if (str == null)
            {
                throw new ArgumentNullException("str", "str cannot be null");
            }
            if (string.IsNullOrWhiteSpace(str))
            {
                throw new ArgumentException("str cannot be an empty string or only contain whitespace", "str");
            }
            TenaIdentifiScale result;
            if (!Enum.TryParse(str, true, out result))
            {
                throw new ArgumentOutOfRangeException("str", str + "is not a valid TenaIdentifi enumeration.");
            }
            return result;
        }

        #endregion  

        #region AbstractUnit Overrides.

        /// <inheritdoc />
        public override string ToString()
        {
            return this.Value.ToString();
        }

        /// <inheritdoc />
        public override string ToString(IFormatProvider provider)
        {
            return this.Value.ToString(/* provider is deprecated for enum */);
        }

        #endregion
    }
}