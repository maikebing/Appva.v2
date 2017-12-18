// <copyright file="BristolUnit.cs" company="Appva AB">
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
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class BristolUnit : AbstractUnit<BristolScale>
    {
        #region Variables.

        /// <summary>
        /// The unit name identifier / name.
        /// </summary>
        private const string Id = "bristol";

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="BristolUnit"/> class.
        /// </summary>
        /// <param name="value">
        /// A string representation of a bristol scale value set code, see
        /// <see cref="BristolScale"/>.
        /// </param>
        public BristolUnit(string value)
            : base(Id, Parse(value))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BristolUnit"/> class.
        /// </summary>
        /// <param name="value">The <see cref="BristolScale"/> enumeration.</param>
        public BristolUnit(BristolScale value)
            : base(Id, value)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BristolUnit"/> class.
        /// </summary>
        /// <remarks>
        /// For internal serialization.
        /// </remarks>
        private BristolUnit()
            : base(Id)
        {
        }

        #endregion

        #region Public Static Functions.

        /// <summary>
        /// Converts a string representation of a bristol scale code to a 
        /// <see cref="BristolScale"/> enum.
        /// </summary>
        /// <param name="str">
        /// A string representation of a bristol scale value set code.
        /// </param>
        /// <returns>
        /// A <see cref="BristolScale"/> which accurately represents the 
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
        /// <typeparamref name="str"/> is not a valid bristol scale enumeration.
        /// </exception>
        public static BristolScale Parse(string str)
        {
            if (str == null)
            {
                throw new ArgumentNullException("str", "str cannot be null");
            }
            if (string.IsNullOrWhiteSpace(str))
            {
                throw new ArgumentException("str cannot be an empty string or only contain whitespace", "str");
            }
            BristolScale result;
            if (! (Enum.TryParse(str, true, out result) && Enum.IsDefined(typeof(BristolScale), result)))
            {
                throw new ArgumentOutOfRangeException("str", str + "is not a valid bristol scale enumeration.");
            }
            return result;
        }

        #endregion  

        #region AbstractUnit Overrides.

        /// <inheritdoc />
        public override string ToString()
        {
            switch (this.Value)
            {
                case BristolScale.Type1: return "Typ 1";
                case BristolScale.Type2: return "Typ 2";
                case BristolScale.Type3: return "Typ 3";
                case BristolScale.Type4: return "Typ 4";
                case BristolScale.Type5: return "Typ 5";
                case BristolScale.Type6: return "Typ 6";
                case BristolScale.Type7: return "Typ 7";
            }
            throw new InvalidOperationException();
        }

        /// <inheritdoc />
        public override string ToString(IFormatProvider provider)
        {
            return this.Value.ToString(/* provider is deprecated for enum */);
        }

        #endregion

        #region Static Members.

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static string ToString(BristolScale value)
        {
            switch (value)
            {
                case BristolScale.Type1: return "Typ 1";
                case BristolScale.Type2: return "Typ 2";
                case BristolScale.Type3: return "Typ 3";
                case BristolScale.Type4: return "Typ 4";
                case BristolScale.Type5: return "Typ 5";
                case BristolScale.Type6: return "Typ 6";
                case BristolScale.Type7: return "Typ 7";
            }
            throw new InvalidOperationException();
        }

        /// <summary>
        /// Determines whether [has valid value] [the specified value].
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if [has valid value] [the specified value]; otherwise, <c>false</c>.</returns>
        public static bool HasValidValue(string value)
        {
            BristolScale result;
            if ((Enum.TryParse(value, true, out result) && Enum.IsDefined(typeof(BristolScale), result)) == false)
            {
                return false;
            }
            return true;
        }

        #endregion
    }
}