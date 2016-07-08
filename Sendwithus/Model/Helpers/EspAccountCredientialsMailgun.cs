using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sendwithus
{
    /// <summary>
    /// sendwithus EspAccountCredientialsMailgun class
    /// </summary>
    public class EspAccountCredientialsMailgun : IEspAccountCredentials
    {
        public string api_key { get; set; }
        public string domain { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="apiKey">The Mailgun account API key</param>
        /// <param name="domain">The Mailgun account domain</param>
        public EspAccountCredientialsMailgun(string apiKey, string domain)
        {
            api_key = apiKey;
            this.domain = domain;
        }
    }
}
