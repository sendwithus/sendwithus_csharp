using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sendwithus
{
    /// <summary>
    /// sendwithus EspAccountCredientialsSendgrid class
    /// </summary>
    public class EspAccountCredientialsSendgrid : IEspAccountCredentials
    {
        public string username { get; set; }
        public string password { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="username">The Sendgrid account username</param>
        /// <param name="password">The Sendgrid account password</param>
        public EspAccountCredientialsSendgrid(string username, string password)
        {
            this.username = username;
            this.password = password;
        }
    }
}
