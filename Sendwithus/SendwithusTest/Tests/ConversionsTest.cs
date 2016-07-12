using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Sendwithus;
using System.Net;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace SendwithusTest
{
    /// <summary>
    /// Unit testing class for the sendwithus Conversions API calls
    /// </summary>
    public class ConversionsTest
    {
        private const string DEFAULT_EMAIL_ADDRESS = "sendwithus.test@gmail.com";
        private const string INVALID_EMAIL_ADDRESS = "invalid_email_address";
        private const int DEFAULT_REVENUE = 1999;
        private const Int64 DEFAULT_TIMESTAMP = 1417321700;

        readonly ITestOutputHelper Output;

        /// <summary>
        /// Default constructor with an output object - used to output messages to the Test Explorer
        /// </summary>
        /// <param name="output"></param>
        public ConversionsTest(ITestOutputHelper output)
        {
            Output = output;
        }

        /// <summary>
        /// Tests the API call POST /customers/[EMAIL_ADDRESS]/conversions with no parameters
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task TestAddConverionWithNoParametersAsync()
        {
            
            Output.WriteLine(String.Format("POST /customers/{0}/converisons", DEFAULT_EMAIL_ADDRESS));
            SendwithusClient.ApiKey = SendwithusClientTest.API_KEY_TEST;

            // Build the conversion object
            var conversion = new Conversion();

            // Make the API call
            var response = await conversion.AddAsync(DEFAULT_EMAIL_ADDRESS);

            // Validate the response
            SendwithusClientTest.ValidateResponse(response, Output);
        }

        /// <summary>
        /// Tests the API call POST /customers/[EMAIL_ADDRESS]/conversions with the revenue specified
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task TestAddConverionWithRevenueAsync()
        {
            Output.WriteLine(String.Format("POST /customers/{0}/converisons", DEFAULT_EMAIL_ADDRESS));
            SendwithusClient.ApiKey = SendwithusClientTest.API_KEY_TEST;

            // Build the conversion object
            var conversion = new Conversion(DEFAULT_REVENUE);

            // Make the API call
            var response = await conversion.AddAsync(DEFAULT_EMAIL_ADDRESS);

            // Validate the response
            SendwithusClientTest.ValidateResponse(response, Output);
        }

        /// <summary>
        /// Tests the API call POST /customers/[EMAIL_ADDRESS]/conversions with the revenue and timestamp specified
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task TestAddConverionWithRevenueAndTimestampAsync()
        {
            Output.WriteLine(String.Format("POST /customers/{0}/converisons", DEFAULT_EMAIL_ADDRESS));
            SendwithusClient.ApiKey = SendwithusClientTest.API_KEY_TEST;

            // Build the conversion object
            var conversion = new Conversion(DEFAULT_REVENUE, DEFAULT_TIMESTAMP);

            // Make the API call
            var response = await conversion.AddAsync(DEFAULT_EMAIL_ADDRESS);

            // Validate the response
            SendwithusClientTest.ValidateResponse(response, Output);
        }

        /// <summary>
        /// Tests the API call POST /customers/[EMAIL_ADDRESS]/conversions with an invalid email address
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task TestAddConverionWithInvalidEmailAddressAsync()
        {
            Output.WriteLine(String.Format("POST /customers/{0}/converisons", DEFAULT_EMAIL_ADDRESS));
            SendwithusClient.ApiKey = SendwithusClientTest.API_KEY_TEST;

            // Build the conversion object
            var conversion = new Conversion();

            // Make the API call
            try { 
                var response = await conversion.AddAsync(INVALID_EMAIL_ADDRESS);
            }
            catch (SendwithusException exception)
            {
                // Make sure the response was HTTP 400 Bad Request 
                SendwithusClientTest.ValidateException(exception, HttpStatusCode.BadRequest, Output);
            }
        }
    }
}
