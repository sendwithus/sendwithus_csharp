using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sendwithus;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SendwithusTest
{
    /// <summary>
    /// Class to test the sendwithus Drip Campaign API
    /// </summary>
    [TestClass]
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
        /// Tests the API call POST /drip_campaigns/(drip_campaign_id)/activate
        /// </summary>
        /// <returns>The associated task</returns>
        [TestMethod]
        public async Task TestActivateDripCampaignAsyncWithMinimumParameters()
        {
            Trace.WriteLine(String.Format("POST /drip_campaigns/{0}/activate with minimum parameters",DEFAULT_CAMPAIGN_ID));

            // Build the drip campaign object
            var recipient = new EmailRecipient(DEFAULT_RECIPIENT_EMAIL_ADDRESS);
            var dripCampaign = new DripCampaign(recipient);
            
            // Make the API call
            try
            { 
                var dripCampaignResponse = await dripCampaign.ActivateAsync(DEFAULT_CAMPAIGN_ID);

                // Validate the response
                SendwithusClientTest.ValidateResponse(dripCampaignResponse);
            }
            catch (AggregateException exception)
            {
                Assert.Fail(exception.ToString());
            }
        }

        /// <summary>
        /// Tests the API call POST /drip_campaigns/(drip_campaign_id)/activate
        /// </summary>
        /// <returns>The associated task</returns>
        [TestMethod]
        public async Task TestActivateDripCampaignAsyncWithAllParameters()
        {
            Trace.WriteLine(String.Format("POST /drip_campaigns/{0}/activate with all parameters", DEFAULT_CAMPAIGN_ID));

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
            try
            { 
                var dripCampaignResponse = await dripCampaign.ActivateAsync(DEFAULT_CAMPAIGN_ID);

                // Validate the response
                SendwithusClientTest.ValidateResponse(dripCampaignResponse);
            }
            catch (AggregateException exception)
            {
                Assert.Fail(exception.ToString());
            }
        }

        /// <summary>
        /// Tests the API call POST /drip_campaigns/(drip_campaign_id)/activate with invalid campaign ID
        /// </summary>
        /// <returns>The associated task</returns>
        [TestMethod]
        public async Task TestActivateDripCampaignWithInvalidParameters()
        {
            Trace.WriteLine(String.Format("POST /drip_campaigns/{0}/activate with invalid campaign ID", INVALID_CAMPAIGN_ID));

            // Build the drip campaign object
            var recipient = new EmailRecipient(DEFAULT_RECIPIENT_EMAIL_ADDRESS);
            var dripCampaign = new DripCampaign(recipient);

            // Make the API call
            try { 
                var response = await dripCampaign.ActivateAsync(INVALID_CAMPAIGN_ID);
            }
            catch (AggregateException exception)
            {
                // Make sure the response was HTTP 400 Bad Request
                SendwithusClientTest.ValidateException(exception, HttpStatusCode.BadRequest);
            }
        }

        /// <summary>
        /// Tests the API call POST /drip_campaigns/(drip_campaign_id)/deactivate
        /// </summary>
        /// <returns>The associated task</returns>
        [TestMethod]
        public async Task TestDeactivateFromDripCampaignAsync()
        {
            Trace.WriteLine(String.Format("POST /drip_campaigns/{0}/deactivate", DEFAULT_CAMPAIGN_ID));

            // Make the API call
            try
            { 
                var dripCampaignDeactivateResponse = await DripCampaign.DeactivateFromCampaignAsync(DEFAULT_CAMPAIGN_ID, DEFAULT_RECIPIENT_EMAIL_ADDRESS);

                // Validate the response
                SendwithusClientTest.ValidateResponse(dripCampaignDeactivateResponse);
            }
            catch (AggregateException exception)
            {
                Assert.Fail(exception.ToString());
            }
        }

        /// <summary>
        /// Tests the API call POST /drip_campaigns/(drip_campaign_id)/deactivate
        /// </summary>
        /// <returns>The associated task</returns>
        [TestMethod]
        public async Task TestDeactivateFromAllDripCampaignsAsync()
        {
            Trace.WriteLine(String.Format("POST /drip_campaigns/{0}/deactivate", DEFAULT_CAMPAIGN_ID));

            // Make the API call
            try
            {
                var dripCampaignDeactivateResponse = await DripCampaign.DeactivateFromAllCampaignsAsync(DEFAULT_RECIPIENT_EMAIL_ADDRESS);

                // Validate the response
                SendwithusClientTest.ValidateResponse(dripCampaignDeactivateResponse);
            }
            catch (AggregateException exception)
            {
                Assert.Fail(exception.ToString());
            }
        }

        /// <summary>
        /// Tests the API call GET /drip_campaigns
        /// </summary>
        /// <returns>The associated task</returns>
        [TestMethod]
        public async Task TestGetDripCampaignsAsync()
        {
            Trace.WriteLine("GET /drip_campaigns");

            // Make the API call
            try
            { 
                var dripCampaignDetails = await DripCampaign.GetDripCampaignsAsync();

                // Validate the response
                SendwithusClientTest.ValidateResponse(dripCampaignDetails);
            }
            catch (AggregateException exception)
            {
                Assert.Fail(exception.ToString());
            }
        }

        /// <summary>
        /// Tests the API call GET /drip_campaigns/(drip_campaign_id)
        /// </summary>
        /// <returns>The associated task</returns>
        [TestMethod]
        public async Task TestGetDripCampaignAsync()
        {
            Trace.WriteLine(String.Format("GET /drip_campaigns/{0}",DEFAULT_CAMPAIGN_ID));

            // Make the API call
            try
            { 
                var response = await DripCampaign.GetDripCampaignAsync(DEFAULT_CAMPAIGN_ID);

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
