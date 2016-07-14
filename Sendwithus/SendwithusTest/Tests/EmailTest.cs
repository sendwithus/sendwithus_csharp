using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sendwithus;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Net;
using Xunit;
using Xunit.Abstractions;

namespace SendwithusTest
{
    /// <summary>
    /// Unit testing class for the Email API calls
    /// </summary>
    public class EmailTest
    {
        private const string ESP_ACCOUNT = "esp_EsgkbqQdDg7F3ncbz9EHW7";
        private const string DEFAULT_TEMPLATE_ID = "tem_yn2viZ8Gm2uaydMK9pgR2B";
        private const string INVALID_TEMPLATE_ID = "invalid_template_id";
        private const string DEFAULT_LOCALE = "en-US";
        private const string DEFAULT_SENDER_EMAIL_ADDRESS = "sendwithus.test+sender@gmail.com";
        private const string DEFAULT_REPLY_TO_EMAIL_ADDRESS = "sendwithus.test+replyto@gmail.com";
        private const string DEFAULT_RECIPIENT_EMAIL_ADDRESS = "sendwithus.test+recipient@gmail.com";
        private const string DEFAULT_CC_EMAIL_ADDRESS_1 = "sendwithus.test+cc.one@gmail.com";
        private const string DEFAULT_CC_EMAIL_ADDRESS_2 = "sendwithus.test+cc.two@gmail.com";
        private const string DEFAULT_BCC_EMAIL_ADDRESS_1 = "sendwithus.test+bcc.one@gmail.com";
        private const string DEFAULT_BCC_EMAIL_ADDRESS_2 = "sendwithus.test+bcc.two@gmail.com";
        private const string DEFAULT_EMAIL_NAME = "Chuck Norris";
        private const string DEFAULT_SENDER_NAME = "Matt Damon";
        private const string DEFAULT_TAG_1 = "tag1";
        private const string DEFAULT_TAG_2 = "tag2";
        private const string DEFAULT_TAG_3 = "tag3";
        private const string DEFAULT_HEADER_NAME = "X-HEADER-ONE";
        private const string DEFAULT_HEADER_VALUE = "header-value";
        private const string DEFAULT_INLINE_ID = "cat.png";
        private const string DEFAULT_INLINE_DATA = "{BASE_64_ENCODED_FILE_DATA}";
        private const string DEFAULT_FILE_NAME_1 = "doct.txt";
        private const string DEFAULT_FILE_NAME_2 = "stuff.zip";
        private const string DEFAULT_FILE_DATA = "{BASE_64_ENCODED_FILE_DATA}";
        private const string DEFAULT_VERSION_NAME = "New Version";

        readonly ITestOutputHelper Output;

        /// <summary>
        /// Default constructor with an output object - used to output messages to the Test Explorer
        /// </summary>
        /// <param name="output"></param>
        public EmailTest(ITestOutputHelper output)
        {
            Output = output;
        }

        /// <summary>
        /// Tests the API call POST /send with only the required email parameters set
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [Fact]
        public async Task TestSendEmailWithOnlyRequiredParametersAsync()
        {
            Output.WriteLine("POST /send");

            // Use the production API key so that the emails are actually sent
            SendwithusClient.ApiKey = SendwithusClientTest.API_KEY_PRODUCTION;

            // Make the API call
            var email = BuildBarebonesEmail();
            var response = await email.Send();

            // Validate the response
            SendwithusClientTest.ValidateResponse(response, Output);
        }

        /// <summary>
        /// Tests the API call POST /send with all email parameters set
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [Fact]
        public async Task TestSendEmailWithAllParametersAsync()
        {
            Output.WriteLine("POST /send");

            // Use the production API key so that the emails are actually sent
            SendwithusClient.ApiKey = SendwithusClientTest.API_KEY_PRODUCTION;

            // Construct and send an email with all of the optional data
            var response = await BuildAndSendEmailWithAllParametersAsync();

            // Validate the response
            SendwithusClientTest.ValidateResponse(response, Output);
        }

