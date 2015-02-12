// <copyright file="NotificationTransformer.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a></author>
namespace Appva.Mcss.ResourceServer.Transformers
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Appva.Mcss.Domain.Entities;
    using Appva.Mcss.ResourceServer.Domain.Models;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class NotificationTransformer
    {
        /// <summary>
        /// Convert a task to an alarm-notification
        /// TODO: Complete notification-message
        /// </summary>
        /// <param name="task">The alarm <see cref="Task"/></param>
        /// <returns>The <see cref="AlarmNotificationModel"/></returns>
        public static AlarmNotificationModel ToAlarmNotification(Task task)
        {
            return new AlarmNotificationModel()
            {
                Id = task.Id,
                IsAlarm = task.Delayed && !task.IsCompleted && !task.DelayHandled,
                Message = "Larm: Försenad insats"
            };
        }
    }
}