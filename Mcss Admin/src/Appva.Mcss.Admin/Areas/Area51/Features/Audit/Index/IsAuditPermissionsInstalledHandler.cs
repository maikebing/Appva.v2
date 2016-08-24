// <copyright file="IsAuditPermissionsInstalledHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Area51.Audit
{
	#region Imports.

    using System.Linq;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using Appva.Persistence;

	#endregion

	/// <summary>
	/// TODO: Add a descriptive summary to increase readability.
	/// </summary>
    public class IsAuditPermissionsInstalledHandler : RequestHandler<Parameterless<IsAuditPermissionsInstalled>, IsAuditPermissionsInstalled>
	{
		#region Variables.

		/// <summary>
        /// The <see cref="IPersistenceContext"/>.
		/// </summary>
        private readonly IPersistenceContext context;

		#endregion

		#region Constructor.

		/// <summary>
        /// Initializes a new instance of the <see cref="IsAuditPermissionsInstalledHandler"/> class.
		/// </summary>
        /// <param name="context">The <see cref="IPersistenceContext"/></param>
        public IsAuditPermissionsInstalledHandler(IPersistenceContext context)
		{
            this.context = context;
		}

		#endregion

		#region RequestHandler Overrides.

		/// <inheritdoc />
        public override IsAuditPermissionsInstalled Handle(Parameterless<IsAuditPermissionsInstalled> message)
		{
            var permissions = this.context.QueryOver<Permission>().List();
            if (
                permissions.Where(x => x.Resource.Equals(Permissions.Reports.SignInValue)).Any() &&
                permissions.Where(x => x.Resource.Equals(Permissions.Reports.AuditReportNotificationValue)).Any() &&
                permissions.Where(x => x.Resource.Equals(Permissions.SecurityConfidentiality.CreateAllDemographicInformationSensitivityValue)).Any() &&
                permissions.Where(x => x.Resource.Equals(Permissions.SecurityConfidentiality.UpdateAllDemographicInformationSensitivityValue)).Any() &&
                permissions.Where(x => x.Resource.Equals(Permissions.SecurityConfidentiality.CreatePeopleofPublicInterestValue)).Any() &&
                permissions.Where(x => x.Resource.Equals(Permissions.SecurityConfidentiality.UpdatePeopleofPublicInterestValue)).Any())
            {
                return new IsAuditPermissionsInstalled { IsInstalled = true };
            }
            return new IsAuditPermissionsInstalled();
		}

		#endregion
	}
}