using System;

namespace Sendwithus
{
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
}
