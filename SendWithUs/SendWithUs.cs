using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * sendwithus API interface.
 * 
 * Reference: https://github.com/sendwithus/sendwithus_java
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
    public abstract class Sendwithus
    {
        public const string API_VERSION = "v1";
        public const string API_PASSWORD = ""; // API uses an empty string as the password
        public const string CLIENT_VERSION = "1.0.0";
        public const string CLIENT_LANGUAGE = "csharp";
        public const string API_PROTO = "https";
        public const string API_HOST = "api.sendwithus.com";
        public const string API_PORT = "443";
        public const string SWU_API_HEADER = "X-SWU-API-KEY";
        public const string SWU_CLIENT_HEADER = "X-SWU-API-CLIENT";
        public const int DEFAULT_RETRY_COUNT = 3;
        public const int DEFAULT_TIMEOUT_MILLISECONDS = 10000; // TODO: check against other APIs to confirm default

        public static string ApiKey;
        public static int RetryCount = DEFAULT_RETRY_COUNT;
        public static int TimeoutDurationMilliseconds;
    }
}
