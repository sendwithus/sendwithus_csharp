using System;

namespace Sendwithus
{
    /// <summary>
    /// sendwithus CustomerGroupResponse class
    /// </summary>
    public class CustomerGroupResponse
    {
        public bool success { get; set; }
        public string status { get; set; }
        public CustomerGroup group { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public CustomerGroupResponse()
        {
            success = false;
            status = String.Empty;
            group = new CustomerGroup();
        }
    }
}
