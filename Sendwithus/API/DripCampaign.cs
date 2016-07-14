using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

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
        /// POST /drip_campaigns/(drip_campaign_id)/activate
        /// </summary>
        /// <returns>A response containing the status of the call and a summary of the drip campaign</returns>
        public async Task<DripCampaignResponse> ActivateAsync(string dripCampaignId)
        {
            // Send the POST request
            var resource = String.Format("drip_campaigns/{0}/activate", dripCampaignId);
            var jsonResponse = await RequestManager.SendPostRequestAsync(resource, this);

            // Convert the JSON result into an object
            var serializer = new JavaScriptSerializer();
            var response = serializer.Deserialize<DripCampaignResponse>(jsonResponse);
            return response;
        }

        /// <summary>
        /// POST /drip_campaigns/(drip_campaign_id)/deactivate
        /// </summary>
        /// <returns>A response containing the status of the call and a summary of the drip campaign</returns>
        public static async Task<DripCampaignDeactivateResponse> DeactivateAsync(string dripCampaignId, string recipientAddress)
        {
            var resource = String.Format("drip_campaigns/{0}/deactivate", dripCampaignId);

            // Package the string into a dictionary so that it renders correctly in JSON as "<variable_name>": "<value>"
            var recipient = new Dictionary<string, string>();
            recipient.Add("recipient_address", recipientAddress);

            // Send the POST request
            var jsonResponse = await RequestManager.SendPostRequestAsync(resource, recipient);

            // Convert the JSON result into an object
            var serializer = new JavaScriptSerializer();
            var response = serializer.Deserialize<DripCampaignDeactivateResponse>(jsonResponse);
            return response;
        }

        /// <summary>
        /// GET /drip_campaigns
        /// </summary>
        /// <returns>Details on all the drip campaigns</returns>
        public static async Task<List<DripCampaignDetails>> GetDripCampaignsAsync()
        {
            // Send the GET request
            var resource = "drip_campaigns";
            var jsonResponse = await RequestManager.SendGetRequestAsync(resource);

            // Convert the JSON result into an object
            var serializer = new JavaScriptSerializer();
            var response = serializer.Deserialize<List<DripCampaignDetails>>(jsonResponse);
            return response;
        }

        /// <summary>
        /// GET /drip_campaigns/(drip_campaign_id)
        /// </summary>
        /// <returns>Details on all the drip campaigns</returns>
        public static async Task<DripCampaignDetails> GetDripCampaignAsync(string dripCampaignId)
        {
            // Send the GET request
            var resource = String.Format("drip_campaigns/{0}", dripCampaignId);
            var jsonResponse = await RequestManager.SendGetRequestAsync(resource);

            // Convert the JSON result into an object
            var serializer = new JavaScriptSerializer();
            var response = serializer.Deserialize<DripCampaignDetails>(jsonResponse);
            return response;
        }
    }
}
