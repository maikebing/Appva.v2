// <copyright file="ListAccountHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Features.Accounts.List
{
	#region Imports.

	using System;
	using System.Collections.Generic;
	using System.Web.Mvc;
	using Appva.Caching.Providers;
	using Appva.Cqrs;
	using Appva.Mcss.Admin.Application.Services.Settings;
	using Appva.Mcss.Admin.Domain.Entities;
	using Appva.Persistence;
	using NHibernate.Criterion;
	using NHibernate.Transform;
	using NHibernate.Type;

	#endregion

	/// <summary>
	/// TODO: Add a descriptive summary to increase readability.
	/// </summary>
	public class ListAccountHandler : IRequestHandler<ListAccountCommand, ListAccountModel>
	{
		#region Variables.

		/// <summary>
		/// The <see cref="ICacheService"/>.
		/// </summary>
		private readonly IRuntimeMemoryCache cache;

		/// <summary>
		/// The <see cref="ISettingsService"/>.
		/// </summary>
		private readonly ISettingsService settings;

		/// <summary>
		/// The <see cref="IPersistenceContext"/>.
		/// </summary>
		private readonly IPersistenceContext persistenceContext;

		#endregion

		#region Constructor.

		/// <summary>
		/// Initializes a new instance of the <see cref="ListAccountHandler"/> class.
		/// </summary>
		public ListAccountHandler(IRuntimeMemoryCache cache, ISettingsService settings, IPersistenceContext persistenceContext)
		{
			this.cache = cache;
			this.settings = settings;
			this.persistenceContext = persistenceContext;
		}

		#endregion

		#region IRequestHandler<ListAccountCommand,ListAccountModel> Members.

		/// <inheritdoc />
		public ListAccountModel Handle(ListAccountCommand message)
		{
			//this.settings.Find<bool>(SettingKey.AutogeneratePasswordForMobileDevice, false);
            this.settings.Find<bool>(ApplicationSettings.IsAccessControlInPreviewMode, false);
            this.settings.Find<bool>(ApplicationSettings.IsAccessControlInstalled, false);
			var query = this.persistenceContext.QueryOver<Appva.Mcss.Admin.Domain.Entities.Account>();
			var accounts = query.List();
			if (message.RoleFilterId.HasValue)
			{
			   // Role role = null;
			   // query.Left.JoinAlias(x => x.Roles, () => role)
			   //     .Where(() => role.Id == message.RoleFilterId);
			}

			/*query.Select(Projections.ProjectionList()
				.Add(Projections.Property<Account>(x => x.Id).WithAlias(() => tenants.Id))
				.Add(Projections.Property<Tenant>(x => x.Slug.Name).WithAlias(() => tenants.Slug))
				.Add(Projections.Property<Tenant>(x => x.Name).WithAlias(() => tenants.Name))
				.Add(Projections.Property<Tenant>(x => x.Description).WithAlias(() => tenants.Description))
				.Add(Projections.Property<Tenant>(x => x.Image.FileName).As("Logotype.FileName"))
				.Add(Projections.Property<Tenant>(x => x.Image.MimeType).As("Logotype.MimeType"))
			);*/

			
			var tests = this.persistenceContext.Session.CreateQuery(
				@"SELECT 
					account.Id AS Id, 
					min(delegation.EndDate) AS EndDate 
				FROM Account account 
					LEFT OUTER JOIN account.Delegations delegation 
				
				GROUP BY account.Id
				ORDER BY (CASE WHEN MIN(delegation.EndDate) IS NOT NULL THEN MIN(delegation.EndDate) ELSE '2099-01-01' END) asc")
				.SetResultTransformer(Transformers.AliasToBean<TestBoll>())
				.Future<TestBoll>();
			/*WHERE account.FullName LIKE '%Johan%'*/

			/*
			 * Role role = null;
			Delegation delegation = null;
			TestBoll boll = null;
			 * var tests = this.persistenceContext.QueryOver<Account>()
				.Left.JoinAlias(x => x.Delegations, () => delegation)
				.Select(Projections.ProjectionList()
					.Add(Projections.GroupProperty(Projections.Property<Account>(x => x.Id)).WithAlias(() => boll.Id))
					//.Add(Projections.Min<DateTime>(x => delegation.EndDate).WithAlias(() => boll.EndDate))
					.Add(Projections.Conditional(
						Restrictions.Where(() => delegation.EndDate == null),
						Projections.Min<DateTime>(x => delegation.EndDate),
						Projections.Constant(DateTime.Now.AddYears(100), new DateTimeType())
					).WithAlias(() => boll.EndDate))
				)
				
				//.OrderByAlias(() => delegation.EndDate).Asc
				//.OrderBy(Projections.Min<DateTime>(x => delegation.EndDate)).Asc

				/*.OrderBy(Projections.Conditional(
					Restrictions.Where(() => delegation.EndDate == null),
					Projections.Min<DateTime>(x => delegation.EndDate),
					Projections.Constant(DateTime.Now.AddYears(100), new DateTimeType()))).Asc*/

			/*.TransformUsing(Transformers.AliasToBean<TestBoll>())
			.Future<TestBoll>();

			/*.SelectList(list => list
				.Select(x => delegation.EndDate).WithAlias(() => boll.EndDate)
				.Select(x => customer.Name).WithAlias(() => orderHeader.Name)
				.Select(o => employee.FirstName).WithAlias(() => orderHeader.FirstName)
				.Select(o => employee.LastName).WithAlias(() => orderHeader.LastName))
			.TransformUsing(Transformers.AliasToBean<OrderHeader>())
			.Future<OrderHeader>();*/

			return new ListAccountModel
			{
				Accounts = accounts,
				Roles = new List<SelectListItem> 
					{ 
						new SelectListItem
						{
							Text = "Roll 1", Value = "Test"
						}
					},
				Delegations = new List<SelectListItem>
					{ 
						new SelectListItem
						{
							Text = "Roll 1", Value = "Test"
						}
					},
				RoleFilterId = message.RoleFilterId,
				DelegationFilterId = message.DelegationFilterId,
				IsFilterByCreatedByEnabled = message.IsFilterByCreatedByEnabled,
				IsFilterByIsActiveEnabled = message.IsFilterByIsActiveEnabled,
				IsFilterByIsPausedEnabled = message.IsFilterByIsPausedEnabled,
				Tests = tests
			};
		}

		#endregion

		#region IRequestHandler Members.

		/// <inheritdoc />
		public object Handle(object message)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}