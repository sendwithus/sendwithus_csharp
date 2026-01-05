using NUnit.Framework;
using Sendwithus;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SendwithusTest
{

    /// <summary>
    /// Unit testing class for the Log API calls
    /// </summary>
    [NonParallelizable]
    [TestFixture]
    public class LogTest
    {
        private string DEFAULT_LOG_ID;
        private const string DEFAULT_EMAIL_ADDRESS = "sendwithus.test@gmail.com";
        private const string DEFAULT_TEMPLATE_ID = "tem_yn2viZ8Gm2uaydMK9pgR2B";
        private const int DEFAULT_COUNT = 5;
        private const int DEFAULT_OFFSET = 1;
        private const string INVALID_COUNT = "12345";
        private const Int64 LOG_CREATED_AFTER_TIME = 1234567890;
        private const Int64 LOG_CREATED_BEFORE_TIME = 9876543210;
        private const int RETRY_MS = 5000;
        private const int MAX_RETRY_ATTEMPTS = 12; // retry for up to ~1 minute


        /// <summary>
        /// Sets the API
        /// </summary>
        [OneTimeSetUp]
        public void InitializeUnitTesting()
        {
            // Set the API key
            SendwithusClient.ApiKey = SendwithusClientTest.API_KEY_TEST;
            try
            {
                var task = Task.Run(async () =>
                {
                    // your existing async logic
                    var templateData = new Dictionary<string, object>();
                    var recipient = new EmailRecipient(DEFAULT_EMAIL_ADDRESS);
                    var email = new Email(DEFAULT_TEMPLATE_ID, templateData, recipient);
                    var emailResponse = await email.Send();
                    this.DEFAULT_LOG_ID = emailResponse.receipt_id;
                });

                task.Wait(); // synchronously block

                if (this.DEFAULT_LOG_ID == null) {
                    Assert.Fail("Could not get log id");
                }
            }

            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        /// <summary>
        /// Tests the API call GET /logs/(:log_id)
        /// </summary>
        /// <returns>The associated task</returns>
        [Test]
        public async Task TestGetLogAsync()
        {
            Trace.WriteLine(String.Format("GET /logs/{0}", this.DEFAULT_LOG_ID));

            for (int attempt = 1; attempt <= MAX_RETRY_ATTEMPTS; attempt++)
            {
                try
                {
                    var log = await Log.GetLogAsync(this.DEFAULT_LOG_ID);

                    // Validate the response
                    SendwithusClientTest.ValidateResponse(log);
                    return;
                }
                catch (Exception exception)
                {
                    if (attempt == MAX_RETRY_ATTEMPTS)
                    {
                        Assert.Fail(exception.ToString());
                    }
                    else
                    {
                        await Task.Delay(RETRY_MS);
                    }
                }
            }
        }

        /// <summary>
        /// Tests the API call GET /logs/(:log_id)/events
        /// </summary>
        /// <returns>The associated task</returns>
        [Test]
        public async Task TestGetLogEventsAsync()
        {
            Trace.WriteLine(String.Format("GET /logs/{0}/events", this.DEFAULT_LOG_ID));

            for (int attempt = 1; attempt <= MAX_RETRY_ATTEMPTS; attempt++)
            {
                try
                {
                    var logEvents = await Log.GetLogEventsAsync(this.DEFAULT_LOG_ID);

                    // Validate the response
                    SendwithusClientTest.ValidateResponse(logEvents);
                    return;
                }
                catch (Exception exception)
                {
                    if (attempt == MAX_RETRY_ATTEMPTS)
                    {
                        Assert.Fail(exception.ToString());
                    }
                    else
                    {
                        await Task.Delay(RETRY_MS);
                    }
                }
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

            for (int attempt = 1; attempt <= MAX_RETRY_ATTEMPTS; attempt++)
            {
                try
                {
                    var logResendResponse = await Log.ResendLogAsync(this.DEFAULT_LOG_ID);

                    // Validate the response
                    SendwithusClientTest.ValidateResponse(logResendResponse);
                    return;
                }
                catch (Exception exception)
                {
                    if (attempt == MAX_RETRY_ATTEMPTS)
                    {
                        Assert.Fail(exception.ToString());
                    }
                    else
                    {
                        await Task.Delay(RETRY_MS);
                    }
                }
            }
        }
    }
}
