using System;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Sendwithus;
using System.Net;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SendwithusTest
{
    /// <summary>
    /// Unit testing class for the Snippet API calls
    /// </summary>
    [TestClass]
    public class SnippetTest
    {
        private const string DEFAULT_SNIPPET_ID = "snp_bn8c87iXuFWdtYLGJrBAWW";
        private const string INVALID_SNIPPET_ID = "invalid_snippet_id";
        private const string NEW_SNIPPET_NAME = "My First Snippet";
        private const string NEW_SNIPPET_BODY = "<h1>Welcome!</h1>";
        private const int UNIQUE_ID_LENGTH = 10;

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
        /// Tests the API call GET /snippets
        /// </summary>
        /// <returns>The associated task</returns>
        [TestMethod]
        public async Task TestGetSnippetsAsync()
        {
            Trace.WriteLine("GET /snippets");

            // Make the API call
            var response = await Snippet.GetSnippetsAsync();

            // Validate the response
            SendwithusClientTest.ValidateResponse(response);
        }

        /// <summary>
        /// Tests the API call GET /snippets/(:id)
        /// </summary>
        /// <returns>The associated task</returns>
        [TestMethod]
        public async Task TestGetSnippetAsync()
        {
            Trace.WriteLine(String.Format("GET /snippets/{0}", DEFAULT_SNIPPET_ID));

            // Make the API call
            var response = await Snippet.GetSnippetAsync(DEFAULT_SNIPPET_ID);

            // Validate the response
            SendwithusClientTest.ValidateResponse(response);
        }

        /// <summary>
        /// Tests the API call GET /snippets/(:id) with an invalid ID
        /// </summary>
        /// <returns>The associated task</returns>
        [TestMethod]
        public async Task TestGetSnippetWithInvalidIDAsync()
        {
            Trace.WriteLine("GET /snippets with an invalid ID");

            // Make the API call
            try
            {
                var response = await Snippet.GetSnippetAsync(INVALID_SNIPPET_ID);
                Assert.Fail("Failed to throw exception");
            }
            catch (SendwithusException exception)
            {
                // Make sure the response was HTTP 400 Bad Request
                SendwithusClientTest.ValidateException(exception, HttpStatusCode.BadRequest);
            }
        }

        /// <summary>
        /// Tests the API call POST /snippets
        /// </summary>
        /// <returns>The associated task</returns>
        [TestMethod]
        public async Task TestCreateSnippetAsync()
        {
            Trace.WriteLine("POST /snippets");

            // Make the API call
            var uniqueName = String.Format("{0}-{1}", NEW_SNIPPET_NAME, SendwithusClientTest.RandomString(UNIQUE_ID_LENGTH));
            var response = await Snippet.CreateSnippetAsync(uniqueName, NEW_SNIPPET_BODY);

            // Validate the response
            SendwithusClientTest.ValidateResponse(response);
        }

        /// <summary>
        /// Tests the API call PUT /snippets/(:id)
        /// </summary>
        /// <returns>The associated task</returns>
        [TestMethod]
        public async Task TestUpdateSnippetAsync()
        {
            Trace.WriteLine("POST /snippets");

            // Make the API call
            var uniqueName = String.Format("{0}-{1}", NEW_SNIPPET_NAME, SendwithusClientTest.RandomString(UNIQUE_ID_LENGTH));
            var response = await Snippet.UpdateSnippetAsync(DEFAULT_SNIPPET_ID, uniqueName, NEW_SNIPPET_BODY);

            // Validate the response
            SendwithusClientTest.ValidateResponse(response);
        }

        /// <summary>
        /// Tests the API call DELETE /snippets/(:id)
        /// </summary>
        /// <returns>The associated task</returns>
        [TestMethod]
        public async Task TestDeleteSnippetAsync()
        {
            // Create a new Snippet so that it can be deleted
            var uniqueName = String.Format("{0}-{1}", NEW_SNIPPET_NAME, SendwithusClientTest.RandomString(UNIQUE_ID_LENGTH));
            var snippetResponse = await Snippet.CreateSnippetAsync(uniqueName, NEW_SNIPPET_BODY);
            var snippetId = snippetResponse.snippet.id;

            // Make the API call
            Trace.WriteLine(String.Format("DELETE /snippets/{0}", snippetId));
            var response = await Snippet.DeleteSnippetAsync(snippetId);

            // Validate the response
            SendwithusClientTest.ValidateResponse(response);
        }
    }
}
