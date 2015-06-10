// <copyright file="AbstractNoOpMailSender.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Core.Messaging
{
    #region Imports.

    using System.Net.Mail;
    using System.Threading.Tasks;
    using Logging;

    #endregion

    /// <summary>
    /// The abstract base <see cref="IMailSender{T}"/> no op implementation.
    /// </summary>
    /// <typeparam name="T">The message type</typeparam>
    public abstract class AbstractNoOpMailSender<T> : IMailSender<T> where T : MailMessage
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ILog"/>.
        /// </summary>
        private static readonly ILog Logger = LogProvider.For<AbstractNoOpMailSender<T>>();

        #endregion

        #region IMailSender<T> Members.

        /// <inheritdoc />
        void IMailSender<T>.Send(T message)
        {
            #if DEBUG
            Logger.DebugJson(this.Handle(message));
            #endif
            return;
        }

        /// <inheritdoc />
        Task IMailSender<T>.SendAsync(T message)
        {
            #if DEBUG
            Logger.DebugJson(this.Handle(message));
            #endif
            return Task.FromResult(0);
        }

        #endregion

        #region Protected Methods.

        /// <summary>
        /// Handles or processes the {T} message before sending.
        /// </summary>
        /// <param name="message">A {T} that contains the message to send</param>
        /// <returns>An instance of {T}</returns>
        protected abstract T Handle(T message);

        #endregion
    }
}