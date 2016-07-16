using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Sendwithus;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.ObjectModel;
using System.Web.Script.Serialization;
using System.Diagnostics;

namespace SendwithusTest
{
    /// <summary>
    /// Unit testing class for the Customer Groups API calls
    /// </summary>
    [TestClass]
    public class BatchApiRequestTest
    {
        private const string DEFAULT_ESP_ACCOUNT_ID = "esp_e3ut7pFtWttcN4HNoQ8Vgm";
        private const string DEFAULT_EMAIL_ADDRESS = "sendwithus.test@gmail.com";

        /// <summary>
        /// Sets the API 
        /// </summary>
        [TestInitialize]
        public void InitializeUnitTesting()
        {
            // Set the API key
            SendwithusClient.ApiKey = SendwithusClientTest.API_KEY_TEST;
        }

        /// <summary>
        /// Tests the API call POST /batch with one API call
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [TestMethod]
        public async Task TestBatchApiRequestsOneRequestAsync()
        {
            Trace.WriteLine("POST /batch");

            // Start the batch request
            BatchApiRequest.StartNewBatchRequest();

            try
            {
                // Make the API calls to be batched
                var templateResponse = await Template.GetTemplatesAsync();

                // Make the batch API Reqeust
                var batchResponses = await BatchApiRequest.SendBatchApiRequest();

                // Validate the response to the batch API request
                ValidateBatchApiCallResponses(batchResponses, 1);

                // Validate the response to the individual API call
                ValidateIndividualBatchedApiCallResponse<List<Template>>(batchResponses[0]);
            }
            catch (AggregateException exception)
            {
                Assert.Fail(exception.ToString());
            }

        }

        /// <summary>
        /// Tests the API call POST /batch with 10 API calls
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [TestMethod]
        public async Task TestBatchApiRequestsTenRequestsAsync()
        {
            Trace.WriteLine("POST /batch");

            // Start the batch request
            BatchApiRequest.StartNewBatchRequest();

            // Make the API calls to be batched (at least one of each type)
            try
            {
                var createTemplateResponse = await TemplateTest.BuildAndSendCreateTemplateRequestWithAllParametersAsync(); // POST
                var getSnippetsResponse = await Snippet.GetSnippetsAsync(); // GET
                var getSegmentsResponse = await Segment.GetSegmentsAsync(); // GET
                var sendRenderTemplateResponse = await RenderTest.BuildAndSendRenderTemplateRequestWithAllParameters(); // POST
                var getLogResponse = await Log.GetLogsAsync(); // GET
                var setDefaultEspAccountResponse = await EspAccount.SetDefaultEspAccountAsync(DEFAULT_ESP_ACCOUNT_ID); // PUT
                var sendEmailRepsonse = await EmailTest.BuildAndSendEmailWithAllParametersAsync(); // POST
                var getDripCampaignsResponse = await DripCampaign.GetDripCampaignsAsync(); // GET
                var createCustomerResponse = await CustomerTest.BuildAndSendCreateCustomerRequest(); // POST
                var deleteCustomerReponse = await Customer.DeleteCustomerAsync(CustomerTest.NEW_CUSTOMER_EMAIL_ADDRESS); // DELETE

                // Make the batch Api Reqeust
                var batchResponses = await BatchApiRequest.SendBatchApiRequest();

                // Validate the response to the batch API request
                ValidateBatchApiCallResponses(batchResponses, 10);

                // Validate the response to the individual API calls
                ValidateIndividualBatchedApiCallResponse<Template>(batchResponses[0]);
                ValidateIndividualBatchedApiCallResponse<List<Snippet>>(batchResponses[1]);
                ValidateIndividualBatchedApiCallResponse<List<Segment>>(batchResponses[2]);
                ValidateIndividualBatchedApiCallResponse<RenderTemplateResponse>(batchResponses[3]);
                ValidateIndividualBatchedApiCallResponse<List<Log>>(batchResponses[4]);
                ValidateIndividualBatchedApiCallResponse<EspAccountResponse>(batchResponses[5]);
                ValidateIndividualBatchedApiCallResponse<EmailResponse>(batchResponses[6]);
                ValidateIndividualBatchedApiCallResponse<List<DripCampaignDetails>>(batchResponses[7]);
                ValidateIndividualBatchedApiCallResponse<GenericApiCallStatus>(batchResponses[8]);
                ValidateIndividualBatchedApiCallResponse<GenericApiCallStatus>(batchResponses[9]);
            }
            catch (AggregateException exception)
            {
                Assert.Fail(exception.ToString());
            }
        }

