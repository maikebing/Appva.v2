// <copyright file="ILogService.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Services
{
    #region Imports.

    using System;
    using System.Collections.Generic;
using Appva.Mcss.Admin.Domain.Entities;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface ILogService : IService
    {
        void Info(string message, Account account, Patient patient);
        void Info(string message, Account account, Patient patient, LogType type);
        void Info(string message, Account account, LogType type);
    }

    public class LogService : ILogService
    {
        #region ILogService Members

        public void Info(string message, Account account, Patient patient, LogType type)
        {
            return;
        }

        public void Info(string message, Account account, LogType type)
        {
            return;
        }

        public void Info(string message, Account account, Patient patient)
        {
            return;
        }

        #endregion
    }
}