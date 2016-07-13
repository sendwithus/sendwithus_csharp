using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sendwithus
{
    /// <summary>
    /// sendwithus EspAccountCredientialsMandrill class
    /// </summary>
    public class EspAccountCredientialsMandrill : IEspAccountCredentials
    {
        public string api_key { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="apiKey">The Mandrill account API key</param>
        public EspAccountCredientialsMandrill(string apiKey)
        {
            api_key = apiKey;
        }
    }
}
