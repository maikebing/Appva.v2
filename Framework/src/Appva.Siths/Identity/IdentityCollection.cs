// <copyright file="IdentityCollection.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Siths.Identity
{
    #region Imports.

    using System.Collections.Generic;
    using Newtonsoft.Json;

    #endregion

    /// <summary>
    /// Representation of an identity collection from Authify.
    /// </summary>
    /// <typeparam name="T">The identity type</typeparam>
    public sealed class IdentityCollection<T> where T : class
    {
        /// <summary>
        /// The identities in the collection.
        /// </summary>
        [JsonProperty("data")]
        public IList<T> Identities
        {
            get;
            set;
        }
    }
}