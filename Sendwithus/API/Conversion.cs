using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Sendwithus
{
    /// <summary>
    /// sendwithus Conversion class
    /// </summary>
    public class Conversion
    {
        public int revenue { get; set; }
        public Int64 timestamp { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public Conversion() : this(0, 0) { }

        /// <summary>
        /// Creates a Conversion object with a given revenue
        /// </summary>
        /// <param name="revenue">The conversion revenue, in cents</param>
        public Conversion(int revenue) : this(revenue, 0) { }

        /// <summary>
        /// Creates a Conversion object with a given revenue and timestamp
        /// </summary>
        /// <param name="revenue">The conversion revenue, in cents</param>
        /// <param name="timestamp">The timestamp</param>
        public Conversion (int revenue, Int64 timestamp)
        {
            this.revenue = revenue;
            this.timestamp = timestamp;
        }

        /// <summary>
        /// Add conversion to customer.
        /// POST /customers/[EMAIL_ADDRESS]/conversions
        /// </summary>
        /// <param name="parameters">The parameters to include in the body of the request.  Options include:
        /// revenue (optional) – Revenue associated with this conversion, in cents.
        /// timestamp(optional) – Timestamp for the conversion time, in seconds.
        /// </param>
        /// <returns>The status of the API call</returns>
        /// <exception cref="SendwithusException">Thrown when the API response status code is not success</exception>
        /// <exception cref="InvalidOperationException">Thrown when making a Batch API Request that has already reached the maxmimum API calls per batch request</exception>
        public async Task<GenericApiCallStatus> AddAsync(string emailAddress)
        {
            // Send the POST request
            var resource = String.Format("customers/{0}/conversions", emailAddress);
            var jsonResponse = await RequestManager.SendPostRequestAsync(resource, this);

            // Convert the JSON result into an object
            var serializer = new JavaScriptSerializer();
            return serializer.Deserialize<GenericApiCallStatus>(jsonResponse);
        }
    }
}
