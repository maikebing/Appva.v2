// <copyright file="SmsService.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Core.Messaging
{
    #region Imports.

    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Net;
    using System.Threading.Tasks;
    using Appva.Logging;
    using Extensions;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class SmsService : IMessageService
    {
        #region Variables.

        /// <summary>
        /// The XML message.
        /// </summary>
        private const string XmlFormat = "<?xml version=\"1.0\" encoding=\"utf-8\" ?><smsteknik><copyright>Copyright (c) 2003-2006 SMS-Teknik AB</copyright><operationtype>0</operationtype><flash>0</flash><multisms>0</multisms><maxmultisms>1</maxmultisms><compresstext>0</compresstext><udmessage>{0}</udmessage><smssender>{1}</smssender><deliverystatustype>0</deliverystatustype><deliverystatusaddress></deliverystatusaddress><usee164>0</usee164><items>{2}</items></smsteknik>";

        /// <summary>
        /// The URL format.
        /// </summary>
        private const string UriFormat = "https://www.smsteknik.se/Member/SMSConnectDirect/SendSMSv3.asp?id={0}&user={1}&pass={2}";

        /// <summary>
        /// Logging for <see cref="EmailMessage"/>.
        /// </summary>
        private static readonly ILog Logger = LogProvider.For<SmsService>();

        /// <summary>
        /// The SMS Teknik endpoint URL.
        /// </summary>
        private readonly Uri uri;

        /// <summary>
        /// The SMS sender.
        /// </summary>
        private readonly string sender;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="SmsService"/> class.
        /// </summary>
        /// <param name="id">The SMS Teknik id</param>
        /// <param name="user">The SMS Teknik username</param>
        /// <param name="password">The SMS Teknik password</param>
        /// <param name="sender">The SMS sender (reply-to)</param>
        public SmsService(string id, string user, string password, string sender)
        {
            this.uri = new Uri(UriFormat.FormatWith(id, user, password));
            this.sender = sender;
        }

        #endregion

        #region IMessageService Members.

        /// <inheritdoc />
        public void Send(IMessage message)
        {
            using (var memoryStream = new MemoryStream())
            {
                var messageBytes = this.BuildMessage(message);
                var request = (HttpWebRequest) WebRequest.Create(this.uri);
                request.ContentType = "application/x-www-form-urlencoded";
                request.Method = "POST";
                using (var requestStream = request.GetRequestStream())
                {
                    requestStream.Write(messageBytes, 0, messageBytes.Length);
                }
                //// TODO: Verify headers.
                using (var response = request.GetResponse())
                using (var responseStream = response.GetResponseStream())
                {
                    Debug.Assert(responseStream != null, "responseStream != null");
                    responseStream.CopyTo(memoryStream);
                }
            }
        }

        /// <inheritdoc />
        public async Task SendAsync(IMessage message)
        {
            var memoryStream = new MemoryStream();
            try
            {
                //// Change to HttpClient!
                var data = this.BuildMessage(message);
                var webRequest = (HttpWebRequest) WebRequest.Create(this.uri);
                webRequest.ContentType = "application/x-www-form-urlencoded";
                webRequest.Method = "POST";
                using (var request = await webRequest.GetRequestStreamAsync())
                {
                    await request.WriteAsync(data, 0, data.Length);
                }
                //// TODO: Verify headers.
                using (var response = await webRequest.GetResponseAsync())
                using (var responseStream = response.GetResponseStream())
                {
                    Debug.Assert(responseStream != null, "responseStream != null");
                    await responseStream.CopyToAsync(memoryStream);
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorException("An exception occured in <SmsService>", ex);
            }
            finally
            {
                memoryStream.Dispose();
            }
        }

        #endregion

        #region Private Functions.

        /// <summary>
        /// Builds the message as byte array.
        /// </summary>
        /// <param name="message">The message</param>
        /// <returns>UTF-8 byte array</returns>
        private byte[] BuildMessage(IMessage message)
        {
            var smsMessage = message as SmsMessage;
            Debug.Assert(smsMessage != null, "smsMessage != null");
            var xmlMessage = XmlFormat.FormatWith(smsMessage.Body, this.sender, "<recipient><orgaddress>{0}</orgaddress></recipient>".FormatWith(smsMessage.To));
            return xmlMessage.ToUtf8Bytes();
        }

        #endregion
    }
}