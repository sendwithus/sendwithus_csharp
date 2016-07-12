using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sendwithus
{
    /// <summary>
    /// sendwithus DripCampaignStep class
    /// </summary>
    public class DripCampaignStep
    {
        public string Object { get; set; }
        public string id { get; set; }
        public string email_id { get; set; }
        public int delay_seconds { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public DripCampaignStep()
        {
            Object = String.Empty;
            id = String.Empty;
            email_id = String.Empty;
            delay_seconds = 0;
        }
    }
}
