// <copyright file="ImmutableAttribute.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johan.sall.larsson@appva.com">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Common
{
    #region Imports.

    using System;

    #endregion

    /// <summary>
    /// Indicates that a type is immutable. After construction, the publicly visible
    /// state of the object will not change.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This attribute only applies to types, not fields:
    /// it's entirely feasible to have a readonly field of a mutable type, or a 
    /// read/write field of an immutable type. In such cases for reference types 
    /// (classes and interfaces) it's important to distinguish between the value of the 
    /// variable (a reference) and the object it refers to.
    /// </para>
    /// <para>
    /// Some types may be publicly immutable, but contain privately mutable aspects, 
    /// e.g. caches. If it proves to be useful to indicate the kind of immutability 
    /// we're implementing, we can add an appropriate property to this attribute.
    /// </para>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    internal sealed class ImmutableAttribute : Attribute
    {
    }
}