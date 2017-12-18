// <copyright file="NonUnitOfMeasurement.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johan.sall.larsson@appva.com">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Entities
{
    /// <summary>
    /// Represents non units.
    /// </summary>
    internal sealed class NonUnitOfMeasurement : UnitOfMeasurement
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="NonUnitOfMeasurement"/> class.
        /// </summary>
        /// <param name="name">The unofficial name of the unit.</param>
        /// <param name="code">The case insensitive code.</param>
        public NonUnitOfMeasurement(string name, string code)
            : base(name, null, code)
        {
        }

        #endregion

        #region Public Static Functions.

        /// <summary>
        /// Creates a new instance of the <see cref="NonUnitOfMeasurement"/> class.
        /// </summary>
        /// <param name="name">The unofficial name of the unit.</param>
        /// <param name="code">The case insensitive code.</param>
        /// <returns>A new <see cref="NonUnitOfMeasurement"/> instance.</returns>
        public static NonUnitOfMeasurement New(string name, string code)
        {
            return new NonUnitOfMeasurement(name, code);
        }

        #endregion
    }
}