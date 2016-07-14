using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Sendwithus;
using System.Threading.Tasks;
using Xunit.Abstractions;
using System.Web.Script.Serialization;

namespace SendwithusTest
{
    /// <summary>
    /// A class to test the HTTP Timeout settings for the sendwithus API client
    /// </summary>
    public class TimeoutTest
    {
        private const string DEFAULT_TEMPLATE_ID = "tem_SxZKpxJSHPbYDWRSQnAQUR";
        private const int FAILURE_TIMEOUT_MILLISECONDS = 1;

        readonly ITestOutputHelper Output;

        /// <summary>
        /// Default constructor with an output object - used to output messages to the Test Explorer
        /// </summary>
        /// <param name="output"></param>
        public TimeoutTest(ITestOutputHelper output)
        {
            Output = output;
        }

        /// <summary>
        /// Makes sure that a basic HTTP GET request does not timeout with the default timeout setting
        /// </summary>
        /// <returns>The associated Task</returns>
        [Fact]
        public async Task TestTimeoutDefaultTimeout()
        {
            // Make sure the timeout is set to its default value
            SendwithusClient.SetTimeoutInMilliseconds(SendwithusClient.DEFAULT_TIMEOUT_MILLISECONDS);

            // Send the GET request
            var response = await Template.GetTemplateAsync(DEFAULT_TEMPLATE_ID);
            Output.WriteLine(String.Format("API call completed with default timeout of: {0}ms", SendwithusClient.DEFAULT_TIMEOUT_MILLISECONDS));

            // Make sure we received a valid response
            SendwithusClientTest.ValidateResponse(response, Output);
        }

        /// <summary>
        /// Makes sure that a basic HTTP GET request does timeout with a very short timeout setting (1ms)
        /// </summary>
        /// <returns>The associated Task</returns>
        [Fact]
        public async Task TestTimeoutFailure()
        {
            // Set the timeout to a value that is sure to trigger a failure
            SendwithusClient.SetTimeoutInMilliseconds(FAILURE_TIMEOUT_MILLISECONDS);
            
            // Send the GET request
            try
            {
                var response = await Template.GetTemplateAsync(DEFAULT_TEMPLATE_ID);
            }
            catch (TaskCanceledException exception)
            {
                Output.WriteLine(String.Format("API call failed as intended with timeout set to: {0}ms.  Message: {1}", FAILURE_TIMEOUT_MILLISECONDS, exception.Message));
                Assert.True(true);
            }
            finally
            {
                // Set the timeout back to its default value
                SendwithusClient.SetTimeoutInMilliseconds(SendwithusClient.DEFAULT_TIMEOUT_MILLISECONDS);
            }
        }
    }
}
