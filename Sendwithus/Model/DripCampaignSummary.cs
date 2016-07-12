using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sendwithus
{
    /// <summary>
    /// sendwithus DripCampaignSummary class
    /// </summary>
    public class DripCampaignSummary
    {
        public string id { get; set; }
        public string name { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public DripCampaignSummary()
        {
            id = String.Empty;
            name = String.Empty;
        }
    }
}
