using NUnit.Framework;
using Sendwithus;
using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;

namespace SendwithusTest
{
    /// <summary>
    /// Unit testing class for the sendwithus Conversions API calls
    /// </summary>
    [TestFixture]
    public class ConversionsTest
    {
        private const string DEFAULT_EMAIL_ADDRESS = "sendwithus.test@gmail.com";
        private const string INVALID_EMAIL_ADDRESS = "invalid_email_address";
        private const int DEFAULT_REVENUE = 1999;
        private const Int64 DEFAULT_TIMESTAMP = 1417321700;

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
        /// Tests the API call POST /customers/[EMAIL_ADDRESS]/conversions with no parameters
        /// </summary>
        /// <returns>The associated task</returns>
        [Test]
        public async Task TestAddConverionWithNoParametersAsync()
        {
            
            Trace.WriteLine(String.Format("POST /customers/{0}/converisons", DEFAULT_EMAIL_ADDRESS));

            // Make the API call
            try
            {
                var genericApiCallStatus = await Conversion.AddConversionAsync(DEFAULT_EMAIL_ADDRESS);

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
        /// <returns>The associated task</returns>
        [Test]
        public async Task TestAddConverionWithRevenueAsync()
        {
            Trace.WriteLine(String.Format("POST /customers/{0}/converisons", DEFAULT_EMAIL_ADDRESS));

            // Make the API call
            try
            {
                var genericApiCallStatus = await Conversion.AddConversionWithRevenueAsync(DEFAULT_EMAIL_ADDRESS, DEFAULT_REVENUE);

                // Validate the response
                SendwithusClientTest.ValidateResponse(genericApiCallStatus);
            }
            catch (AggregateException exception)
            {
                Assert.Fail(exception.ToString());
            }
        }

        /// <summary>
        /// Tests the API call POST /customers/[EMAIL_ADDRESS]/conversions with the timestamp specified
        /// </summary>
        /// <returns>The associated task</returns>
        [Test]
        public async Task TestAddConverionWithTimestampAsync()
        {
            Trace.WriteLine(String.Format("POST /customers/{0}/converisons", DEFAULT_EMAIL_ADDRESS));

            // Make the API call
            try
            {
                var genericApiCallStatus = await Conversion.AddConversionWithTimestampAsync(DEFAULT_EMAIL_ADDRESS, DEFAULT_TIMESTAMP);

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
        /// <returns>The associated task</returns>
        [Test]
        public async Task TestAddConverionWithRevenueAndTimestampAsync()
        {
            Trace.WriteLine(String.Format("POST /customers/{0}/converisons", DEFAULT_EMAIL_ADDRESS));

            // Make the API call
            try
            {
                var genericApiCallStatus = await Conversion.AddConversionWithRevenueAndTimestampAsync(DEFAULT_EMAIL_ADDRESS, DEFAULT_REVENUE, DEFAULT_TIMESTAMP);

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
        /// <returns>The associated task</returns>
        [Test]
        public async Task TestAddConverionWithInvalidEmailAddressAsync()
        {
            Trace.WriteLine(String.Format("POST /customers/{0}/converisons", DEFAULT_EMAIL_ADDRESS));

            // Make the API call
            try
            { 
                var genericApiCallStatus = await Conversion.AddConversionAsync(INVALID_EMAIL_ADDRESS);
            }
            catch (AggregateException exception)
            {
                // Make sure the response was HTTP 400 Bad Request 
                SendwithusClientTest.ValidateException(exception, HttpStatusCode.BadRequest);
            }
        }
    }
}
