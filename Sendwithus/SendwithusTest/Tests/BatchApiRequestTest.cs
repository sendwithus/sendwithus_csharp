using NUnit.Framework;
using Sendwithus;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SendwithusTest
{
    /// <summary>
    /// Unit testing class for the Customer Groups API calls
    /// </summary>
    [TestFixture]
    public class BatchApiRequestTest
    {
        private const string DEFAULT_ESP_ACCOUNT_ID = "esp_e3ut7pFtWttcN4HNoQ8Vgm";
        private const string DEFAULT_EMAIL_ADDRESS = "sendwithus.test@gmail.com";

        /// <summary>
        /// Sets the API
        /// </summary>
        [SetUp]
        public void InitializeUnitTesting()
        {
            // Set the API key
            SendwithusClient.ApiKey = SendwithusClientTest.API_KEY_TEST;
        }

        /// <summary>
        /// Tests the API call POST /batch with one API call
        /// </summary>
        /// <returns>The asynchronous task</returns>
		[Test]
        public async Task TestBatchApiRequestsOneRequestAsync()
        {
            Trace.WriteLine("POST /batch");

            // Start the batch request
            BatchApiRequest.StartNewBatchRequest();

            try
            {
                // Make the API calls to be batched
                await Template.GetTemplatesAsync();

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
		[Test]
        public async Task TestBatchApiRequestsTenRequestsAsync()
        {
            Trace.WriteLine("POST /batch");

            // Start the batch request
            BatchApiRequest.StartNewBatchRequest();

            // Make the API calls to be batched (at least one of each type)
            try
            {
                await TemplateTest.BuildAndSendCreateTemplateRequestWithAllParametersAsync(); // POST
                await Snippet.GetSnippetsAsync(); // GET
                await Customer.GetCustomerAsync(CustomerTest.DEFAULT_CUSTOMER_EMAIL_ADDRESS); // GET
                await RenderTest.BuildAndSendRenderTemplateRequestWithAllParametersId(); // POST
                await Log.GetLogsAsync(); // GET
                await EspAccount.SetDefaultEspAccountAsync(DEFAULT_ESP_ACCOUNT_ID); // PUT
                await EmailTest.BuildAndSendEmailWithAllParametersAsync(); // POST
                await DripCampaign.GetDripCampaignsAsync(); // GET
                await CustomerTest.BuildAndSendCreateCustomerRequest(); // POST
                await Customer.DeleteCustomerAsync(CustomerTest.NEW_CUSTOMER_EMAIL_ADDRESS); // DELETE

                // Make the batch Api Reqeust
                var batchResponses = await BatchApiRequest.SendBatchApiRequest();

                // Validate the response to the batch API request
                ValidateBatchApiCallResponses(batchResponses, 10);

                // Validate the response to the individual API calls
                ValidateIndividualBatchedApiCallResponse<Template>(batchResponses[0]);
                ValidateIndividualBatchedApiCallResponse<List<Snippet>>(batchResponses[1]);
                ValidateIndividualBatchedApiCallResponse<Customer>(batchResponses[2]);
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
		[Test]
        public async Task TestBatchApiRequestsElevenRequestsWithoutOverrideAsync()
        {
            Trace.WriteLine("POST /batch");

            // Start the batch request
            BatchApiRequest.StartNewBatchRequest();

            try
            {
                // Make the API calls to be batched (at least one of each type)
                await TemplateTest.BuildAndSendCreateTemplateRequestWithAllParametersAsync(); // POST
                await Snippet.GetSnippetsAsync(); // GET
                await Customer.GetCustomerAsync(CustomerTest.DEFAULT_CUSTOMER_EMAIL_ADDRESS); // GET
                await RenderTest.BuildAndSendRenderTemplateRequestWithAllParametersId(); // POST
                await Log.GetLogsAsync(); // GET
                await EspAccount.SetDefaultEspAccountAsync(DEFAULT_ESP_ACCOUNT_ID); // PUT
                await EmailTest.BuildAndSendEmailWithAllParametersAsync(); // POST
                await DripCampaign.GetDripCampaignsAsync(); // GET
                await CustomerTest.BuildAndSendCreateCustomerRequest(); // POST
                await Customer.DeleteCustomerAsync(CustomerTest.NEW_CUSTOMER_EMAIL_ADDRESS); // DELETE

                // Add the 11th API Request
                try
                {
                    await Customer.GetCustomerAsync(CustomerTest.DEFAULT_CUSTOMER_EMAIL_ADDRESS); // GET
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
                ValidateIndividualBatchedApiCallResponse<Customer>(batchResponses[2]);
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
		[Test]
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
                await TemplateTest.BuildAndSendCreateTemplateRequestWithAllParametersAsync(); // POST
                await Snippet.GetSnippetsAsync(); // GET
                await Customer.GetCustomerAsync(CustomerTest.DEFAULT_CUSTOMER_EMAIL_ADDRESS); // GET
                await RenderTest.BuildAndSendRenderTemplateRequestWithAllParametersId(); // POST
                await Log.GetLogsAsync(); // GET
                await EspAccount.SetDefaultEspAccountAsync(DEFAULT_ESP_ACCOUNT_ID); // PUT
                await EmailTest.BuildAndSendEmailWithAllParametersAsync(); // POST
                await DripCampaign.GetDripCampaignsAsync(); // GET
                await CustomerTest.BuildAndSendCreateCustomerRequest(); // POST
                await Customer.DeleteCustomerAsync(CustomerTest.NEW_CUSTOMER_EMAIL_ADDRESS); // DELETE
                await Customer.GetCustomerAsync(CustomerTest.DEFAULT_CUSTOMER_EMAIL_ADDRESS); // GET

                // Make the batch Api Request
                var batchResponses = await BatchApiRequest.SendBatchApiRequest();

                // Validate the response to the batch API request
                ValidateBatchApiCallResponses(batchResponses, 12);

                // Validate the response to the individual API calls
                ValidateIndividualBatchedApiCallResponse<Template>(batchResponses[0]);
                ValidateIndividualBatchedApiCallResponse<List<Snippet>>(batchResponses[1]);
                ValidateIndividualBatchedApiCallResponse<Customer>(batchResponses[2]);
                ValidateIndividualBatchedApiCallResponse<RenderTemplateResponse>(batchResponses[3]);
                ValidateIndividualBatchedApiCallResponse<List<Log>>(batchResponses[4]);
                ValidateIndividualBatchedApiCallResponse<EspAccountResponse>(batchResponses[5]);
                ValidateIndividualBatchedApiCallResponse<EmailResponse>(batchResponses[6]);
                ValidateIndividualBatchedApiCallResponse<List<DripCampaignDetails>>(batchResponses[7]);
                ValidateIndividualBatchedApiCallResponse<GenericApiCallStatus>(batchResponses[8]);
                ValidateIndividualBatchedApiCallResponse<GenericApiCallStatus>(batchResponses[9]);
                ValidateIndividualBatchedApiCallResponse<Customer>(batchResponses[10]);
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
        /// Tests the AbortBatchRequest function
        /// </summary>
        /// <returns>The asynchronous task</returns>
		[Test]
        public async Task TestBatchApiRequestsAbortRequestAsync()
        {
            Trace.WriteLine("POST /batch");

            // Start the batch request
            BatchApiRequest.StartNewBatchRequest();

            try
            {
                // Make the API call to be batched
                await Template.GetTemplatesAsync();

                // Abort the batch request
                BatchApiRequest.AbortBatchRequest();

                // Make another API call and make sure it goes through
                var snippets = await Snippet.GetSnippetsAsync();
                SendwithusClientTest.ValidateResponse(snippets);

                // Make the aborted batch API Reqeust anyways
                var batchResponses = await BatchApiRequest.SendBatchApiRequest();

                // Make sure no API calls were included in the batch request (empty request)
                ValidateBatchApiCallResponses(batchResponses, 0);
            }
            catch (AggregateException exception)
            {
                Assert.Fail(exception.ToString());
            }
        }

        /// <summary>
        /// Tests PauseBatchRequest and ResumeBatchRequest functions
        /// </summary>
        /// <returns>The asynchronous task</returns>
		[Test]
        public async Task TestBatchApiRequestsPauseAndResumeRequestAsync()
        {
            Trace.WriteLine("POST /batch");

            // Start the batch request
            BatchApiRequest.StartNewBatchRequest();

            try
            {
                // Make the API call to be batched
                await Template.GetTemplatesAsync();

                // Pause the batch request
                BatchApiRequest.PauseBatchRequest();

                // Make another API call and make sure it goes through
                var snippets = await Snippet.GetSnippetsAsync();
                SendwithusClientTest.ValidateResponse(snippets);

                // Resume the batch request and add another API call to it
                BatchApiRequest.ResumeBatchRequest();
                await Customer.GetCustomerAsync(CustomerTest.DEFAULT_CUSTOMER_EMAIL_ADDRESS);

                // Make the final batch request
                var batchResponses = await BatchApiRequest.SendBatchApiRequest();

                // Make sure both API calls were included in the batch request
                ValidateBatchApiCallResponses(batchResponses, 2);

                // Valideate each of those requests
                ValidateIndividualBatchedApiCallResponse<List<Template>>(batchResponses[0]);
                ValidateIndividualBatchedApiCallResponse<Customer>(batchResponses[1]);
            }
            catch (AggregateException exception)
            {
                Assert.Fail(exception.ToString());
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
            var responseBody = response.GetBody<T>();
            SendwithusClientTest.ValidateResponse(responseBody);
        }
    }
}
