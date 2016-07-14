using System;
using System.Collections.Generic;
using Xunit;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Net;
using Sendwithus;
using Xunit.Abstractions;

namespace SendwithusTest
{
    /// <summary>
    /// Unit testing class for the Render Template API calls
    /// </summary>
    public class RenderTest
    {
        private const string DEFAULT_TEMPLATE_ID = "tem_SxZKpxJSHPbYDWRSQnAQUR";
        private const string INVALID_TEMPLATE_ID = "invalid_template_id";
        private const string DEFAULT_VERSION_ID = "ver_ET3j2snkKhqsjRjtK6bXJE";
        private const string DEFAULT_LOCALE = "en-US";

        readonly ITestOutputHelper Output;

        /// <summary>
        /// Default constructor with an output object - used to output messages to the Test Explorer
        /// </summary>
        /// <param name="output"></param>
        public RenderTest(ITestOutputHelper output)
        {
            Output = output;
        }

        /// <summary>
        /// Tests the API call POST /render with only the required parameters
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [Fact]
        public async Task TestRenderTemplateWithOnlyRequiredParametersAsync()
        {
            Output.WriteLine("POST /render");

            // Use the production API key so that the emails are actually sent when the template is rendered
            SendwithusClient.ApiKey = SendwithusClientTest.API_KEY_PRODUCTION;

            // Make the API call
            var templateData = new Dictionary<string, object>();
            templateData.Add("amount", "$12.00");
            var renderTemplate = new Render(DEFAULT_TEMPLATE_ID, templateData);
            var response = await renderTemplate.RenderTemplateAsync();

            // Validate the response
            SendwithusClientTest.ValidateResponse(response, Output);
        }

        /// <summary>
        /// Tests the API call POST /render with all of the parameters
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [Fact]
        public async Task TestRenderTemplateWithAllParametersAsync()
        {
            Output.WriteLine("POST /render");

            // Use the production API key so that the emails are actually sent when the template is rendered
            SendwithusClient.ApiKey = SendwithusClientTest.API_KEY_PRODUCTION;

            // Make the API call
            var response = await BuildAndSendRenderTemplateRequestWithAllParameters();

            // Validate the response
            SendwithusClientTest.ValidateResponse(response, Output);
        }

        /// <summary>
        /// Tests the API call POST /render with an invalid template ID
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [Fact]
        public async Task TestRenderTemplateWithInvalidIdAsync()
        {
            Output.WriteLine("POST /render");

            // Use the production API key so that the emails are actually sent when the template is rendered
            SendwithusClient.ApiKey = SendwithusClientTest.API_KEY_PRODUCTION;

            // Build the object
            var templateData = new Dictionary<string, object>();
            templateData.Add("amount", "$12.00");
            var renderTemplate = new Render(INVALID_TEMPLATE_ID, templateData);

            // Make the API call
            try
            {
                var response = await renderTemplate.RenderTemplateAsync();
                Assert.True(false, "Failed to throw exception");
            }
            catch (SendwithusException exception)
            {
                // Make sure the response was HTTP 400 Bad Request 
                SendwithusClientTest.ValidateException(exception, HttpStatusCode.BadRequest, Output);
            }
        }
        
        /// <summary>
        /// Builds and sends a RenderTemplate request with all the parameters.
        /// Public so that it can also be used by the BatchApiRequestTest library
        /// </summary>
        /// <returns></returns>
        public static async Task<RenderTemplateResponse> BuildAndSendRenderTemplateRequestWithAllParameters()
        {
            var templateData = new Dictionary<string, object>();
            templateData.Add("amount", "$12.00");
            var renderTemplate = new Render(DEFAULT_TEMPLATE_ID, templateData);
            renderTemplate.version_id = DEFAULT_VERSION_ID;
            renderTemplate.locale = DEFAULT_LOCALE;
            renderTemplate.strict = true;
            return await renderTemplate.RenderTemplateAsync();
        }
    }
}
