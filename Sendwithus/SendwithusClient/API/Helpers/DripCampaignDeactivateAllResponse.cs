using System;

namespace Sendwithus
{
    /// <summary>
    /// sendwithus DripCampaignDeactivateAllResponse class
    /// </summary>
    public class DripCampaignDeactivateAllResponse
    {
        public bool success { get; set; }
        public string status { get; set; }
        public string recipient_address { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public DripCampaignDeactivateAllResponse()
        {
            this.success = false;
            this.status = String.Empty;
            this.recipient_address = String.Empty;
        }
    }
}
