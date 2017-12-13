// <copyright file="FecesUnit.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>
namespace Appva.Mcss.Domain.Unit
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// 
    /// </summary>
    public sealed class FecesUnit : AbstractUnit<FecesScale>
    {
        #region Variables.

        /// <summary>
        /// The unit name identifier / name.
        /// </summary>
        private const string Id = "common";

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="FecesUnit"/> class.
        /// </summary>
        /// <param name="value">
        /// A string representation of a bristol scale value set code, see
        /// <see cref="FecesScale"/>.
        /// </param>
        public FecesUnit(string value)
            : base(Id, Parse(value))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FecesUnit"/> class.
        /// </summary>
        /// <param name="value">The <see cref="FecesScale"/> enumeration.</param>
        public FecesUnit(FecesScale value)
            : base(Id, value)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FecesUnit"/> class.
        /// </summary>
        /// <remarks>
        /// For internal serialization.
        /// </remarks>
        private FecesUnit()
            : base(Id)
        {
        }

        #endregion

        #region Public Static Functions.

        /// <summary>
        /// Converts a string representation of a feces scale code to a 
        /// <see cref="FecesScale"/> enum.
        /// </summary>
        /// <param name="str">
        /// A string representation of a feces scale value set code.
        /// </param>
        /// <returns>
        /// A <see cref="FecesScale"/> which accurately represents the 
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
        /// <typeparamref name="str"/> is not a valid feces scale enumeration.
        /// </exception>
        public static FecesScale Parse(string str)
        {
            if (str == null)
            {
                throw new ArgumentNullException("str", "str cannot be null");
            }
            if (string.IsNullOrWhiteSpace(str))
            {
                throw new ArgumentException("str cannot be an empty string or only contain whitespace", "str");
            }
            FecesScale result;
            if ((Enum.TryParse(str, true, out result)) && (Enum.IsDefined(typeof(FecesScale), result)) == false)
            {
                throw new ArgumentOutOfRangeException("str", str + "is not a valid feces enumeration.");
            }
            return result;
        }

        /// <summary>
        /// Converts a string representation of a feces scale code to a 
        /// <see cref="FecesScale"/> enum. The return value indicates whether the conversion 
        /// succeeded.
        /// </summary>
        /// <param name="str">
        /// A string representation of a feces scale value set code.
        /// </param>
        /// <param name="result">
        /// When this method returns, result contains an object of type 
        /// <see cref="FecesScale"/> whose value is represented by value if the parse 
        /// operation succeeds. If the parse operation fails, result contains the default 
        /// value of the underlying type <see cref="FecesScale"/>.
        /// </param>
        /// <returns>
        /// <c>true</c> if the the conversion succeeded; false otherwise.
        /// </returns>
        internal static bool TryParse(string str, out FecesScale result)
        {
            switch (str)
            {
                case "AAA":
                    result = FecesScale.BigTripleA;
                    return true;
                case "AA":
                    result = FecesScale.BigDoubleA;
                    return true;
                case "A":
                    result = FecesScale.BigA;
                    return true;
                case "a":
                    result = FecesScale.SmallA;
                    return true;
                case "aaa":
                    result = FecesScale.SmallTripleA;
                    return true;
                case "d":
                    result = FecesScale.SmallD;
                    return true;
                case "D":
                    result = FecesScale.BigD;
                    return true;
                case "k":
                    result = FecesScale.K;
                    return true;
                default:
                    result = (FecesScale)0;
                    return false;
            }
        }

        /// <summary>
        /// Alls this instance.
        /// </summary>
        /// <returns>IEnumerable&lt;FecesScale&gt;.</returns>
        public static IEnumerable<FecesScale> All()
        {
            //// UNRESOLELVED: Does this work?
            return Enum.GetValues(typeof(FecesScale)).ToString().Cast<FecesScale>();
        }


        #endregion

        /// <inheritdoc />
        public override string ToString(IFormatProvider provider)
        {
            return this.ToString();
        }

        /// <inheritdoc />
        public override string ToString()
        {
            switch (this.Value)
            {
                case FecesScale.BigTripleA: return "AAA";
                case FecesScale.BigDoubleA: return "AA";
                case FecesScale.BigA: return "A";
                case FecesScale.SmallA: return "a";
                case FecesScale.SmallTripleA: return "aaa";
                case FecesScale.SmallD: return "d";
                case FecesScale.BigD: return "D";
                case FecesScale.K: return "k";
            }
            throw new InvalidOperationException();
        }
    }
}