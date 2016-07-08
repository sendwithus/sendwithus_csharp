using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sendwithus
{
    /// <summary>
    /// sendwithus LogIdParameter.
    /// Used when it's necessary to serialize just the log_id parameter into a JSON object
    /// </summary>
    public class LogIdParameter
    {
        public string log_id { get; set; }

        public LogIdParameter(string logId)
        {
            log_id = logId;
        }
    }
}
