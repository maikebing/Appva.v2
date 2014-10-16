// <copyright file="EmailMessageSignerKey.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Cryptography.Messaging
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Mail;
    using System.Security.Cryptography.Pkcs;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;
    using System.Threading.Tasks;
    using Core.Extensions;
    using Core.Messaging;
    using Validation;

    #endregion

    /// <summary>
    /// S/MIME (Secure/Multipurpose Internet Mail Extensions) is a standard for 
    /// public key encryption and signing of MIME data. S/MIME is on an IETF standards 
    /// track and defined in a number of documents, most importantly RFCs 3369, 3370, 
    /// 3850 and 3851.
    /// See <a href="http://en.wikipedia.org/wiki/S/MIME">S/MIME</a>
    /// </summary>
    public interface IEmailMessageSignerKey
    {
        /// <summary>
        /// Signs and encrypts the E-mail message.
        /// </summary>
        /// <param name="message">The message to be signed</param>
        void SignAndEncrypt(EmailMessage message);
    }

    /// <summary>
    /// Implementation of <see cref="IEmailMessageSignerKey"/>.
    /// </summary>
    public sealed class EmailMessageSignerKey
    {
        #region Variables.

        /// <summary>
        /// The <see cref="X509Certificate2"/> used for encryption
        /// and signing the E-mail message.
        /// </summary>
        private readonly X509Certificate2 x509Certificate;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailMessageSignerKey"/> class.
        /// </summary>
        private EmailMessageSignerKey(X509Certificate2 x509Certificate)
        {
            Requires.NotNull(x509Certificate, "x509Certificate");
            this.x509Certificate = x509Certificate;
        }

        #endregion

        #region IEmailMessageSignerKey Members

        /// <inheritdoc />
        public void SignAndEncrypt(EmailMessage message)
        {/*
            var signerKey = new CmsSigner(SubjectIdentifierType.IssuerAndSerialNumber, this.x509Certificate);
            var signedCms = new SignedCms(new ContentInfo(message.Body.ToUtf8Bytes()));
            signedCms.ComputeSignature(signerKey);
            
            EnvelopedCms envelopedCms = new EnvelopedCms(new ContentInfo(signedCms.Encode()));
            CmsRecipient recipient = new CmsRecipient(SubjectIdentifierType.SubjectKeyIdentifier, this.certificate);
            envelopedCms.Encrypt(recipient);

            byte[] encryptedBytes = envelopedCms.Encode();

            MemoryStream encryptedStream = new MemoryStream(encryptedBytes);
            AlternateView encryptedView = new AlternateView(encryptedStream, "application/pkcs7-mime; smime-type=enveloped-data");
            message.AlternateViews.Add(encryptedView);*/
        }

        /// <inheritdoc />
        public void Encrypt(EmailMessage message)
        {
            //// No ops.
        }

        #endregion

        #region Public Static Functions.

        public static void ReadFrom()
        {
            ////new X509Certificate2("C:\my certificate.pfx", "The password I Used");
        }

        #endregion
    }

    /// <summary>
    /// Implementation of <see cref="IEmailMessageSignerKey"/>.
    /// </summary>
    public sealed class EmailMessageWithoutSignerKey : IEmailMessageSignerKey
    {
        #region IEmailMessageSignerKey Members

        /// <inheritdoc />
        public void Sign(EmailMessage message)
        {
            //// No ops.
        }

        /// <inheritdoc />
        public void Encrypt(EmailMessage message)
        {
            //// No ops.
        }

        #endregion

        #region IEmailMessageSignerKey Members

        public void SignAndEncrypt(EmailMessage message)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}