        /// <summary>
        /// Tests the POST /send API call with an invalid template ID
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [Fact]
        public async Task TestSendEmailWithInvalidTemplateId()
        {
            Output.WriteLine("POST /send with an invalid template ID");
            SendwithusClient.ApiKey = SendwithusClientTest.API_KEY_TEST;

            // Constuct the email
            Email email = BuildBarebonesEmail();
            email.template = INVALID_TEMPLATE_ID;
            try
            {
                var response = await email.Send();
                Assert.True(false, "Failed to throw exception");
            }
            catch (SendwithusException exception)
            {
                // Make sure the response was HTTP 400 Bad Request 
                SendwithusClientTest.ValidateException(exception, HttpStatusCode.BadRequest, Output);
            }
        }

        /// <summary>
        /// Creates an email object with only the required parameters
        /// </summary>
        /// <returns></returns>
        private static Email BuildBarebonesEmail()
        {
            // Construct the template data
            var templateData = new Dictionary<string, object>();
            templateData.Add("first_name", "Chuck");
            templateData.Add("last_name", "Norris");
            templateData.Add("img", "http://placekitten.com/50/60");
            var link = new Dictionary<string, string>();
            link.Add("url", "https://www.sendwithus.com");
            link.Add("text", "sendwithus!");
            templateData.Add("link", link);

            // Construct the sender and recipients
            var recipient = new EmailRecipient(DEFAULT_RECIPIENT_EMAIL_ADDRESS);
            var cc = new Collection<EmailRecipient>();
            cc.Add(new EmailRecipient(DEFAULT_CC_EMAIL_ADDRESS_1));
            cc.Add(new EmailRecipient(DEFAULT_CC_EMAIL_ADDRESS_2));
            var bcc = new Collection<EmailRecipient>();
            bcc.Add(new EmailRecipient(DEFAULT_BCC_EMAIL_ADDRESS_1));
            bcc.Add(new EmailRecipient(DEFAULT_BCC_EMAIL_ADDRESS_2));

            // Construct and return the email
            return new Email(DEFAULT_TEMPLATE_ID, templateData, recipient);
        }

        /// <summary>
        /// Build and send an email with all of the parameters included
        /// Public so that it can also be used by the BatchApiRequestTest library
        /// </summary>
        /// <returns>The API response to the Email Send call</returns>
        public static async Task<EmailResponse> BuildAndSendEmailWithAllParametersAsync()
        {
            var email = BuildBarebonesEmail();
            email.cc.Add(new EmailRecipient(DEFAULT_CC_EMAIL_ADDRESS_1, DEFAULT_EMAIL_NAME));
            email.cc.Add(new EmailRecipient(DEFAULT_CC_EMAIL_ADDRESS_2, DEFAULT_EMAIL_NAME));
            email.bcc.Add(new EmailRecipient(DEFAULT_BCC_EMAIL_ADDRESS_1, DEFAULT_EMAIL_NAME));
            email.bcc.Add(new EmailRecipient(DEFAULT_BCC_EMAIL_ADDRESS_2, DEFAULT_EMAIL_NAME));
            email.sender.address = DEFAULT_SENDER_EMAIL_ADDRESS;
            email.sender.reply_to = DEFAULT_REPLY_TO_EMAIL_ADDRESS;
            email.sender.name = DEFAULT_SENDER_NAME;
            email.tags.Add(DEFAULT_TAG_1);
            email.tags.Add(DEFAULT_TAG_2);
            email.tags.Add(DEFAULT_TAG_3);
            email.headers.Add(DEFAULT_HEADER_NAME, DEFAULT_HEADER_VALUE);
            email.inline.id = DEFAULT_INLINE_ID;
            email.inline.data = DEFAULT_INLINE_DATA;
            email.files.Add(new EmailFileData(DEFAULT_FILE_NAME_1, DEFAULT_FILE_DATA));
            email.files.Add(new EmailFileData(DEFAULT_FILE_NAME_2, DEFAULT_FILE_DATA));
            email.version_name = DEFAULT_VERSION_NAME;

            // Make the API call
            return await email.Send();
        }
    }
}
