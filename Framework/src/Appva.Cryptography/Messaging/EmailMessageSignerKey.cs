// <copyright file="EmailMessageSignerKey.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
// ReSharper disable All
namespace Appva.Cryptography.Messaging
{
    #region Imports.

    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Net.Mail;
    using System.Security.Cryptography.Pkcs;
    using System.Security.Cryptography.X509Certificates;
    using Core.Extensions;
    using Core.Messaging;
    using Validation;    

    #endregion

    /// <summary>
    /// FIXME: Create class and new service
    /// https://support.google.com/mail/answer/180707?hl=en
    /// S/MIME (Secure/Multipurpose Internet Mail Extensions) is a standard for public 
    /// key encryption and signing of MIME data. S/MIME is on an IETF standards track 
    /// and defined in a number of documents, most importantly RFCs 3369, 3370, 3850 and 
    /// 3851.
    /// <externalLink>
    ///     <linkText>S/MIME</linkText>
    ///     <linkUri>
    ///         http://en.wikipedia.org/wiki/S/MIME
    ///     </linkUri>
    /// </externalLink>
    /// </summary>
    public interface IEmailMessageSignerKey
    {
        /// <summary>
        /// Signs and encrypts the E-mail message.
        /// </summary>
        /// <param name="message">The message to be signed</param>
        void SignAndEncrypt(MailMessage message);
    }

    /// <summary>
    /// Implementation of <see cref="IEmailMessageSignerKey"/>.
    /// </summary>
    public sealed class EmailMessageSignerKey
    {
        #region Variables.

        /// <summary>
        /// The <see cref="X509Certificate2"/> used for encryption and signing the E-mail 
        /// message.
        /// </summary>
        private readonly X509Certificate2 x509Certificate;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailMessageSignerKey"/> class.
        /// </summary>
        /// <param name="x509Certificate">The signing certificate</param>
        public EmailMessageSignerKey(X509Certificate2 x509Certificate)
        {
            Requires.NotNull(x509Certificate, "x509Certificate");
            this.x509Certificate = x509Certificate;
        }

        #endregion

        #region Public Static Functions.

        /// <summary>
        /// TODO: Summary.
        /// </summary>
        public static void ReadFrom()
        {
            ////new X509Certificate2("C:\my certificate.pfx", "The password I Used");
        }

        #endregion

        #region IEmailMessageSignerKey Members

        /// <summary>
        /// TODO: Summary.
        /// </summary>
        /// <param name="message">TODO: message</param>
        public void SignAndEncrypt(MailMessage message)
        {
            var signerKey = new CmsSigner(SubjectIdentifierType.IssuerAndSerialNumber, this.x509Certificate);
            var signedCms = new SignedCms(new ContentInfo(message.Body.ToUtf8Bytes()));
            signedCms.ComputeSignature(signerKey);
            
            EnvelopedCms envelopedCms = new EnvelopedCms(new ContentInfo(signedCms.Encode()));
            CmsRecipient recipient = new CmsRecipient(SubjectIdentifierType.SubjectKeyIdentifier, this.x509Certificate);
            envelopedCms.Encrypt(recipient);

            byte[] encryptedBytes = envelopedCms.Encode();

            MemoryStream encryptedStream = new MemoryStream(encryptedBytes);
            AlternateView encryptedView = new AlternateView(encryptedStream, "application/pkcs7-mime; smime-type=enveloped-data");
            message.AlternateViews.Add(encryptedView);
            message.Body = null;
        }

        /// <summary>
        /// TODO: Summary.
        /// </summary>
        /// <param name="message">TODO: message</param>
        public void Encrypt(MailMessage message)
        {
            //// No ops.
        }

        #endregion
    }

    /// <summary>
    /// Implementation of <see cref="IEmailMessageSignerKey"/>.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public sealed class EmailMessageWithoutSignerKey : IEmailMessageSignerKey
    {
        #region IEmailMessageSignerKey Members

        /// <inheritdoc />
        public void Sign(MailMessage message)
        {
            //// No ops.
        }

        /// <inheritdoc />
        public void Encrypt(MailMessage message)
        {
            //// No ops.
        }

        #endregion

        #region IEmailMessageSignerKey Members

        /// <summary>
        /// TODO: Summary.
        /// </summary>
        /// <param name="message">TODO: message</param>
        public void SignAndEncrypt(MailMessage message)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}