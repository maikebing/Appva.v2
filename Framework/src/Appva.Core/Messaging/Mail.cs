// <copyright file="Mail.cs" company="Appva AB">
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
    public static class Mail
    {
        /// <summary>
        /// Sends the specified message to an SMTP server for delivery.
        /// </summary>
        /// <param name="message">
        /// A <c>System.Net.Mail.MailMessage</c> that contains the message to send
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// Mssage is null
        /// </exception>
        /// <exception cref="System.InvalidOperationException">
        /// This System.Net.Mail.SmtpClient has a Overload: 
        /// <c>System.Net.Mail.SmtpClient.SendAsync</c> call in progress.-or- 
        /// <c>System.Net.Mail.MailMessage.From</c> is null.-or- There are no recipients 
        /// specified in <c>System.Net.Mail.MailMessage.To</c>, 
        /// <c>System.Net.Mail.MailMessage.CC</c>, and <c>System.Net.Mail.MailMessage.Bcc</c> 
        /// properties.-or- <c>System.Net.Mail.SmtpClient.DeliveryMethod</c> property is set 
        /// to <c>System.Net.Mail.SmtpDeliveryMethod.Network</c> and 
        /// <c>System.Net.Mail.SmtpClient.Host</c> is null.-or-
        /// <c>System.Net.Mail.SmtpClient.DeliveryMethod</c> property is set to 
        /// <c>System.Net.Mail.SmtpDeliveryMethod.Network</c> and 
        /// <c>System.Net.Mail.SmtpClient.Host</c> is equal to the empty string ("").-or- 
        /// <c>System.Net.Mail.SmtpClient.DeliveryMethod</c> property is set to 
        /// <c>System.Net.Mail.SmtpDeliveryMethod.Network</c> and 
        /// <c>System.Net.Mail.SmtpClient.Port</c> is zero, a negative number, or greater 
        /// than 65,535.
        /// </exception>
        /// <exception cref="System.ObjectDisposedException">
        /// This object has been disposed
        /// </exception>
        /// <exception cref="System.Net.Mail.SmtpException">
        /// The connection to the SMTP server failed.-or-Authentication failed.-or-The 
        /// operation timed out.-or- <c>System.Net.Mail.SmtpClient.EnableSsl</c> is set to 
        /// true but the <c>System.Net.Mail.SmtpClient.DeliveryMethod</c> property is set to 
        /// <c>System.Net.Mail.SmtpDeliveryMethod.SpecifiedPickupDirectory</c> or 
        /// <c>System.Net.Mail.SmtpDeliveryMethod.PickupDirectoryFromIis</c>.-or-
        /// <c>System.Net.Mail.SmtpClient.EnableSsl</c> is set to true, but the SMTP mail 
        /// server did not advertise STARTTLS in the response to the EHLO command.
        /// </exception>
        /// <exception cref="System.Net.Mail.SmtpFailedRecipientsException">
        /// The message could not be delivered to one or more of the recipients in 
        /// <c>System.Net.Mail.MailMessage.To,</c> <c>System.Net.Mail.MailMessage.CC</c>, or 
        /// <c>System.Net.Mail.MailMessage.Bcc</c>.
        /// </exception>
        public static void Send(MailMessage message)
        {
            using (var client = new SmtpClient())
            {
                client.Send(message);
            }
        }

        /// <summary>
        /// Sends the specified message to an SMTP server for delivery as an asynchronous 
        /// operation.
        /// </summary>
        /// <param name="message">
        /// A <c>System.Net.Mail.MailMessage</c> that contains the message to send
        /// </param>
        /// <returns>
        /// Returns <c>System.Threading.Tasks.Task</c>. The task object representing the 
        /// asynchronous operation
        /// </returns>
        /// <exception cref="System.ArgumentNullException">Message is null</exception>
        public static async Task SendAsync(MailMessage message)
        {
            using (var client = new SmtpClient())
            {
                await client.SendMailAsync(message).ConfigureAwait(false);
            }
        }
    }
}