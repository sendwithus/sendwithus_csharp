namespace Sendwithus
{
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
        public EmailRecipient(string address, string name)
        {
            this.address = address;
            this.name = name;
        }

        /// <summary>
        /// Constructor for a new email recipient
        /// </summary>
        /// <param name="address">The email address of the recipient</param>
        public EmailRecipient(string address) : this(address, "") { }
    }
}
