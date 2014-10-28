// <copyright file="DetailsUserTenantsHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Models.Handlers
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Appva.Mcss.AuthorizationServer.Domain.Entities;
    using Appva.Mcss.AuthorizationServer.Models.Handlers;
    using Appva.Persistence;
    using NHibernate.Criterion;
    using NHibernate.Transform;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    internal class DetailsUserTenantsHandler : PersistentRequestHandler<DetailsUserTenantsId, IList<DetailsUserTenants>>
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="DetailsUserTenantsHandler"/> class.
        /// </summary>
        /// <param name="persistenceContext">The <see cref="IPersistenceContext"/></param>
        public DetailsUserTenantsHandler(IPersistenceContext persistenceContext) 
            : base(persistenceContext)
        {
        }

        #endregion

        #region Overrides.

        /// <inheritdocs />
        public override IList<DetailsUserTenants> Handle(DetailsUserTenantsId message)
        {
            var user = this.Persistence.Get<User>(message.Id);
            var retval = user.Tenants.Select(x => new DetailsUserTenants { Name = x.Name, ToolTip = x.Description }).ToList();
            return retval;
        }

        #endregion
    }
}