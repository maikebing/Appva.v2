// <copyright file="IMailSender.cs" company="Appva AB">
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

    #endregion

    /// <summary>
    /// Allows applications to send e-mail by using the Simple Mail Transfer Protocol 
    /// (SMTP).
    /// </summary>
    /// <typeparam name="T">The message type</typeparam>
    public interface IMailSender<T> where T : MailMessage
    {
        /// <summary>
        /// Sends the specified message to an SMTP server for delivery.
        /// </summary>
        /// <param name="message">A {T} that contains the message to send</param>
        void Send(T message);

        /// <summary>
        /// Sends the specified message to an SMTP server for delivery as an asynchronous 
        /// operation.
        /// </summary>
        /// <param name="message">A {T} that contains the message to send</param>
        /// <returns>
        /// Returns <c>System.Threading.Tasks.Task</c>. The task object representing the 
        /// asynchronous operation
        /// </returns>
        Task SendAsync(T message);
    }
}