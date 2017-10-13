using Newtonsoft.Json;
using NUnit.Framework;
using Sendwithus;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net;

[assembly: CLSCompliant(true)]
namespace SendwithusTest
{
    public static class SendwithusClientTest
    {
        public const string API_KEY_TEST = "test_3e7ae15aeb9b8a4b50bce7138c88d81c696edd0d"; // Must use this account for all of the unit tests to work
        static Random _random = new Random();

        /// <summary>
        /// Validates the response from an API call
        /// </summary>
        /// <param name="response">The api call's response</param>
        public static void ValidateResponse(object response)
        {
            // Print the response
            Trace.WriteLine(String.Format("Response: {0}", JsonConvert.SerializeObject(response)));

            // Validate the response
            Assert.IsNotNull(response);
        }

        /// <summary>
        /// Validates that the correct exception was thrown from an API call
        /// </summary>
        /// <param name="exception">The aggregate exception to validate</param>
        /// <param name="expectedStatusCode">The expected exception status code</param>
        public static void ValidateException(AggregateException exception, HttpStatusCode expectedStatusCode)
        {
            // Make sure the exception parameter isn't null
            if (exception == null)
            {
                throw new ArgumentNullException("exception");
            }

            // Make sure that only one exception was occurred as we were expecting a non-retriable exception
            Assert.AreEqual(1, exception.InnerExceptions.Count);

            // Try converting the exception to a SendwithusException
            var sendwithusException = exception.InnerExceptions[0] as SendwithusException;
            Assert.IsNotNull(sendwithusException);

            // Print the exception details
            Trace.WriteLine(String.Format("Exception Status Code: {0}", sendwithusException.StatusCode.ToString()));
            Trace.WriteLine(String.Format("Exception Message: {0}", sendwithusException.Message));

            // Check the exception's status code
            Assert.AreEqual(expectedStatusCode, sendwithusException.StatusCode);
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
              .Select(s => s[_random.Next(s.Length)]).ToArray());
        }

        public static string TimeStampAndRandomNumber()
        {
            var now = DateTimeOffset.UtcNow.ToUniversalTime().ToString();
            var random = new Random();
            return String.Format("{0}_{1}", now, random.Next(0, 1000000));
        }

        /// <summary>
        /// Validates an aggregated exception.
        /// Checks the number of inner exceptions included in it, and the type of each of those exceptions.
        /// </summary>
        /// <typeparam name="T">The expected type of exception</typeparam>
        /// <param name="expectedRetryCount">The expected number of retries (there should be one exception per retry)</param>
        /// <param name="ex">The exception object to validate</param>
        public static void ValidateAggregateException<T>(int expectedRetryCount, Exception ex)
        {
            Trace.WriteLine(String.Format("Aggregate exception message: {0}", ex.Message));

            // Make sure the exception is actually an AggregateException
            var aggregateException = ex as AggregateException;
            Assert.IsNotNull(aggregateException);

            // Make sure the number of exceptions matches the expected number of API call retries
            Assert.AreEqual(expectedRetryCount, aggregateException.InnerExceptions.Count);

            // Make sure that all of the exceptions are of the expected type
            foreach (Exception individualException in aggregateException.InnerExceptions)
            {
                Trace.WriteLine(String.Format("Exception: {0}", individualException.Message));
                Assert.IsTrue(individualException is T);
            }
        }

        /// <summary>
        /// Validates whether or not the API call took the expected amount of time to complete.
        /// Assumes the API call failed on a timeout and used the full retry count.
        /// </summary>
        /// <param name="measuredExecutionTimeMilliseconds">The measured execution time of the API call</param>
        public static void ValidateApiCallExecutionTime(double measuredExecutionTimeMilliseconds)
        {            
            const int DURATION_WINDOW_SIZE_MILLISECONDS = 1000; // 1000ms A large window to handle variability in run time

            // Print the relevant parameters
            var retryCount = SendwithusClient.RetryCount;
            var timeoutMilliseconds = SendwithusClient.GetTimeout().TotalMilliseconds;
            var retryIntervalMilliseconds = SendwithusClient.RetryIntervalMilliseconds;
            Trace.WriteLine(String.Format("Retry Count: {0}", retryCount));
            Trace.WriteLine(String.Format("Timeout: {0}ms", timeoutMilliseconds));
            Trace.WriteLine(String.Format("Retry Interval: {0}ms", retryIntervalMilliseconds));

            // Calculate the expected execution time
            var lowerExecutionTimeLimit = retryCount * timeoutMilliseconds +
                (retryCount - 1) * retryIntervalMilliseconds;
            var upperExecutionTimeLimit = lowerExecutionTimeLimit + DURATION_WINDOW_SIZE_MILLISECONDS;

            Trace.WriteLine(String.Format("Measured Execution Time: {0}ms", measuredExecutionTimeMilliseconds.ToString()));
            Trace.WriteLine(String.Format("Minimum Expected Execution Time: {0}ms", lowerExecutionTimeLimit.ToString()));
            Trace.WriteLine(String.Format("Maximum Expected Execution Time: {0}ms", upperExecutionTimeLimit.ToString()));

            // Check if the measured execution time matches the expected execution time
            Assert.IsTrue(measuredExecutionTimeMilliseconds >= lowerExecutionTimeLimit && measuredExecutionTimeMilliseconds <= upperExecutionTimeLimit);
        }
    }
}
