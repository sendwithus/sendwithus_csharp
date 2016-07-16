using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;
using Sendwithus;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SendwithusTest
{
    /// <summary>
    /// Unit testing class for the Template API calls
    /// </summary>
    [TestClass]
    public class TemplateTest
    {
        private const string DEFAULT_TEMPLATE_ID = "tem_SxZKpxJSHPbYDWRSQnAQUR";
        private const string INVALID_TEMPLATE_ID = "invalid_template_id";
        private const string DEFAULT_VERSION_ID = "ver_ET3j2snkKhqsjRjtK6bXJE";
        private const string DEFAULT_LOCALE = "en-US";
        private const string ALTERNATE_LOCALE = "fr-FR";
        private const string INVALID_LOCALE = "invalid_locale";
        private const string INVALID_API_KEY = "invalid_api_key";

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
        /// Tests the GET /templates API call
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [TestMethod]
        public async Task TestGetTemplatesAsync()
        {
            // Make the API call
            Trace.WriteLine("GET /templates");
            try
            { 
                var response = await Template.GetTemplatesAsync();

                // Validate the response
                SendwithusClientTest.ValidateResponse(response);
            }
            catch (AggregateException exception)
            {
                Assert.Fail(exception.ToString());
            }
        }


        /// <summary>
        /// Tests the GET /templates with an invalid API Key
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [TestMethod]
        public async Task TestGetTemplatesWithInvalidApiKeyAsync()
        {
            Trace.WriteLine(String.Format("GET /templates with invalid API Key: {0}", INVALID_API_KEY));

            // Set the API Key to an invalid key and save the original key
            var originalApiKey = SendwithusClient.ApiKey;
            SendwithusClient.ApiKey = INVALID_API_KEY;

            // Make the API call
            try
            {
                var response = await Template.GetTemplatesAsync();
            }
            catch (AggregateException exception)
            {
                // Make sure the response was HTTP 403 Forbidden
                SendwithusClientTest.ValidateException(exception, HttpStatusCode.Forbidden);
            }
            finally
            {
                // Set the API Key back to its original value
                SendwithusClient.ApiKey = originalApiKey;
            }
        }

        /// <summary>
        /// Tests the GET /templates/(:template_id) API call
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [TestMethod]
        public async Task TestGetTemplateByIdAsync()
        {
            // Make the API call
            Trace.WriteLine(String.Format("GET /templates/{0}", DEFAULT_TEMPLATE_ID));
            try
            { 
                var response = await Template.GetTemplateAsync(DEFAULT_TEMPLATE_ID);

                // Validate the response
                SendwithusClientTest.ValidateResponse(response);
            }
            catch (AggregateException exception)
            {
                Assert.Fail(exception.ToString());
            }
        }

        /// <summary>
        /// Tests the GET /templates/(:template_id) API call with an invalid ID
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [TestMethod]
        public async Task TestGetTemplateByIdInvalidIDAsync()
        {
            // Make the API call
            Trace.WriteLine(String.Format("GET /templates/{0} with invalid ID", INVALID_TEMPLATE_ID));
            try
            {
                var response = await Template.GetTemplateAsync(INVALID_TEMPLATE_ID);
                Assert.Fail("Failed to throw exception");
            }
            catch (AggregateException exception)
            {
                // Make sure the response was HTTP 400 Bad Request 
                SendwithusClientTest.ValidateException(exception, HttpStatusCode.BadRequest);
            }
        }

        /// <summary>
        /// Tests the GET /templates/(:template_id)/locales/(:locale) API call
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [TestMethod]
        public async Task TestGetTemplateByIdAndLocaleAsync()
        {
            // Make the API call
            Trace.WriteLine(String.Format("GET /templates/{0}/locales/{1}", DEFAULT_TEMPLATE_ID, DEFAULT_LOCALE));
            try
            { 
                var response = await Template.GetTemplateAsync(DEFAULT_TEMPLATE_ID, DEFAULT_LOCALE);

                // Validate the response
                SendwithusClientTest.ValidateResponse(response);
            }
            catch (AggregateException exception)
            {
                Assert.Fail(exception.ToString());
            }
        }

        /// <summary>
        /// Tests the GET /templates/(:template_id)/locales/(:locale) API call
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [TestMethod]
        public async Task TestGetTemplateByIdAndLocaleInvalidLocaleAsync()
        {
            // Make the API call
            Trace.WriteLine(String.Format("GET /templates/{0}/locales/{1} with invalid locale", DEFAULT_TEMPLATE_ID, INVALID_LOCALE));
            try
            {
                var response = await Template.GetTemplateAsync(DEFAULT_TEMPLATE_ID, INVALID_LOCALE);
                Assert.Fail("Failed to throw exception");
            }
            catch (AggregateException exception)
            {
                // Make sure the response was HTTP 400 Bad Request
                SendwithusClientTest.ValidateException(exception, HttpStatusCode.BadRequest);
            }
        }

        /// <summary>
        /// Tests the GET /templates/(:template_id)/versions API call
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [TestMethod]
        public async Task TestGetTemplateVersionsByIdAsync()
        {
            // Make the API call
            Trace.WriteLine(String.Format("GET /templates/{0}/versions", DEFAULT_TEMPLATE_ID));
            try
            { 
                var response = await Template.GetTemplateVersionsAsync(DEFAULT_TEMPLATE_ID);

                // Validate the response
                SendwithusClientTest.ValidateResponse(response);
            }
            catch (AggregateException exception)
            {
                Assert.Fail(exception.ToString());
            }
        }

        /// <summary>
        /// Tests the GET /templates/(:template_id)/locales/(:locale)/versions API call
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [TestMethod]
        public async Task TestGetTemplateVersionsByIdAndLocaleAsync()
        {
            // Make the API call
            Trace.WriteLine(String.Format("GET /templates/{0}/locales/{1}/versions", DEFAULT_TEMPLATE_ID, DEFAULT_LOCALE));
            try
            { 
                var response = await Template.GetTemplateVersionsAsync(DEFAULT_TEMPLATE_ID, DEFAULT_LOCALE);

                // Validate the response
                SendwithusClientTest.ValidateResponse(response);
            }
            catch (AggregateException exception)
            {
                Assert.Fail(exception.ToString());
            }
        }

        /// <summary>
        /// Tests the GET /templates/(:template_id)/versions/(:version_id) API call
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [TestMethod]
        public async Task TestGetTemplateVersionByIdAsync()
        {
            // Make the API call
            Trace.WriteLine(String.Format("GET /templates/{0}/versions/{1}", DEFAULT_TEMPLATE_ID, DEFAULT_VERSION_ID));
            try
            {
                var response = await Template.GetTemplateVersionAsync(DEFAULT_TEMPLATE_ID, DEFAULT_VERSION_ID);

                // Validate the response
                SendwithusClientTest.ValidateResponse(response);
            }
            catch (AggregateException exception)
            {
                Assert.Fail(exception.ToString());
            }
        }

        /// <summary>
        /// Tests the GET /templates/(:template_id)/locales/(:locale)/versions/(:version_id) API call
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [TestMethod]
        public async Task TestGetTemplateVersionByIdAndLocaleAsync()
        {
            // Make the API call
            Trace.WriteLine(String.Format("GET /templates/{0}/locales/{1}/versions/{2}", DEFAULT_TEMPLATE_ID, DEFAULT_LOCALE, DEFAULT_VERSION_ID));
            try
            { 
                var response = await Template.GetTemplateVersionAsync(DEFAULT_TEMPLATE_ID, DEFAULT_LOCALE, DEFAULT_VERSION_ID);

                // Validate the response
                SendwithusClientTest.ValidateResponse(response);
            }
            catch (AggregateException exception)
            {
                Assert.Fail(exception.ToString());
            }
        }

        /// <summary>
        /// Tests the API call PUT /templates/(:template_id)/versions/(:version_id)
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [TestMethod]
        public async Task TestUpdateTemplateVersionByIdAsync()
        {
            // Make the API call
            Trace.WriteLine(String.Format("PUT /templates/{0}/versions/{1}", DEFAULT_TEMPLATE_ID, DEFAULT_VERSION_ID));
            var updatedTemplateVersion = new TemplateVersion();
            updatedTemplateVersion.name = "New Version";
            updatedTemplateVersion.subject = "edited!";
            updatedTemplateVersion.html = "<html><head></head><body><h1>UPDATE</h1></body></html>";
            updatedTemplateVersion.text = "sometext";
            try
            { 
                var response = await Template.UpdateTemplateVersionAsync(DEFAULT_TEMPLATE_ID, DEFAULT_VERSION_ID, updatedTemplateVersion);

                // Validate the response
                SendwithusClientTest.ValidateResponse(response);
            }
            catch (AggregateException exception)
            {
                Assert.Fail(exception.ToString());
            }
        }

        /// <summary>
        /// Tests the API call PUT /templates/(:template_id)/locales/(:locale)/versions/(:version_id)
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [TestMethod]
        public async Task TestUpdateTemplateVersionByIdAndLocaleAsync()
        {
            // Make the API call
            Trace.WriteLine(String.Format("PUT /templates/{0}/locales/{1}/versions/{2}", DEFAULT_TEMPLATE_ID, DEFAULT_LOCALE, DEFAULT_VERSION_ID));
            var updatedTemplateVersion = new TemplateVersion();
            updatedTemplateVersion.name = "New Version";
            updatedTemplateVersion.subject = "edited!";
            updatedTemplateVersion.html = "<html><head></head><body><h1>UPDATE</h1></body></html>";
            updatedTemplateVersion.text = "sometext";
            try
            { 
                var response = await Template.UpdateTemplateVersionAsync(DEFAULT_TEMPLATE_ID, DEFAULT_LOCALE, DEFAULT_VERSION_ID, updatedTemplateVersion);

                // Validate the response
                SendwithusClientTest.ValidateResponse(response);
            }
            catch (AggregateException exception)
            {
                Assert.Fail(exception.ToString());
            }
        }

        /// <summary>
        /// Tests the API call POST /templates
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [TestMethod]
        public async Task TestCreateTemplateAsync()
        {
            // Make the API call
            Trace.WriteLine(String.Format("PUT /templates/"));
            try
            { 
                var response = await BuildAndSendCreateTemplateRequestAsync();

                // Validate the response
                SendwithusClientTest.ValidateResponse(response);
            }
            catch (AggregateException exception)
            {
                Assert.Fail(exception.ToString());
            }
        }

        /// <summary>
        /// Tests the API call POST /templates/(:template_id)/locales
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [TestMethod]
        public async Task TestAddLocaleToTemplateAsync()
        {
            // Create a new template to add a locale to
            // Otherwise, if an existing template were used, this test might fail because the new locale could already exist on the template
            var newTemplate = await BuildAndSendCreateTemplateRequestAsync();
            var templateId = newTemplate.id;

            // Make the API call
            Trace.WriteLine(String.Format("POST /templates/{0}/locales", templateId));
            var templateVersion = new TemplateVersion();
            templateVersion.name = "Published French Version";
            templateVersion.subject = "Ce est un nouveau modèle!";
            templateVersion.html = "<html><head></head><body><h1>Nouveau modèle!</h1></body></html>";
            templateVersion.text = "un texte";
            try { 
                var response = await Template.AddLocaleToTemplate(templateId, ALTERNATE_LOCALE, templateVersion);

                // Validate the response
                SendwithusClientTest.ValidateResponse(response);
            }
            catch (AggregateException exception)
            {
                Assert.Fail(exception.ToString());
            }
        }

        /// <summary>
        /// Tests the API call POST /templates/(:template_id)/versions
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [TestMethod]
        public async Task TestCreateTemplateVersionAsync()
        {
            // Make the API call
            Trace.WriteLine(String.Format("POST /templates/{0}/versions", DEFAULT_TEMPLATE_ID));
            var templateVersion = new TemplateVersion();
            templateVersion.name = "New Template Version";
            templateVersion.subject = "New Version!";
            templateVersion.html = "<html><head></head><body><h1>NEW TEMPLATE VERSION</h1></body></html>";
            templateVersion.text = "some text";
            try
            {
                var response = await Template.CreateTemplateVersion(DEFAULT_TEMPLATE_ID, templateVersion);

                // Validate the response
                SendwithusClientTest.ValidateResponse(response);
            }
            catch (AggregateException exception)
            {
                Assert.Fail(exception.ToString());
            }
        }

        /// <summary>
        /// Tests the API call POST /templates/(:template_id)/locales/(:locale)/versions
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [TestMethod]
        public async Task TestCreateTemplateVersionWithLocaleAsync()
        {
            // Make the API call
            Trace.WriteLine(String.Format("POST /templates/{0}/locales/{1}/versions", DEFAULT_TEMPLATE_ID, DEFAULT_LOCALE));
            var templateVersion = new TemplateVersion();
            templateVersion.name = "New Template Version";
            templateVersion.subject = "New Version!";
            templateVersion.html = "<html><head></head><body><h1>NEW TEMPLATE VERSION</h1></body></html>";
            templateVersion.text = "some text";
            try
            { 
                var response = await Template.CreateTemplateVersion(DEFAULT_TEMPLATE_ID, DEFAULT_LOCALE, templateVersion);

                // Validate the response
                SendwithusClientTest.ValidateResponse(response);
            }
            catch (AggregateException exception)
            {
                Assert.Fail(exception.ToString());
            }
        }

        /// <summary>
        /// Tests the API call DELETE /templates/(:template_id)
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [TestMethod]
        public async Task TestDeleteTemplateAsync()
        {
            // Create a new template to use for deletion
            // Otherwise, if an existing template were used, this test might fail because the new locale could already exist on the template
            var newTemplate = await BuildAndSendCreateTemplateRequestAsync();
            var templateId = newTemplate.id;

            // Make the API call
            Trace.WriteLine(String.Format("DELETE /templates/{0}", templateId));
            try
            { 
                var response = await Template.DeleteTemplate(templateId);

                // Validate the response
                SendwithusClientTest.ValidateResponse(response);
            }
            catch (AggregateException exception)
            {
                Assert.Fail(exception.ToString());
            }
        }

        /// <summary>
        /// Tests the API call DELETE /templates/(:template_id)/locales/(:locale)
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [TestMethod]
        public async Task TestDeleteTemplateWithLocaleAsync()
        {
            // Create a new template to use for deletion
            // Otherwise, if an existing template were used, this test might fail because the new locale could already exist on the template
            var newTemplate = await BuildAndSendCreateTemplateRequestAsync();
            var templateId = newTemplate.id;

            // Make the API call
            Trace.WriteLine(String.Format("DELETE /templates/{0}/locales/{1}", templateId, DEFAULT_LOCALE));
            try { 
                var response = await Template.DeleteTemplate(templateId, DEFAULT_LOCALE);

                // Validate the response
                SendwithusClientTest.ValidateResponse(response);
            }
            catch (AggregateException exception)
            {
                Assert.Fail(exception.ToString());
            }
        }

        public static async Task<Template> BuildAndSendCreateTemplateRequestAsync()
        {
            var newTemplateVersion = new TemplateVersion();
            newTemplateVersion.name = "New Template";
            newTemplateVersion.subject = "This is a new template!";
            newTemplateVersion.html = "<html><head></head><body><h1>NEW TEMPLATE</h1></body></html>";
            newTemplateVersion.text = "some text";
            return await Template.CreateTemplateAsync(newTemplateVersion);
        }
    }
}
