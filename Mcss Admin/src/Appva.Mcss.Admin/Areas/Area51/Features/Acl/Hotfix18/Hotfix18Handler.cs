// <copyright file="Hotfix18Handler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Model.Handlers
{
    #region Imports.

    using Appva.Core.Resources;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Models;
    using Appva.Persistence;
    using NHibernate.Criterion;
    using NHibernate.Transform;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class Hotfix18Handler : NotificationHandler<Hotfix18>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IRoleService"/>.
        /// </summary>
        private readonly IAccountService accountService;

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext persistence;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Hotfix18Handler"/> class.
        /// </summary>
        /// <param name="accountService">The <see cref="IAccountService"/></param>
        /// <param name="persistence">The <see cref="IPersistenceContext"/></param>
        public Hotfix18Handler(IAccountService accountService, IPersistenceContext persistence)
        {
            this.accountService = accountService;
            this.persistence = persistence;
        }

        #endregion

        #region NotificationHandler Overrides.

        /// <inheritdoc />
        public override void Handle(Hotfix18 notification)
        {
            //// Retrieve all accounts which has not either backend or device role -
            //// since these are the once that actually need to be updated - EVEN - inactive accounts.
            //// Question is with inactive, if we actually can update them since that would change
            //// their updatedAt date which means we postpone archiving. 
            var u = this.persistence.QueryOver<Account>().List();
            var accountsWithoutBaOrDa = this.persistence.QueryOver<Account>()
                .TransformUsing(Transformers.DistinctRootEntity)
                .JoinQueryOver<Role>(x => x.Roles)
                .Where(x => x.IsActive)
                .List();
            var deviceRole  = this.persistence.QueryOver<Role>().Where(x => x.MachineName == RoleTypes.Device).Take(1).SingleOrDefault();
            var backendRole = this.persistence.QueryOver<Role>().Where(x => x.MachineName == RoleTypes.Backend).Take(1).SingleOrDefault();
            var filtered = accountsWithoutBaOrDa.Where(x => (! x.Roles.Contains(deviceRole)) && (! x.Roles.Contains(backendRole))).ToList();
            foreach (var account in filtered)
            {
                bool isAccountUpgradedForAdminAccess;
                bool isAccountUpgradedForDeviceAccess;
                this.accountService.UpdateRoles(account, account.Roles, out isAccountUpgradedForAdminAccess, out isAccountUpgradedForDeviceAccess);
            }
            return;
        }

        #endregion
    }
}