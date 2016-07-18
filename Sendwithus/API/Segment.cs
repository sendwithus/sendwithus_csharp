using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Web.Script.Serialization;

namespace Sendwithus
{
    /// <summary>
    /// sendwithus Segment class
    /// </summary>
    public class Segment
    {
        public string Object { get; set; }
        public Int64 created { get; set; }
        public string id { get; set; }
        public string name { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public Segment()
        {
            Object = String.Empty;
            created = 0;
            id = String.Empty;
            name = String.Empty;
        }

        /// <summary>
        /// GET /segments
        /// </summary>
        /// <returns>All of the segments</returns>
        /// <exception cref="AggregateException">Thrown when the API response status code is not success or when the API call times out</exception>
        /// <exception cref="InvalidOperationException">Thrown when making a Batch API Request that has already reached the maxmimum API calls per batch request</exception>
        public static async Task<List<Segment>> GetSegmentsAsync()
        {
            // Send the GET request
            var resource = "segments";
            var jsonResponse = await RequestManager.SendGetRequestAsync(resource);

            // Convert the JSON result into an object
            var serializer = new JavaScriptSerializer();
            var response = serializer.Deserialize<List<Segment>>(jsonResponse);
            return response;
        }

        /// <summary>
        /// POST /segments/(:segment_id)/send
        /// </summary>
        /// <param name="segmentId">The ID of the segment to send</param>
        /// <param name="segmentContent">The content for the segment</param>
        /// <returns>All of the segments</returns>
        /// <exception cref="AggregateException">Thrown when the API response status code is not success or when the API call times out</exception>
        /// <exception cref="InvalidOperationException">Thrown when making a Batch API Request that has already reached the maxmimum API calls per batch request</exception>
        public static async Task<GenericApiCallStatus> SendToSegmentAsync(string segmentId, SegmentContent segmentContent)
        {
            // Send the POST request
            var resource = String.Format("segments/{0}/send", segmentId);
            var jsonResponse = await RequestManager.SendPostRequestAsync(resource, segmentContent);

            // Convert the JSON result into an object
            var serializer = new JavaScriptSerializer();
            var response = serializer.Deserialize<GenericApiCallStatus>(jsonResponse);
            return response;
        }
    }
}
