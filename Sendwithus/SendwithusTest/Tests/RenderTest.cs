using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Net;
using Sendwithus;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SendwithusTest
{
    /// <summary>
    /// Unit testing class for the Render Template API calls
    /// </summary>
    [TestClass]
    public class RenderTest
    {
        private const string DEFAULT_TEMPLATE_ID = "tem_SxZKpxJSHPbYDWRSQnAQUR";
        private const string INVALID_TEMPLATE_ID = "invalid_template_id";
        private const string DEFAULT_VERSION_ID = "ver_GzHYyNnDR3XFofVZRyz736";
        private const string DEFAULT_VERSION_NAME = "Template Version Name";
        private const string DEFAULT_LOCALE = "en-US";

        /// <summary>
        /// Sets the API 
        /// </summary>
        [TestInitialize]
        public void InitializeUnitTesting()
        {
            // Set the API key
            SendwithusClient.ApiKey = SendwithusClientTest.API_KEY_TEST;
        }

        /// <summary>
        /// Tests the API call POST /render with only the required parameters
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [TestMethod]
        public async Task TestRenderTemplateWithOnlyRequiredParametersAsync()
        {
            Trace.WriteLine("POST /render");

            // Make the API call
            var templateData = new Dictionary<string, object>();
            templateData.Add("amount", "$12.00");
            var renderTemplate = new Render(DEFAULT_TEMPLATE_ID, templateData);
            try
            { 
                var renderTemplateResponse = await renderTemplate.RenderTemplateAsync();

                // Validate the response
                SendwithusClientTest.ValidateResponse(renderTemplateResponse);
            }
            catch (AggregateException exception)
            {
                Assert.Fail(exception.ToString());
            }
        }

        /// <summary>
        /// Tests the API call POST /render with all of the parameters, specifying the template version ID istead of the version name
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [TestMethod]
        public async Task TestRenderTemplateWithAllParametersIdAsync()
        {
            Trace.WriteLine("POST /render");

            // Make the API call
            try
            { 
                var renderTemplateResponse = await BuildAndSendRenderTemplateRequestWithAllParametersId();

                // Validate the response
                SendwithusClientTest.ValidateResponse(renderTemplateResponse);
            }
            catch (AggregateException exception)
            {
                Assert.Fail(exception.ToString());
            }
        }

        /// <summary>
        /// Tests the API call POST /render with all of the parameters, specifying the template version name istead of the version ID
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [TestMethod]
        public async Task TestRenderTemplateWithAllParametersNameAsync()
        {
            Trace.WriteLine("POST /render");

            // Make the API call
            try
            {
                var renderTemplateResponse = await BuildAndSendRenderTemplateRequestWithAllParametersName();

                // Validate the response
                SendwithusClientTest.ValidateResponse(renderTemplateResponse);
            }
            catch (AggregateException exception)
            {
                Assert.Fail(exception.ToString());
            }
        }

        /// <summary>
        /// Tests the API call POST /render with an invalid template ID
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [TestMethod]
        public async Task TestRenderTemplateWithInvalidIdAsync()
        {
            Trace.WriteLine("POST /render");

            // Build the object
            var templateData = new Dictionary<string, object>();
            templateData.Add("amount", "$12.00");
            var renderTemplate = new Render(INVALID_TEMPLATE_ID, templateData);

            // Make the API call
            try
            {
                var renderTemplateResponse = await renderTemplate.RenderTemplateAsync();
                Assert.Fail("Failed to throw exception");
            }
            catch (AggregateException exception)
            {
                // Make sure the response was HTTP 400 Bad Request 
                SendwithusClientTest.ValidateException(exception, HttpStatusCode.BadRequest);
            }
        }

        /// <summary>
        /// Builds and sends a RenderTemplate request with all the parameters, using the version ID instead of the version name.
        /// Public so that it can also be used by the BatchApiRequestTest library
        /// </summary>
        /// <returns></returns>
        public static async Task<RenderTemplateResponse> BuildAndSendRenderTemplateRequestWithAllParametersId()
        {
            var templateData = new Dictionary<string, object>();
            templateData.Add("amount", "$12.00");
            var renderTemplate = new Render(DEFAULT_TEMPLATE_ID, templateData);
            renderTemplate.version_id = DEFAULT_VERSION_ID;
            renderTemplate.locale = DEFAULT_LOCALE;
            renderTemplate.strict = true;
            return await renderTemplate.RenderTemplateAsync();
        }

        /// <summary>
        /// Builds and sends a RenderTemplate request with all the parameters, using the version name instead of the version ID.
        /// </summary>
        /// <returns></returns>
        private static async Task<RenderTemplateResponse> BuildAndSendRenderTemplateRequestWithAllParametersName()
        {
            var templateData = new Dictionary<string, object>();
            templateData.Add("amount", "$12.00");
            var renderTemplate = new Render(DEFAULT_TEMPLATE_ID, templateData);
            renderTemplate.version_name = DEFAULT_VERSION_NAME;
            renderTemplate.locale = DEFAULT_LOCALE;
            renderTemplate.strict = true;
            return await renderTemplate.RenderTemplateAsync();
        }
    }
}
