using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * sendwithus API interface.
 * 
 * Reference: https://github.com/sendwithus/sendwithus_csharp
 * 
 * Author: Chris Hennig
 *  Email: hennig.chris@gmail.com
 *  Github: chennig
 */

[assembly: CLSCompliant(true)]
namespace Sendwithus
{
    /// <summary>
    /// Contains the sendwithus API settings
    /// </summary>
    public static class SendwithusClient
    {
        public static string API_VERSION { get; set; } = "v1";
        public static string CLIENT_VERSION { get; set; } = typeof(SendwithusClient).Assembly.ToString();
        public static string CLIENT_LANGUAGE { get; set; } = "csharp";
        public static string API_PROTO { get; set; } = "https";
        public static string API_HOST { get; set; } = "api.sendwithus.com";
        public static string API_PORT { get; set; } = "443";
        public static string SWU_API_HEADER { get; set; } = "X-SWU-API-KEY";
        public static string SWU_CLIENT_HEADER { get; set; } = "X-SWU-API-CLIENT";
        public const int DEFAULT_RETRY_COUNT = 3; // Default to 3 retries for each API call
        public const int DEFAULT_RETRY_INTERVAL_MILLISECONDS = 100; // Default retry interval of 100ms
        public const Int32 DEFAULT_TIMEOUT_MILLISECONDS = 30000; // 30s

        public static string ApiKey { get; set; }
        public static string ApiPassword { get; set; } = ""; // Password is currently an empty string
        public static int RetryCount { get; set; } = DEFAULT_RETRY_COUNT;
        public static int RetryIntervalMilliseconds { get; set; } = DEFAULT_RETRY_INTERVAL_MILLISECONDS;

        private static TimeSpan _timeout = new TimeSpan(days: 0, hours: 0, minutes: 0, seconds: 0, milliseconds: DEFAULT_TIMEOUT_MILLISECONDS);

        /// <summary>
        /// Sets the timeout setting for the API client to the given timeout, in milliseconds
        /// </summary>
        /// <param name="timeout">The new timeout to use, in milliseconds</param>
        public static void SetTimeoutInMilliseconds(int timeout)
        {
            _timeout = new TimeSpan(days: 0, hours: 0, minutes: 0, seconds: 0, milliseconds: timeout);
        }

        /// <summary>
        /// Gets the timeout setting for the API client
        /// </summary>
        /// <returns>The timeout setting</returns>
        public static TimeSpan GetTimeout()
        {
            return _timeout;
        }
    }
}
