using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Sendwithus
{
    /// <summary>
    /// sendwithus Render class.
    /// For rendering email templates
    /// </summary>
    public class Render
    {
        public string template { get; set; }
        public Dictionary<string, object> template_data  { get; set; }
        public string version_id { get; set; }
        public string locale { get; set; }
        public bool strict { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="templateId">The ID of the template</param>
        /// <param name="template_data">The template data</param>
        public Render(string templateId, Dictionary<string, object> template_data)
        {
            this.template = templateId;
            this.template_data = template_data;
            version_id = String.Empty;
            locale = String.Empty;
            strict = false;
        }

        /// <summary>
        /// Render a Template with data.
        /// POST /render
        /// </summary>
        /// <returns>The success of the call and newly rendered template</returns>
        public async Task<RenderTemplateResponse> RenderTemplateAsync()
        {
            // Send the POST request
            var resource = "render";
            var jsonResponse = await RequestManager.SendPostRequestAsync(resource, this);

            // Convert the JSON result into an object
            var serializer = new JavaScriptSerializer();
            var response = serializer.Deserialize<RenderTemplateResponse>(jsonResponse);
            return response;
        }
    }
}
