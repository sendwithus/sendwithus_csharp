using System;
using System.Collections.ObjectModel;

namespace Sendwithus
{
    /// <summary>
    /// sendwithus CustomerEmailLogsResponse
    /// </summary>
    public class CustomerEmailLogsResponse
    {
        public bool success { get; set; }
        public string status { get; set; }
        public Collection<Log> logs { get; set; }

        /// <summary>
        /// default Constructor
        /// </summary>
        public CustomerEmailLogsResponse()
        {
            success = false;
            status = String.Empty;
            logs = new Collection<Log>();
        }
    }
}
