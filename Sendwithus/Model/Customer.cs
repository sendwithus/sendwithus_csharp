using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Sendwithus
{
    /// <summary>
    /// sendwithus Customer class
    /// </summary>
    public class Customer
    {
        public string email { get; set; }
        public Dictionary<string, object> data { get; }
        public UInt64 created { get; set; }
        public string locale { get; set; }
        public List<string> groups { get; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public Customer() : this(String.Empty) { }

        /// <summary>
        /// Constructor wtih the customer's email address given
        /// </summary>
        /// <param name="email">The customer's email address</param>
        public Customer(string email)
        {
            this.email = email;
            data = new Dictionary<string, object>();
            created = 0;
            locale = String.Empty;
            groups = new List<string>();
        }

        /// <summary>
        /// Gets a specific customer
        /// GET /customers/customer@example.com
        /// </summary>
        /// <param name="customerEmailAddress">The customer's email address</param>
        /// <returns>A response containing the customer with the given email address 
        /// and the status of the API call</returns>
        public static async Task<CustomerResponse> GetCustomerAsync(string customerEmailAddress)
        {
            // Send the GET request
            var resource = String.Format("customers/{0}", customerEmailAddress);
            var jsonResponse = await RequestManager.SendGetRequestAsync(resource);

            // Convert the JSON result into an object
            var serializer = new JavaScriptSerializer();
            return serializer.Deserialize<CustomerResponse>(jsonResponse);
        }

        /// <summary>
        /// Creatings a new cusotmer or updates an existing customer if the customer already exists
        /// POST /customers
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public static async Task<GenericApiCallStatus> CreateOrUpdateCustomerAsync(Customer customer)
        {
            // Send the POST request
            var resource = "customers";
            var jsonResponse = await RequestManager.SendPostRequestAsync(resource, customer);

            // Convert the JSON result into an object
            var serializer = new JavaScriptSerializer();
            return serializer.Deserialize<GenericApiCallStatus>(jsonResponse);
        }

        /// <summary>
        /// Deletes a given customer
        /// DELETE /customers/(:email)
        /// </summary>
        /// <param name="emailAddress">The customer's email address</param>
        /// <returns></returns>
        public static async Task<GenericApiCallStatus> DeleteCustomerAsync(string emailAddress)
        {
            // Send the POST request
            var resource = String.Format("customers/{0}", emailAddress);
            var jsonResponse = await RequestManager.SendDeleteRequestAsync(resource);

            // Convert the JSON result into an object
            var serializer = new JavaScriptSerializer();
            return serializer.Deserialize<GenericApiCallStatus>(jsonResponse);
        }

        /// <summary>
        /// Get email logs for a Customer
        /// GET /customers/matt@sendwithus.com/logs?count={count}&created_lt={timestamp}&created_gt={timestamp}
        /// </summary>
        /// <param name="emailAddress">The customer's email address</param>
        /// <returns></returns>
        public static async Task<CustomerEmailLogsResponse> GetCustomerEmailLogsAsync(string emailAddress, Dictionary<string, object> queryParameters = null)
        {
            // Send the GET request
            var resource = String.Format("customers/{0}/logs", emailAddress);
            var jsonResponse = await RequestManager.SendGetRequestAsync(resource, queryParameters);

            // Convert the JSON result into an object
            var serializer = new JavaScriptSerializer();
            return serializer.Deserialize<CustomerEmailLogsResponse>(jsonResponse);
        }

        /// <summary>
        /// Add a customer to a group.
        /// POST /customers/(:email)/groups/(:group_id)
        /// </summary>
        /// <param name="emailAddress">The customer's email address</param>
        /// <param name="groupId">The ID of the group</param>
        /// <returns>The API call status</returns>
        public static async Task<GenericApiCallStatus> AddCustomerToGroupAsync(string emailAddress, string groupId)
        {
            // Send the GET request
            var resource = String.Format("customers/{0}/groups/{1}", emailAddress, groupId);
            var jsonResponse = await RequestManager.SendPostRequestAsync(resource);

            // Convert the JSON result into an object
            var serializer = new JavaScriptSerializer();
            return serializer.Deserialize<GenericApiCallStatus>(jsonResponse);
        }

        /// <summary>
        /// Remove a customer from a group.
        /// DELETE /customers/(:email)/groups/(:group_id)
        /// </summary>
        /// <param name="emailAddress">The customer's email address</param>
        /// <param name="groupId">The ID of the group</param>
        /// <returns>The API call status</returns>
        public static async Task<GenericApiCallStatus> RemoveCustomerFromGroupAsync(string emailAddress, string groupId)
        {
            // Send the GET request
            var resource = String.Format("customers/{0}/groups/{1}", emailAddress, groupId);
            var jsonResponse = await RequestManager.SendDeleteRequestAsync(resource);

            // Convert the JSON result into an object
            var serializer = new JavaScriptSerializer();
            return serializer.Deserialize<GenericApiCallStatus>(jsonResponse);
        }
    }
}
