﻿using System;

namespace Sendwithus
{
    /// <summary>
    /// sendwithus DripCampaignResponse class
    /// </summary>
    public class DripCampaignResponse
    {
        public bool success { get; set; }
        public string status { get; set; }
        public DripCampaignSummary drip_campaign { get; set; }
        public string recipient_address { get; set; }
        public string message { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public DripCampaignResponse()
        {
            success = false;
            status = String.Empty;
            drip_campaign = new DripCampaignSummary();
            recipient_address = String.Empty;
            message = String.Empty;
        }
    }
}
