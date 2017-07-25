using Sendwithus.Net;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Sendwithus
{
    /// <summary>
    /// sendwithus Log class
    /// </summary>
    public class Log
    {
        public string Object { get; set; } // capitalized because "object" is a C# datatype
        public string id { get; set; }
        public Int64 created { get; set; }
        public string recipient_name { get; set; }
        public string recipient_address { get; set; }
        public string status { get; set; }
        public string message { get; set; }
        public string email_id { get; set; }
        public string email_name { get; set; }
        public string email_version { get; set; }

        /// <summary>
        /// Get all the logs associated with the account without any query parameters
        /// GET /logs
        /// </summary>
        /// <returns>A list of all the logs that match the given filters</returns>
        /// <exception cref="AggregateException">Thrown when the API response status code is not success or when the API call times out</exception>
        /// <exception cref="InvalidOperationException">Thrown when making a Batch API Request that has already reached the maxmimum API calls per batch request</exception>
        public static async Task<List<Log>> GetLogsAsync()
        {
            return await GetLogsAsync(null);
        }

        /// <summary>
        /// Get the log with the given log ID.
        /// GET /logs/(:log_id)
        /// </summary>
        /// <param name="logID">The ID of the log to retrieve</param>
        /// <returns>The log with the given ID</returns>
        /// <exception cref="AggregateException">Thrown when the API response status code is not success or when the API call times out</exception>
        /// <exception cref="InvalidOperationException">Thrown when making a Batch API Request that has already reached the maxmimum API calls per batch request</exception>
        public static async Task<Log> GetLogAsync(string logID)
        {
            // Send the GET request
            var resource = String.Format("logs/{0}", logID);
            var jsonResponse = await RequestManager.SendGetRequestAsync(resource);

            // Convert the JSON result into an object
            var serializer = new JavaScriptSerializer();
            return serializer.Deserialize<Log>(jsonResponse);
        }

        /// <summary>
        /// Get the events associated with a given log ID.
        /// GET /logs/(:log_id)/events
        /// </summary>
        /// <param name="logID">The ID of the log to retrieve the events</param>
        /// <returns>The events of the given log ID</returns>
        /// <exception cref="AggregateException">Thrown when the API response status code is not success or when the API call times out</exception>
        /// <exception cref="InvalidOperationException">Thrown when making a Batch API Request that has already reached the maxmimum API calls per batch request</exception>
        public static async Task<List<LogEvent>> GetLogEventsAsync(string logID)
        {
            // Send the GET request
            var resource = String.Format("logs/{0}/events", logID);
            var jsonResponse = await RequestManager.SendGetRequestAsync(resource);

            // Convert the JSON result into an object
            var serializer = new JavaScriptSerializer();
            return serializer.Deserialize<List<LogEvent>>(jsonResponse);
        }

        /// <summary>
        /// Resend an existing Log. Resend a specific email by id.
        /// POST /resend
        /// </summary>
        /// <param name="log_id">The ID of the log to resend</param>
        /// <returns>The events of the given log ID</returns>
        /// <exception cref="AggregateException">Thrown when the API response status code is not success or when the API call times out</exception>
        /// <exception cref="InvalidOperationException">Thrown when making a Batch API Request that has already reached the maxmimum API calls per batch request</exception>
        public static async Task<LogResendResponse> ResendLogAsync(string logId)
        {
            // Send the POST request
            var resource = "resend";
            var logIdParameter = new LogIdParameter(logId);
            var jsonResponse = await RequestManager.SendPostRequestAsync(resource, logIdParameter);

            // Convert the JSON result into an object
            var serializer = new JavaScriptSerializer();
            return serializer.Deserialize<LogResendResponse>(jsonResponse);
        }
    }
}
