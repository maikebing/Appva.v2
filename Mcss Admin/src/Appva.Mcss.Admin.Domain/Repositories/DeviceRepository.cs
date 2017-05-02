// <copyright file="DeviceRepository.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:kalle.jigfors@appva.se">Kalle Jigfors</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Repositories
{
    using Contracts;
    #region Imports.

    using Core.Extensions;
    using Entities;
    using Models;
    using NHibernate.Criterion;
    using NHibernate.Transform;
    using Persistence;
    using Repositories;
    using Repository;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface IDeviceRepository : 
        IRepository, 
        IIdentityRepository<Device>,
        IUpdateRepository<Device>
    {
        /// <summary>
        /// Lists devices by search query
        /// </summary>
        /// <param name="model"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        PageableSet<DeviceModel> Search(SearchDeviceModel model, int page = 1, int pageSize = 10);

        /// <summary>
        /// Get a list of device alerts.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        DeviceAlert GetAlert(Guid id);

        /// <summary>
        /// Get a list of all escalation levels.
        /// </summary>
        /// <returns></returns>
        IList<EscalationLevel> GetEscalationLevels();
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class DeviceRepository : IDeviceRepository
    {
        #region Variables

        /// <summary>
        /// The <see cref="IPersistenceContext"/>
        /// </summary>
        private readonly IPersistenceContext context;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceRepository"/> class.
        /// </summary>
        public DeviceRepository(IPersistenceContext context)
        {
            this.context = context;
        }
        
        #endregion

        #region IDeviceRepository Members.

        /// <inheritdoc />
        public PageableSet<DeviceModel> Search(SearchDeviceModel model, int page = 1, int pageSize = 10)
        {
            Device device = null;
            Taxon organization = null;
            var query = this.context.QueryOver<Device>(() => device);

            if (model.IsCurrentNode)
            {
                query.JoinAlias(x => x.Taxon, () => organization)
                .WhereRestrictionOn(() => organization.Path).IsLike(model.TaxonFilter, MatchMode.Start);
            }

            if (model.IsActive.HasValue)
            {
                query.Where(x => x.IsActive == model.IsActive.Value);
            }
            
            if (model.SearchQuery.IsNotEmpty())
            {
                Expression<Func<Device, object>> expression = x => x.Description;
                query.Where(Restrictions.On<Device>(expression).IsLike(model.SearchQuery, MatchMode.Anywhere));
            }

            DeviceModel dto = null;
            query.Select(Projections.ProjectionList()
                .Add(Projections.Property<Device>(x => x.Id).WithAlias(() => dto.Id))
                .Add(Projections.Property<Device>(x => x.CreatedAt).WithAlias(() => dto.CreatedAt))
                .Add(Projections.Property<Device>(x => x.Description).WithAlias(() => dto.Description))
                .Add(Projections.Property<Device>(x => x.AppBundle).WithAlias(() => dto.AppBundle))
                .Add(Projections.Property<Device>(x => x.AppVersion).WithAlias(() => dto.AppVersion))
                .Add(Projections.Property<Device>(x => x.OS).WithAlias(() => dto.OS))
                .Add(Projections.Property<Device>(x => x.OSVersion).WithAlias(() => dto.OSVersion))
                .Add(Projections.Property<Device>(x => x.Hardware).WithAlias(() => dto.Hardware))
                .Add(Projections.Property<Device>(x => x.IsActive).WithAlias(() => dto.IsActive)));

            if (model.OrderBy != null)
            {
                if (model.IsAscending)
                {
                    query.OrderBy(Projections.Property(model.OrderBy)).Desc.TransformUsing(Transformers.AliasToBean<DeviceModel>());
                }
                else
                {
                    query.OrderBy(Projections.Property(model.OrderBy)).Asc.TransformUsing(Transformers.AliasToBean<DeviceModel>());
                }
            }
            else
            {
                query.OrderByAlias(() => dto.CreatedAt).Desc.TransformUsing(Transformers.AliasToBean<DeviceModel>());
            }

            var start = page < 1 ? 1 : page;
            var first = (start - 1) * pageSize;
            var items = query.Skip(first).Take(pageSize).List<DeviceModel>();
            return new PageableSet<DeviceModel>
            {
                CurrentPage = (long)start,
                NextPage = (long)start++,
                PageSize = (long)pageSize,
                TotalCount = (long)query.RowCount(),
                Entities = items
            };
        }

        /// <inheritdoc />
        public DeviceAlert GetAlert(Guid id)
        {
            return this.context.QueryOver<DeviceAlert>().Where(x => x.Device.Id == id).SingleOrDefault();
        }

        /// <inheritdoc />
        public IList<EscalationLevel> GetEscalationLevels()
        {
            return this.context.QueryOver<EscalationLevel>().List();
        }

        #endregion

        #region IIdentityRepository Members.

        /// <inheritdoc />
        public Device Find(Guid id)
        {
            return this.context.Get<Device>(id);
        }

        #endregion

        #region IUpdateRepository Members.

        /// <inheritdoc />
        public void Update(Device entity)
        {
            entity.UpdatedAt = DateTime.Now;
            entity.Version = entity.Version++;
            this.context.Update<Device>(entity);
        }

        #endregion
    }
}