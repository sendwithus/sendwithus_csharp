using Sendwithus.Net;
using System;
using System.Collections.Generic;
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
        public string version_name { get; set; }
        public string locale { get; set; }
        public bool strict { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="templateId">The ID of the template</param>
        /// <param name="templateData">The template data</param>
        public Render(string templateId, Dictionary<string, object> templateData)
        {
            this.template = templateId;
            this.template_data = templateData;
            version_id = String.Empty;
            version_name = String.Empty;
            locale = String.Empty;
            strict = true;
        }

        /// <summary>
        /// Render a Template with data.
        /// POST /render
        /// </summary>
        /// <returns>The success of the call and newly rendered template</returns>
        /// <exception cref="AggregateException">Thrown when the API response status code is not success or when the API call times out</exception>
        /// <exception cref="InvalidOperationException">Thrown when making a Batch API Request that has already reached the maxmimum API calls per batch request</exception>
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
