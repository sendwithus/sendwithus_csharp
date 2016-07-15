using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sendwithus;
using System.Web.Script.Serialization;
using System.Diagnostics;
using Xunit;
using System.Net;
using Xunit.Abstractions;

[assembly: CLSCompliant(true)]
namespace SendwithusTest
{
    public abstract class SendwithusClientTest
    {
        public const string API_KEY_TEST = "test_3e7ae15aeb9b8a4b50bce7138c88d81c696edd0d";
        public const string API_KEY_PRODUCTION = "live_3cb190a9c3df0defbd0c0ab56e34f3b1747eedfa";
        private static Random random = new Random();

        /// <summary>
        /// Validates the response from an API call
        /// </summary>
        /// <param name="response">The api call's response</param>
        public static void ValidateResponse(object response, ITestOutputHelper output)
        {
            // Print the response
            var serializer = new JavaScriptSerializer();
            output.WriteLine(String.Format("Response: {0}", serializer.Serialize(response)));

            // Validate the response
            Assert.NotNull(response);
        }

        /// <summary>
        /// Validates that the correct exception was thrown from an API call
        /// </summary>
        /// <param name="exception">The exception to validate</param>
        /// <param name="stausCode">The expected exception status code</param>
        public static void ValidateException(SendwithusException exception, HttpStatusCode expectedStatusCode, ITestOutputHelper output)
        {
            // Make sure the exception parameter isn't null
            if (exception == null)
            {
                throw new ArgumentNullException("exception");
            }

            // Print the exception details
            output.WriteLine(String.Format("Exception Status Code: {0}", exception.StatusCode.ToString()));
            output.WriteLine(String.Format("Exception Message: {0}", exception.Message));

            // Check the exception's status code
            Assert.Equal(expectedStatusCode, exception.StatusCode);
        }

        /// <summary>
        /// Generates a random string of a fixed length.
        /// To be used for creating unique object names.
        /// Uses capital letters and numbers 0-9
        /// </summary>
        /// <param name="length">The length of the random string</param>
        /// <returns>A random string of alphanumeric characters</returns>
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        /// <summary>
        /// Validates an aggregated exception.
        /// Checks the number of inner exceptions included in it, and the type of each of those exceptions.
        /// </summary>
        /// <typeparam name="T">The expected type of exception</typeparam>
        /// <param name="expectedRetryCount">The expected number of retries (there should be one exception per retry)</param>
        /// <param name="ex">The exception object to validate</param>
        public static void ValidateAggregateException<T>(int expectedRetryCount, Exception ex, ITestOutputHelper output)
        {
            output.WriteLine(String.Format("Aggregate exception message: {0}", ex.Message));

            // Make sure the exception is actually an AggregateException
            var aggregateException = ex as AggregateException;
            Assert.NotNull(aggregateException);

            // Make sure the number of exceptions matches the expected number of API call retries
            Assert.Equal(expectedRetryCount, aggregateException.InnerExceptions.Count);

            // Make sure that all of the exceptions are of the expected type
            foreach (Exception individualException in aggregateException.InnerExceptions)
            {
                output.WriteLine(String.Format("Exception: {0}", individualException.Message));
                Assert.True(individualException is T);
            }
        }

        /// <summary>
        /// Validates whether or not the API call took the expected amount of time to complete.
        /// Assumes the API call failed on a timeout and used the full retry count.
        /// </summary>
        /// <param name="measuredExecutionTimeMilliseconds">The measured execution time of the API call</param>
        public static void ValidateApiCallExecutionTime(double measuredExecutionTimeMilliseconds, ITestOutputHelper output)
        {
            const int DURATION_WINDOW_SIZE_MILLISECONDS = 500; // 500ms A large window to handle variability in run time

            // Print the relevant parameters
            var retryCount = SendwithusClient.RetryCount;
            var timeoutMilliseconds = SendwithusClient.GetTimeout().TotalMilliseconds;
            var retryIntervalMilliseconds = SendwithusClient.RetryIntervalMilliseconds;
            output.WriteLine(String.Format("Retry Count: {0}", retryCount));
            output.WriteLine(String.Format("Timeout: {0}ms", timeoutMilliseconds));
            output.WriteLine(String.Format("Retry Interval: {0}ms", retryIntervalMilliseconds));

            // Calculate the expected execution time
            var lowerExecutionTimeLimit = retryCount * timeoutMilliseconds +
                (retryCount - 1) * retryIntervalMilliseconds;
            var upperExecutionTimeLimit = lowerExecutionTimeLimit + DURATION_WINDOW_SIZE_MILLISECONDS;

            output.WriteLine(String.Format("Measured Execution Time: {0}ms", measuredExecutionTimeMilliseconds.ToString()));
            output.WriteLine(String.Format("Minimum Expected Execution Time: {0}ms", lowerExecutionTimeLimit.ToString()));
            output.WriteLine(String.Format("Maximum Expected Execution Time: {0}ms", upperExecutionTimeLimit.ToString()));

            // Check if the measured execution time matches the expected execution time
            Assert.InRange<double>(measuredExecutionTimeMilliseconds, lowerExecutionTimeLimit, upperExecutionTimeLimit);
        }
    }
}
