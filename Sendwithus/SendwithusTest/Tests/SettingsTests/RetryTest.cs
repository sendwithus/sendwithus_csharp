using NUnit.Framework;
using Sendwithus;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SendwithusTest
{
    /// <summary>
    /// A class to test the sendwithus API's retry functionality.
    /// Covers the number of retries and the interval between retries
    /// </summary>
    [TestFixture]
    public class RetryTest
    {
        private const int FAILURE_TIMEOUT_MILLISECONDS = 1; // 1ms
        private const string DEFAULT_TEMPLATE_ID = "tem_SxZKpxJSHPbYDWRSQnAQUR";
        private const string DEFAULT_ESP_ACCOUNT_ID = "esp_e3ut7pFtWttcN4HNoQ8Vgm";
        private const int NON_DEFAULT_RETRY_COUNT = SendwithusClient.DEFAULT_RETRY_COUNT + 2;
        private const int NON_DEFAULT_RETRY_INTERVAL_MILLISECONDS = 1000; // 1 second
        private const string DEFAULT_SNIPPET_ID = "snp_bn8c87iXuFWdtYLGJrBAWW";
        private const string NEW_SNIPPET_NAME = "My First Snippet";
        private const string NEW_SNIPPET_BODY = "<h1>Welcome!</h1>";
        private const int UNIQUE_ID_LENGTH = 10;

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
        /// Tests that the default retry count works with an HTTP GET request
        /// </summary>
        /// <returns>The associated task</returns>
        [Test]
        public async Task TestDefaultRetryCountWithGetAsync()
        {

            // Set the timeout low enough that the API call is guaranteed to fail
            SendwithusClient.SetTimeoutInMilliseconds(FAILURE_TIMEOUT_MILLISECONDS);

            // Make sure we're using the default number of retries
            SendwithusClient.RetryCount = SendwithusClient.DEFAULT_RETRY_COUNT;

            // Send a GET request
            try
            {
                var response = await Template.GetTemplateAsync(DEFAULT_TEMPLATE_ID);
            }
            catch (Exception ex)
            {
                SendwithusClientTest.ValidateAggregateException<TaskCanceledException>(SendwithusClient.DEFAULT_RETRY_COUNT, ex);
            }
            finally
            {
                // Set the timeout back to its default value
                SendwithusClient.SetTimeoutInMilliseconds(SendwithusClient.DEFAULT_TIMEOUT_MILLISECONDS);
            }
        }

        /// <summary>
        /// Tests that the default retry count works with an HTTP PUT request
        /// </summary>
        /// <returns>The associated task</returns>
        [Test]
        public async Task TestDefaultRetryCountWithPutAsync()
        {

            // Set the timeout low enough that the API call is guaranteed to fail
            SendwithusClient.SetTimeoutInMilliseconds(FAILURE_TIMEOUT_MILLISECONDS);

            // Make sure we're using the default number of retries
            SendwithusClient.RetryCount = SendwithusClient.DEFAULT_RETRY_COUNT;

            var uniqueName = String.Format("{0}-{1}", NEW_SNIPPET_NAME, SendwithusClientTest.RandomString(UNIQUE_ID_LENGTH));

            // Send a PUT request
            try
            {
                var UpdateSnippetResponse = await Snippet.UpdateSnippetAsync(DEFAULT_SNIPPET_ID, uniqueName, NEW_SNIPPET_BODY);;

            }
            catch (Exception ex)
            {
                SendwithusClientTest.ValidateAggregateException<TaskCanceledException>(SendwithusClient.DEFAULT_RETRY_COUNT, ex);
            }
            finally
            {
                // Set the timeout back to its default value
                SendwithusClient.SetTimeoutInMilliseconds(SendwithusClient.DEFAULT_TIMEOUT_MILLISECONDS);
            }
        }

        /// <summary>
        /// Tests that the default retry count works with an HTTP POST request
        /// </summary>
        /// <returns>The associated task</returns>
        [Test]
        public async Task TestDefaultRetryCountWithPostAsync()
        {
            // Set the timeout low enough that the API call is guaranteed to fail
            SendwithusClient.SetTimeoutInMilliseconds(FAILURE_TIMEOUT_MILLISECONDS);

            // Make sure we're using the default number of retries
            SendwithusClient.RetryCount = SendwithusClient.DEFAULT_RETRY_COUNT;

            // Send a POST request
            try
            {
                var createTemplateResponse = await TemplateTest.BuildAndSendCreateTemplateRequestWithAllParametersAsync();
            }
            catch (Exception ex)
            {
                SendwithusClientTest.ValidateAggregateException<TaskCanceledException>(SendwithusClient.DEFAULT_RETRY_COUNT, ex);
            }
            finally
            {
                // Set the timeout back to its default value
                SendwithusClient.SetTimeoutInMilliseconds(SendwithusClient.DEFAULT_TIMEOUT_MILLISECONDS);
            }
        }

        /// <summary>
        /// Tests that the default retry count works with an HTTP DELETE request
        /// </summary>
        /// <returns>The associated task</returns>
        [Test]
        public async Task TestDefaultRetryCountWithDeleteAsync()
        {
            // Set the timeout low enough that the API call is guaranteed to fail
            SendwithusClient.SetTimeoutInMilliseconds(FAILURE_TIMEOUT_MILLISECONDS);

            // Make sure we're using the default number of retries
            SendwithusClient.RetryCount = SendwithusClient.DEFAULT_RETRY_COUNT;

            // Send a DELETE request
            try
            {   
                var deleteCustomerReponse = await Customer.DeleteCustomerAsync(CustomerTest.NEW_CUSTOMER_EMAIL_ADDRESS);
            }
            catch (Exception ex)
            {
                SendwithusClientTest.ValidateAggregateException<TaskCanceledException>(SendwithusClient.DEFAULT_RETRY_COUNT, ex);
            }
            finally
            {
                // Set the timeout back to its default value
                SendwithusClient.SetTimeoutInMilliseconds(SendwithusClient.DEFAULT_TIMEOUT_MILLISECONDS);
            }
        }

        /// <summary>
        /// Tests that the default retry count can be set to a non-default value
        /// </summary>
        /// <returns>The associated task</returns>
        [Test]
        public async Task TestNonDefaultRetryCountAsync()
        {
            // Set the timeout low enough that the API call is guaranteed to fail
            SendwithusClient.SetTimeoutInMilliseconds(FAILURE_TIMEOUT_MILLISECONDS);

            // Use a non-default number of retries
            SendwithusClient.RetryCount = NON_DEFAULT_RETRY_COUNT;

            // Send a GET request
            try
            {
                var response = await Template.GetTemplateAsync(DEFAULT_TEMPLATE_ID);
            }
            catch (Exception ex)
            {
                SendwithusClientTest.ValidateAggregateException<TaskCanceledException>(NON_DEFAULT_RETRY_COUNT, ex);
            }
            finally
            {
                // Set the timeout back to its default value
                SendwithusClient.SetTimeoutInMilliseconds(SendwithusClient.DEFAULT_TIMEOUT_MILLISECONDS);

                // Set the retry count back to its default value
                SendwithusClient.RetryCount = SendwithusClient.DEFAULT_RETRY_COUNT;
            }
        }

        /// <summary>
        /// Tests that the default retry interval is used properly
        /// </summary>
        /// <returns>The associated task</returns>
        [Test]
        public async Task TestDefaultRetryInterval()
        {
            // Set the timeout low enough that the API call is guaranteed to fail and has a negligible effect on the run time
            SendwithusClient.SetTimeoutInMilliseconds(FAILURE_TIMEOUT_MILLISECONDS);

            // Make sure we're using the default number of retries
            SendwithusClient.RetryCount = SendwithusClient.DEFAULT_RETRY_COUNT;

            // Make sure we're using the default retry interval
            SendwithusClient.RetryIntervalMilliseconds = SendwithusClient.DEFAULT_RETRY_INTERVAL_MILLISECONDS;

            // Send a GET request
            var startTime = Stopwatch.GetTimestamp();
            try
            {
                var response = await Template.GetTemplateAsync(DEFAULT_TEMPLATE_ID);
            }
            catch (Exception ex)
            {
                var endTime = Stopwatch.GetTimestamp();
                
                // Make sure the API call failed on a timeout
                SendwithusClientTest.ValidateAggregateException<TaskCanceledException>(SendwithusClient.DEFAULT_RETRY_COUNT, ex);

                // Validate the API call's execution time
                var elapsedTime = endTime - startTime;
                var elapsedMilliSeconds = elapsedTime * (1000.0 / Stopwatch.Frequency);
                SendwithusClientTest.ValidateApiCallExecutionTime(elapsedMilliSeconds);
            }
            finally
            {
                // Set the timeout back to its default value
                SendwithusClient.SetTimeoutInMilliseconds(SendwithusClient.DEFAULT_TIMEOUT_MILLISECONDS);

                // Set the retry count back to its default value
                SendwithusClient.RetryCount = SendwithusClient.DEFAULT_RETRY_COUNT;
            }
        }

        /// <summary>
        /// Tests that the retry interval works properly when set to a non-default value
        /// </summary>
        /// <returns>The associated task</returns>
        [Test]
        public async Task TestNonDefaultRetryInterval()
        {
            // Set the timeout low enough that the API call is guaranteed to fail and has a negligible effect on the run time
            SendwithusClient.SetTimeoutInMilliseconds(FAILURE_TIMEOUT_MILLISECONDS);

            // Make sure we're using the default number of retries
            //SendwithusClient.RetryCount = SendwithusClient.DEFAULT_RETRY_COUNT;
            SendwithusClient.RetryCount = NON_DEFAULT_RETRY_COUNT;

            // Make sure we're using the default retry interval
            SendwithusClient.RetryIntervalMilliseconds = NON_DEFAULT_RETRY_INTERVAL_MILLISECONDS;

            // Send a GET request
            var startTime = Stopwatch.GetTimestamp();
            try
            {
                var response = await Template.GetTemplateAsync(DEFAULT_TEMPLATE_ID);
            }
            catch (Exception ex)
            {
                var endTime = Stopwatch.GetTimestamp();

                // Make sure the API call failed on a timeout
                //SendwithusClientTest.ValidateAggregateException<TaskCanceledException>(SendwithusClient.DEFAULT_RETRY_COUNT, ex);
                SendwithusClientTest.ValidateAggregateException<TaskCanceledException>(NON_DEFAULT_RETRY_COUNT, ex);

                // Validate the API call's execution time
                var elapsedTime = endTime - startTime;
                var elapsedMilliSeconds = elapsedTime * (1000.0 / Stopwatch.Frequency);
                SendwithusClientTest.ValidateApiCallExecutionTime(elapsedMilliSeconds);
            }
            finally
            {
                // Set the timeout back to its default value
                SendwithusClient.SetTimeoutInMilliseconds(SendwithusClient.DEFAULT_TIMEOUT_MILLISECONDS);

                // Set the retry count back to its default value
                SendwithusClient.RetryCount = SendwithusClient.DEFAULT_RETRY_COUNT;

                // Set the retry interval back to its default value
                SendwithusClient.RetryIntervalMilliseconds = SendwithusClient.DEFAULT_RETRY_INTERVAL_MILLISECONDS;
            }
        }
    }
}
