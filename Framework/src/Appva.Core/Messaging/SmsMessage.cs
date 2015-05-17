// <copyright file="SmsMessage.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Core.Messaging
{
    /// <summary>
    /// Represents an SMS message.
    /// </summary>
    public sealed class SmsMessage
    {
        #region Variables.

        /// <summary>
        /// The receipiant telephone number.
        /// </summary>
        private readonly string to;

        /// <summary>
        /// The SMS text body.
        /// </summary>
        private readonly string body;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="SmsMessage"/> class.
        /// </summary>
        /// <param name="to">The receipiant telephone number</param>
        /// <param name="body">The SMS text body</param>
        public SmsMessage(string to, string body)
        {
            this.to = to;
            this.body = body;
        }

        #endregion

        #region Public Properties.

        /// <summary>
        /// Returns the recipiant telephone number.
        /// </summary>
        public string To
        {
            get
            {
                return this.to;
            }
        }

        /// <summary>
        /// Returns the SMS text body.
        /// </summary>
        public string Body
        {
            get
            {
                return this.body;
            }
        }

        #endregion
    }
}
