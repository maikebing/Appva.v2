// <copyright file="ETag.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.WebApi
{
    #region Imports.

    using System;
    using Core.Extensions;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public interface IETag
    {
        /// <summary>
        /// The identity.
        /// </summary>
        Guid Id
        {
            get;
        }

        /// <summary>
        /// The version.
        /// </summary>
        int Version
        {
            get;
        }
    }

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public class ETag : IETag
    {
        /// <inheritdoc />
        public Guid Id
        {
            get;
            set;
        }

        /// <inheritdoc />
        public int Version
        {
            get;
            set;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return "{0}.{1}".FormatWith(this.Id, this.Version);
        }
    }
}