using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sendwithus;
using System.Diagnostics;
using System.Net;
using Xunit;
using Xunit.Abstractions;
using System.Collections.ObjectModel;

namespace SendwithusTest
{
    /// <summary>
    /// Unit testing class for the Customer Groups API calls
    /// </summary>
    public class CustomerGroupTest
    {
        private const string DEFAULT_CUSTOMER_GROUP_ID = "grp_7zpRYpExEBPpd6dGvyAfcT";
        private const string INVALID_CUSTOMER_GROUP_ID = "invalid_customer_group_id";
        private const string DEFAULT_CUSTOMER_GROUP_NAME = "new_group";
        private const string DEFAULT_CUSTOMER_GROUP_DESCRIPTION = "a description of the group";

        private static Collection<string> NewCustomerGroupIds = new Collection<string>();

        readonly ITestOutputHelper Output;

        /// <summary>
        /// Default constructor with an output object - used to output messages to the Test Explorer
        /// </summary>
        /// <param name="output"></param>
        public CustomerGroupTest(ITestOutputHelper output)
        {
            Output = output;
        }

        /// <summary>
        /// Tests the API call GET /groups
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [Fact]
        public async Task TestGetCustomerGroupsAsync()
        {
            Output.WriteLine("GET /groups");

            // Use the production API key so that the emails are actually sent
            SendwithusClient.ApiKey = SendwithusClientTest.API_KEY_PRODUCTION;

            // Make the API call
            var response = await CustomerGroup.GetCustomeGroupsAsync();

            // Validate the response
            SendwithusClientTest.ValidateResponse(response, Output);
        }

        /// <summary>
        /// Tests the API call POST /groups with only the group name
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [Fact]
        public async Task TestCreateCustomerGroupWithNameOnlyAsync()
        {
            Output.WriteLine("POST /groups with only the group name");

            // Use the production API key so that the emails are actually sent
            SendwithusClient.ApiKey = SendwithusClientTest.API_KEY_PRODUCTION;

            // Make the API call
            var groupName = String.Format("{0}_{1}", DEFAULT_CUSTOMER_GROUP_NAME, SendwithusClientTest.RandomString(10));
            var response = await CustomerGroup.CreateCustomerGroupAsync(groupName);

            // Add the new customer group ID to the list of customer groups that can be used for deletion
            NewCustomerGroupIds.Add(response.group.id);

            // Validate the response
            SendwithusClientTest.ValidateResponse(response, Output);
        }

        /// <summary>
        /// Tests the API call POST /groups with a name and description
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [Fact]
        public async Task TestCreateCustomerGroupWithNameAndDescriptionAsync()
        {
            Output.WriteLine("POST /groups with a name and description");

            // Use the production API key so that the emails are actually sent
            SendwithusClient.ApiKey = SendwithusClientTest.API_KEY_PRODUCTION;

            // Make the API call
            var groupName = String.Format("{0}_{1}", DEFAULT_CUSTOMER_GROUP_NAME, SendwithusClientTest.RandomString(10));
            var response = await CustomerGroup.CreateCustomerGroupAsync(groupName, DEFAULT_CUSTOMER_GROUP_DESCRIPTION);

            // Add the new customer group ID to the list of customer groups that can be used for deletion
            NewCustomerGroupIds.Add(response.group.id);

            // Validate the response
            SendwithusClientTest.ValidateResponse(response, Output);
        }

        /// <summary>
        /// Tests the API call PUT /groups/(:group_id) with only the name of the group
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [Fact]
        public async Task TestUpdateCustomerGroupNameAsync()
        {
            Output.WriteLine(String.Format("PUT /groups/{0} with a new name", DEFAULT_CUSTOMER_GROUP_ID));

            // Use the production API key so that the emails are actually sent
            SendwithusClient.ApiKey = SendwithusClientTest.API_KEY_PRODUCTION;

            // Make the API call
            var groupName = String.Format("{0}_{1}", DEFAULT_CUSTOMER_GROUP_NAME, SendwithusClientTest.RandomString(10));
            var response = await CustomerGroup.UpdateCustomerGroupNameAsync(DEFAULT_CUSTOMER_GROUP_ID, groupName);

            // Validate the response
            SendwithusClientTest.ValidateResponse(response, Output);
        }

