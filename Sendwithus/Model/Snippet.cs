using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Sendwithus
{
    /// <summary>
    /// sendwithus Snippet class
    /// </summary>
    public class Snippet
    {
        public string Object { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string body { get; set; }
        public UInt64 created { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public Snippet()
        {
            Object = String.Empty;
            id = String.Empty;
            name = String.Empty;
            body = String.Empty;
            created = 0;
        }

        /// <summary>
        /// Get all snippets.
        /// GET /snippets
        /// </summary>
        /// <returns>A list of all the snippets</returns>
        public static async Task<List<Snippet>> GetSnippetsAsync()
        {
            // Send the GET request
            var resource = "snippets";
            var jsonResponse = await RequestManager.SendGetRequestAsync(resource);

            // Convert the JSON result into an object
            var serializer = new JavaScriptSerializer();
            return serializer.Deserialize<List<Snippet>>(jsonResponse);
        }

        /// <summary>
        /// Get specific snippet.
        /// GET /snippets/(:id)
        /// </summary>
        /// /// <param name="snippetId">The ID of the snippet to get</param>
        /// <returns>The snippet with the given ID</returns>
        public static async Task<List<Snippet>> GetSnippetAsync(string snippetId)
        {
            // Send the GET request
            var resource = String.Format("snippets/{0}", snippetId);
            var jsonResponse = await RequestManager.SendGetRequestAsync(resource);

            // Convert the JSON result into an object
            var serializer = new JavaScriptSerializer();
            return serializer.Deserialize<List<Snippet>>(jsonResponse);
        }

        /// <summary>
        /// Create a new snippet
        /// POST /snippets
        /// </summary>
        /// <param name="name">The name of the new snippet</param>
        /// <param name="body">The body of the new snippet</param>
        /// <returns>A response with the success status and new snippet</returns>
        public static async Task<SnippetResponse> CreateSnippetAsync(string name, string body)
        {
            // Build the SnippetDefinition
            var newSnippet = new SnippetDefinition(name, body);

            // Send the POST request
            var resource = "snippets";
            var jsonResponse = await RequestManager.SendPostRequestAsync(resource, newSnippet);

            // Convert the JSON result into an object
            var serializer = new JavaScriptSerializer();
            return serializer.Deserialize<SnippetResponse>(jsonResponse);
        }

        /// <summary>
        /// Update an existing snippet
        /// PUT /snippets/(:id)
        /// </summary>
        /// <param name="snippetId">The ID of the snippet to update</param>
        /// <param name="name">The new name of the snippet</param>
        /// <param name="body">The new body of the snippet</param>
        /// <returns>A response with the success status and snippet</returns>
        public static async Task<SnippetResponse> UpdateSnippetAsync(string snippetId, string name, string body)
        {
            // Build the SnippetDefinition
            var newSnippet = new SnippetDefinition(name, body);

            // Send the PUT request
            var resource = String.Format("snippets/{0}", snippetId);
            var jsonResponse = await RequestManager.SendPutRequestAsync(resource, newSnippet);

            // Convert the JSON result into an object
            var serializer = new JavaScriptSerializer();
            return serializer.Deserialize<SnippetResponse>(jsonResponse);
        }

        /// <summary>
        /// Delete an existing snippet
        /// DELETE /snippets/(:id)
        /// </summary>
        /// <param name="snippetId">The ID of the snippet to delete</param>
        /// <returns>The status of the deletion</returns>
        public static async Task<GenericApiCallStatus> DeleteSnippetAsync(string snippetId)
        {
            // Send the PUT request
            var resource = String.Format("snippets/{0}", snippetId);
            var jsonResponse = await RequestManager.SendDeleteRequestAsync(resource);

            // Convert the JSON result into an object
            var serializer = new JavaScriptSerializer();
            return serializer.Deserialize<GenericApiCallStatus>(jsonResponse);
        }
    }
}
