using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sendwithus;
using System.Diagnostics;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SendwithusTest.Tests
{
    /// <summary>
    /// Unit testing class for the Segment API calls
    /// </summary>
    [TestClass]
    public class SegmentTest
    {
        private const string DEFAULT_SEGMENT_ID = "seg_TBe8jzK8xUjT5GExjkPgtm";
        private const string INVALID_SEGMENT_ID = "invalid_segment_id";
        private const string DEFAULT_TEMPLATE_ID = "tem_SxZKpxJSHPbYDWRSQnAQUR";
        private const string DEFAULT_ESP_ACCOUNT_ID = "esp_e3ut7pFtWttcN4HNoQ8Vgm";

        /// <summary>
        /// Sets the API 
        /// </summary>
        [TestInitialize]
        public void InitializeUnitTesting()
        {
            // Set the API key
            SendwithusClient.ApiKey = SendwithusClientTest.API_KEY_TEST;
        }

        /// <summary>
        /// Tests the API call GET /segments
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [TestMethod]
        public async Task TestGetSegmentsAsync()
        {
            Trace.WriteLine("GET /segments");

            // Make the API call
            var response = await Segment.GetSegmentsAsync();

            // Validate the response
            SendwithusClientTest.ValidateResponse(response);
        }

        /// <summary>
        /// Tests the API call POST /segments/(:segment_id)/send with the minimum parameters
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [TestMethod]
        public async Task TestSendToSegmentWithMinimumParametersAsync()
        {
            Trace.WriteLine(String.Format("POST /segments/{0}/send with the minimum parameters", DEFAULT_SEGMENT_ID));

            // Build the segment content
            var segmentContent = new SegmentContent(DEFAULT_TEMPLATE_ID);

            // Make the API call
            var response = await Segment.SendToSegmentAsync(DEFAULT_SEGMENT_ID, segmentContent);

            // Validate the response
            SendwithusClientTest.ValidateResponse(response);
        }

        /// <summary>
        /// Tests the API call POST /segments/(:segment_id)/send with all parameters
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [TestMethod]
        public async Task TestSendToSegmentWithAllParametersAsync()
        {
            Trace.WriteLine(String.Format("POST /segments/{0}/send with all parameters", DEFAULT_SEGMENT_ID));

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
            SendwithusClientTest.ValidateResponse(response);
        }

        /// <summary>
        /// Tests the API call POST /segments/(:segment_id)/send with an invalid segment ID
        /// </summary>
        /// <returns>The asynchronous task</returns>
        [TestMethod]
        public async Task TestSendToSegmentWithInvalidSegmentIdAsync()
        {
            Trace.WriteLine(String.Format("POST /segments/{0}/send with invalid segment ID", INVALID_SEGMENT_ID));

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
                SendwithusClientTest.ValidateException(exception, HttpStatusCode.BadRequest);
            }
        }
    }
}
