// <copyright file="IValidator.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
// ReSharper disable CheckNamespace
namespace Appva.Core
{
    /// <summary>
    /// Represents a generic validator for classes.
    /// </summary>
    public interface IValidator
    {
        /// <summary>
        /// Returns whether or not the class is valid.
        /// </summary>
        /// <returns>True if the class is valid</returns>
        bool IsValid();
    }
}