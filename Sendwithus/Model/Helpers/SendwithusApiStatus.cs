using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sendwithus
{
    /// <summary>
    /// sendwithus API Status
    /// </summary>
    public class SendwithusApiStatus
    {
        public string status { get; set; }
        public bool success { get; set; }

        public SendwithusApiStatus()
        {
            this.status = String.Empty;
            this.success = false;
        }
    }
}
