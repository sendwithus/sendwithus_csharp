using System;
using System.Collections.Generic;
using Xunit;
using System.Threading.Tasks;
using Sendwithus;
using System.Net;
using System.Diagnostics;
using Xunit.Abstractions;

namespace SendwithusTest
{ 

    /// <summary>
    /// Unit testing class for the Log API calls
    /// </summary>
    public class LogTest
    {
        private const string DEFAULT_LOG_ID = "log_88be2c0f8b5c6d3933dd578b6a0f13e5";
        private const int DEFAULT_COUNT = 5;
        private const int DEFAULT_OFFSET = 1;
        private const string INVALID_COUNT = "12345";
        private const Int64 LOG_CREATED_AFTER_TIME = 1234567890;
        private const Int64 LOG_CREATED_BEFORE_TIME = 9876543210;

        readonly ITestOutputHelper Output;

        /// <summary>
        /// Default constructor with an output object - used to output messages to the Test Explorer
        /// </summary>
        /// <param name="output"></param>
        public LogTest(ITestOutputHelper output)
        {
            Output = output;
        }

        /// <summary>
        /// Tests the API call GET /logs without any parameters
        /// </summary>
        /// <returns>The associated task</returns>
        [Fact]
        public async Task TestGetLogsWithNoParametersAsync()
        {
            Output.WriteLine("GET /logs");
            SendwithusClient.ApiKey = SendwithusClientTest.API_KEY_TEST;

            // Make the API call
            var response = await Log.GetLogsAsync();

            // Validate the response
            SendwithusClientTest.ValidateResponse(response, Output);
        }


        /// <summary>
        /// Tests the API call GET /logs with all parameters included
        /// </summary>
        /// <returns>The associated task</returns>
        [Fact]
        public async Task TestGetLogsWithAllParametersAsync()
        {
            Output.WriteLine("GET /logs");
            SendwithusClient.ApiKey = SendwithusClientTest.API_KEY_TEST;

            // Build the query parameters
            Dictionary<string, object> queryParameters = new Dictionary<string, object>();
            queryParameters.Add("count", DEFAULT_COUNT);
            queryParameters.Add("offset", DEFAULT_OFFSET);
            queryParameters.Add("created_gt", LOG_CREATED_AFTER_TIME);
            queryParameters.Add("created_gte", LOG_CREATED_AFTER_TIME);
            queryParameters.Add("created_lt", LOG_CREATED_BEFORE_TIME);
            queryParameters.Add("created_lte", LOG_CREATED_BEFORE_TIME);

            // Make the API call
            var response = await Log.GetLogsAsync(queryParameters);

            // Validate the response
            SendwithusClientTest.ValidateResponse(response, Output);
        }

        /// <summary>
        /// Tests the API call GET /logs with an invalid count number (passes a string instead of an int)
        /// </summary>
        /// <returns>The associated task</returns>
        [Fact]
        public async Task TestGetLogsWithInvalidCountAsync()
        {
            Output.WriteLine("GET /logs");
            SendwithusClient.ApiKey = SendwithusClientTest.API_KEY_TEST;

            // Build the query parameters
            Dictionary<string, object> queryParameters = new Dictionary<string, object>();
            queryParameters.Add("count", INVALID_COUNT);

            // Make the API call
            try
            {
                var response = await Log.GetLogsAsync(queryParameters);
                Assert.True(false, "Failed to throw exception");
            }
            catch (SendwithusException exception)
            {
                // Make sure the response was HTTP 500 Internal Server Error
                SendwithusClientTest.ValidateException(exception, HttpStatusCode.InternalServerError, Output);
            }
        }

        /// <summary>
        /// Tests the API call GET /logs/(:log_id)
        /// </summary>
        /// <returns>The associated task</returns>
        [Fact]
        public async Task TestGetLogAsync()
        {
            Output.WriteLine(String.Format("GET /logs/{0}", DEFAULT_LOG_ID));
            SendwithusClient.ApiKey = SendwithusClientTest.API_KEY_TEST;

            // Make the API call
            var response = await Log.GetLogAsync(DEFAULT_LOG_ID);

            // Validate the response
            SendwithusClientTest.ValidateResponse(response, Output);
        }

        /// <summary>
        /// Tests the API call GET /logs/(:log_id)/events
        /// </summary>
        /// <returns>The associated task</returns>
        [Fact]
        public async Task TestGetLogEventsAsync()
        {
            Output.WriteLine(String.Format("GET /logs/{0}/events", DEFAULT_LOG_ID));
            SendwithusClient.ApiKey = SendwithusClientTest.API_KEY_TEST;

            // Make the API call
            var response = await Log.GetLogEventsAsync(DEFAULT_LOG_ID);

            // Validate the response
            SendwithusClientTest.ValidateResponse(response, Output);
        }

        /// <summary>
        /// Tests the API call POST /resend
        /// </summary>
        /// <returns>The associated task</returns>
        [Fact]
        public async Task TestResendLogAsync()
        {
            Output.WriteLine("POST /resend");
            SendwithusClient.ApiKey = SendwithusClientTest.API_KEY_TEST;

            // Make the API call
            var response = await Log.ResendLogAsync(DEFAULT_LOG_ID);

            // Validate the response
            SendwithusClientTest.ValidateResponse(response, Output);
        }
    }
}
