using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sendwithus;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Net;
using Xunit;
using Xunit.Abstractions;

namespace SendwithusTest.Tests
{
    /// <summary>
    /// Unit testing class for the Segment API calls
    /// </summary>
    public class SegmentTest
    {
        private const string DEFAULT_SEGMENT_ID = "seg_TBe8jzK8xUjT5GExjkPgtm";
        private const string INVALID_SEGMENT_ID = "invalid_segment_id";
        private const string DEFAULT_TEMPLATE_ID = "tem_SxZKpxJSHPbYDWRSQnAQUR";
        private const string DEFAULT_ESP_ACCOUNT_ID = "esp_e3ut7pFtWttcN4HNoQ8Vgm";

        readonly ITestOutputHelper Output;

        /// <summary>
        /// Default constructor with an output object - used to output messages to the Test Explorer
        /// </summary>
        /// <param name="output"></param>
        public SegmentTest(ITestOutputHelper output)
        {
            Output = output;
        }

        /// <summary>
        /// Tests the API call GET /segments
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [Fact]
        public async Task TestGetSegmentsAsync()
        {
            Output.WriteLine("GET /segments");

            // Use the production API key so that the emails are actually sent
            SendwithusClient.ApiKey = SendwithusClientTest.API_KEY_PRODUCTION;

            // Make the API call
            var response = await Segment.GetSegmentsAsync();

            // Validate the response
            SendwithusClientTest.ValidateResponse(response, Output);
        }

        /// <summary>
        /// Tests the API call POST /segments/(:segment_id)/send with the minimum parameters
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [Fact]
        public async Task TestSendToSegmentWithMinimumParametersAsync()
        {
            Output.WriteLine(String.Format("POST /segments/{0}/send with the minimum parameters", DEFAULT_SEGMENT_ID));

            // Use the production API key so that the emails are actually sent
            SendwithusClient.ApiKey = SendwithusClientTest.API_KEY_PRODUCTION;

            // Build the segment content
            var segmentContent = new SegmentContent(DEFAULT_TEMPLATE_ID);

            // Make the API call
            var response = await Segment.SendToSegmentAsync(DEFAULT_SEGMENT_ID, segmentContent);

            // Validate the response
            SendwithusClientTest.ValidateResponse(response, Output);
        }

        /// <summary>
        /// Tests the API call POST /segments/(:segment_id)/send with all parameters
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [Fact]
        public async Task TestSendToSegmentWithAllParametersAsync()
        {
            Output.WriteLine(String.Format("POST /segments/{0}/send with all parameters", DEFAULT_SEGMENT_ID));

            // Use the production API key so that the emails are actually sent
            SendwithusClient.ApiKey = SendwithusClientTest.API_KEY_PRODUCTION;

            // Build the segment content
            var segmentContent = new SegmentContent(DEFAULT_TEMPLATE_ID);
            var contentOne = new Dictionary<string, string>();
            contentOne.Add("url", "http://www.example.com/1");
            contentOne.Add("image", "http://www.example.com/image1.jpg");
            contentOne.Add("text", "Check this sweet thing out!");
            var contentTwo = new Dictionary<string, string>();
            contentTwo.Add("url", "http://www.example.com/2");
            contentTwo.Add("image", "http://www.example.com/image2.jpg");
            contentTwo.Add("text", "Check this other sweet thing out!");
            var contentList = new List<Dictionary<string, string>>();
            contentList.Add(contentOne);
            contentList.Add(contentTwo);
            segmentContent.email_data.Add("Weekly_Newsletter", contentList);
            segmentContent.esp_account = DEFAULT_ESP_ACCOUNT_ID;

            // Make the API call
            var response = await Segment.SendToSegmentAsync(DEFAULT_SEGMENT_ID, segmentContent);

            // Validate the response
            SendwithusClientTest.ValidateResponse(response, Output);
        }

        /// <summary>
        /// Tests the API call POST /segments/(:segment_id)/send with an invalid segment ID
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [Fact]
        public async Task TestSendToSegmentWithInvalidSegmentIdAsync()
        {
            Output.WriteLine(String.Format("POST /segments/{0}/send with invalid segment ID", INVALID_SEGMENT_ID));

            // Use the production API key so that the emails are actually sent
            SendwithusClient.ApiKey = SendwithusClientTest.API_KEY_PRODUCTION;

            // Build the segment content
            var segmentContent = new SegmentContent(DEFAULT_TEMPLATE_ID);

            // Make the API call
            try
            {
                var response = await Segment.SendToSegmentAsync(INVALID_SEGMENT_ID, segmentContent);
            }
            catch (SendwithusException exception)
            {
                // Make sure the response was HTTP 400 Bad Request 
                SendwithusClientTest.ValidateException(exception, HttpStatusCode.BadRequest, Output);
            }
        }
    }
}
