using System;

namespace Sendwithus
{
    /// <summary>
    /// sendwithus RenderTemplateResponseTemplate class
    /// </summary>
    public class RenderTemplateResponseTemplate
    {
        public string id { get; set; }
        public string name { get; set; }
        public string version_name { get; set; }
        public string locale { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public RenderTemplateResponseTemplate()
        {
            id = String.Empty;
            name = String.Empty;
            version_name = String.Empty;
            locale = String.Empty;
        }
    }
}
