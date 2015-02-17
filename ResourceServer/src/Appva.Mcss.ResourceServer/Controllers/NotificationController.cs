// <copyright file="NotificationController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.ResourceServer.Controllers
{
    #region Imports.

    using System;
    using System.Web.Http;
    using Appva.Logging;
    using Appva.Mcss.ResourceServer.Application;
    using Appva.Mcss.ResourceServer.Application.Authorization;
    using Appva.Mcss.ResourceServer.Domain.Repositories;
    using Appva.Mcss.ResourceServer.Transformers;

    #endregion

    /// <summary>
    /// Notification endpoint.
    /// </summary>
    [RoutePrefix("v1/notification")]
    public class NotificationController : ApiController
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ILog"/> for <see cref="NotificationController"/>.
        /// </summary>
        private static readonly ILog Log = LogProvider.For<NotificationController>();

        /// <summary>
        /// The <see cref="ITaskRepository"/>.
        /// </summary>
        private readonly ITaskRepository taskRepository;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationController"/> class.
        /// </summary>
        /// <param name="taskRepository">The <see cref="ITaskRepository"/></param>
        public NotificationController(ITaskRepository taskRepository)
        {
            this.taskRepository = taskRepository;
        }

        #endregion

        #region Routes.

        /// <summary>
        /// Endpoint for devices to check if an alarm is still active. Returns the notification-message.
        /// </summary>
        /// <param name="id">The Alarm id</param>
        /// <returns>The <see cref="AlarmNotificationModel"/></returns>
        [AuthorizeToken(Scope.ReadWrite)]
        [HttpGet, Route("alarm/{id}")]
        public IHttpActionResult CheckAlarm(Guid id)
        {
            var alarm = this.taskRepository.Get(id);

            return this.Ok(NotificationTransformer.ToAlarmNotification(alarm));
        }

        #endregion
    }
}