        /// <summary>
        /// Tests the API call POST /batch with 11 API calls (without overriding the limit)
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [TestMethod]
        public async Task TestBatchApiRequestsElevenRequestsWithoutOverrideAsync()
        {
            Trace.WriteLine("POST /batch");

            // Start the batch request
            BatchApiRequest.StartNewBatchRequest();

            try
            { 
                // Make the API calls to be batched (at least one of each type)
                var createTemplateResponse = await TemplateTest.BuildAndSendCreateTemplateRequestWithAllParametersAsync(); // POST
                var getSnippetsResponse = await Snippet.GetSnippetsAsync(); // GET
                var getSegmentsResponse = await Segment.GetSegmentsAsync(); // GET
                var sendRenderTemplateResponse = await RenderTest.BuildAndSendRenderTemplateRequestWithAllParameters(); // POST
                var getLogResponse = await Log.GetLogsAsync(); // GET
                var setDefaultEspAccountResponse = await EspAccount.SetDefaultEspAccountAsync(DEFAULT_ESP_ACCOUNT_ID); // PUT
                var sendEmailRepsonse = await EmailTest.BuildAndSendEmailWithAllParametersAsync(); // POST
                var getDripCampaignsResponse = await DripCampaign.GetDripCampaignsAsync(); // GET
                var createCustomerResponse = await CustomerTest.BuildAndSendCreateCustomerRequest(); // POST
                var deleteCustomerReponse = await Customer.DeleteCustomerAsync(CustomerTest.NEW_CUSTOMER_EMAIL_ADDRESS); // DELETE

                // Add the 11th API Request
                try
                {
                    var getCustomerGroupsResponse = await CustomerGroup.GetCustomeGroupsAsync(); // GET
                }
                catch (InvalidOperationException exception)
                {
                    Trace.WriteLine(String.Format("Successfully caught exception triggered by adding too many API calls to the batch API request. Error message: {0}", exception.Message));
                    Assert.IsTrue(true);
                }

                // Send the batch AP Request and make sure it still goes through (with the previous 10 requests included)
                var batchResponses = await BatchApiRequest.SendBatchApiRequest();

                // Validate the response to the batch API request
                ValidateBatchApiCallResponses(batchResponses, 10);

                // Validate the response to the individual API calls
                ValidateIndividualBatchedApiCallResponse<Template>(batchResponses[0]);
                ValidateIndividualBatchedApiCallResponse<List<Snippet>>(batchResponses[1]);
                ValidateIndividualBatchedApiCallResponse<List<Segment>>(batchResponses[2]);
                ValidateIndividualBatchedApiCallResponse<RenderTemplateResponse>(batchResponses[3]);
                ValidateIndividualBatchedApiCallResponse<List<Log>>(batchResponses[4]);
                ValidateIndividualBatchedApiCallResponse<EspAccountResponse>(batchResponses[5]);
                ValidateIndividualBatchedApiCallResponse<EmailResponse>(batchResponses[6]);
                ValidateIndividualBatchedApiCallResponse<List<DripCampaignDetails>>(batchResponses[7]);
                ValidateIndividualBatchedApiCallResponse<GenericApiCallStatus>(batchResponses[8]);
                ValidateIndividualBatchedApiCallResponse<GenericApiCallStatus>(batchResponses[9]);
            }
            catch (AggregateException exception)
            {
                Assert.Fail(exception.ToString());
            }
        }

