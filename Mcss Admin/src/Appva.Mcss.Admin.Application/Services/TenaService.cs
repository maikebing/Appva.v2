// <copyright file="TenaService.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>


namespace Appva.Mcss.Admin.Application.Services
{
    #region Imports
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Appva.Core.Extensions;
    using Appva.Mcss.Admin.Application.Auditing;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Domain.Models;
    using Appva.Mcss.Admin.Domain.Repositories;
    using Appva.Mcss.Admin.Application.Security.Identity;
    using Appva.Repository;
    using Appva.Tenant.Identity;
    using Microsoft.Owin;
    using Appva.Persistence;
    #endregion

    public interface ITenaService : IService
    {

    }

    /// <summary>
    /// The <see cref="TenaService"/> service.
    /// </summary>
    public sealed class TenaService : ITenaService
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ITenaRepository"/>
        /// </summary>
        private readonly ITenaRepository repository;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="TenaService"/> class.
        /// </summary>
        public TenaService(ITenaRepository repository)
        {
            this.repository = repository;
        }

        #endregion

        #region ITenaService members
        
        #endregion
    }
}
