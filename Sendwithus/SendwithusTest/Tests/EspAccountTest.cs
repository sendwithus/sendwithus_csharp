using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sendwithus;
using System.Net;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SendwithusTest
{
    /// <summary>
    /// Unit tests for the sendwithus EspAccount API calls
    /// </summary>
    [TestClass]
    public class EspAccountTest
    {
        private const string DEFAULT_ESP_ACCOUNT_ID = "esp_e3ut7pFtWttcN4HNoQ8Vgm";
        private const string DEFAULT_ESP_ACCOUNT_TYPE = "sendgrid";
        private const int INVALID_ESP_ACCOUNT_TYPE = 12345;
        private const string SENDGRID_USERNAME = "sendwithus.test@gmail.com";
        private const string SENDGRID_PASSWORD = "^5eMR863*0tmP8qLD&IT";
        private const string MAILGUN_API_KEY = "key-b9b1f8b90e355a3ff10e076411c23dd3";
        private const string MAILGUN_DOMAIN = "sandboxcab24e8a41e2413e8e0c0c7f9e6e312a.mailgun.org";
        private const string MANDRILL_API_KEY = "EFwe7N8whG3pjDR0asmCcw";
        private const string POSTMARK_API_KEY = "30feb45f-e6f9-471f-b38e-696d5d7ae419";
        private const string SES_ACCESS_KEY_ID = "mysesaccesskeyid"; // NOTE: this will fail authentication. To properly test, add your own SES Access Key ID
        private const string SES_SECRET_ACCESS_KEY = "mysessecretaccesskey"; // NOTE: this will fail authentication.  To properly test, add your own SES Secret Access Key
        private const string SES_REGION = "us-east-1";
        private const string MAILJET_API_KEY = "4c3b9d91df19868f29c6b37a37fea0c2";
        private const string MAILJET_SECRET_KEY = "ad26f1fbe609090561b38012bf57d91d";
        private const string DYN_API_KEY = "mydynapikey"; // NOTE: this will fail authentication.  To properly test, add your own DYN API Key
        private const string SMTP_HOST = "smtp.mandrillapp.com"; // Use Mandrill's SMTP server for testing
        private const int SMTP_PORT = 587;
        private const string SMTP_USERNAME = "sendwithus";
        private const string SMTP_PASSWORD = MANDRILL_API_KEY;

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
        /// Tests the API call GET /esp_accounts without any parameters
        /// </summary>
        /// <returns>The associated task</returns>
        [TestMethod]
        public async Task TestGetEspAccountsWithoutParametersAsync()
        {
            Trace.WriteLine("GET /esp_accounts");

            // Make the API call
            try
            { 
                var response = await EspAccount.GetAccountsAsync();

                // Validate the response
                SendwithusClientTest.ValidateResponse(response);
            }
            catch (AggregateException exception)
            {
                Assert.Fail(exception.ToString());
            }
        }

        /// <summary>
        /// Tests the API call GET /esp_accounts with all parameters
        /// </summary>
        /// <returns>The associated task</returns>
        [TestMethod]
        public async Task TestGetEspAccountsWithAllParametersAsync()
        {
            Trace.WriteLine("GET /esp_accounts");

            // Build the query parameters
            var queryParameters = new Dictionary<string, object>();
            queryParameters.Add("esp_type", DEFAULT_ESP_ACCOUNT_TYPE);

            // Make the API call
            try
            { 
                var response = await EspAccount.GetAccountsAsync(queryParameters);

                // Validate the response
                SendwithusClientTest.ValidateResponse(response);
            }
            catch (AggregateException exception)
            {
                Assert.Fail(exception.ToString());
            }
        }

        /// <summary>
        /// Tests the API call GET /esp_accounts with an invalid parameter
        /// </summary>
        /// <returns>The associated task</returns>
        [TestMethod]
        public async Task TestGetEspAccountsWithInvalidParameterAsync()
        {
            Trace.WriteLine("GET /esp_accounts");

            // Build the query parameters
            var queryParameters = new Dictionary<string, object>();
            queryParameters.Add("esp_type", INVALID_ESP_ACCOUNT_TYPE);

            // Make the API call
            try
            {
                var response = await EspAccount.GetAccountsAsync(queryParameters);
                Assert.Fail("Failed to throw exception");
            }
            catch (AggregateException exception)
            {
                // Make sure the response was HTTP 400 Bad Request
                SendwithusClientTest.ValidateException(exception, HttpStatusCode.BadRequest);
            }
        }

        /// <summary>
        /// Tests the API call POST /esp_accounts with a sendgrid account
        /// </summary>
        /// <returns>The associated task</returns>
        [TestMethod]
        public async Task TestAddAccountSendgridAsync()
        {
            Trace.WriteLine("POST /esp_accounts");

            // Build the Add Account Request for a sendgrid account
            var credentials = new EspAccountCredientialsSendgrid(SENDGRID_USERNAME, SENDGRID_PASSWORD);
            var addAccountRequest = new EspAccountAddAccountRequest("My SendGrid Account", "sendgrid", credentials);

            // Make the API call
            try
            { 
                var response = await EspAccount.AddAccountAsync(addAccountRequest);

                // Validate the response
                SendwithusClientTest.ValidateResponse(response);
            }
            catch (AggregateException exception)
            {
                Assert.Fail(exception.ToString());
            }
        }

        /// <summary>
        /// Tests the API call POST /esp_accounts with a mailgun account
        /// </summary>
        /// <returns>The associated task</returns>
        [TestMethod]
        public async Task TestAddAccountMailgunAsync()
        {
            Trace.WriteLine("POST /esp_accounts");

            // Build the Add Account Request for a mailgun account
            var credentials = new EspAccountCredientialsMailgun(MAILGUN_API_KEY, MAILGUN_DOMAIN);
            var addAccountRequest = new EspAccountAddAccountRequest("My Mailgun Account", "mailgun", credentials);

            // Make the API call
            try
            { 
                var response = await EspAccount.AddAccountAsync(addAccountRequest);

                // Validate the response
                SendwithusClientTest.ValidateResponse(response);
            }
            catch (AggregateException exception)
            {
                Assert.Fail(exception.ToString());
            }
        }

        // <summary>
        /// Tests the API call POST /esp_accounts with a mandrill account
        /// </summary>
        /// <returns>The associated task</returns>
        [TestMethod]
        public async Task TestAddAccountMandrillAsync()
        {
            Trace.WriteLine("POST /esp_accounts");

            // Build the Add Account Request for a mailgun account
            var credentials = new EspAccountCredientialsMandrill(MANDRILL_API_KEY);
            var addAccountRequest = new EspAccountAddAccountRequest("My Mandrill Account", "mandrill", credentials);

            // Make the API call
            try
            {
                var response = await EspAccount.AddAccountAsync(addAccountRequest);

                // Validate the response
                SendwithusClientTest.ValidateResponse(response);
            }
            catch (AggregateException exception)
            {
                Assert.Fail(exception.ToString());
            }
        }

        // <summary>
        /// Tests the API call POST /esp_accounts with a postmark account
        /// </summary>
        /// <returns>The associated task</returns>
        [TestMethod]
        public async Task TestAddAccountPostmarkAsync()
        {
            Trace.WriteLine("POST /esp_accounts");

            // Build the Add Account Request for a mailgun account
            var credentials = new EspAccountCredientialsPostmark(POSTMARK_API_KEY);
            var addAccountRequest = new EspAccountAddAccountRequest("My Postmark Account", "postmark", credentials);

            // Make the API call
            try
            { 
                var response = await EspAccount.AddAccountAsync(addAccountRequest);

                // Validate the response
                SendwithusClientTest.ValidateResponse(response);
            }
            catch (AggregateException exception)
            {
                Assert.Fail(exception.ToString());
            }
        }

        // <summary>
        /// Tests the API call POST /esp_accounts with an SES account
        /// Expected to fail on authorization as proper SES credentials aren't included here
        /// </summary>
        /// <returns>The associated task</returns>
        [TestMethod]
        public async Task TestAddAccountSesAsync()
        {
            Trace.WriteLine("POST /esp_accounts");

            // Build the Add Account Request for a mailgun account
            var credentials = new EspAccountCredientialsSes(SES_ACCESS_KEY_ID, SES_SECRET_ACCESS_KEY, SES_REGION);
            var addAccountRequest = new EspAccountAddAccountRequest("My SES Account", "ses", credentials);

            // Make the API call
            try
            {
                var response = await EspAccount.AddAccountAsync(addAccountRequest);
            }
            catch (AggregateException exception)
            {
                // Make sure the response was HTTP 400 Bad Request (because of an authorization error)
                // This means that we formatted the API call correctly and only failed because we don't
                // have proper authorization credentials to test an SES account
                SendwithusClientTest.ValidateException(exception, HttpStatusCode.BadRequest);
            }
        }

        // <summary>
        /// Tests the API call POST /esp_accounts with a Mailjet account
        /// </summary>
        /// <returns>The associated task</returns>
        [TestMethod]
        public async Task TestAddAccountMailjetAsync()
        {
            Trace.WriteLine("POST /esp_accounts");

            // Build the Add Account Request for a mailgun account
            var credentials = new EspAccountCredientialsMailjet(MAILJET_API_KEY, MAILJET_SECRET_KEY);
            var addAccountRequest = new EspAccountAddAccountRequest("My Mailjet Account", "mailjet", credentials);

            // Make the API call
            try
            { 
                var response = await EspAccount.AddAccountAsync(addAccountRequest);

                // Validate the response
                SendwithusClientTest.ValidateResponse(response);
            }
            catch (AggregateException exception)
            {
                Assert.Fail(exception.ToString());
            }
        }

        // <summary>
        /// Tests the API call POST /esp_accounts with a DYN account
        /// Expected to fail on authorization as proper DYN credentials aren't included here
        /// </summary>
        /// <returns>The associated task</returns>
        [TestMethod]
        public async Task TestAddAccountDynAsync()
        {
            Trace.WriteLine("POST /esp_accounts");

            // Build the Add Account Request for a DYN account
            var credentials = new EspAccountCredientialsDyn(DYN_API_KEY);
            var addAccountRequest = new EspAccountAddAccountRequest("My DYN Account", "dyn", credentials);

            // Make the API call
            try
            {
                var response = await EspAccount.AddAccountAsync(addAccountRequest);
            }
            catch (AggregateException exception)
            {
                // Make sure the response was HTTP 400 Bad Request (because of an authorization error)
                // This means that we formatted the API call correctly and only failed because we don't
                // have proper authorization credentials to test a DYN account
                SendwithusClientTest.ValidateException(exception, HttpStatusCode.BadRequest);
            }
        }

        // <summary>
        /// Tests the API call POST /esp_accounts with an SMTP account
        /// </summary>
        /// <returns>The associated task</returns>
        [TestMethod]
        public async Task TestAddAccountSmtpAsync()
        {
            Trace.WriteLine("POST /esp_accounts");

            // Build the Add Account Request for a DYN account
            var credentials = new EspAccountCredientialsSmtp(SMTP_HOST, SMTP_PORT, SMTP_USERNAME, SMTP_PASSWORD, false);
            var addAccountRequest = new EspAccountAddAccountRequest("My SMTP Account", "smtp", credentials);

            // Make the API call
            try
            { 
                var response = await EspAccount.AddAccountAsync(addAccountRequest);

                // Validate the response
                SendwithusClientTest.ValidateResponse(response);
            }
            catch (AggregateException exception)
            {
                Assert.Fail(exception.ToString());
            }
        }

        // <summary>
        /// Tests the API call PUT /esp_accounts/set_default
        /// </summary>
        /// <returns>The associated task</returns>
        [TestMethod]
        public async Task TestSetDefaultEspAccountAsync()
        {
            Trace.WriteLine("PUT /esp_accounts/set_default");

            // Make the API call
            try
            { 
                var response = await EspAccount.SetDefaultEspAccountAsync(DEFAULT_ESP_ACCOUNT_ID);

                // Validate the response
                SendwithusClientTest.ValidateResponse(response);
            }
            catch (AggregateException exception)
            {
                Assert.Fail(exception.ToString());
            }
        }
    }
}
