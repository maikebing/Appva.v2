// <copyright file="Symbol.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Pdf.Prescriptions
{
    #region Imports.

    using Appva.Common.Domain;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class Symbol : ValueObject<Symbol>
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Symbol"/> class.
        /// </summary>
        /// <param name="key">The symbol key</param>
        /// <param name="value">The symbol value</param>
        public Symbol(string key, string value)
        {
            this.Key = key;
            this.Value = value;
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The symbol key.
        /// </summary>
        public string Key
        {
            get;
            private set;
        }

        /// <summary>
        /// The symbol value.
        /// </summary>
        public string Value
        {
            get;
            private set;
        }

        #endregion

        #region ValueObject<Symbol> Overrides.

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return this.Key.GetHashCode() + this.Value.GetHashCode();
        }

        /// <inheritdoc />
        public override bool Equals(Symbol other)
        {
            return this.Key.Equals(other.Key) && this.Value.Equals(other.Value);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return this.Key + ": " + this.Value;
        }

        #endregion
    }
}