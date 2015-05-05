// <copyright file="Id.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Models
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using Appva.Cqrs;

    #endregion

    /// <summary>
    /// Identity request.
    /// </summary>
    /// <typeparam name="T">The output class</typeparam>
    public class Identity<T> : IRequest<T>
    {
        /// <summary>
        /// The ID.
        /// </summary>
        public Guid Id
        {
            get;
            set;
        }
    }
}