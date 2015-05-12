// <copyright file="ListSettingsHandler.cs" company="Appva AB">
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
	using Appva.Cqrs;
	using Appva.Mcss.Admin.Application.Services.Settings;
	using Appva.Mcss.Admin.Features.Area51.Cache;
	using Appva.Mcss.Admin.Infrastructure.Models;
	using Appva.Persistence;

	#endregion

	/// <summary>
	/// TODO: Add a descriptive summary to increase readability.
	/// </summary>
	public class ListSettingsHandler : RequestHandler<Parameterless<IEnumerable<ListSetting>>, IEnumerable<ListSetting>>
	{
		#region Variables.

		/// <summary>
		/// The <see cref="ISettingsService"/>.
		/// </summary>
		private readonly ISettingsService settings;

		#endregion

		#region Constructor.

		/// <summary>
		/// Initializes a new instance of the <see cref="ListSettingsHandler"/> class.
		/// </summary>
		/// <param name="settings">The <see cref="ISettingsService"/> implementation</param>
		public ListSettingsHandler(ISettingsService settings)
		{
			this.settings = settings;
		}

		#endregion

		#region RequestHandler<ListAccountCommand,ListAccountModel> Overrides.

		/// <inheritdoc />
		public override IEnumerable<ListSetting> Handle(Parameterless<IEnumerable<ListSetting>> message)
		{
			var retval = new List<ListSetting>();
			var settings = this.settings.List();
			foreach (var setting in settings)
			{
				retval.Add(new ListSetting
					{
						Id = setting.Id,
                        Name = setting.Name,
                        Description = setting.Description,
                        Key = setting.MachineName,
                        Value = setting.Value,
                        Namespace = setting.Namespace
					});
			}
			return retval;
		}

		#endregion
	}
}