        /// <summary>
        /// Tests the API call PUT /groups/(:group_id) with only the description of the group
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [Fact]
        public async Task TestUpdateCustomerGroupDescriptionAsync()
        {
            Output.WriteLine(String.Format("PUT /groups/{0} with a new description", DEFAULT_CUSTOMER_GROUP_ID));

            // Use the production API key so that the emails are actually sent
            SendwithusClient.ApiKey = SendwithusClientTest.API_KEY_PRODUCTION;

            // Make the API call
            var groupDescription = String.Format("{0}_{1}", DEFAULT_CUSTOMER_GROUP_DESCRIPTION, SendwithusClientTest.RandomString(10));
            var response = await CustomerGroup.UpdateCustomerGroupDescriptionAsync(DEFAULT_CUSTOMER_GROUP_ID, groupDescription);

            // Validate the response
            SendwithusClientTest.ValidateResponse(response, Output);
        }

        /// <summary>
        /// Tests the API call PUT /groups/(:group_id) with the name and description of the group
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [Fact]
        public async Task TestUpdateCustomerGroupNameAndDescriptionAsync()
        {
            Output.WriteLine(String.Format("PUT /groups/{0} with a new name and description", DEFAULT_CUSTOMER_GROUP_ID));

            // Use the production API key so that the emails are actually sent
            SendwithusClient.ApiKey = SendwithusClientTest.API_KEY_PRODUCTION;

            // Make the API call
            var groupName = String.Format("{0}_{1}", DEFAULT_CUSTOMER_GROUP_NAME, SendwithusClientTest.RandomString(10));
            var groupDescription = String.Format("{0}_{1}", DEFAULT_CUSTOMER_GROUP_DESCRIPTION, SendwithusClientTest.RandomString(10));
            var response = await CustomerGroup.UpdateCustomerGroupNameAndDescriptionAsync(DEFAULT_CUSTOMER_GROUP_ID, groupName, groupDescription);

            // Validate the response
            SendwithusClientTest.ValidateResponse(response, Output);
        }

        /// <summary>
        /// Tests the API call PUT /groups/(:group_id) with an invalid customer group ID
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [Fact]
        public async Task TestUpdateCustomerGroupNameInvalidGroupIdAsync()
        {
            Output.WriteLine(String.Format("PUT /groups/{0} with an invalid customer group ID", INVALID_CUSTOMER_GROUP_ID));

            // Use the production API key so that the emails are actually sent
            SendwithusClient.ApiKey = SendwithusClientTest.API_KEY_PRODUCTION;

            // Make the API call
            var groupName = String.Format("{0}_{1}", DEFAULT_CUSTOMER_GROUP_NAME, SendwithusClientTest.RandomString(10));
            try
            {
                var response = await CustomerGroup.UpdateCustomerGroupNameAsync(INVALID_CUSTOMER_GROUP_ID, groupName);
            }
            catch (SendwithusException exception)
            {
                // Make sure the response was HTTP 400 Bad Request 
                SendwithusClientTest.ValidateException(exception, HttpStatusCode.BadRequest, Output);
            }
        }

        /// <summary>
        /// Tests the API call DELETE /groups/(:group_id)
        /// </summary>
        /// <returns>The asynchronous task</returns>
        public async Task TestDeleteCustomerGroupAsync()
        {
            Output.WriteLine(String.Format("PUT /groups/{0} with a new name and description", DEFAULT_CUSTOMER_GROUP_ID));

            // Use the customer group ID of a newly created customer group to make sure the ID being deleted actually exists
            string customerGroupId = String.Empty;
            if (NewCustomerGroupIds.Count > 0)
            {
                customerGroupId = NewCustomerGroupIds[0];
            }
            else
            {
                Assert.True(false, "No new customer groups available to delete");
            }

            SendwithusClient.ApiKey = SendwithusClientTest.API_KEY_TEST;

            // Make the API call
            var response = await CustomerGroup.DeleteCustomerGroupAsync(customerGroupId);

            // Validate the response
            SendwithusClientTest.ValidateResponse(response, Output);
        }
    }
}
