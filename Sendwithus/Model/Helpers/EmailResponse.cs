using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sendwithus;

namespace Sendwithus
{
    public class EmailResponse
    {
        public bool success { get; set; }
        public string status { get; set; }
        public string receipt_id { get; set; }
        public EmailSummary email { get; set; }

        public EmailResponse()
        {
            success = false;
            status = String.Empty;
            receipt_id = String.Empty;
            email = new EmailSummary();
        }
    }
}
