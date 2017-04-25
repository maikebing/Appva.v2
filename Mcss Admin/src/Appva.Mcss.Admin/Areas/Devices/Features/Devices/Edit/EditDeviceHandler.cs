// <copyright file="EditDeviceHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:ziemanncarl@gmail.com">Carl Ziemann</a>
// </author>
// <author>
//     <a href="mailto:h4nsson@gmail.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Areas.Devices.Features.Devices.Edit
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Models;
    using Appva.Persistence;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class EditDeviceHandler : RequestHandler<Identity<EditDeviceModel>, EditDeviceModel>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IPersistenceContext"/>
        /// </summary>
        private readonly IPersistenceContext persistenceContext;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="EditDeviceHandler"/> class.
        /// </summary>
        public EditDeviceHandler(IPersistenceContext persistenceContext)
        {
            this.persistenceContext = persistenceContext;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override EditDeviceModel Handle(Identity<EditDeviceModel> message)
        {
            var query = this.persistenceContext.QueryOver<Device>()
                .Where(x => x.Id == message.Id)
                    .SingleOrDefault();

            return new EditDeviceModel
            {
                Description = query.Description
            };
        }

        #endregion
    }
}