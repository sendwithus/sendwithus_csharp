namespace Sendwithus
{
    /// <summary>
    /// sendwithus EspAccountCredientialsSes class
    /// </summary>
    public class EspAccountCredientialsSes : IEspAccountCredentials
    {
        public string access_key_id { get; set; }
        public string secret_access_key { get; set; }
        public string region { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="accessKeyId">The SES account API key ID</param>
        /// <param name="secretAccessKey">The SES account secret access key</param>
        /// <param name="region">The region for the SES account</param>
        public EspAccountCredientialsSes(string accessKeyId, string secretAccessKey, string region)
        {
            access_key_id = accessKeyId;
            secret_access_key = secretAccessKey;
            this.region = region;
        }
    }
}
