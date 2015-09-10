// <copyright file="Signature.cs" company="Appva AB">
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
    public sealed class Signature : ValueObject<Signature>
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Signature"/> class.
        /// </summary>
        /// <param name="name">The full name</param>
        /// <param name="initials">The initials</param>
        public Signature(string name, string initials)
        {
            this.Name = name;
            this.Initials = initials;
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The full human name.
        /// </summary>
        public string Name
        {
            get;
            private set;
        }

        /// <summary>
        /// The initials.
        /// </summary>
        public string Initials
        {
            get;
            private set;
        }

        #endregion

        #region ValueObject<Signature> Overrides.

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return this.Name.GetHashCode() + this.Initials.GetHashCode();
        }

        /// <inheritdoc />
        public override bool Equals(Signature other)
        {
            if (other == null)
            {
                return false;
            }
            return this.Name.Equals(other.Name) && this.Initials.Equals(other.Initials);
        }

        #endregion
    }
}