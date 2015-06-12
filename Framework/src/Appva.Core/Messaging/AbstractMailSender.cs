// <copyright file="AbstractMailSender.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Core.Messaging
{
    #region Imports.

    using System;
    using System.Net.Mail;
    using System.Threading.Tasks;
    using Appva.Core.Logging;
    using Appva.Core.Resources;

    #endregion

    /// <summary>
    /// The abstract base <see cref="IMailSender{T}"/> implementation.
    /// </summary>
    /// <typeparam name="T">The message type</typeparam>
    public abstract class AbstractMailSender<T> : IMailSender<T> where T : MailMessage
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ILog"/>.
        /// </summary>
        private static readonly ILog Logger = LogProvider.For<AbstractMailSender<T>>();

        #endregion

        #region IMailSender<T> Members.

        /// <inheritdoc />
        public void Send(T message)
        {
            try
            {
                Mail.Send(this.Handle(message));
            }
            catch (Exception ex)
            {
                this.HandleException(message, ex);
            }
        }

        /// <inheritdoc />
        public async Task SendAsync(T message)
        {
            try
            {
                await Mail.SendAsync(this.Handle(message));
            }
            catch (Exception ex)
            {
                this.HandleException(message, ex);
            }
        }

        #endregion

        #region Protected Methods.

        /// <summary>
        /// Handles or processes the {T} message before sending.
        /// </summary>
        /// <param name="message">A {T} that contains the message to send</param>
        /// <returns>An instance of {T}</returns>
        protected abstract T Handle(T message);

        /// <summary>
        /// Handles E-mail exceptions.
        /// </summary>
        /// <param name="message">
        /// A {T} that contains the message to send
        /// </param>
        /// <param name="exception">
        /// The <c>System.Exception</c> that was raised
        /// </param>
        protected virtual void HandleException(T message, Exception exception)
        {
            if (exception is SmtpException || exception is SmtpFailedRecipientsException)
            {
                //// TODO: Log specifics.
            }
            Logger.Error(exception, ExceptionWhen.SendingEmail, message.To.ToString(), message.Subject, message.Body);
        }

        #endregion
    }
}