// <copyright file="ListCacheHandler.cs" company="Appva AB">
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
	using Appva.Caching.Providers;
	using Appva.Cqrs;
	using Appva.Mcss.Admin.Application.Services.Settings;
	using Appva.Mcss.Admin.Features.Area51.Cache;
	using Appva.Mcss.Admin.Infrastructure.Models;
	using Appva.Persistence;

	#endregion

	/// <summary>
	/// TODO: Add a descriptive summary to increase readability.
	/// </summary>
	public class ListCacheHandler : RequestHandler<Parameterless<ListCache>, ListCache>
	{
		#region Variables.

		/// <summary>
		/// The <see cref="IRuntimeMemoryCache"/>.
		/// </summary>
		private readonly IRuntimeMemoryCache cache;

		#endregion

		#region Constructor.

		/// <summary>
		/// Initializes a new instance of the <see cref="ListCacheHandler"/> class.
		/// </summary>
		/// <param name="cache">The <see cref="IRuntimeMemoryCache"/> implementation</param>
		public ListCacheHandler(IRuntimeMemoryCache cache)
		{
			this.cache = cache;
		}

		#endregion

		#region RequestHandler<ListAccountCommand,ListAccountModel> Overrides.

		/// <inheritdoc />
		public override ListCache Handle(Parameterless<ListCache> message)
		{
			return new ListCache
			{
				Items = this.cache.List()
			};
		}

		#endregion
	}
}