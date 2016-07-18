using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Sendwithus;
using System.Net;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SendwithusTest
{
    /// <summary>
    /// Unit testing class for the sendwithus Conversions API calls
    /// </summary>
    [TestClass]
    public class ConversionsTest
    {
        private const string DEFAULT_EMAIL_ADDRESS = "sendwithus.test@gmail.com";
        private const string INVALID_EMAIL_ADDRESS = "invalid_email_address";
        private const int DEFAULT_REVENUE = 1999;
        private const Int64 DEFAULT_TIMESTAMP = 1417321700;

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
        /// Tests the API call POST /customers/[EMAIL_ADDRESS]/conversions with no parameters
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task TestAddConverionWithNoParametersAsync()
        {
            
            Trace.WriteLine(String.Format("POST /customers/{0}/converisons", DEFAULT_EMAIL_ADDRESS));

            // Build the conversion object
            var conversion = new Conversion();

            // Make the API call
            try
            {
                var genericApiCallStatus = await conversion.AddAsync(DEFAULT_EMAIL_ADDRESS);

                // Validate the response
                SendwithusClientTest.ValidateResponse(genericApiCallStatus);
            }
            catch (AggregateException exception)
            {
                Assert.Fail(exception.ToString());
            }
        }

        /// <summary>
        /// Tests the API call POST /customers/[EMAIL_ADDRESS]/conversions with the revenue specified
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task TestAddConverionWithRevenueAsync()
        {
            Trace.WriteLine(String.Format("POST /customers/{0}/converisons", DEFAULT_EMAIL_ADDRESS));

            // Build the conversion object
            var conversion = new Conversion(DEFAULT_REVENUE);

            // Make the API call
            try
            {
                var genericApiCallStatus = await conversion.AddAsync(DEFAULT_EMAIL_ADDRESS);

                // Validate the response
                SendwithusClientTest.ValidateResponse(genericApiCallStatus);
            }
            catch (AggregateException exception)
            {
                Assert.Fail(exception.ToString());
            }
        }

        /// <summary>
        /// Tests the API call POST /customers/[EMAIL_ADDRESS]/conversions with the revenue and timestamp specified
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task TestAddConverionWithRevenueAndTimestampAsync()
        {
            Trace.WriteLine(String.Format("POST /customers/{0}/converisons", DEFAULT_EMAIL_ADDRESS));

            // Build the conversion object
            //var conversion = new Conversion(DEFAULT_REVENUE, DEFAULT_TIMESTAMP);
            var conversion = new Conversion(DEFAULT_REVENUE, DEFAULT_TIMESTAMP);

            // Make the API call
            try
            {
                var genericApiCallStatus = await conversion.AddAsync(DEFAULT_EMAIL_ADDRESS);

                // Validate the response
                SendwithusClientTest.ValidateResponse(genericApiCallStatus);
            }
            catch (AggregateException exception)
            {
                Assert.Fail(exception.ToString());
            }
        }

        /// <summary>
        /// Tests the API call POST /customers/[EMAIL_ADDRESS]/conversions with an invalid email address
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task TestAddConverionWithInvalidEmailAddressAsync()
        {
            Trace.WriteLine(String.Format("POST /customers/{0}/converisons", DEFAULT_EMAIL_ADDRESS));

            // Build the conversion object
            var conversion = new Conversion();

            // Make the API call
            try
            { 
                var genericApiCallStatus = await conversion.AddAsync(INVALID_EMAIL_ADDRESS);
            }
            catch (AggregateException exception)
            {
                // Make sure the response was HTTP 400 Bad Request 
                SendwithusClientTest.ValidateException(exception, HttpStatusCode.BadRequest);
            }
        }
    }
}
