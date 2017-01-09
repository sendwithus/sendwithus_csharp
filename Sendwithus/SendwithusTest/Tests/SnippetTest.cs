using NUnit.Framework;
using Sendwithus;
using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;

namespace SendwithusTest
{
    /// <summary>
    /// Unit testing class for the Snippet API calls
    /// </summary>
    [TestFixture]
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
        [SetUp]
        public void InitializeUnitTesting()
        {
            // Set the API key
            SendwithusClient.ApiKey = SendwithusClientTest.API_KEY_TEST;
        }

        /// <summary>
        /// Tests the API call GET /snippets
        /// </summary>
        /// <returns>The associated task</returns>
        [Test]
        public async Task TestGetSnippetsAsync()
        {
            Trace.WriteLine("GET /snippets");

            // Make the API call
            try
            {
                var snippets = await Snippet.GetSnippetsAsync();

                // Validate the response
                SendwithusClientTest.ValidateResponse(snippets);
            }
            catch (AggregateException exception)
            {
                Assert.Fail(exception.ToString());
            }
        }

        /// <summary>
        /// Tests the API call GET /snippets/(:id)
        /// </summary>
        /// <returns>The associated task</returns>
        [Test]
        public async Task TestGetSnippetAsync()
        {
            Trace.WriteLine(String.Format("GET /snippets/{0}", DEFAULT_SNIPPET_ID));

            // Make the API call
            try
            { 
                var snippet = await Snippet.GetSnippetAsync(DEFAULT_SNIPPET_ID);

                // Validate the response
                SendwithusClientTest.ValidateResponse(snippet);
            }
            catch (AggregateException exception)
            {
                Assert.Fail(exception.ToString());
            }
        }

        /// <summary>
        /// Tests the API call GET /snippets/(:id) with an invalid ID
        /// </summary>
        /// <returns>The associated task</returns>
        [Test]
        public async Task TestGetSnippetWithInvalidIDAsync()
        {
            Trace.WriteLine("GET /snippets with an invalid ID");

            // Make the API call
            try
            {
                var snippet = await Snippet.GetSnippetAsync(INVALID_SNIPPET_ID);
                Assert.Fail("Failed to throw exception");
            }
            catch (AggregateException exception)
            {
                // Make sure the response was HTTP 400 Bad Request
                SendwithusClientTest.ValidateException(exception, HttpStatusCode.BadRequest);
            }
        }

        /// <summary>
        /// Tests the API call POST /snippets
        /// </summary>
        /// <returns>The associated task</returns>
        [Test]
        public async Task TestCreateSnippetAsync()
        {
            Trace.WriteLine("POST /snippets");

            // Make the API call
            var uniqueName = String.Format("{0}-{1}", NEW_SNIPPET_NAME, SendwithusClientTest.RandomString(UNIQUE_ID_LENGTH));
            try
            { 
                var snippetResponse = await Snippet.CreateSnippetAsync(uniqueName, NEW_SNIPPET_BODY);

                // Validate the response
                SendwithusClientTest.ValidateResponse(snippetResponse);
            }
            catch (AggregateException exception)
            {
                Assert.Fail(exception.ToString());
            }
        }

        /// <summary>
        /// Tests the API call PUT /snippets/(:id)
        /// </summary>
        /// <returns>The associated task</returns>
        [Test]
        public async Task TestUpdateSnippetAsync()
        {
            Trace.WriteLine("PUT /snippets/(:id)");

            // Make the API call
            var uniqueName = String.Format("{0}-{1}", NEW_SNIPPET_NAME, SendwithusClientTest.RandomString(UNIQUE_ID_LENGTH));
            try
            {
                var snippetResponse = await Snippet.UpdateSnippetAsync(DEFAULT_SNIPPET_ID, uniqueName, NEW_SNIPPET_BODY);

                // Validate the response
                SendwithusClientTest.ValidateResponse(snippetResponse);
            }
            catch (AggregateException exception)
            {
                Assert.Fail(exception.ToString());
            }
        }

        /// <summary>
        /// Tests the API call DELETE /snippets/(:id)
        /// </summary>
        /// <returns>The associated task</returns>
        [Test]
        public async Task TestDeleteSnippetAsync()
        {
            // Create a new Snippet so that it can be deleted
            var uniqueName = String.Format("{0}-{1}", NEW_SNIPPET_NAME, SendwithusClientTest.RandomString(UNIQUE_ID_LENGTH));
            var snippetResponse = await Snippet.CreateSnippetAsync(uniqueName, NEW_SNIPPET_BODY);
            var snippetId = snippetResponse.snippet.id;

            // Make the API call
            Trace.WriteLine(String.Format("DELETE /snippets/{0}", snippetId));
            try
            { 
                var genericApiCallStatus = await Snippet.DeleteSnippetAsync(snippetId);

                // Validate the response
                SendwithusClientTest.ValidateResponse(genericApiCallStatus);
            }
            catch (AggregateException exception)
            {
                Assert.Fail(exception.ToString());
            }
        }
    }
}
