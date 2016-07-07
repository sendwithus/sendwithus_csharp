using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sendwithus
{
    /// <summary>
    /// sendwithus Template Version
    /// </summary>
    public class TemplateVersion
    {
        // all lowercase to match expected JSON format (case-sensitive on server side)
        public string name { get; set; }
        public string id { get; set; }
        public string created { get; set; }
        public string modified { get; set; }
        public string html { get; set; }
        public string text { get; set; }
        public string subject { get; set; }
        public string locale { get; set; }
        public bool published { get; set; }

        /// <summary>
        /// Default constuctor for a template version
        /// </summary>
        public TemplateVersion()
        {
            name = String.Empty;
            id = String.Empty;
            created = String.Empty;
            modified = String.Empty;
            html = String.Empty;
            text = String.Empty;
            subject = String.Empty;
            locale = String.Empty;
            published = false;
        }
    }
}
