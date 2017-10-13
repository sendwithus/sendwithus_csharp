using Newtonsoft.Json;
using Sendwithus.Net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Sendwithus
{
    /// <summary>
    /// sendwithus DripCampaign class
    /// </summary>
    public class DripCampaign
    {
        public EmailRecipient recipient { get; set; }
        public Collection<EmailRecipient> cc { get; set; }
        public Collection<EmailRecipient> bcc { get; set; }
        public EmailSender sender { get; set; }
        public Dictionary<string, object> email_data { get; set; }
        public Collection<string> tags { get; set; }
        public string esp_account { get; set; }
        public string locale { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public DripCampaign(EmailRecipient recipient)
        {
            this.recipient = recipient;
            cc = new Collection<EmailRecipient>();
            bcc = new Collection<EmailRecipient>();
            sender = new EmailSender();
            email_data = new Dictionary<string, object>();
            tags = new Collection<string>();
            esp_account = String.Empty;
            locale = String.Empty;
        }

        /// <summary>
        /// Activate campaign for a customer.
        /// POST /drip_campaigns/(drip_campaign_id)/activate
        /// </summary>
        /// <param name="dripCampaignId">The ID of the drip campaign to activate</param>
        /// <returns>A response containing the status of the call and a summary of the drip campaign</returns>
        /// <exception cref="AggregateException">Thrown when the API response status code is not success or when the API call times out</exception>
        /// <exception cref="InvalidOperationException">Thrown when making a Batch API Request that has already reached the maxmimum API calls per batch request</exception>
        public async Task<DripCampaignResponse> ActivateAsync(string dripCampaignId)
        {
            // Send the POST request
            var resource = String.Format("drip_campaigns/{0}/activate", dripCampaignId);
            var jsonResponse = await RequestManager.SendPostRequestAsync(resource, this);

            // Convert the JSON result into an object            
            return JsonConvert.DeserializeObject<DripCampaignResponse>(jsonResponse);
        }

        /// <summary>
        /// Deactivate a campaign for customer.
        /// POST /drip_campaigns/(drip_campaign_id)/deactivate
        /// </summary>
        /// <param name="dripCampaignId">The ID of the drip campaign to deactivate</param>
        /// <param name="recipientAddress">The email address of the recipient to deactivate the campaign for</param>
        /// <returns>A response containing the status of the call and a summary of the drip campaign</returns>
        /// <exception cref="AggregateException">Thrown when the API response status code is not success or when the API call times out</exception>
        /// <exception cref="InvalidOperationException">Thrown when making a Batch API Request that has already reached the maxmimum API calls per batch request</exception>
        public static async Task<DripCampaignResponse> DeactivateFromCampaignAsync(string dripCampaignId, string recipientAddress)
        {
            var resource = String.Format("drip_campaigns/{0}/deactivate", dripCampaignId);

            // Package the string into a dictionary so that it renders correctly in JSON as "<variable_name>": "<value>"
            var recipient = new Dictionary<string, string>();
            recipient.Add("recipient_address", recipientAddress);

            // Send the POST request
            var jsonResponse = await RequestManager.SendPostRequestAsync(resource, recipient);

            // Convert the JSON result into an object
            return JsonConvert.DeserializeObject<DripCampaignResponse>(jsonResponse);
        }

        /// <summary>
        /// Deactivate all campaigns for a given customer.
        /// POST /drip_campaigns/(drip_campaign_id)/deactivate
        /// </summary>
        /// <param name="recipientAddress">The email address of the customer to deactivate all campaigns for.</param>
        /// <returns>A response containing the status of the call and a summary of the drip campaign</returns>
        /// <exception cref="AggregateException">Thrown when the API response status code is not success or when the API call times out</exception>
        /// <exception cref="InvalidOperationException">Thrown when making a Batch API Request that has already reached the maxmimum API calls per batch request</exception>
        public static async Task<DripCampaignDeactivateAllResponse> DeactivateFromAllCampaignsAsync(string recipientAddress)
        {
            var resource = String.Format("drip_campaigns/deactivate");

            // Package the string into a dictionary so that it renders correctly in JSON as "<variable_name>": "<value>"
            var recipient = new Dictionary<string, string>();
            recipient.Add("recipient_address", recipientAddress);

            // Send the POST request
            var jsonResponse = await RequestManager.SendPostRequestAsync(resource, recipient);

            // Convert the JSON result into an object
            return JsonConvert.DeserializeObject<DripCampaignDeactivateAllResponse>(jsonResponse);
        }

        /// <summary>
        /// Get a list of campaigns.
        /// GET /drip_campaigns
        /// </summary>
        /// <returns>Details on all the drip campaigns</returns>
        /// <exception cref="AggregateException">Thrown when the API response status code is not success or when the API call times out</exception>
        /// <exception cref="InvalidOperationException">Thrown when making a Batch API Request that has already reached the maxmimum API calls per batch request</exception>
        public static async Task<List<DripCampaignDetails>> GetDripCampaignsAsync()
        {
            // Send the GET request
            var resource = "drip_campaigns";
            var jsonResponse = await RequestManager.SendGetRequestAsync(resource);

            // Convert the JSON result into an object
            return JsonConvert.DeserializeObject<List<DripCampaignDetails>>(jsonResponse);
        }

        /// <summary>
        /// Get the details on a specific drip campaign.
        /// GET /drip_campaigns/(drip_campaign_id)
        /// </summary>
        /// <param name="dripCampaignId">The ID of the drip campaign</param>
        /// <returns>Details on all the drip campaigns</returns>
        /// <exception cref="AggregateException">Thrown when the API response status code is not success or when the API call times out</exception>
        /// <exception cref="InvalidOperationException">Thrown when making a Batch API Request that has already reached the maxmimum API calls per batch request</exception>
        public static async Task<DripCampaignDetails> GetDripCampaignAsync(string dripCampaignId)
        {
            // Send the GET request
            var resource = String.Format("drip_campaigns/{0}", dripCampaignId);
            var jsonResponse = await RequestManager.SendGetRequestAsync(resource);

            // Convert the JSON result into an object
            return JsonConvert.DeserializeObject<DripCampaignDetails>(jsonResponse);
        }
    }
}
