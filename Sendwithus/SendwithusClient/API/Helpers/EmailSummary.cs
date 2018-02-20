using System;

namespace Sendwithus
{
    /// <summary>
    /// A class to represent the email summary that's included in the response to the send email API call
    /// </summary>
    public class EmailSummary
    {
        public string name { get; set; }
        public string version_name { get; set; }
        public string locale { get; set; }

        public EmailSummary()
        {
            name = String.Empty;
            version_name = String.Empty;
            locale = String.Empty;
        }
    }
}
