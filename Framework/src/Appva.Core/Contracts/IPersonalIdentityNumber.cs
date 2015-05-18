// <copyright file="IPersonalIdentityNumber.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
// ReSharper disable CheckNamespace
namespace Appva.Core
{
    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface IPersonalIdentityNumber : IValidator
    {
        /// <summary>
        /// Returns the underlying personal identity number.
        /// </summary>
        string Value
        {
            get;
        }
    }
}