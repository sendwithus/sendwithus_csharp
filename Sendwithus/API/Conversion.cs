using Sendwithus.Net;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Sendwithus
{
    /// <summary>
    /// sendwithus Conversion class
    /// </summary>
    public static class Conversion
    {
        /// <summary>
        /// Add conversion to customer.
        /// POST /customers/[EMAIL_ADDRESS]/conversions
        /// </summary>
        /// <param name="emailAddress">The email address of the cusotmer to add the conversion to</param>
        /// <returns>The status of the API call</returns>
        /// <exception cref="AggregateException">Thrown when the API response status code is not success or when the API call times out</exception>
        /// <exception cref="InvalidOperationException">Thrown when making a Batch API Request that has already reached the maxmimum API calls per batch request</exception>
        public static async Task<GenericApiCallStatus> AddConversionAsync(string emailAddress)
        {
            // Build an empty list as the API call fails if there isn't at least an empty list in the body
            var conversionParameters = new Dictionary<string, object>();

            // Send the POST request
            var resource = String.Format("customers/{0}/conversions", emailAddress);
            var jsonResponse = await RequestManager.SendPostRequestAsync(resource, conversionParameters);

            // Convert the JSON result into an object
            var serializer = new JavaScriptSerializer();
            return serializer.Deserialize<GenericApiCallStatus>(jsonResponse);
        }

        /// <summary>
        /// Add conversion to customer.
        /// POST /customers/[EMAIL_ADDRESS]/conversions
        /// </summary>
        /// <param name="emailAddress">The email address of the cusotmer to add the conversion to</param>
        /// <param name="revenue">The revenue associated with this conversion, in cents</param>
        /// <returns>The status of the API call</returns>
        /// <exception cref="AggregateException">Thrown when the API response status code is not success or when the API call times out</exception>
        /// <exception cref="InvalidOperationException">Thrown when making a Batch API Request that has already reached the maxmimum API calls per batch request</exception>
        public static async Task<GenericApiCallStatus> AddConversionWithRevenueAsync(string emailAddress, int revenue)
        {
            // Build the conversion parameters list to include as the body of the request
            var conversionParameters = new Dictionary<string, object>();
            conversionParameters.Add("revenue", revenue);

            // Send the POST request
            var resource = String.Format("customers/{0}/conversions", emailAddress);
            var jsonResponse = await RequestManager.SendPostRequestAsync(resource, conversionParameters);

            // Convert the JSON result into an object
            var serializer = new JavaScriptSerializer();
            return serializer.Deserialize<GenericApiCallStatus>(jsonResponse);
        }

        /// <summary>
        /// Add conversion to customer.
        /// POST /customers/[EMAIL_ADDRESS]/conversions
        /// </summary>
        /// <param name="emailAddress">The email address of the cusotmer to add the conversion to</param>
        /// <param name="timestamp">The timestamp for the conversion time, in seconds</param>
        /// <returns>The status of the API call</returns>
        /// <exception cref="AggregateException">Thrown when the API response status code is not success or when the API call times out</exception>
        /// <exception cref="InvalidOperationException">Thrown when making a Batch API Request that has already reached the maxmimum API calls per batch request</exception>
        public static async Task<GenericApiCallStatus> AddConversionWithTimestampAsync(string emailAddress, Int64 timestamp)
        {
            // Build the conversion parameters list to include as the body of the request
            var conversionParameters = new Dictionary<string, object>();
            conversionParameters.Add("timestamp", timestamp);

            // Send the POST request
            var resource = String.Format("customers/{0}/conversions", emailAddress);
            var jsonResponse = await RequestManager.SendPostRequestAsync(resource, conversionParameters);

            // Convert the JSON result into an object
            var serializer = new JavaScriptSerializer();
            return serializer.Deserialize<GenericApiCallStatus>(jsonResponse);
        }

        /// <summary>
        /// Add conversion to customer.
        /// POST /customers/[EMAIL_ADDRESS]/conversions
        /// </summary>
        /// <param name="emailAddress">The email address of the cusotmer to add the conversion to</param>
        /// <param name="revenue">The revenue associated with this conversion, in cents</param>
        /// <param name="timestamp">The timestamp for the conversion time, in seconds</param>
        /// <returns>The status of the API call</returns>
        /// <exception cref="AggregateException">Thrown when the API response status code is not success or when the API call times out</exception>
        /// <exception cref="InvalidOperationException">Thrown when making a Batch API Request that has already reached the maxmimum API calls per batch request</exception>
        public static async Task<GenericApiCallStatus> AddConversionWithRevenueAndTimestampAsync(string emailAddress, int revenue, Int64 timestamp)
        {
            // Build the conversion parameters list to include as the body of the request
            var conversionParameters = new Dictionary<string, object>();
            conversionParameters.Add("revenue", revenue);
            conversionParameters.Add("timestamp", timestamp);

            // Send the POST request
            var resource = String.Format("customers/{0}/conversions", emailAddress);
            var jsonResponse = await RequestManager.SendPostRequestAsync(resource, conversionParameters);

            // Convert the JSON result into an object
            var serializer = new JavaScriptSerializer();
            return serializer.Deserialize<GenericApiCallStatus>(jsonResponse);
        }
    }
}
