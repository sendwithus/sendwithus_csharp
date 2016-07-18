namespace Sendwithus
{
    /// <summary>
    /// sendwithus EspAccountCredientialsPostmark class
    /// </summary>
    public class EspAccountCredientialsPostmark : IEspAccountCredentials
    {
        public string api_key { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="apiKey">The Postmark account API key</param>
        public EspAccountCredientialsPostmark(string apiKey)
        {
            api_key = apiKey;
        }
    }
}
