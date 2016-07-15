using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Sendwithus
{
    /// <summary>
    /// sendwithus DripCampaignDetails
    /// </summary>
    public class DripCampaignDetails
    {
        public string Object { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public bool enabled { get; set; }
        public string trigger_email_id { get; set; }
        public Collection<DripCampaignStep> drip_steps { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public DripCampaignDetails()
        {
            Object = String.Empty;
            id = String.Empty;
            name = String.Empty;
            enabled = false;
            trigger_email_id = String.Empty;
            drip_steps = new Collection<DripCampaignStep>();
        }
    }
}
