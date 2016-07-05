using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;
using SendWithUs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Script.Serialization;
using System.Net;

namespace SendWithUsTest
{
    [TestClass]
    public class SendWithUsTest
    {
        private const string API_KEY_TEST = "test_3e7ae15aeb9b8a4b50bce7138c88d81c696edd0d";

        /// <summary>
        /// Tests an invalid API Key
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [TestMethod]
        public async Task TestInvalidApiKeyAsync()
        {
            // Make the API call
            Trace.WriteLine("GET /templates with invalid API Key");
            SendWithUs.SendWithUs.ApiKey = "";
            try
            {
                var response = await Template.GetTemplatesAsync();
            }
            catch (SendWithUsException exception)
            {
                // Make sure the response was HTTP 403 Forbidden
                ValidateException(exception, HttpStatusCode.Forbidden);
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
            SendWithUs.SendWithUs.ApiKey = API_KEY_TEST;
            var response = await Template.GetTemplatesAsync();

            // Validate the response
            ValidateResponse(response);
        }

        /// <summary>
        /// Tests the GET /templates/(:template_id) API call
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [TestMethod]
        public async Task TestGetTemplateByIdAsync()
        {
            // Make the API call
            const string TEMPLATE_ID = "tem_SxZKpxJSHPbYDWRSQnAQUR";
            Trace.WriteLine(String.Format("GET /templates/{0}", TEMPLATE_ID));
            SendWithUs.SendWithUs.ApiKey = API_KEY_TEST;
            var response = await Template.GetTemplateAsync(TEMPLATE_ID);

            // Validate the response
            ValidateResponse(response);
        }

        /// <summary>
        /// Tests the GET /templates/(:template_id) API call with an invalid ID
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [TestMethod]
        public async Task TestGetTemplateByIdInvalidIDAsync()
        {
            // Make the API call
            const string TEMPLATE_ID = "invalid_id";
            Trace.WriteLine(String.Format("GET /templates/{0} with invalid ID", TEMPLATE_ID));
            SendWithUs.SendWithUs.ApiKey = API_KEY_TEST;
            try
            {
                var response = await Template.GetTemplateAsync(TEMPLATE_ID);
                Assert.Fail("Failed to throw exception");
            }
            catch (SendWithUsException exception)
            {
                // Make sure the response was HTTP 400 Bad Request 
                ValidateException(exception, HttpStatusCode.BadRequest);
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
            const string TEMPLATE_ID = "tem_SxZKpxJSHPbYDWRSQnAQUR";
            const string LOCALE = "en-US";
            Trace.WriteLine(String.Format("GET /templates/{0}/locales/{1}", TEMPLATE_ID, LOCALE));
            SendWithUs.SendWithUs.ApiKey = API_KEY_TEST;
            var response = await Template.GetTemplateAsync(TEMPLATE_ID, LOCALE);

            // Validate the response
            ValidateResponse(response);
        }

        /// <summary>
        /// Tests the GET /templates/(:template_id)/locales/(:locale) API call
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [TestMethod]
        public async Task TestGetTemplateByIdAndLocaleInvalidLocaleAsync()
        {
            // Make the API call
            const string TEMPLATE_ID = "tem_SxZKpxJSHPbYDWRSQnAQUR";
            const string LOCALE = "invalid_locale";
            Trace.WriteLine(String.Format("GET /templates/{0}/locales/{1} with invalid locale", TEMPLATE_ID, LOCALE));
            SendWithUs.SendWithUs.ApiKey = API_KEY_TEST;
            try
            {
                var response = await Template.GetTemplateAsync(TEMPLATE_ID, LOCALE);
                Assert.Fail("Failed to throw exception");
            }
            catch (SendWithUsException exception)
            {
                // Make sure the response was HTTP 400 Bad Request
                ValidateException(exception, HttpStatusCode.BadRequest);
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
            const string TEMPLATE_ID = "tem_SxZKpxJSHPbYDWRSQnAQUR";
            Trace.WriteLine(String.Format("GET /templates/{0}/versions", TEMPLATE_ID));
            SendWithUs.SendWithUs.ApiKey = API_KEY_TEST;
            var response = await Template.GetTemplateVersionsAsync(TEMPLATE_ID);

            // Validate the response
            ValidateResponse(response);
        }

        /// <summary>
        /// Tests the GET /templates/(:template_id)/locales/(:locale)/versions API call
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [TestMethod]
        public async Task TestGetTemplateVersionsByIdAndLocaleAsync()
        {
            // Make the API call
            const string TEMPLATE_ID = "tem_SxZKpxJSHPbYDWRSQnAQUR";
            const string LOCALE = "en-US";
            Trace.WriteLine(String.Format("GET /templates/{0}/locales/{1}/versions", TEMPLATE_ID, LOCALE));
            SendWithUs.SendWithUs.ApiKey = API_KEY_TEST;
            var response = await Template.GetTemplateVersionsAsync(TEMPLATE_ID, LOCALE);

            // Validate the response
            ValidateResponse(response);
        }

        /// <summary>
        /// Tests the GET /templates/(:template_id)/versions/(:version_id) API call
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [TestMethod]
        public async Task TestGetTemplateVersionByIdAsync()
        {
            // Make the API call
            const string TEMPLATE_ID = "tem_SxZKpxJSHPbYDWRSQnAQUR";
            const string VERSION_ID = "ver_ET3j2snkKhqsjRjtK6bXJE";
            Trace.WriteLine(String.Format("GET /templates/{0}/versions/{1}", TEMPLATE_ID, VERSION_ID));
            SendWithUs.SendWithUs.ApiKey = API_KEY_TEST;
            var response = await Template.GetTemplateVersionAsync(TEMPLATE_ID, VERSION_ID);

            // Validate the response
            ValidateResponse(response);
        }

        /// <summary>
        /// Tests the GET /templates/(:template_id)/locales/(:locale)/versions/(:version_id) API call
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [TestMethod]
        public async Task TestGetTemplateVersionByIdAndLocaleAsync()
        {
            // Make the API call
            const string TEMPLATE_ID = "tem_SxZKpxJSHPbYDWRSQnAQUR";
            const string LOCALE = "en-US";
            const string VERSION_ID = "ver_ET3j2snkKhqsjRjtK6bXJE";
            Trace.WriteLine(String.Format("GET /templates/{0}/locales/{1}/versions/{2}", TEMPLATE_ID, LOCALE, VERSION_ID));
            SendWithUs.SendWithUs.ApiKey = API_KEY_TEST;
            var response = await Template.GetTemplateVersionAsync(TEMPLATE_ID, LOCALE, VERSION_ID);

            // Validate the response
            ValidateResponse(response);
        }

        /// <summary>
        /// Tests the API call PUT /templates/(:template_id)/versions/(:version_id)
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [TestMethod]
        public async Task TestUpdateTemplateVersionByIdAsync()
        {
            // Make the API call
            const string TEMPLATE_ID = "tem_SxZKpxJSHPbYDWRSQnAQUR";
            const string VERSION_ID = "ver_ET3j2snkKhqsjRjtK6bXJE";
            Trace.WriteLine(String.Format("PUT /templates/{0}/versions/{1}", TEMPLATE_ID, VERSION_ID));
            SendWithUs.SendWithUs.ApiKey = API_KEY_TEST;
            var updatedTemplateVersion = new Template.TemplateVersion();
            updatedTemplateVersion.name = "New Version";
            updatedTemplateVersion.subject = "edited!";
            updatedTemplateVersion.html = "<html><head></head><body><h1>UPDATE</h1></body></html>";
            updatedTemplateVersion.text = "sometext";
            var response = await Template.UpdateTemplateVersionAsync(TEMPLATE_ID, VERSION_ID, updatedTemplateVersion);

            // Validate the response
            ValidateResponse(response);
        }

        /// <summary>
        /// Tests the API call PUT /templates/(:template_id)/locales/(:locale)/versions/(:version_id)
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [TestMethod]
        public async Task TestUpdateTemplateVersionByIdAndLocaleAsync()
        {
            // Make the API call
            const string TEMPLATE_ID = "tem_SxZKpxJSHPbYDWRSQnAQUR";
            const string LOCALE = "en-US";
            const string VERSION_ID = "ver_ET3j2snkKhqsjRjtK6bXJE";
            Trace.WriteLine(String.Format("PUT /templates/{0}/locales/{1}/versions/{2}", TEMPLATE_ID, LOCALE, VERSION_ID));
            SendWithUs.SendWithUs.ApiKey = API_KEY_TEST;
            var updatedTemplateVersion = new Template.TemplateVersion();
            updatedTemplateVersion.name = "New Version";
            updatedTemplateVersion.subject = "edited!";
            updatedTemplateVersion.html = "<html><head></head><body><h1>UPDATE</h1></body></html>";
            updatedTemplateVersion.text = "sometext";
            var response = await Template.UpdateTemplateVersionAsync(TEMPLATE_ID, LOCALE, VERSION_ID, updatedTemplateVersion);

            // Validate the response
            ValidateResponse(response);
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
            SendWithUs.SendWithUs.ApiKey = API_KEY_TEST;
            var newTemplateVersion = new Template.TemplateVersion();
            newTemplateVersion.name = "New Template";
            newTemplateVersion.subject = "This is a new template!";
            newTemplateVersion.html = "<html><head></head><body><h1>NEW TEMPLATE</h1></body></html>";
            newTemplateVersion.text = "some text";
            var response = await Template.CreateTemplateAsync(newTemplateVersion);

            // Validate the response
            ValidateResponse(response);
        }

        /// <summary>
        /// Tests the API call POST /templates/(:template_id)/locales
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [TestMethod]
        public async Task TestAddLocaleToTemplateAsync()
        {
            // Make the API call
            const string TEMPLATE_ID = "tem_SxZKpxJSHPbYDWRSQnAQUR";
            const string LOCALE = "fr-FR";
            Trace.WriteLine(String.Format("PUT /templates/"));
            SendWithUs.SendWithUs.ApiKey = API_KEY_TEST;
            var templateVersion = new Template.TemplateVersion();
            templateVersion.name = "Published French Version";
            templateVersion.subject = "Ce est un nouveau modèle!";
            templateVersion.html = "<html><head></head><body><h1>Nouveau modèle!</h1></body></html>";
            templateVersion.text = "un texte";
            var response = await Template.AddLocaleToTemplate(TEMPLATE_ID, LOCALE, templateVersion);

            // Validate the response
            ValidateResponse(response);
        }

        /// <summary>
        /// Tests the API call POST /templates/(:template_id)/versions
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [TestMethod]
        public async Task TestCreateTemplateVersionAsync()
        {
            // Make the API call
            const string TEMPLATE_ID = "tem_SxZKpxJSHPbYDWRSQnAQUR";
            Trace.WriteLine(String.Format("POST /templates/{0}/versions", TEMPLATE_ID));
            SendWithUs.SendWithUs.ApiKey = API_KEY_TEST;
            var templateVersion = new Template.TemplateVersion();
            templateVersion.name = "New Template Version";
            templateVersion.subject = "New Version!";
            templateVersion.html = "<html><head></head><body><h1>NEW TEMPLATE VERSION</h1></body></html>";
            templateVersion.text = "some text";
            var response = await Template.CreateTemplateVersion(TEMPLATE_ID, templateVersion);

            // Validate the response
            ValidateResponse(response);
        }

        /// <summary>
        /// Tests the API call POST /templates/(:template_id)/locales/(:locale)/versions
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [TestMethod]
        public async Task TestCreateTemplateVersionWithLocaleAsync()
        {
            // Make the API call
            const string TEMPLATE_ID = "tem_SxZKpxJSHPbYDWRSQnAQUR";
            const string LOCALE = "en-US";
            Trace.WriteLine(String.Format("POST /templates/{0}/locales/{1}/versions", TEMPLATE_ID, LOCALE));
            SendWithUs.SendWithUs.ApiKey = API_KEY_TEST;
            var templateVersion = new Template.TemplateVersion();
            templateVersion.name = "New Template Version";
            templateVersion.subject = "New Version!";
            templateVersion.html = "<html><head></head><body><h1>NEW TEMPLATE VERSION</h1></body></html>";
            templateVersion.text = "some text";
            var response = await Template.CreateTemplateVersion(TEMPLATE_ID, LOCALE, templateVersion);

            // Validate the response
            ValidateResponse(response);
        }

        /// <summary>
        /// Tests the API call DELETE /templates/(:template_id)
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [TestMethod]
        public async Task TestDeleteTemplateAsync()
        {
            // Make the API call
            const string TEMPLATE_ID = "tem_yHHNkdrWvvePcjEmWBFvAD";
            Trace.WriteLine(String.Format("DELETE /templates/{0}", TEMPLATE_ID));
            SendWithUs.SendWithUs.ApiKey = API_KEY_TEST;
            var response = await Template.DeleteTemplate(TEMPLATE_ID);

            // Validate the response
            ValidateResponse(response);
        }

        /// <summary>
        /// Tests the API call DELETE /templates/(:template_id)/locales/(:locale)
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [TestMethod]
        public async Task TestDeleteTemplateWithLocaleAsync()
        {
            // Make the API call
            const string TEMPLATE_ID = "tem_VacVbTBmrLGajqo2Cd83Cd";
            const string LOCALE = "en-US";
            Trace.WriteLine(String.Format("DELETE /templates/{0}/locales/{1}", TEMPLATE_ID, LOCALE));
            SendWithUs.SendWithUs.ApiKey = API_KEY_TEST;
            var response = await Template.DeleteTemplate(TEMPLATE_ID, LOCALE);

            // Validate the response
            ValidateResponse(response);
        }


        /// <summary>
        /// Validates the response from an API call
        /// </summary>
        /// <param name="response">The api call's response</param>
        private void ValidateResponse(object response)
        {
            // Print the response
            var serializer = new JavaScriptSerializer();
            Trace.Write("Response: ");
            Trace.WriteLine(serializer.Serialize(response));
            Trace.Flush();

            // Validate the response
            Assert.IsNotNull(response);
        }

        /// <summary>
        /// Validates that the correct exception was thrown from an API call
        /// </summary>
        /// <param name="exception">The exception to validate</param>
        /// <param name="stausCode">The expected exception status code</param>
        private void ValidateException(SendWithUsException exception, HttpStatusCode expectedStatusCode)
        {
            // Print the exception details
            Trace.Write("Exception Status Code: ");
            Trace.WriteLine(exception.StatusCode.ToString());
            Trace.Write("Exception Message: ");
            Trace.WriteLine(exception.Message);

            // Check the exception's status code
            Assert.AreEqual(exception.StatusCode, expectedStatusCode);
        }
    }
}
