using NUnit.Framework;
using Sendwithus;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;

namespace SendwithusTest
{

    /// <summary>
    /// Unit testing class for the Log API calls
    /// </summary>
    [TestFixture]
    public class LogTest
    {
        private const string DEFAULT_LOG_ID = "log_e7c783fc7efca27006e043ca12282e9f-3";
        private const int DEFAULT_COUNT = 5;
        private const int DEFAULT_OFFSET = 1;
        private const string INVALID_COUNT = "12345";
        private const Int64 LOG_CREATED_AFTER_TIME = 1234567890;
        private const Int64 LOG_CREATED_BEFORE_TIME = 9876543210;

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
        /// Tests the API call GET /logs/(:log_id)
        /// </summary>
        /// <returns>The associated task</returns>
        [Test]
        public async Task TestGetLogAsync()
        {
            Trace.WriteLine(String.Format("GET /logs/{0}", DEFAULT_LOG_ID));

            // Make the API call
            try
            { 
                var log = await Log.GetLogAsync(DEFAULT_LOG_ID);

                // Validate the response
                SendwithusClientTest.ValidateResponse(log);
            }
            catch (AggregateException exception)
            {
                Assert.Fail(exception.ToString());
            }
        }

        /// <summary>
        /// Tests the API call GET /logs/(:log_id)/events
        /// </summary>
        /// <returns>The associated task</returns>
        [Test]
        public async Task TestGetLogEventsAsync()
        {
            Trace.WriteLine(String.Format("GET /logs/{0}/events", DEFAULT_LOG_ID));

            // Make the API call
            try
            { 
                var logEvents = await Log.GetLogEventsAsync(DEFAULT_LOG_ID);

                // Validate the response
                SendwithusClientTest.ValidateResponse(logEvents);
            }
            catch (AggregateException exception)
            {
                Assert.Fail(exception.ToString());
            }
        }

        /// <summary>
        /// Tests the API call POST /resend
        /// </summary>
        /// <returns>The associated task</returns>
        [Test]
        public async Task TestResendLogAsync()
        {
            Trace.WriteLine("POST /resend");

            // Make the API call
            try
            { 
                var logResendResponse = await Log.ResendLogAsync(DEFAULT_LOG_ID);

                // Validate the response
                SendwithusClientTest.ValidateResponse(logResendResponse);
            }
            catch (AggregateException exception)
            {
                Assert.Fail(exception.ToString());
            }
        }
    }
}
