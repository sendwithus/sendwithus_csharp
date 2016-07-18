using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Sendwithus
{
    /// <summary>
    /// sendwithus Email class
    /// </summary>
    public class Email
    {
        public string template { get; set; } // The template ID
        public Dictionary<string, object> template_data { get; set; }
        public EmailRecipient recipient { get; set; }
        public Collection<EmailRecipient> cc { get; set; }
        public Collection<EmailRecipient> bcc { get; set; }
        public EmailSender sender { get; set; }
        public Collection<string> tags { get; set; }
        public Dictionary<string, string> headers { get; set; }  // TODO: Confirm that this is the right representation for headers (looks like it is from the python library's usage)
        public EmailFileData inline { get; set; }   // TODO: is this the right representation of inline?  Should it be a list of EmailFileData? Are other fields possibe beyond ID and Data?
        public Collection<EmailFileData> files { get; set; }
        public string esp_account { get; set; }
        public string locale { get; set; }
        public string version_name { get; set; }

        /// <summary>
        /// Constructor for an email
        /// </summary>
        /// <param name="template">The template ID to send</param>
        /// <param name="templateData">Object containing email template data</param>
        public Email(string template, Dictionary<string, object> templateData, EmailRecipient recipient)
        {
            this.template = template;
            this.template_data = template_data;
            this.recipient = recipient;
            sender = new EmailSender();
            cc = new Collection<EmailRecipient>();
            bcc = new Collection<EmailRecipient>();
            tags = new Collection<string>();
            headers = new Dictionary<string, string>();
            inline = new EmailFileData();   // TODO: is this the right representation of inline?  Should it be a list of EmailFileData? Are other fields possibe beyond ID and Data?
            files = new Collection<EmailFileData>();
        }

        /// <summary>
        /// Send the email.
        /// POST /send
        /// </summary>
        /// <returns>A response indicating whether the message was sent successfully and a summary of the message</returns>
        /// <exception cref="AggregateException">Thrown when the API response status code is not success or when the API call times out</exception>
        /// <exception cref="InvalidOperationException">Thrown when making a Batch API Request that has already reached the maxmimum API calls per batch request</exception>
        public async Task<EmailResponse> Send()
        {
            // Send the POST request
            var resource = "send";
            var jsonResponse = await RequestManager.SendPostRequestAsync(resource, this);

            // Convert the JSON result into an object
            var serializer = new JavaScriptSerializer();
            var response = serializer.Deserialize<EmailResponse>(jsonResponse);
            return response;
        }
    }
}
