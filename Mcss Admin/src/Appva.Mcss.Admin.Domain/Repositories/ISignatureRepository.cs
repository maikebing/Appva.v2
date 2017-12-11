// <copyright file="ISignatureRepository.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Repositories
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Persistence;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface ISignatureRepository : IRepository<Signature>, ISaveRepository<Signature>, IUpdateRepository<Signature>
    {
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class SignatureRepository : Repository<Signature>, ISignatureRepository
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="SignatureRepository"/> class.
        /// </summary>
        /// <param name="context">The <see cref="IPersistenceContext"/>.</param>
        public SignatureRepository(IPersistenceContext context)
            : base(context)
        {
        }

        #endregion
    }
}