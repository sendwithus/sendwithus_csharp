using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Sendwithus;
using System.Net;
using System.Diagnostics;

namespace SendwithusTest
{ 

    /// <summary>
    /// Unit testing class for the Log API calls
    /// </summary>
    [TestClass]
    public class LogTest
    {
        private const string API_KEY_TEST = "test_3e7ae15aeb9b8a4b50bce7138c88d81c696edd0d";
        private const string DEFAULT_LOG_ID = "log_88be2c0f8b5c6d3933dd578b6a0f13e5";
        private const int DEFAULT_COUNT = 5;
        private const int DEFAULT_OFFSET = 1;
        private const string INVALID_COUNT = "12345";
        private const UInt64 LOG_CREATED_AFTER_TIME = 1234567890;
        private const UInt64 LOG_CREATED_BEFORE_TIME = 9876543210;
        

        /// <summary>
        /// Tests the API call GET /logs without any parameters
        /// </summary>
        /// <returns>The associated task</returns>
        [TestMethod]
        public async Task TestGetLogsWithNoParametersAsync()
        {
            Trace.WriteLine("GET /logs");
            Sendwithus.Sendwithus.ApiKey = API_KEY_TEST;

            // Make the API call
            var response = await Log.GetLogsAsync();

            // Validate the response
            TestValidation.ValidateResponse(response);
        }


        /// <summary>
        /// Tests the API call GET /logs with all parameters included
        /// </summary>
        /// <returns>The associated task</returns>
        [TestMethod]
        public async Task TestGetLogsWithAllParametersAsync()
        {
            Trace.WriteLine("GET /logs");
            Sendwithus.Sendwithus.ApiKey = API_KEY_TEST;

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
            TestValidation.ValidateResponse(response);
        }

        /// <summary>
        /// Tests the API call GET /logs with an invalid count number (passes a string instead of an int)
        /// </summary>
        /// <returns>The associated task</returns>
        [TestMethod]
        public async Task TestGetLogsWithInvalidCountAsync()
        {
            Trace.WriteLine("GET /logs");
            Sendwithus.Sendwithus.ApiKey = API_KEY_TEST;

            // Build the query parameters
            Dictionary<string, object> queryParameters = new Dictionary<string, object>();
            queryParameters.Add("count", INVALID_COUNT);

            // Make the API call
            try
            {
                var response = await Log.GetLogsAsync(queryParameters);
                Assert.Fail("Failed to throw exception");
            }
            catch (SendwithusException exception)
            {
                // Make sure the response was HTTP 500 Internal Server Error
                TestValidation.ValidateException(exception, HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Tests the API call GET /logs/(:log_id)
        /// </summary>
        /// <returns>The associated task</returns>
        [TestMethod]
        public async Task TestGetLogAsync()
        {
            Trace.WriteLine(String.Format("GET /logs/{0}", DEFAULT_LOG_ID));
            Sendwithus.Sendwithus.ApiKey = API_KEY_TEST;

            // Make the API call
            var response = await Log.GetLogAsync(DEFAULT_LOG_ID);

            // Validate the response
            TestValidation.ValidateResponse(response);
        }

        /// <summary>
        /// Tests the API call GET /logs/(:log_id)/events
        /// </summary>
        /// <returns>The associated task</returns>
        [TestMethod]
        public async Task TestGetLogEventsAsync()
        {
            Trace.WriteLine(String.Format("GET /logs/{0}/events", DEFAULT_LOG_ID));
            Sendwithus.Sendwithus.ApiKey = API_KEY_TEST;

            // Make the API call
            var response = await Log.GetLogEventsAsync(DEFAULT_LOG_ID);

            // Validate the response
            TestValidation.ValidateResponse(response);
        }

        /// <summary>
        /// Tests the API call POST /resend
        /// </summary>
        /// <returns>The associated task</returns>
        [TestMethod]
        public async Task TestResendLogAsync()
        {
            Trace.WriteLine("POST /resend");
            Sendwithus.Sendwithus.ApiKey = API_KEY_TEST;

            // Make the API call
            var response = await Log.ResendLogAsync(DEFAULT_LOG_ID);

            // Validate the response
            TestValidation.ValidateResponse(response);
        }
    }
}