        /// <summary>
        /// Tests the API call POST /batch with 11 API calls (without overriding the limit)
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [TestMethod]
        public async Task TestBatchApiRequestsTwelveRequestsWithoutOverrideAsync()
        {
            Trace.WriteLine("POST /batch");

            // Start the batch request
            BatchApiRequest.StartNewBatchRequest();

            // Override the maximum number of API calls that can be included in this batch
            BatchApiRequest.OverrideMaximumBatchRequests(12);

            // Make the API calls to be batched (at least one of each type)
            try
            {
                var createTemplateResponse = await TemplateTest.BuildAndSendCreateTemplateRequestWithAllParametersAsync(); // POST
                var getSnippetsResponse = await Snippet.GetSnippetsAsync(); // GET
                var getSegmentsResponse = await Segment.GetSegmentsAsync(); // GET
                var sendRenderTemplateResponse = await RenderTest.BuildAndSendRenderTemplateRequestWithAllParameters(); // POST
                var getLogResponse = await Log.GetLogsAsync(); // GET
                var setDefaultEspAccountResponse = await EspAccount.SetDefaultEspAccountAsync(DEFAULT_ESP_ACCOUNT_ID); // PUT
                var sendEmailRepsonse = await EmailTest.BuildAndSendEmailWithAllParametersAsync(); // POST
                var getDripCampaignsResponse = await DripCampaign.GetDripCampaignsAsync(); // GET
                var createCustomerResponse = await CustomerTest.BuildAndSendCreateCustomerRequest(); // POST
                var deleteCustomerReponse = await Customer.DeleteCustomerAsync(CustomerTest.NEW_CUSTOMER_EMAIL_ADDRESS); // DELETE
                var getCustomerGroupsResponse = await CustomerGroup.GetCustomeGroupsAsync(); // GET
                var conversion = new Conversion();
                var addConversionResponse = await conversion.AddAsync(DEFAULT_EMAIL_ADDRESS); // POST

                // Make the batch Api Request
                var batchResponses = await BatchApiRequest.SendBatchApiRequest();

                // Validate the response to the batch API request
                ValidateBatchApiCallResponses(batchResponses, 12);

                // Validate the response to the individual API calls
                ValidateIndividualBatchedApiCallResponse<Template>(batchResponses[0]);
                ValidateIndividualBatchedApiCallResponse<List<Snippet>>(batchResponses[1]);
                ValidateIndividualBatchedApiCallResponse<List<Segment>>(batchResponses[2]);
                ValidateIndividualBatchedApiCallResponse<RenderTemplateResponse>(batchResponses[3]);
                ValidateIndividualBatchedApiCallResponse<List<Log>>(batchResponses[4]);
                ValidateIndividualBatchedApiCallResponse<EspAccountResponse>(batchResponses[5]);
                ValidateIndividualBatchedApiCallResponse<EmailResponse>(batchResponses[6]);
                ValidateIndividualBatchedApiCallResponse<List<DripCampaignDetails>>(batchResponses[7]);
                ValidateIndividualBatchedApiCallResponse<GenericApiCallStatus>(batchResponses[8]);
                ValidateIndividualBatchedApiCallResponse<GenericApiCallStatus>(batchResponses[9]);
                ValidateIndividualBatchedApiCallResponse<CustomerGroupResponseMultipleGropus>(batchResponses[10]);
                ValidateIndividualBatchedApiCallResponse<GenericApiCallStatus>(batchResponses[11]);
            }
            catch (InvalidOperationException exception)
            {
                Trace.WriteLine(String.Format("Successfully caught exception triggered by adding too many API calls to the batch API request. Error message: {0}", exception.Message));
                Assert.IsTrue(true);
            }
            finally
            {
                // Return the max batch request limit to its default value
                BatchApiRequest.SetMaximumBatchRequestsToDefault();
            }
        }

        /// <summary>
        /// Validates a batch API call response
        /// Makes sure the reponse is not null, and checks the number of API responses against the number that were expected
        /// </summary>
        /// <param name="responses">The collection of BatchApiResponses from the batch API call</param>
        /// <param name="expectedResponseCount">The expected number of BatchApiResponse</param>
        private void ValidateBatchApiCallResponses(List<BatchApiResponse> responses, int expectedResponseCount)
        {
            // Standard validation of the response (make sure the response isn't null)
            SendwithusClientTest.ValidateResponse(responses);

            // Make sure we received the expected number of batch API responses
            Assert.AreEqual(expectedResponseCount, responses.Count);
        }

        /// <summary>
        /// Validates an individual batch API call response from the list that was received in the batch API call
        /// </summary>
        /// <typeparam name="T">The type to convert the BatchApiResponse's body to (the type that the individual API call returns)</typeparam>
        /// <param name="response">The individual batch API call</param>
        private void ValidateIndividualBatchedApiCallResponse<T>(BatchApiResponse response)
        {
            var repsponseBody = response.GetBody<T>();
            SendwithusClientTest.ValidateResponse(repsponseBody);
        }
    }
}
