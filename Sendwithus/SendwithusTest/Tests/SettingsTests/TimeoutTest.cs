using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sendwithus;
using System.Web.Script.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace SendwithusTest
{
    /// <summary>
    /// A class to test the HTTP Timeout settings for the sendwithus API client
    /// </summary>
    [TestClass]
    public class TimeoutTest
    {
        private const string DEFAULT_TEMPLATE_ID = "tem_SxZKpxJSHPbYDWRSQnAQUR";
        private const int FAILURE_TIMEOUT_MILLISECONDS = 1; // 1ms

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
        /// Makes sure that a basic HTTP GET request does not timeout with the default timeout setting
        /// </summary>
        /// <returns>The associated Task</returns>
        [TestMethod]
        public async Task TestTimeoutDefaultTimeout()
        {
            // Make sure the timeout is set to its default value
            SendwithusClient.SetTimeoutInMilliseconds(SendwithusClient.DEFAULT_TIMEOUT_MILLISECONDS);

            // Send the GET request
            try
            {
                var response = await Template.GetTemplateAsync(DEFAULT_TEMPLATE_ID);
                Trace.WriteLine(String.Format("API call completed with default timeout of: {0}ms", SendwithusClient.DEFAULT_TIMEOUT_MILLISECONDS));

                // Make sure we received a valid response
                SendwithusClientTest.ValidateResponse(response);
            }
            catch (AggregateException exception)
            {
                Assert.Fail(exception.ToString());
            }
        }

        /// <summary>
        /// Makes sure that a basic HTTP GET request time's out when using a very short timeout setting (1ms)
        /// </summary>
        /// <returns>The associated Task</returns>
        [TestMethod]
        public async Task TestTimeoutFailure()
        {
            // Set the timeout to a value that is sure to trigger a failure
            SendwithusClient.SetTimeoutInMilliseconds(FAILURE_TIMEOUT_MILLISECONDS);
            
            // Send the GET request
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
    }
}
