using System;
using System.Collections.Generic;

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
        public string amp_html { get; set; }
        public string html { get; set; }
        public string text { get; set; }
        public string subject { get; set; }
        public string preheader { get; set; }
        public Dictionary<string, object> template_data { get; set; }
        public string locale { get; set; }
        public bool published { get; set; }

        /// <summary>
        /// Constuctor for a template version.
        /// Name and subject are mandatory parameters for a subject version
        /// </summary>
        /// <param name="name">The name of the template</param>
        /// <param name="subject">The subject line of the template</param>
        public TemplateVersion(string name, string subject)
        {
            this.name = name;
            this.subject = subject;
            id = String.Empty;
            created = String.Empty;
            modified = String.Empty;
            amp_html = String.Empty;
            html = String.Empty;
            text = String.Empty;
            preheader = String.Empty;
            template_data = new Dictionary<string, object>();
            ;
            locale = String.Empty;
            published = false;
        }

        /// <summary>
        /// Default constuctor for a template version.
        /// Name and subject are mandatory parameters for a subject version
        /// </summary>
        /// <param name="name">The name of the template</param>
        /// <param name="subject">The subject line of the template</param>
        public TemplateVersion() : this(String.Empty, String.Empty) { }
    }
}
