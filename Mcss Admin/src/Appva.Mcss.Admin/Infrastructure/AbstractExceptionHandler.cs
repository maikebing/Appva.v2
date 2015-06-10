// <copyright file="AbstractExceptionHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Infrastructure
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Appva.Core.Exceptions;
    using Appva.Mvc.Messaging;
    using Appva.Mcss.Admin.Configuration;
    using Appva.Core.Logging;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public abstract class AbstractExceptionHandler : IExceptionHandler
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ILog"/>.
        /// </summary>
        private static readonly ILog Log = LogProvider.For<AbstractExceptionHandler>();

        /// <summary>
        /// The <see cref="IRazorMailService"/>.
        /// </summary>
        private readonly IRazorMailService mail;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractExceptionHandler"/> class.
        /// </summary>
        /// <param name="mail">The <see cref="IRazorMailService"/></param>
        protected AbstractExceptionHandler(IRazorMailService mail)
        {
            this.mail = mail;
        }

        #endregion

        #region IExceptionHandler Members.

        public void Handle(Exception exception)
        {
            try
            {
                if (exception == null)
                {
                    return;
                }
                this.HandleException(exception);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
        }

        public void Handle(AggregateException exception)
        {
            try
            {
                if (exception == null)
                {
                    return;
                }
                this.HandleException(((AggregateException)exception).Flatten());
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="model"></param>
        protected void SendMail(string title, ExceptionMail model)
        {
            Log.DebugJson(model);
            var subject = string.Format("MCSS Admin Exception {0} - {1} - {2} - {3} - {4:yyyy-MM-dd HH:mm:ss}", title, Application.Environment, Application.Version, Application.MachineName, DateTime.Now);
            this.mail.Send(MailMessage.CreateNew().Template("ExceptionEmail").Model(model).To("johansalllarsson@appva.se,richard.henriksson@appva.se").Subject(subject).Build());
        }

        #region Abstract.

        protected abstract void HandleException(Exception exception);

        #endregion
    }
}