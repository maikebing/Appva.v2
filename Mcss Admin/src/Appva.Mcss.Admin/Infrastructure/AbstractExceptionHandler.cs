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
    using Appva.Core.Environment;
    using Appva.Core.Exceptions;
    using Appva.Core.Logging;
    using Appva.Mcss.Admin.Configuration;
    using Appva.Core.Messaging.RazorMail;
    using Appva.Mcss.Admin.Application.Utils.i18n;

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

        /// <summary>
        /// The <see cref="IApplicationEnvironment"/>.
        /// </summary>
        private readonly IApplicationEnvironment environment;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractExceptionHandler"/> class.
        /// </summary>
        /// <param name="environment">The <see cref="IApplicationEnvironment"/></param>
        /// <param name="mail">The <see cref="IRazorMailService"/></param>
        protected AbstractExceptionHandler(IApplicationEnvironment environment, IRazorMailService mail)
        {
            this.environment = environment;
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
                this.HandleException(((AggregateException) exception).Flatten());
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
            var subject = string.Format("MCSS Admin Exception {0} - {1} - {2} - {3} - {4:yyyy-MM-dd HH:mm:ss}", title, this.environment.Environment.AsUserFriendlyText(), this.environment.Info.Version, this.environment.OperatingSystem.MachineName, DateTime.Now);
            this.mail.Send(MailMessage.CreateNew().Template(I18nUtils.GetEmailTemplatePath("ExceptionEmail")).Model(model).To("it@appva.se").Subject(subject).Build());
        }

        #region Abstract.

        protected abstract void HandleException(Exception exception);

        #endregion
    }
}