using System;

namespace Sendwithus
{
    /// <summary>
    /// sendwithus LogResendResponse class
    /// </summary>
    public class LogResendResponse
    {
        public bool success { get; set; }
        public string status { get; set; }
        public EmailSummary email { get; set; }
        public string receipt_id { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public LogResendResponse()
        {
            success = false;
            status = String.Empty;
            email = new EmailSummary();
            receipt_id = String.Empty;
        }
    }
}
