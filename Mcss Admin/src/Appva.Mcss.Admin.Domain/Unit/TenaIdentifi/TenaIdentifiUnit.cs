// <copyright file="TenaIdentifiUnit.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:$emailAddress$">$developer$</a>
// </author>
namespace Appva.Mcss.Domain.Unit
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class TenaIdentifiUnit : AbstractUnit<TenaIdentifiScale>
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="TenaIdentifiUnit"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public TenaIdentifiUnit(string value)
            : base(ParseFromString(value))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TenaIdentifiUnit"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public TenaIdentifiUnit(TenaIdentifiScale value)
            :base(value)
        {
        }

        #endregion

        #region AbstractUnit overrides

        /// <inheritdoc />
        public override string ToString(IFormatProvider provider)
        {
            return this.Value.ToString();
        }

        /// <inheritdoc />
        public override string UnitName
        {
            get { return "tena-identifi"; }
        }

        #endregion

        #region Static members

        /// <summary>
        /// Gets all available units.
        /// </summary>
        /// <value>
        /// All available units.
        /// </value>
        public static IEnumerable<TenaIdentifiScale> AllAvailableUnits
        {
            get { return Enum.GetValues(typeof(TenaIdentifiScale)).Cast<TenaIdentifiScale>(); }
        }

        /// <summary>
        /// Parses from string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException"></exception>
        public static TenaIdentifiScale ParseFromString(string value)
        {
            foreach (var unit in AllAvailableUnits)
            {
                if (Enum.GetName(typeof(TenaIdentifiScale), unit).ToLower() == value.ToLower())
                {
                    return unit;
                }
            }
            throw new ArgumentException(string.Format("{0} cannot be parsed to TenaIdentifiScale"));
        }

        #endregion  
    }    
}