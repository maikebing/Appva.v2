using System;
using System.Collections.Generic;
using System.Linq;
using Appva.Mcss.Web.ViewModels;
using Appva.Mcss.Admin.Domain.Entities;
using Appva.Core.Extensions;
using NHibernate.Criterion;
using NHibernate.Transform;
using Appva.Cqrs;
using Appva.Persistence;
using Appva.Mcss.Admin.Application.Services.Settings;
using Appva.Mcss.Admin.Models;

namespace Appva.Mcss.Web.Controllers
{
    public class CreateOverviewCommand : IRequest<HomeViewModel>
    {
        public Account Identity { get; set; }

        public Taxon Taxon { get; set; }

    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class CreateOverViewhandler : RequestHandler<CreateOverviewCommand, HomeViewModel>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IPersistenceContext"/> dispatcher.
        /// </summary>
        private readonly IPersistenceContext persistence;

        /// <summary>
        /// The <see cref="ISettingsService"/>.
        /// </summary>
        private readonly ISettingsService settingsService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateChartCommandHandler"/> class.
        /// </summary>
        public CreateOverViewhandler(ISettingsService settingsService, IPersistenceContext persistence)
        {
            this.persistence = persistence;
            this.settingsService = settingsService;
        }

        #endregion

        #region RequestHandler<AccountQuickSearch, IEnumerable<object>> Members.

        /// <inheritdoc /> 
        public override HomeViewModel Handle(CreateOverviewCommand message)
        {
            var taxon = FilterCache.Get(this.persistence);
            if (!FilterCache.HasCache())
            {
                taxon = FilterCache.GetOrSet(message.Identity, this.persistence);
            }
            return new HomeViewModel
            {
                HasCalendarOverview = this.settingsService.HasCalendarOverview(),
                HasOrderOverview = this.settingsService.HasOrderRefill()
            };
        }

        #endregion
    }
}