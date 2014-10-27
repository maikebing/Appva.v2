// <copyright file="UpgradeHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Models.Handlers
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Core.Extensions;
    using Appva.Mcss.AuthorizationServer.Domain.Authentication;
    using Appva.Mcss.AuthorizationServer.Domain.Entities;
    using Appva.Mcss.AuthorizationServer.Models;
    using Appva.Persistence;
    using NHibernate.Criterion;
    using NHibernate.Transform;

    #endregion

    internal sealed class UpgradeGetHandler : PersistentRequestHandler<NoParameter<Upgrade>, Upgrade>
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="UpgradeGetHandler"/> class.
        /// </summary>
        /// <param name="persistenceContext">The <see cref="IPersistenceContext"/></param>
        public UpgradeGetHandler(IPersistenceContext persistenceContext)
            : base(persistenceContext)
        {
        }

        #endregion

        #region Overrides.

        /// <inheritdocs />
        public override Upgrade Handle(NoParameter<Upgrade> message)
        {
            var setting = this.Persistence.QueryOver<Setting>()
                .Where(x => x.Key == "system.update.1")
                .SingleOrDefault();
            var isUpgraded = setting.IsNull() ? false : setting.Value.Equals("True");
            return new Upgrade()
            {
                IsUpgraded = isUpgraded
            };
        }

        #endregion
    }

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class UpgradeHandler : PersistentRequestHandler<Upgrade, Upgrade>
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="UpgradeHandler"/> class.
        /// </summary>
        /// <param name="persistenceContext">The <see cref="IPersistenceContext"/></param>
        public UpgradeHandler(IPersistenceContext persistenceContext)
            : base(persistenceContext)
        {
        }

        #endregion

        #region Overrides.

        /// <inheritdocs />
        public override Upgrade Handle(Upgrade message)
        {
            try
            {
                var setting = this.Persistence.QueryOver<Setting>()
                .Where(x => x.Key == "system.update.1")
                .SingleOrDefault();
                if (setting.IsNotNull())
                {

                }
            }
            catch(Exception e)
            {
                return new Upgrade { IsException = true, ExceptionMessage = e.StackTrace };
            }
            return new Upgrade{ IsUpgraded = true };
        }

        #endregion
    }
}