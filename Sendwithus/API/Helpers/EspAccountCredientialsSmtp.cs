namespace Sendwithus
{
    /// <summary>
    /// sendwithus EspAccountCredientialsSmtp class
    /// </summary>
    public class EspAccountCredientialsSmtp : IEspAccountCredentials
    {
        public string host { get; set; }
        public int port { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public bool use_tls { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="host">The SMTP host</param>
        /// <param name="port">The port to use</param>
        /// <param name="username">The username for the SMTP account</param>
        /// <param name="password">The passord for the SMTP account</param>
        /// <param name="useTls">Whether to use TLS (true) or not (false)</param>
        public EspAccountCredientialsSmtp(string host, int port, string username, string password, bool useTls)
        {
            this.host = host;
            this.port = port;
            this.username = username;
            this.password = password;
            use_tls = useTls;
        }
    }
}
