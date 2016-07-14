using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sendwithus
{
    /// <summary>
    /// sendwithus SegmentContent
    /// </summary>
    public class SegmentContent
    {
        public string email_id { get; set; }
        public Dictionary<string, object> email_data { get; set; }
        public string esp_account { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="email_id">The ID of the template to use with the segment</param>
        public SegmentContent(string templateId)
        {
            email_id = templateId;
            email_data = new Dictionary<string, object>();
            esp_account = String.Empty;
        }
    }
}
