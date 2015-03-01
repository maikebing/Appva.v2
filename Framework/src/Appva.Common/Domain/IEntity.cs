// <copyright file="IEntity.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Common.Domain
{
    /// <summary>
    /// An object that is not defined by its attributes, but rather by a thread of 
    /// continuity and its identity.
    /// </summary>
    public interface IEntity : IIdentity
    {
        /// <summary>
        /// The version.
        /// </summary>
        int Version
        {
            get;
        }
    }
}
