// <copyright file="UploadTenaObserverPeriodHandlerAsync.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>
namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using System;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using System.Threading.Tasks;

    #endregion

    internal sealed class UploadTenaObserverPeriodHandlerAsync
    {
        #region Fields.


        /// <summary>
        /// The <see cref="ITenaService"/>.
        /// </summary>
        private readonly ITenaService tenaService;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="UploadTenaObserverPeriodHandlerAsync"/> class.
        /// </summary>
        /// <param name="tenaService">The <see cref="ITenaService"/>.</param>
        public UploadTenaObserverPeriodHandlerAsync(ITenaService tenaService)
        {
            this.tenaService = tenaService;
        }

        public async Task<string> Handle()
        {
            return null;
        }

        #endregion
    }
}