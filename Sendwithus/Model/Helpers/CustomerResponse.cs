using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sendwithus
{
    /// <summary>
    /// sendwithus CustomerResponse class.
    /// Used by API calls to encapulate a customer object along with data on the success of the API call
    /// </summary>
    public class CustomerResponse
    {
        public bool success { get; set; }
        public string status { get; set; }
        public Customer customer { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public CustomerResponse()
        {
            success = false;
            status = String.Empty;
            customer = new Customer();
        }
    }
}
