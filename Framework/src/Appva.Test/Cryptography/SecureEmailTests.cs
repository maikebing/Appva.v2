// <copyright file="SecureEmailTests.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Test.Cryptography
{
    #region Import.

    using System;
    using System.Net;
    using System.Net.Mail;
    using System.Security.Cryptography;
    using Appva.Core.Messaging;
    using Appva.Cryptography;
    using Appva.Cryptography.Messaging;
    using Appva.Cryptography.X509;
    using Xunit;
    using Xunit.Extensions;

    #endregion

    /// <summary>
    /// 
    /// </summary>
    public class SecureEmailTests
    {
        [Fact]
        public void SecureEmail_SendMail_IsTrue()
        {
            /*string x = "johansalllarsson@gmail.com";
            var email = new EmailMessage(x, x, "Secure", "<h1>Test</h1><p>It worked</p>", true);
            var ca = Certificate.FindBySubjectDistinguishedName("CN=RandomCA");
            if (ca == null)
            {
                //throw new Exception("oh no");
                Certificate.CertificateAuthority().Subject("RandomCA")
                    .WriteToDisk("C:\\Certificates\\RandomCA.pfx", "password");
            }
            else
            {
                var signer = new EmailMessageSignerKey(ca);
                signer.SignAndEncrypt(email);
                using (var client = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential(x, ""),
                    EnableSsl = true
                })
                {
                    email.IsBodyHtml = true;
                    client.Send(email);
                }
            }*/
        }
    }
}
