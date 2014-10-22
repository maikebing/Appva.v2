// <copyright file="IValueObject.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Common.Domain
{
    /// <summary>
    /// An object that contains attributes but has no conceptual identity. 
    /// They should be treated as immutable.
    /// </summary>
    /// <typeparam name="T">The value object type</typeparam>
    public interface IValueObject<T>
    {
    }
}