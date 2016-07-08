using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sendwithus;
using System.Web.Script.Serialization;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;

namespace SendwithusTest
{
    public abstract class SendwithusTest
    {
        public const string API_KEY_TEST = "test_3e7ae15aeb9b8a4b50bce7138c88d81c696edd0d";
        public const string API_KEY_PRODUCTION = "live_3cb190a9c3df0defbd0c0ab56e34f3b1747eedfa";
        private static Random random = new Random();

        /// <summary>
        /// Validates the response from an API call
        /// </summary>
        /// <param name="response">The api call's response</param>
        public static void ValidateResponse(object response)
        {
            // Print the response
            var serializer = new JavaScriptSerializer();
            Trace.Write("Response: ");
            Trace.WriteLine(serializer.Serialize(response));
            Trace.Flush();

            // Validate the response
            Assert.IsNotNull(response);
        }

        /// <summary>
        /// Validates that the correct exception was thrown from an API call
        /// </summary>
        /// <param name="exception">The exception to validate</param>
        /// <param name="stausCode">The expected exception status code</param>
        public static void ValidateException(SendwithusException exception, HttpStatusCode expectedStatusCode)
        {
            // Print the exception details
            Trace.Write("Exception Status Code: ");
            Trace.WriteLine(exception.StatusCode.ToString());
            Trace.Write("Exception Message: ");
            Trace.WriteLine(exception.Message);

            // Check the exception's status code
            Assert.AreEqual(expectedStatusCode, exception.StatusCode);
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
    }
}
