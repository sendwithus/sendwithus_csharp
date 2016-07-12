using System;
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
    /// Class to test the sendwithus Drip Campaign API
    /// </summary>
    public class DripCampaignTest
    {
        private const string DEFAULT_CAMPAIGN_ID = "dc_VXKGx85NmwHnRv9FZv88TW";
        private const string INVALID_CAMPAIGN_ID = "invalid_campaign_id";
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
        private const string DEFAULT_VERSION_NAME = "New Version";
        private const string DEFAULT_LOCALE = "en-US";
        private const string DEFAULT_ESP_ACCOUNT_ID = "esp_e3ut7pFtWttcN4HNoQ8Vgm";

        readonly ITestOutputHelper Output;

        /// <summary>
        /// Default constructor with an output object - used to output messages to the Test Explorer
        /// </summary>
        /// <param name="output"></param>
        public DripCampaignTest(ITestOutputHelper output)
        {
            Output = output;
        }

        /// <summary>
        /// Tests the API call POST /drip_campaigns/(drip_campaign_id)/activate
        /// </summary>
        /// <returns>The associated task</returns>
        [Fact]
        public async Task TestActivateDripCampaignAsyncWithMinimumParameters()
        {
            Output.WriteLine(String.Format("POST /drip_campaigns/{0}/activate with minimum parameters",DEFAULT_CAMPAIGN_ID));

            // Use the production API key so that the emails are actually sent
            SendwithusClient.ApiKey = SendwithusClientTest.API_KEY_PRODUCTION;

            // Build the drip campaign object
            var recipient = new EmailRecipient(DEFAULT_RECIPIENT_EMAIL_ADDRESS);
            var dripCampaign = new DripCampaign(recipient);
            
            // Make the API call
            var response = await dripCampaign.ActivateAsync(DEFAULT_CAMPAIGN_ID);

            // Validate the response
            SendwithusClientTest.ValidateResponse(response, Output);
        }

        /// <summary>
        /// Tests the API call POST /drip_campaigns/(drip_campaign_id)/activate
        /// </summary>
        /// <returns>The associated task</returns>
        [Fact]
        public async Task TestActivateDripCampaignAsyncWithAllParameters()
        {
            Output.WriteLine(String.Format("POST /drip_campaigns/{0}/activate with all parameters", DEFAULT_CAMPAIGN_ID));

            // Use the production API key so that the emails are actually sent
            SendwithusClient.ApiKey = SendwithusClientTest.API_KEY_PRODUCTION;

            // Build the drip campaign object
            var recipient = new EmailRecipient(DEFAULT_RECIPIENT_EMAIL_ADDRESS, DEFAULT_EMAIL_NAME);
            var dripCampaign = new DripCampaign(recipient);
            dripCampaign.cc.Add(new EmailRecipient(DEFAULT_CC_EMAIL_ADDRESS_1, DEFAULT_EMAIL_NAME));
            dripCampaign.cc.Add(new EmailRecipient(DEFAULT_CC_EMAIL_ADDRESS_2, DEFAULT_EMAIL_NAME));
            dripCampaign.bcc.Add(new EmailRecipient(DEFAULT_BCC_EMAIL_ADDRESS_1, DEFAULT_EMAIL_NAME));
            dripCampaign.bcc.Add(new EmailRecipient(DEFAULT_BCC_EMAIL_ADDRESS_2, DEFAULT_EMAIL_NAME));
            dripCampaign.sender.address = DEFAULT_SENDER_EMAIL_ADDRESS;
            dripCampaign.sender.name = DEFAULT_SENDER_NAME;
            dripCampaign.sender.reply_to = DEFAULT_REPLY_TO_EMAIL_ADDRESS;
            dripCampaign.tags.Add(DEFAULT_TAG_1);
            dripCampaign.tags.Add(DEFAULT_TAG_2);
            dripCampaign.tags.Add(DEFAULT_TAG_3);
            dripCampaign.locale = DEFAULT_LOCALE;
            dripCampaign.esp_account = DEFAULT_ESP_ACCOUNT_ID;

            // Make the API call
            var response = await dripCampaign.ActivateAsync(DEFAULT_CAMPAIGN_ID);

            // Validate the response
            SendwithusClientTest.ValidateResponse(response, Output);
        }

        /// <summary>
        /// Tests the API call POST /drip_campaigns/(drip_campaign_id)/activate with invalid campaign ID
        /// </summary>
        /// <returns>The associated task</returns>
        [Fact]
        public async Task TestActivateDripCampaignWithInvalidParameters()
        {
            Output.WriteLine(String.Format("POST /drip_campaigns/{0}/activate with invalid campaign ID", INVALID_CAMPAIGN_ID));

            // Use the production API key so that the emails are actually sent
            SendwithusClient.ApiKey = SendwithusClientTest.API_KEY_PRODUCTION;

            // Build the drip campaign object
            var recipient = new EmailRecipient(DEFAULT_RECIPIENT_EMAIL_ADDRESS);
            var dripCampaign = new DripCampaign(recipient);

            // Make the API call
            try { 
                var response = await dripCampaign.ActivateAsync(INVALID_CAMPAIGN_ID);
            }
            catch (SendwithusException exception)
            {
                // Make sure the response was HTTP 400 Bad Request
                SendwithusClientTest.ValidateException(exception, HttpStatusCode.BadRequest, Output);
            }
        }

        /// <summary>
        /// Tests the API call POST /drip_campaigns/(drip_campaign_id)/deactivate
        /// </summary>
        /// <returns>The associated task</returns>
        [Fact]
        public async Task TestDeactivateDripCampaignAsync()
        {
            Output.WriteLine(String.Format("POST /drip_campaigns/{0}/deactivate", DEFAULT_CAMPAIGN_ID));
            SendwithusClient.ApiKey = SendwithusClientTest.API_KEY_TEST;

            // Make the API call
            var response = await DripCampaign.DeactivateAsync(DEFAULT_CAMPAIGN_ID, DEFAULT_RECIPIENT_EMAIL_ADDRESS);

            // Validate the response
            SendwithusClientTest.ValidateResponse(response, Output);
        }

        /// <summary>
        /// Tests the API call GET /drip_campaigns
        /// </summary>
        /// <returns>The associated task</returns>
        [Fact]
        public async Task TestGetDripCampaignsAsync()
        {
            Output.WriteLine("GET /drip_campaigns");
            SendwithusClient.ApiKey = SendwithusClientTest.API_KEY_TEST;

            // Make the API call
            var response = await DripCampaign.GetDripCampaignsAsync();

            // Validate the response
            SendwithusClientTest.ValidateResponse(response, Output);
        }

        /// <summary>
        /// Tests the API call GET /drip_campaigns/(drip_campaign_id)
        /// </summary>
        /// <returns>The associated task</returns>
        [Fact]
        public async Task TestGetDripCampaignAsync()
        {
            Output.WriteLine(String.Format("GET /drip_campaigns/{0}",DEFAULT_CAMPAIGN_ID));
            SendwithusClient.ApiKey = SendwithusClientTest.API_KEY_TEST;

            // Make the API call
            var response = await DripCampaign.GetDripCampaignAsync(DEFAULT_CAMPAIGN_ID);

            // Validate the response
            SendwithusClientTest.ValidateResponse(response, Output);
        }
    }
}
