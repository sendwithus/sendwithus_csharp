using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;
using Sendwithus;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;

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

        private static Collection<string> NewTemplateIds = new Collection<string>();

        /// <summary>
        /// Tests the GET /templates with an invalid API Key
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [TestMethod]
        public async Task TestGetTeamplatesWithInvalidApiKeyAsync()
        {
            // Make the API call
            Trace.WriteLine("GET /templates with invalid API Key");
            Sendwithus.SendwithusClient.ApiKey = "";
            try
            {
                var response = await Template.GetTemplatesAsync();
            }
            catch (SendwithusException exception)
            {
                // Make sure the response was HTTP 403 Forbidden
                SendwithusClientTest.ValidateException(exception, HttpStatusCode.Forbidden);
            }
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
            Sendwithus.SendwithusClient.ApiKey = SendwithusClientTest.API_KEY_TEST;
            var response = await Template.GetTemplatesAsync();

            // Validate the response
            SendwithusClientTest.ValidateResponse(response);
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
            Sendwithus.SendwithusClient.ApiKey = SendwithusClientTest.API_KEY_TEST;
            var response = await Template.GetTemplateAsync(DEFAULT_TEMPLATE_ID);

            // Validate the response
            SendwithusClientTest.ValidateResponse(response);
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
            Sendwithus.SendwithusClient.ApiKey = SendwithusClientTest.API_KEY_TEST;
            try
            {
                var response = await Template.GetTemplateAsync(INVALID_TEMPLATE_ID);
                Assert.Fail("Failed to throw exception");
            }
            catch (SendwithusException exception)
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
            Sendwithus.SendwithusClient.ApiKey = SendwithusClientTest.API_KEY_TEST;
            var response = await Template.GetTemplateAsync(DEFAULT_TEMPLATE_ID, DEFAULT_LOCALE);

            // Validate the response
            SendwithusClientTest.ValidateResponse(response);
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
            Sendwithus.SendwithusClient.ApiKey = SendwithusClientTest.API_KEY_TEST;
            try
            {
                var response = await Template.GetTemplateAsync(DEFAULT_TEMPLATE_ID, INVALID_LOCALE);
                Assert.Fail("Failed to throw exception");
            }
            catch (SendwithusException exception)
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
            Sendwithus.SendwithusClient.ApiKey = SendwithusClientTest.API_KEY_TEST;
            var response = await Template.GetTemplateVersionsAsync(DEFAULT_TEMPLATE_ID);

            // Validate the response
            SendwithusClientTest.ValidateResponse(response);
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
            Sendwithus.SendwithusClient.ApiKey = SendwithusClientTest.API_KEY_TEST;
            var response = await Template.GetTemplateVersionsAsync(DEFAULT_TEMPLATE_ID, DEFAULT_LOCALE);

            // Validate the response
            SendwithusClientTest.ValidateResponse(response);
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
            Sendwithus.SendwithusClient.ApiKey = SendwithusClientTest.API_KEY_TEST;
            var response = await Template.GetTemplateVersionAsync(DEFAULT_TEMPLATE_ID, DEFAULT_VERSION_ID);

            // Validate the response
            SendwithusClientTest.ValidateResponse(response);
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
            Sendwithus.SendwithusClient.ApiKey = SendwithusClientTest.API_KEY_TEST;
            var response = await Template.GetTemplateVersionAsync(DEFAULT_TEMPLATE_ID, DEFAULT_LOCALE, DEFAULT_VERSION_ID);

            // Validate the response
            SendwithusClientTest.ValidateResponse(response);
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
            Sendwithus.SendwithusClient.ApiKey = SendwithusClientTest.API_KEY_TEST;
            var updatedTemplateVersion = new TemplateVersion();
            updatedTemplateVersion.name = "New Version";
            updatedTemplateVersion.subject = "edited!";
            updatedTemplateVersion.html = "<html><head></head><body><h1>UPDATE</h1></body></html>";
            updatedTemplateVersion.text = "sometext";
            var response = await Template.UpdateTemplateVersionAsync(DEFAULT_TEMPLATE_ID, DEFAULT_VERSION_ID, updatedTemplateVersion);

            // Validate the response
            SendwithusClientTest.ValidateResponse(response);
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
            Sendwithus.SendwithusClient.ApiKey = SendwithusClientTest.API_KEY_TEST;
            var updatedTemplateVersion = new TemplateVersion();
            updatedTemplateVersion.name = "New Version";
            updatedTemplateVersion.subject = "edited!";
            updatedTemplateVersion.html = "<html><head></head><body><h1>UPDATE</h1></body></html>";
            updatedTemplateVersion.text = "sometext";
            var response = await Template.UpdateTemplateVersionAsync(DEFAULT_TEMPLATE_ID, DEFAULT_LOCALE, DEFAULT_VERSION_ID, updatedTemplateVersion);

            // Validate the response
            SendwithusClientTest.ValidateResponse(response);
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
            Sendwithus.SendwithusClient.ApiKey = SendwithusClientTest.API_KEY_TEST;
            var newTemplateVersion = new TemplateVersion();
            newTemplateVersion.name = "New Template";
            newTemplateVersion.subject = "This is a new template!";
            newTemplateVersion.html = "<html><head></head><body><h1>NEW TEMPLATE</h1></body></html>";
            newTemplateVersion.text = "some text";
            var response = await Template.CreateTemplateAsync(newTemplateVersion);

            // Add the new template to the list of templates for deletion
            NewTemplateIds.Add(response.id);

            // Validate the response
            SendwithusClientTest.ValidateResponse(response);
        }

        /// <summary>
        /// Tests the API call POST /templates/(:template_id)/locales
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [TestMethod]
        public async Task TestAddLocaleToTemplateAsync()
        {
            // Use the template ID of a newly created template
            // Otherwise, this test might fail because the new locale could already exist on the template
            string templateId = String.Empty;
            if (NewTemplateIds.Count > 0)
            {
                templateId = NewTemplateIds[0];
            }
            else
            {
                Assert.Fail("No new templates available to add a locale to");
            }

            // Make the API call
            Trace.WriteLine(String.Format("PUT /templates/"));
            Sendwithus.SendwithusClient.ApiKey = SendwithusClientTest.API_KEY_TEST;
            var templateVersion = new TemplateVersion();
            templateVersion.name = "Published French Version";
            templateVersion.subject = "Ce est un nouveau modèle!";
            templateVersion.html = "<html><head></head><body><h1>Nouveau modèle!</h1></body></html>";
            templateVersion.text = "un texte";
            var response = await Template.AddLocaleToTemplate(templateId, ALTERNATE_LOCALE, templateVersion);

            // Validate the response
            SendwithusClientTest.ValidateResponse(response);
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
            Sendwithus.SendwithusClient.ApiKey = SendwithusClientTest.API_KEY_TEST;
            var templateVersion = new TemplateVersion();
            templateVersion.name = "New Template Version";
            templateVersion.subject = "New Version!";
            templateVersion.html = "<html><head></head><body><h1>NEW TEMPLATE VERSION</h1></body></html>";
            templateVersion.text = "some text";
            var response = await Template.CreateTemplateVersion(DEFAULT_TEMPLATE_ID, templateVersion);

            // Validate the response
            SendwithusClientTest.ValidateResponse(response);
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
            Sendwithus.SendwithusClient.ApiKey = SendwithusClientTest.API_KEY_TEST;
            var templateVersion = new TemplateVersion();
            templateVersion.name = "New Template Version";
            templateVersion.subject = "New Version!";
            templateVersion.html = "<html><head></head><body><h1>NEW TEMPLATE VERSION</h1></body></html>";
            templateVersion.text = "some text";
            var response = await Template.CreateTemplateVersion(DEFAULT_TEMPLATE_ID, DEFAULT_LOCALE, templateVersion);

            // Validate the response
            SendwithusClientTest.ValidateResponse(response);
        }

        /// <summary>
        /// Tests the API call DELETE /templates/(:template_id)
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [TestMethod]
        public async Task TestDeleteTemplateAsync()
        {
            // Use a newly added Template ID for deletion
            string templateId = String.Empty;
            if (NewTemplateIds.Count > 0)
            {
                templateId = NewTemplateIds[0];
                NewTemplateIds.RemoveAt(0);
            }
            else
            {
                Assert.Fail("No template IDs available to delete");
            }

            // Make the API call
            Trace.WriteLine(String.Format("DELETE /templates/{0}", templateId));
            Sendwithus.SendwithusClient.ApiKey = SendwithusClientTest.API_KEY_TEST;
            var response = await Template.DeleteTemplate(templateId);

            // Validate the response
            SendwithusClientTest.ValidateResponse(response);

            // Replace the deleted template ID in case a new template is needed in other test functions
            await TestCreateTemplateAsync();
        }

        /// <summary>
        /// Tests the API call DELETE /templates/(:template_id)/locales/(:locale)
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [TestMethod]
        public async Task TestDeleteTemplateWithLocaleAsync()
        {
            // Use a newly added Template ID for deletion
            string templateId = String.Empty;
            if (NewTemplateIds.Count > 0)
            {
                templateId = NewTemplateIds[0];
            }
            else
            {
                Assert.Fail("No template IDs available to delete");
            }

            // Make the API call
            Trace.WriteLine(String.Format("DELETE /templates/{0}/locales/{1}", templateId, DEFAULT_LOCALE));
            Sendwithus.SendwithusClient.ApiKey = SendwithusClientTest.API_KEY_TEST;
            var response = await Template.DeleteTemplate(templateId, DEFAULT_LOCALE);

            // Validate the response
            SendwithusClientTest.ValidateResponse(response);

            // Replace the deleted template ID in case a new template is needed in other test functions
            await TestCreateTemplateAsync();
        }
    }
}
