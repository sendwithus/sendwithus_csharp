using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Sendwithus
{
    public class Email
    {
        public const string EMAIL_RESOURCE = "send";

        public string template { get; set; } // The template ID
        public Dictionary<string, object> template_data { get; }
        public EmailRecipient recipient { get; set; }
        public List<EmailRecipient> cc { get; }
        public List<EmailRecipient> bcc { get; }
        public EmailSender sender { get; set; }
        public List<string> tags { get; }
        public Dictionary<string, string> headers { get; }  // TODO: Confirm that this is the right representation for headers (looks like it is from the python library's usage)
        public FileData inline { get; set; }   // TODO: is this the right representation of inline?  Should it be a list of FileData? Are other fields possibe beyond ID and Data?
        public List<FileData> files { get; }
        public string esp_account { get; set; }
        public string locale { get; set; }
        public string version_name { get; set; }

        /// <summary>
        /// Constructor for an email
        /// </summary>
        /// <param name="template">The template ID to send</param>
        /// <param name="template_data">Object containing email template data</param>
        /// <param name="recipient">The email recipient</param>
        /// <param name="cc">An array of CC recipients</param>
        /// <param name="bcc">An array of BCC recipients</param>
        public Email(string template, Dictionary<string, object> template_data, EmailRecipient recipient)
        {
            this.template = template;
            this.template_data = template_data;
            this.recipient = recipient;
            sender = new EmailSender();
            cc = new List<EmailRecipient>();
            bcc = new List<EmailRecipient>();
            tags = new List<string>();
            headers = new Dictionary<string, string>();
            inline = new FileData();   // TODO: is this the right representation of inline?  Should it be a list of FileData? Are other fields possibe beyond ID and Data?
            files = new List<FileData>();
    }

        /// <summary>
        /// Send the given email.
        /// POST /send
        /// </summary>
        /// <param name="email">The email to send</param>
        /// <returns>A response indicating whether the message was sent successfully and a summary of the message</returns>
        public async Task<EmailResponse> Send()
        {
            // Send the POST request
            var resource = String.Format("{0}", EMAIL_RESOURCE);
            var jsonResponse = await RequestManager.SendPostRequestAsync(resource, this);

            // Convert the JSON result into an object
            var serializer = new JavaScriptSerializer();
            var response = serializer.Deserialize<EmailResponse>(jsonResponse);
            return response;
        }
        

        /// <summary>
        /// A class to represent an email recepient
        /// </summary>
        public class EmailRecipient
        {
            public string address { get; set; }
            public string name { get; set; }

            /// <summary>
            /// Constructor for a new email recipient
            /// </summary>
            /// <param name="address">The email address of the recipient</param>
            /// <param name="name">The name of the recipient (optional)</param>
            public EmailRecipient(string address, string name = "")
            {
                this.address = address;
                this.name = name;
            }
        }

        /// <summary>
        /// A class to represent an email sender
        /// </summary>
        public class EmailSender
        {
            public string address { get; set; }
            public string reply_to { get; set; }
            public string name { get; set; }


            /// <summary>
            /// Default constructor for a new email sender
            /// </summary>
            public EmailSender()
            {
                address = String.Empty;
                reply_to = String.Empty;
                name = String.Empty;
            }

            /// <summary>
            /// Constructor for a new email sender
            /// </summary>
            /// <param name="address">The email address of the sender</param>
            /// <param name="reply_to">The reply-to email address of the sender</param>
            /// <param name="name">The name of the sender</param>
            public EmailSender(string address, string reply_to, string name)
            {
                this.address = address;
                this.reply_to = reply_to;
                this.name = name;
            }
        }

        /// <summary>
        /// A class to represent inline data for an email
        /// </summary>
        public class FileData
        {
            public string id;
            public string data;

            public FileData()
            {
                id = String.Empty;
                data = String.Empty;
            }

            /// <summary>
            /// Constructor for the file data
            /// </summary>
            /// <param name="id">The id of the file data</param>
            /// <param name="data">The file data, as a base 64 encoded string</param>
            public FileData(string id, string data)
            {
                this.id = id;
                this.data = data;
            }
        }

        public class EmailResponse
        {
            public bool success { get; set; }
            public string status { get; set; }
            public string receipt_id { get; set; }
            public EmailSummary email { get; set; }
    }

        /// <summary>
        /// A class to represent the email summary that's included in the response to the send email API call
        /// </summary>
        public class EmailSummary
        {
            public string name { get; set; }
            public string version_name { get; set; }
            public string locale { get; set; }
        }


    }
}
