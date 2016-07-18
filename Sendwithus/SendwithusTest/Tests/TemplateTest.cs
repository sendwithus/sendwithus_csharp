using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sendwithus;
using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;

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
                var templates = await Template.GetTemplatesAsync();

                // Validate the response
                SendwithusClientTest.ValidateResponse(templates);
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
                var templates = await Template.GetTemplatesAsync();
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
                var template = await Template.GetTemplateAsync(DEFAULT_TEMPLATE_ID);

                // Validate the response
                SendwithusClientTest.ValidateResponse(template);
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
                var template = await Template.GetTemplateAsync(INVALID_TEMPLATE_ID);
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
                var template = await Template.GetTemplateAsync(DEFAULT_TEMPLATE_ID, DEFAULT_LOCALE);

                // Validate the response
                SendwithusClientTest.ValidateResponse(template);
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
                var template = await Template.GetTemplateAsync(DEFAULT_TEMPLATE_ID, INVALID_LOCALE);
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
                var template = await Template.GetTemplateVersionsAsync(DEFAULT_TEMPLATE_ID);

                // Validate the response
                SendwithusClientTest.ValidateResponse(template);
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
                var templateVersions = await Template.GetTemplateVersionsAsync(DEFAULT_TEMPLATE_ID, DEFAULT_LOCALE);

                // Validate the response
                SendwithusClientTest.ValidateResponse(templateVersions);
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
                var templateVersion = await Template.GetTemplateVersionAsync(DEFAULT_TEMPLATE_ID, DEFAULT_VERSION_ID);

                // Validate the response
                SendwithusClientTest.ValidateResponse(templateVersion);
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
                var templateVersion = await Template.GetTemplateVersionAsync(DEFAULT_TEMPLATE_ID, DEFAULT_LOCALE, DEFAULT_VERSION_ID);

                // Validate the response
                SendwithusClientTest.ValidateResponse(templateVersion);
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
        public async Task TestUpdateTemplateVersionByIdWithAllParametersAsync()
        {
            // Make the API call
            Trace.WriteLine(String.Format("PUT /templates/{0}/versions/{1}", DEFAULT_TEMPLATE_ID, DEFAULT_VERSION_ID));
            var templateVersionName = "New Template Version";
            var templateSubject = "New Version!";
            var updatedTemplateVersion = new TemplateVersion(templateVersionName, templateSubject);
            updatedTemplateVersion.html = "<html><head></head><body><h1>NEW TEMPLATE VERSION</h1></body></html>";
            updatedTemplateVersion.text = "some text";
            try
            { 
                var templateVersion = await Template.UpdateTemplateVersionAsync(DEFAULT_TEMPLATE_ID, DEFAULT_VERSION_ID, updatedTemplateVersion);

                // Validate the response
                SendwithusClientTest.ValidateResponse(templateVersion);
            }
            catch (AggregateException exception)
            {
                Assert.Fail(exception.ToString());
            }
        }

        /// <summary>
        /// Tests the API call PUT /templates/(:template_id)/versions/(:version_id), using HTML instead of text
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [TestMethod]
        public async Task TestUpdateTemplateVersionByIdWithMinimumParametersHtmlAsync()
        {
            // Make the API call
            Trace.WriteLine(String.Format("PUT /templates/{0}/versions/{1}", DEFAULT_TEMPLATE_ID, DEFAULT_VERSION_ID));
            var templateVersionName = "New Template Version";
            var templateSubject = "New Version!";
            var updatedTemplateVersion = new TemplateVersion(templateVersionName, templateSubject);
            updatedTemplateVersion.html = "<html><head></head><body><h1>UPDATE</h1></body></html>";
            try
            {
                var templateVersion = await Template.UpdateTemplateVersionAsync(DEFAULT_TEMPLATE_ID, DEFAULT_VERSION_ID, updatedTemplateVersion);

                // Validate the response
                SendwithusClientTest.ValidateResponse(templateVersion);
            }
            catch (AggregateException exception)
            {
                Assert.Fail(exception.ToString());
            }
        }

        /// <summary>
        /// Tests the API call PUT /templates/(:template_id)/versions/(:version_id), using text instead of HTML
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [TestMethod]
        public async Task TestUpdateTemplateVersionByIdWithMinimumParametersTextAsync()
        {
            // Make the API call
            Trace.WriteLine(String.Format("PUT /templates/{0}/versions/{1}", DEFAULT_TEMPLATE_ID, DEFAULT_VERSION_ID));
            var templateVersionName = "New Template Version";
            var templateSubject = "New Version!";
            var updatedTemplateVersion = new TemplateVersion(templateVersionName, templateSubject);
            updatedTemplateVersion.text = "sometext";
            try
            {
                var templateVersion = await Template.UpdateTemplateVersionAsync(DEFAULT_TEMPLATE_ID, DEFAULT_VERSION_ID, updatedTemplateVersion);

                // Validate the response
                SendwithusClientTest.ValidateResponse(templateVersion);
            }
            catch (AggregateException exception)
            {
                Assert.Fail(exception.ToString());
            }
        }

        /// <summary>
        /// Tests the API call PUT /templates/(:template_id)/locales/(:locale)/versions/(:version_id) with only mandatory parameters, using HTML instead of text
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [TestMethod]
        public async Task TestUpdateTemplateVersionByIdAndLocaleWithMinimumParametersHtmlAsync()
        {
            // Make the API call
            Trace.WriteLine(String.Format("PUT /templates/{0}/locales/{1}/versions/{2}", DEFAULT_TEMPLATE_ID, DEFAULT_LOCALE, DEFAULT_VERSION_ID));
            var templateVersionName = "New Version";
            var templateSubject = "edited!";
            var updatedTemplateVersion = new TemplateVersion(templateVersionName, templateSubject);
            updatedTemplateVersion.html = "<html><head></head><body><h1>UPDATE</h1></body></html>";
            try
            {
                var templateVersion = await Template.UpdateTemplateVersionAsync(DEFAULT_TEMPLATE_ID, DEFAULT_LOCALE, DEFAULT_VERSION_ID, updatedTemplateVersion);

                // Validate the response
                SendwithusClientTest.ValidateResponse(templateVersion);
            }
            catch (AggregateException exception)
            {
                Assert.Fail(exception.ToString());
            }
        }

        /// <summary>
        /// Tests the API call PUT /templates/(:template_id)/locales/(:locale)/versions/(:version_id) with only mandatory parameters, using text instead of HTML
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [TestMethod]
        public async Task TestUpdateTemplateVersionByIdAndLocaleWithMinimumParametersTextAsync()
        {
            // Make the API call
            Trace.WriteLine(String.Format("PUT /templates/{0}/locales/{1}/versions/{2}", DEFAULT_TEMPLATE_ID, DEFAULT_LOCALE, DEFAULT_VERSION_ID));
            var templateVersionName = "New Version";
            var templateSubject = "edited!";
            var updatedTemplateVersion = new TemplateVersion(templateVersionName, templateSubject);
            updatedTemplateVersion.text = "sometext";
            try
            {
                var templateVersion = await Template.UpdateTemplateVersionAsync(DEFAULT_TEMPLATE_ID, DEFAULT_LOCALE, DEFAULT_VERSION_ID, updatedTemplateVersion);

                // Validate the response
                SendwithusClientTest.ValidateResponse(templateVersion);
            }
            catch (AggregateException exception)
            {
                Assert.Fail(exception.ToString());
            }
        }

        /// <summary>
        /// Tests the API call PUT /templates/(:template_id)/locales/(:locale)/versions/(:version_id) with all parameters
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [TestMethod]
        public async Task TestUpdateTemplateVersionByIdAndLocaleWithAllParametersAsync()
        {
            // Make the API call
            Trace.WriteLine(String.Format("PUT /templates/{0}/locales/{1}/versions/{2}", DEFAULT_TEMPLATE_ID, DEFAULT_LOCALE, DEFAULT_VERSION_ID));
            var templateVersionName = "New Version";
            var templateSubject = "edited!";
            var updatedTemplateVersion = new TemplateVersion(templateVersionName, templateSubject);
            updatedTemplateVersion.html = "<html><head></head><body><h1>UPDATE</h1></body></html>";
            updatedTemplateVersion.text = "sometext";
            try
            { 
                var templateVersion = await Template.UpdateTemplateVersionAsync(DEFAULT_TEMPLATE_ID, DEFAULT_LOCALE, DEFAULT_VERSION_ID, updatedTemplateVersion);

                // Validate the response
                SendwithusClientTest.ValidateResponse(templateVersion);
            }
            catch (AggregateException exception)
            {
                Assert.Fail(exception.ToString());
            }
        }

        /// <summary>
        /// Tests the API call POST /templates with the minimum parameters
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [TestMethod]
        public async Task TestCreateTemplateWithMinimumParametersAsync()
        {
            // Make the API call
            Trace.WriteLine(String.Format("PUT /templates/"));
            try
            {
                var template = await BuildAndSendCreateTemplateRequestWithMinimumParametersAsync();

                // Validate the response
                SendwithusClientTest.ValidateResponse(template);
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
        public async Task TestCreateTemplateWithAllParametersAsync()
        {
            // Make the API call
            Trace.WriteLine(String.Format("PUT /templates/"));
            try
            { 
                var template = await BuildAndSendCreateTemplateRequestWithAllParametersAsync();

                // Validate the response
                SendwithusClientTest.ValidateResponse(template);
            }
            catch (AggregateException exception)
            {
                Assert.Fail(exception.ToString());
            }
        }

        /// <summary>
        /// Tests the API call POST /templates/(:template_id)/locales with the minimum parameters
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [TestMethod]
        public async Task TestAddLocaleToTemplateWithMinimumParmetersAsync()
        {
            // Create a new template to add a locale to
            // Otherwise, if an existing template were used, this test might fail because the new locale could already exist on the template
            var newTemplate = await BuildAndSendCreateTemplateRequestWithAllParametersAsync();
            var templateId = newTemplate.id;

            // Make the API call
            Trace.WriteLine(String.Format("POST /templates/{0}/locales", templateId));
            var templateVersionName = "Published French Version";
            var templateSubject = "Ce est un nouveau modèle!";
            var updatedTemplateVersion = new TemplateVersion(templateVersionName, templateSubject);

            try
            {
                var template = await Template.AddLocaleToTemplate(templateId, ALTERNATE_LOCALE, updatedTemplateVersion);

                // Validate the response
                SendwithusClientTest.ValidateResponse(template);
            }
            catch (AggregateException exception)
            {
                Assert.Fail(exception.ToString());
            }
        }

        /// <summary>
        /// Tests the API call POST /templates/(:template_id)/locales with all parameters
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [TestMethod]
        public async Task TestAddLocaleToTemplateWithAllParmetersAsync()
        {
            // Create a new template to add a locale to
            // Otherwise, if an existing template were used, this test might fail because the new locale could already exist on the template
            var newTemplate = await BuildAndSendCreateTemplateRequestWithAllParametersAsync();
            var templateId = newTemplate.id;

            // Make the API call
            Trace.WriteLine(String.Format("POST /templates/{0}/locales", templateId));
            var templateVersionName = "Published French Version";
            var templateSubject = "Ce est un nouveau modèle!";
            var updatedTemplateVersion = new TemplateVersion(templateVersionName, templateSubject);
            updatedTemplateVersion.html = "<html><head></head><body><h1>Nouveau modèle!</h1></body></html>";
            updatedTemplateVersion.text = "un texte";

            try
            { 
                var template = await Template.AddLocaleToTemplate(templateId, ALTERNATE_LOCALE, updatedTemplateVersion);

                // Validate the response
                SendwithusClientTest.ValidateResponse(template);
            }
            catch (AggregateException exception)
            {
                Assert.Fail(exception.ToString());
            }
        }

        /// <summary>
        /// Tests the API call POST /templates/(:template_id)/versions with minimum parameters using html, not text
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [TestMethod]
        public async Task TestCreateTemplateVersionWithMinimumParametersHtmlAsync()
        {
            // Make the API call
            Trace.WriteLine(String.Format("POST /templates/{0}/versions", DEFAULT_TEMPLATE_ID));
            var templateVersionName = "New Template Version";
            var templateSubject = "New Version!";
            var updatedTemplateVersion = new TemplateVersion(templateVersionName, templateSubject);
            updatedTemplateVersion.html = "<html><head></head><body><h1>NEW TEMPLATE VERSION</h1></body></html>";
            try
            {
                var templateVersion = await Template.CreateTemplateVersion(DEFAULT_TEMPLATE_ID, updatedTemplateVersion);

                // Validate the response
                SendwithusClientTest.ValidateResponse(templateVersion);
            }
            catch (AggregateException exception)
            {
                Assert.Fail(exception.ToString());
            }
        }

        /// <summary>
        /// Tests the API call POST /templates/(:template_id)/versions with minimum parameters using text, not html
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [TestMethod]
        public async Task TestCreateTemplateVersionWithMinimumParametersTextAsync()
        {
            // Make the API call
            Trace.WriteLine(String.Format("POST /templates/{0}/versions", DEFAULT_TEMPLATE_ID));
            var templateVersionName = "New Template Version";
            var templateSubject = "New Version!";
            var updatedTemplateVersion = new TemplateVersion(templateVersionName, templateSubject);
            updatedTemplateVersion.text = "some text";
            try
            {
                var templateVersion = await Template.CreateTemplateVersion(DEFAULT_TEMPLATE_ID, updatedTemplateVersion);

                // Validate the response
                SendwithusClientTest.ValidateResponse(templateVersion);
            }
            catch (AggregateException exception)
            {
                Assert.Fail(exception.ToString());
            }
        }

        /// <summary>
        /// Tests the API call POST /templates/(:template_id)/versions with all parameters
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [TestMethod]
        public async Task TestCreateTemplateVersionWithAllParametersAsync()
        {
            // Make the API call
            Trace.WriteLine(String.Format("POST /templates/{0}/versions", DEFAULT_TEMPLATE_ID));
            var templateVersionName = "New Template Version";
            var templateSubject = "New Version!";
            var updatedTemplateVersion = new TemplateVersion(templateVersionName, templateSubject);
            updatedTemplateVersion.html = "<html><head></head><body><h1>NEW TEMPLATE VERSION</h1></body></html>";
            updatedTemplateVersion.text = "some text";
            try
            {
                var templateVersion = await Template.CreateTemplateVersion(DEFAULT_TEMPLATE_ID, updatedTemplateVersion);

                // Validate the response
                SendwithusClientTest.ValidateResponse(templateVersion);
            }
            catch (AggregateException exception)
            {
                Assert.Fail(exception.ToString());
            }
        }

        /// <summary>
        /// Tests the API call POST /templates/(:template_id)/locales/(:locale)/versions with the minimum parameters, using HTML instead of text
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [TestMethod]
        public async Task TestCreateTemplateVersionWithLocaleWithMinimumParametersHtmlAsync()
        {
            // Make the API call
            Trace.WriteLine(String.Format("POST /templates/{0}/locales/{1}/versions", DEFAULT_TEMPLATE_ID, DEFAULT_LOCALE));
            var templateVersionName = "New Template Version";
            var templateSubject = "New Version!";
            var updatedTemplateVersion = new TemplateVersion(templateVersionName, templateSubject);
            updatedTemplateVersion.html = "<html><head></head><body><h1>NEW TEMPLATE VERSION</h1></body></html>";
            try
            {
                var templateVersion = await Template.CreateTemplateVersion(DEFAULT_TEMPLATE_ID, DEFAULT_LOCALE, updatedTemplateVersion);

                // Validate the response
                SendwithusClientTest.ValidateResponse(templateVersion);
            }
            catch (AggregateException exception)
            {
                Assert.Fail(exception.ToString());
            }
        }

        /// <summary>
        /// Tests the API call POST /templates/(:template_id)/locales/(:locale)/versions with the minimum parameters, using text instead of HTML
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [TestMethod]
        public async Task TestCreateTemplateVersionWithLocaleWithMinimumParametersTextAsync()
        {
            // Make the API call
            Trace.WriteLine(String.Format("POST /templates/{0}/locales/{1}/versions", DEFAULT_TEMPLATE_ID, DEFAULT_LOCALE));
            var templateVersionName = "New Template Version";
            var templateSubject = "New Version!";
            var updatedTemplateVersion = new TemplateVersion(templateVersionName, templateSubject);
            updatedTemplateVersion.text = "some text";
            try
            {
                var templateVersion = await Template.CreateTemplateVersion(DEFAULT_TEMPLATE_ID, DEFAULT_LOCALE, updatedTemplateVersion);

                // Validate the response
                SendwithusClientTest.ValidateResponse(templateVersion);
            }
            catch (AggregateException exception)
            {
                Assert.Fail(exception.ToString());
            }
        }

        /// <summary>
        /// Tests the API call POST /templates/(:template_id)/locales/(:locale)/versions with all parameters
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [TestMethod]
        public async Task TestCreateTemplateVersionWithLocaleWithAllParametersAsync()
        {
            // Make the API call
            Trace.WriteLine(String.Format("POST /templates/{0}/locales/{1}/versions", DEFAULT_TEMPLATE_ID, DEFAULT_LOCALE));
            var templateVersionName = "New Template Version";
            var templateSubject = "New Version!";
            var updatedTemplateVersion = new TemplateVersion(templateVersionName, templateSubject);
            updatedTemplateVersion.html = "<html><head></head><body><h1>NEW TEMPLATE VERSION</h1></body></html>";
            updatedTemplateVersion.text = "some text";
            try
            { 
                var templateVersion = await Template.CreateTemplateVersion(DEFAULT_TEMPLATE_ID, DEFAULT_LOCALE, updatedTemplateVersion);

                // Validate the response
                SendwithusClientTest.ValidateResponse(templateVersion);
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
            var newTemplate = await BuildAndSendCreateTemplateRequestWithAllParametersAsync();
            var templateId = newTemplate.id;

            // Make the API call
            Trace.WriteLine(String.Format("DELETE /templates/{0}", templateId));
            try
            { 
                var genericApiCallStatus = await Template.DeleteTemplate(templateId);

                // Validate the response
                SendwithusClientTest.ValidateResponse(genericApiCallStatus);
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
            var newTemplate = await BuildAndSendCreateTemplateRequestWithAllParametersAsync();
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

        /// <summary>
        /// Builds a new template with all parameters and sends the Create Template request
        /// </summary>
        /// <returns>The response to the Create Template API request</returns>
        public static async Task<Template> BuildAndSendCreateTemplateRequestWithAllParametersAsync()
        {
            var templateVersionName = "New Template Version";
            var templateSubject = "New Version!";
            var updatedTemplateVersion = new TemplateVersion(templateVersionName, templateSubject);
            updatedTemplateVersion.html = "<html><head></head><body><h1>NEW TEMPLATE VERSION</h1></body></html>";
            updatedTemplateVersion.text = "some text";
            updatedTemplateVersion.locale = DEFAULT_LOCALE;
            return await Template.CreateTemplateAsync(updatedTemplateVersion);
        }

        /// <summary>
        /// Builds a new template with the minimum parameters and sends the Create Template request
        /// </summary>
        /// <returns>The response to the Create Template API request</returns>
        public static async Task<Template> BuildAndSendCreateTemplateRequestWithMinimumParametersAsync()
        {
            var templateVersionName = "New Template Version";
            var templateSubject = "New Version!";
            var updatedTemplateVersion = new TemplateVersion(templateVersionName, templateSubject);
            return await Template.CreateTemplateAsync(updatedTemplateVersion);
        }
    }
}
