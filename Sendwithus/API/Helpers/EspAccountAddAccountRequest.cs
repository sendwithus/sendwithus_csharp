namespace Sendwithus
{
    /// <summary>
    /// sendwithus EspAccountAddRequest class
    /// </summary>
    public class EspAccountAddAccountRequest
    {
        public string name { get; set; }
        public string esp_type { get; set; }
        public IEspAccountCredentials credentials { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="name">The name of the ESP account</param>
        /// <param name="espType">The type of the ESP account</param>
        /// <param name="credentials">The credentials for the ESP account</param>
        public EspAccountAddAccountRequest(string name, string espType, IEspAccountCredentials credentials)
        {
            this.name = name;
            esp_type = espType;
            this.credentials = credentials;
        }
    }
}
