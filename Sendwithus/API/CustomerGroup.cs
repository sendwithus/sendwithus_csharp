using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Web.Script.Serialization;

namespace Sendwithus
{
    /// <summary>
    /// sendwithus CustomerGroup class
    /// </summary>
    public class CustomerGroup
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public CustomerGroup()
        {
            id = String.Empty;
            name = String.Empty;
            description = String.Empty;
        }

        /// <summary>
        /// Get all customer groups.
        /// GET /groups
        /// </summary>
        /// <returns>The API call status and the name and description of all groups in the account</returns>
        /// <exception cref="SendwithusException">Thrown when the API response status code is not success</exception>
        /// <exception cref="InvalidOperationException">Thrown when making a Batch API Request that has already reached the maxmimum API calls per batch request</exception>
        public static async Task<CustomerGroupResponseMultipleGropus> GetCustomeGroupsAsync()
        {
            // Send the GET request
            var resource = "groups";
            var jsonResponse = await RequestManager.SendGetRequestAsync(resource);

            // Convert the JSON result into an object
            var serializer = new JavaScriptSerializer();
            var response = serializer.Deserialize<CustomerGroupResponseMultipleGropus>(jsonResponse);
            return response;
        }

        /// <summary>
        /// Create a customer group.
        /// POST /groups
        /// </summary>
        /// <param name="name">The name of the group</param>
        /// <param name="description">A description of the group</param>
        /// <returns>The API call status and the new group</returns>
        /// <exception cref="SendwithusException">Thrown when the API response status code is not success</exception>
        /// <exception cref="InvalidOperationException">Thrown when making a Batch API Request that has already reached the maxmimum API calls per batch request</exception>
        public static async Task<CustomerGroupResponse> CreateCustomerGroupAsync(string name, string description)
        {
            // Build the request object
            var request = new Dictionary<string, string>();
            request.Add("name", name);
            request.Add("description", description);

            // Send the POST request
            var resource = "groups";
            var jsonResponse = await RequestManager.SendPostRequestAsync(resource, request);

            // Convert the JSON result into an object
            var serializer = new JavaScriptSerializer();
            var response = serializer.Deserialize<CustomerGroupResponse>(jsonResponse);
            return response;
        }

        /// <summary>
        /// Create a customer group.
        /// POST /groups
        /// </summary>
        /// <param name="name">The name of the group</param>
        /// <returns>The API call status and the new group</returns>
        /// <exception cref="SendwithusException">Thrown when the API response status code is not success</exception>
        /// <exception cref="InvalidOperationException">Thrown when making a Batch API Request that has already reached the maxmimum API calls per batch request</exception>
        public static async Task<CustomerGroupResponse> CreateCustomerGroupAsync(string name)
        {
            return await CreateCustomerGroupAsync(name, String.Empty);
        }

        /// <summary>
        /// Update a customer group's name.
        /// PUT /groups/(:group_id)
        /// </summary>
        /// <param name="groupId">The ID of the group to update</param>
        /// <param name="name">The new name of the group</param>
        /// <returns>The API call status and the new group</returns>
        /// <exception cref="SendwithusException">Thrown when the API response status code is not success</exception>
        /// <exception cref="InvalidOperationException">Thrown when making a Batch API Request that has already reached the maxmimum API calls per batch request</exception>
        public static async Task<CustomerGroupResponse> UpdateCustomerGroupNameAsync(string groupId, string name)
        {
            // Build the request object
            var request = new Dictionary<string, string>();
            request.Add("name", name);

            // Send the PUT request
            var resource = String.Format("groups/{0}", groupId);
            var jsonResponse = await RequestManager.SendPutRequestAsync(resource, request);

            // Convert the JSON result into an object
            var serializer = new JavaScriptSerializer();
            var response = serializer.Deserialize<CustomerGroupResponse>(jsonResponse);
            return response;
        }

        /// <summary>
        /// Update a customer group's description.
        /// PUT /groups/(:group_id)
        /// </summary>
        /// <param name="groupId">The ID of the group to update</param>
        /// <param name="description">The new description of the group</param>
        /// <returns>The API call status and the new group</returns>
        /// <exception cref="SendwithusException">Thrown when the API response status code is not success</exception>
        /// <exception cref="InvalidOperationException">Thrown when making a Batch API Request that has already reached the maxmimum API calls per batch request</exception>
        public static async Task<CustomerGroupResponse> UpdateCustomerGroupDescriptionAsync(string groupId, string description)
        {
            // Build the request object
            var request = new Dictionary<string, string>();
            request.Add("description", description);

            // Send the PUT request
            var resource = String.Format("groups/{0}", groupId);
            var jsonResponse = await RequestManager.SendPutRequestAsync(resource, request);

            // Convert the JSON result into an object
            var serializer = new JavaScriptSerializer();
            var response = serializer.Deserialize<CustomerGroupResponse>(jsonResponse);
            return response;
        }

        /// <summary>
        /// Update a customer group's name and description.
        /// PUT /groups/(:group_id)
        /// </summary>
        /// <param name="groupId">The ID of the group to update</param>
        /// <param name="name">The new name of the group</param>
        /// <param name="description">The new description of the group</param>
        /// <returns>The API call status and the new group</returns>
        /// <exception cref="SendwithusException">Thrown when the API response status code is not success</exception>
        /// <exception cref="InvalidOperationException">Thrown when making a Batch API Request that has already reached the maxmimum API calls per batch request</exception>
        public static async Task<CustomerGroupResponse> UpdateCustomerGroupNameAndDescriptionAsync(string groupId, string name, string description)
        {
            // Build the request object
            var request = new Dictionary<string, string>();
            request.Add("name", name);
            request.Add("description", description);

            // Send the PUT request
            var resource = String.Format("groups/{0}", groupId);
            var jsonResponse = await RequestManager.SendPutRequestAsync(resource, request);

            // Convert the JSON result into an object
            var serializer = new JavaScriptSerializer();
            var response = serializer.Deserialize<CustomerGroupResponse>(jsonResponse);
            return response;
        }

        /// <summary>
        /// Delete a customer group.
        /// DELETE /groups/(:group_id)
        /// </summary>
        /// <param name="groupId">The ID of the group to delete</param>
        /// <returns>The API call status</returns>
        /// <exception cref="SendwithusException">Thrown when the API response status code is not success</exception>
        /// <exception cref="InvalidOperationException">Thrown when making a Batch API Request that has already reached the maxmimum API calls per batch request</exception>
        public static async Task<GenericApiCallStatus> DeleteCustomerGroupAsync(string groupId)
        {
            // Send the PUT request
            var resource = String.Format("groups/{0}", groupId);
            var jsonResponse = await RequestManager.SendDeleteRequestAsync(resource);

            // Convert the JSON result into an object
            var serializer = new JavaScriptSerializer();
            var response = serializer.Deserialize<GenericApiCallStatus>(jsonResponse);
            return response;
        }
    }
}
