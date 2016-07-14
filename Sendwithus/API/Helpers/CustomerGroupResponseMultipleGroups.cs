using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Sendwithus
{
    /// <summary>
    /// sendwithus CustomerGroupResponseMultipleGropus class
    /// Same as a CustomerGroupResponse, but with a collection of groups, instead of just one group
    /// </summary>
    public class CustomerGroupResponseMultipleGropus
    {
        public bool success { get; set; }
        public string status { get; set; }
        public Collection<CustomerGroup> groups { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public CustomerGroupResponseMultipleGropus()
        {
            success = false;
            status = String.Empty;
            groups = new Collection<CustomerGroup>();
        }
    }
}
