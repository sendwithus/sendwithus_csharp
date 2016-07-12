using System;
using Xunit;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Sendwithus;
using System.Net;
using System.Collections.Generic;
using System.Diagnostics;
using Xunit.Abstractions;

namespace SendwithusTest
{
    /// <summary>
    /// Unit testing class for the Snippet API calls
    /// </summary>
    public class SnippetTest
    {
        private const string DEFAULT_SNIPPET_ID = "snp_bn8c87iXuFWdtYLGJrBAWW";
        private const string INVALID_SNIPPET_ID = "invalid_snippet_id";
        private const string NEW_SNIPPET_NAME = "My First Snippet";
        private const string NEW_SNIPPET_BODY = "<h1>Welcome!</h1>";
        private const int UNIQUE_ID_LENGTH = 10;

        private static Collection<string> NewSnippetIds = new Collection<string>();

        readonly ITestOutputHelper Output;

        /// <summary>
        /// Default constructor with an output object - used to output messages to the Test Explorer
        /// </summary>
        /// <param name="output"></param>
        public SnippetTest(ITestOutputHelper output)
        {
            Output = output;
        }

        /// <summary>
        /// Tests the API call GET /snippets
        /// </summary>
        /// <returns>The associated task</returns>
        [Fact]
        public async Task TestGetSnippetsAsync()
        {
            Output.WriteLine("GET /snippets");
            SendwithusClient.ApiKey = SendwithusClientTest.API_KEY_TEST;

            // Make the API call
            var response = await Snippet.GetSnippetsAsync();

            // Validate the response
            SendwithusClientTest.ValidateResponse(response, Output);
        }

        /// <summary>
        /// Tests the API call GET /snippets/(:id)
        /// </summary>
        /// <returns>The associated task</returns>
        [Fact]
        public async Task TestGetSnippetAsync()
        {
            Output.WriteLine(String.Format("GET /snippets/{0}", DEFAULT_SNIPPET_ID));
            SendwithusClient.ApiKey = SendwithusClientTest.API_KEY_TEST;

            // Make the API call
            var response = await Snippet.GetSnippetAsync(DEFAULT_SNIPPET_ID);

            // Validate the response
            SendwithusClientTest.ValidateResponse(response, Output);
        }

        /// <summary>
        /// Tests the API call GET /snippets/(:id) with an invalid ID
        /// </summary>
        /// <returns>The associated task</returns>
        [Fact]
        public async Task TestGetSnippetWithInvalidIDAsync()
        {
            Output.WriteLine("GET /snippets with an invalid ID");
            SendwithusClient.ApiKey = SendwithusClientTest.API_KEY_TEST;

            // Make the API call
            try
            {
                var response = await Snippet.GetSnippetAsync(INVALID_SNIPPET_ID);
                Assert.True(false, "Failed to throw exception");
            }
            catch (SendwithusException exception)
            {
                // Make sure the response was HTTP 400 Bad Request
                SendwithusClientTest.ValidateException(exception, HttpStatusCode.BadRequest, Output);
            }
        }

        /// <summary>
        /// Tests the API call POST /snippets
        /// </summary>
        /// <returns>The associated task</returns>
        [Fact]
        public async Task TestCreateSnippetAsync()
        {
            Output.WriteLine("POST /snippets");
            SendwithusClient.ApiKey = SendwithusClientTest.API_KEY_TEST;

            // Make the API call
            var uniqueName = String.Format("{0}-{1}", NEW_SNIPPET_NAME, SendwithusClientTest.RandomString(UNIQUE_ID_LENGTH));
            var response = await Snippet.CreateSnippetAsync(uniqueName, NEW_SNIPPET_BODY);

            // Validate the response
            SendwithusClientTest.ValidateResponse(response, Output);

            // Save the snippet ID for use in a future DELETE call
            NewSnippetIds.Add(response.snippet.id);
        }

        /// <summary>
        /// Tests the API call PUT /snippets/(:id)
        /// </summary>
        /// <returns>The associated task</returns>
        [Fact]
        public async Task TestUpdateSnippetAsync()
        {
            Output.WriteLine("POST /snippets");
            SendwithusClient.ApiKey = SendwithusClientTest.API_KEY_TEST;

            // Make the API call
            var uniqueName = String.Format("{0}-{1}", NEW_SNIPPET_NAME, SendwithusClientTest.RandomString(UNIQUE_ID_LENGTH));
            var response = await Snippet.UpdateSnippetAsync(DEFAULT_SNIPPET_ID, uniqueName, NEW_SNIPPET_BODY);

            // Validate the response
            SendwithusClientTest.ValidateResponse(response, Output);
        }

        /// <summary>
        /// Tests the API call DELETE /snippets/(:id)
        /// </summary>
        /// <returns>The associated task</returns>
        [Fact]
        public async Task TestDeleteSnippetAsync()
        {
            // Get the ID of a newly added snippet that can be deleted
            string snippetId = String.Empty;
            if (NewSnippetIds.Count > 0)
            {
                snippetId = NewSnippetIds[0];
            }
            else
            {
                Assert.True(false, "No new templates available to add a locale to");
            }
            Output.WriteLine(String.Format("DELETE /snippets/{0}", snippetId));
            SendwithusClient.ApiKey = SendwithusClientTest.API_KEY_TEST;

            // Make the API call
            var response = await Snippet.DeleteSnippetAsync(snippetId);

            // Validate the response
            SendwithusClientTest.ValidateResponse(response, Output);
        }
    }
}
