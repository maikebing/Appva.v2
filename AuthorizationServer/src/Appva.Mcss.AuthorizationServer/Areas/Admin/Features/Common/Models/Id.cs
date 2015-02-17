// <copyright file="Id.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.AuthorizationServer.Models
{
    #region Imports.

    using System;
    using Appva.Cqrs;

    #endregion

    /// <summary>
    /// Identity request.
    /// </summary>
    public class Idg
    {
        /// <summary>
        /// The identity.
        /// </summary>
        public Guid Id
        {
            get;
            set;
        }

        /// <summary>
        /// The Slug.
        /// </summary>
        public string Slug
        {
            get;
            set;
        }
    }

    /// <summary>
    /// Identity request.
    /// </summary>
    /// <typeparam name="T">The output class</typeparam>
    public class Id<T> : Idg, IRequest<T> where T : class
    {
    }
}