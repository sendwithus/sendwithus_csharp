﻿using Sendwithus.Net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Sendwithus
{
    /// <summary>
    /// sendwithus BatchApiRequest
    /// </summary>
    public class BatchApiRequest
    {
        private const int DEFAULT_MAXIMUM_BATCH_REQUESTS = 10;

        private static Collection<BatchApiRequest> _batchApiRequests = new Collection<BatchApiRequest>();
        private static int _maxBatchRequests = DEFAULT_MAXIMUM_BATCH_REQUESTS;
        private static bool _batchApiModeEnabled = false;

        public string path { get; set; }
        public string method { get; set; }
        public object body { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public BatchApiRequest() : this(String.Empty, String.Empty, null) { }

        /// <summary>
        /// Constructor with a given path and method
        /// </summary>
        /// <param name="path">The full resource path for the request (starting from /api/...)</param>
        /// <param name="method">The HTTP method for the request ("GET", "PUT", "POST", "DELETE")</param>
        public BatchApiRequest(string path, string method) : this(path, method, null) { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="path">The full resource path for the request (starting from /api/...)</param>
        /// <param name="method">The HTTP method for the request ("GET", "PUT", "POST", "DELETE")</param>
        /// <param name="body">The body of the API request</param>
        public BatchApiRequest(string path, string method, object body)
        {
            this.path = path;
            this.method = method;
            this.body = body;
        }

        /// <summary>
        /// Starts a new Batch Request.
        /// Enables batch API mode and clears the list of existing batch requests.
        /// </summary>
        public static void StartNewBatchRequest()
        {
            _batchApiModeEnabled = true;
            _batchApiRequests.Clear();
        }

        /// <summary>
        /// Sends a batch API request with all of the currently batched API request.
        /// POST /batch
        /// </summary>
        /// <returns>The responses for each API request in the batch</returns>
        /// <exception cref="AggregateException">Thrown when the API response status code is not success or when the API call times out</exception>
        /// <exception cref="InvalidOperationException">Thrown when making a Batch API Request that has already reached the maxmimum API calls per batch request</exception>
        public static async Task<List<BatchApiResponse>> SendBatchApiRequest()
        {
            _batchApiModeEnabled = false;

            // Send the batch request
            var resource = "batch";
            var jsonResponse = await RequestManager.SendPostRequestAsync(resource, _batchApiRequests);

            // Clear the list of batched API requests
            _batchApiRequests.Clear();

            // Convert the JSON result into an object and return it
            return JsonConvert.DeserializeObject<List<BatchApiResponse>>(jsonResponse);
        }

        /// <summary>
        /// Checks if batch API mode is enabled or not
        /// </summary>
        /// <returns>True if batch API mode is enabled, false otherwise</returns>
        public static bool IsBatchApiModeEnabled()
        {
            return _batchApiModeEnabled;
        }

        /// <summary>
        /// Overrides the default maximum number of API requests that can be sent in one batch
        /// NOTE: Changing to any value above the default value could result in the server 
        /// failing to handle the requests.
        /// </summary>
        /// <param name="newMaximum">The new requests maximum batch requests</param>
        public static void OverrideMaximumBatchRequests(int newMaximum)
        {
            _maxBatchRequests = newMaximum;
        }

        /// <summary>
        /// Returns the current maximum number of API requests that can be sent in one batch
        /// </summary>
        /// <returns>The current maximum number of API requests that can be sent in one batch</returns>
        public static int GetMaximumBatchRequests()
        {
            return _maxBatchRequests;
        }

        /// <summary>
        /// Resets the maximum number of API requests that can be sent in one batch to the default amount
        /// </summary>
        public static void SetMaximumBatchRequestsToDefault()
        {
            _maxBatchRequests = DEFAULT_MAXIMUM_BATCH_REQUESTS;
        }

        /// <summary>
        /// Returns the default maximum number of API requests that can be sent in one batch
        /// </summary>
        /// <returns>The default maximum number of batch requests</returns>
        public static int GetDefaultMaximumBatchRequests()
        {
            return DEFAULT_MAXIMUM_BATCH_REQUESTS;
        }

        /// <summary>
        /// Adds a new Api request to the current batch
        /// </summary>
        /// <param name="newRequest">The new API request to add</param>
        /// <exception cref="InvalidOperationException">Thrown when the call is made in batch mode but the current batch has no more room for additional API calls</exception>
        public static void AddApiRequest(BatchApiRequest newRequest)
        {
            // Throw an exception if there isn't room for the new API request in the current batch
            if (_batchApiRequests.Count >= _maxBatchRequests)
            {
                throw new InvalidOperationException(String.Format("Batch API request limit of {0} requests has already been reached.  Cannot add another API request to current batch.", _maxBatchRequests));
            }

            // Otherwise, add the new request to the batch
            _batchApiRequests.Add(newRequest);
        }

        /// <summary>
        /// Exits batch mode without calling any of the batched API calls.
        /// Clears the list of batched API calls
        /// </summary>
        public static void AbortBatchRequest()
        {
            _batchApiModeEnabled = false;
            _batchApiRequests.Clear();
        }


        /// <summary>
        /// Exits Batch API mode without clearing the list of batched API calls.
        /// Allows other API calls can be made immediately.
        /// Call ResumeBatchRequest to add API calls to the existing list of batched API calls
        /// </summary>
        public static void PauseBatchRequest()
        {
            _batchApiModeEnabled = false;
        }

        /// <summary>
        /// Enters batch request mode without clearning the existing list of batched API calls.
        /// New API calls will be added to the existing list.
        /// </summary>
        public static void ResumeBatchRequest()
        {
            _batchApiModeEnabled = true;
        }
    }
}
