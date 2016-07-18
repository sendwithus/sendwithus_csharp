namespace Sendwithus
{
    /// <summary>
    /// sendwithus EspAccountCredientialsMailjet class
    /// </summary>
    public class EspAccountCredientialsMailjet : IEspAccountCredentials
    {
        public string api_key { get; set; }
        public string secret_key { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="apiKey">The Mailjet account API key</param>
        /// <param name="secretKey">The Mailjet account secret access key</param>
        public EspAccountCredientialsMailjet(string apiKey, string secretKey)
        {
            api_key = apiKey;
            secret_key = secretKey;
        }
    }
}
