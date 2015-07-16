// <copyright file="IPermission.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Core.Contracts.Permissions
{
    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface IPermission
    {
        /// <summary>
        /// The permission key.
        /// </summary>
        string Key
        {
            get;
        }

        /// <summary>
        /// The permission value.
        /// </summary>
        string Value
        {
            get;
        }
    }
}