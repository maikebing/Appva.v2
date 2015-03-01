// <copyright file="IMessageService.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Core.Messaging
{
    #region Imports.

    using System.Threading.Tasks;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public interface IMessageService
    {
        /// <summary>
        /// Sends a message.
        /// </summary>
        /// <param name="message">The message</param>
        void Send(IMessage message);

        /// <summary>
        /// Sends a message asyncronous.
        /// </summary>
        /// <param name="message">The message</param>
        /// <returns>The <see cref="Task"/></returns>
        Task SendAsync(IMessage message);
    }
}
