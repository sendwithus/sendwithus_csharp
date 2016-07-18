using System;

namespace Sendwithus
{
    /// <summary>
    /// sendwithus RenderTemplateResponse class
    /// </summary>
    public class RenderTemplateResponse
    {
        public bool success { get; set; }
        public string status { get; set; }
        public RenderTemplateResponseTemplate template { get; set; }
        public string subject { get; set; }
        public string html { get; set; }
        public string text { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public RenderTemplateResponse()
        {
            success = false;
            status = String.Empty;
            template = new RenderTemplateResponseTemplate();
            subject = String.Empty;
            html = String.Empty;
            text = String.Empty;
        }

    }

    
}
