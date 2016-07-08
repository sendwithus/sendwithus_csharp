using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sendwithus
{
    /// <summary>
    /// sendwithus EspAccountCredientialsDyn class
    /// </summary>
    public class EspAccountCredientialsDyn : IEspAccountCredentials
    {
        public string api_key { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="apiKey">The Dyn account API key</param>
        public EspAccountCredientialsDyn(string apiKey)
        {
            api_key = apiKey;
        }
    }
}
