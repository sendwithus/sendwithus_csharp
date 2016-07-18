using Sendwithus.Net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public Dictionary<string, object> data { get; set; }
        public Int64 created { get; set; }
        public string locale { get; set; }
        public Collection<string> groups { get; set; }

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
            groups = new Collection<string>();
        }

        /// <summary>
        /// Gets a specific customer.
        /// GET /customers/customer@example.com
        /// </summary>
        /// <param name="customerEmailAddress">The customer's email address</param>
        /// <returns>A response containing the customer with the given email address and the status of the API call</returns>
        /// <exception cref="AggregateException">Thrown when the API response status code is not success or when the API call times out</exception>
        /// <exception cref="InvalidOperationException">Thrown when making a Batch API Request that has already reached the maxmimum API calls per batch request</exception>
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
        /// Creates a new customer or updates an existing customer if the customer already exists.
        /// Merge opterations will replace existing attributes with new values and add any new attributes to the customer.
        /// POST /customers
        /// </summary>
        /// <param name="customer">The new or updated customer</param>
        /// <returns>The status of the API call</returns>
        /// <exception cref="AggregateException">Thrown when the API response status code is not success or when the API call times out</exception>
        /// <exception cref="InvalidOperationException">Thrown when making a Batch API Request that has already reached the maxmimum API calls per batch request</exception>
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
        /// Deletes a given customer.
        /// DELETE /customers/(:email)
        /// </summary>
        /// <param name="emailAddress">The customer's email address</param>
        /// <returns>The status of the API call</returns>
        /// <exception cref="AggregateException">Thrown when the API response status code is not success or when the API call times out</exception>
        /// <exception cref="InvalidOperationException">Thrown when making a Batch API Request that has already reached the maxmimum API calls per batch request</exception>
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
        /// Get email logs for a customer.
        /// GET /customers/matt@sendwithus.com/logs?count={count}&created_lt={timestamp}&created_gt={timestamp}
        /// </summary>
        /// <param name="emailAddress">The customer's email address</param>
        /// <param name="queryParameters">The query parameters to include with the request, options include:
        /// count (optional) – A number between 1 and 100 to specify the number of logs returned (including scheduled drips). If none is specified, a limit of 100 sent logs is automatically imposed.
        /// created_lt (optional) – A Unix Timestamp used as a index for the search.The logs retrieved will have been sent before the timestamp specified.
        /// created_gt (optional) – A Unix Timestamp used as a index for the search.The logs retrieved will have been sent after the timestamp specified.</param>
        /// <returns>A response containing the status of the API call and the logs that match the request parameters</returns>
        /// <exception cref="AggregateException">Thrown when the API response status code is not success or when the API call times out</exception>
        /// <exception cref="InvalidOperationException">Thrown when making a Batch API Request that has already reached the maxmimum API calls per batch request</exception>
        public static async Task<CustomerEmailLogsResponse> GetCustomerEmailLogsAsync(string emailAddress, Dictionary<string, object> queryParameters)
        {
            // Send the GET request
            var resource = String.Format("customers/{0}/logs", emailAddress);
            var jsonResponse = await RequestManager.SendGetRequestAsync(resource, queryParameters);

            // Convert the JSON result into an object
            var serializer = new JavaScriptSerializer();
            return serializer.Deserialize<CustomerEmailLogsResponse>(jsonResponse);
        }

        /// <summary>
        /// Get email logs for a customer.
        /// GET /customers/matt@sendwithus.com without any query parameters
        /// </summary>
        /// <param name="emailAddress">The customer's email address</param>
        /// <returns>A response containing the status of the API call and the logs that match the request parameters</returns>
        /// <exception cref="AggregateException">Thrown when the API response status code is not success or when the API call times out</exception>
        /// <exception cref="InvalidOperationException">Thrown when making a Batch API Request that has already reached the maxmimum API calls per batch request</exception>
        public static async Task<CustomerEmailLogsResponse> GetCustomerEmailLogsAsync(string emailAddress)
        {
            return await GetCustomerEmailLogsAsync(emailAddress, null);
        }

        /// <summary>
        /// Add a customer to a group.
        /// POST /customers/(:email)/groups/(:group_id)
        /// </summary>
        /// <param name="emailAddress">The customer's email address</param>
        /// <param name="groupId">The ID of the group</param>
        /// <returns>The API call status</returns>
        /// <exception cref="AggregateException">Thrown when the API response status code is not success or when the API call times out</exception>
        /// <exception cref="InvalidOperationException">Thrown when making a Batch API Request that has already reached the maxmimum API calls per batch request</exception>
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
        /// <exception cref="AggregateException">Thrown when the API response status code is not success or when the API call times out</exception>
        /// <exception cref="InvalidOperationException">Thrown when making a Batch API Request that has already reached the maxmimum API calls per batch request</exception>
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
