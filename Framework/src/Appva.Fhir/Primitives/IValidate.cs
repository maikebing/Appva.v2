// <copyright file="IValidate.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//    <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Fhir.Primitives
{
    /// <summary>
    /// Validation interface.
    /// </summary>
    public interface IValidate
    {
        /// <summary>
        /// Returns whther or not the primitive is a valid struct.
        /// </summary>
        /// <returns>True if valid</returns>
        bool IsValid();
    }
}
