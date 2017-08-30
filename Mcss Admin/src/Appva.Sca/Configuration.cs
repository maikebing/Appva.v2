using System;
using System.Collections.Generic;
using System.Text;

namespace Appva.Sca
{
    public class Configuration
    {
        private string clientId; // = "EABE6751-2ABD-4311-A794-70A833D31C31"
        private string clientSecret; // = "C5C8DAEB-6C07-423D-82CF-8177C8CB9604"
        internal string Credentials { get; private set; }
        internal Uri BaseAddress { get; set; }

        public Configuration(Uri baseAddress, string clientId, string clientSecret)
        {
            this.BaseAddress = baseAddress;
            this.clientId = clientId;
            this.clientSecret = clientSecret;
            this.SetCredentials(clientId, clientSecret);
        }

        private void SetCredentials(string clientId, string clientSecret)
        {
            this.Credentials = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(clientId + ":" + clientSecret));
        }
    }
}
