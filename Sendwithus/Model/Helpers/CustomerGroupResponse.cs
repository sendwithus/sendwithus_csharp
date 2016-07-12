using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Sendwithus
{
    /// <summary>
    /// sendwithus CustomerGroupResponse class
    /// </summary>
    public class CustomerGroupResponse
    {
        public bool success { get; set; }
        public string status { get; set; }
        public CustomerGroup group { get; }

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
