using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;


namespace Sendwithus
{
    public abstract class RequestManager
    {
        public static Uri GetBaseAddress()
        {
            var uriString = String.Format(@"{0}://{1}:{2}", Sendwithus.API_PROTO,
                    Sendwithus.API_HOST, Sendwithus.API_PORT);
            return new Uri(uriString);
        }

        public static string BuildURI(string resource)
        {
            return String.Format("/api/{0}/{1}", Sendwithus.API_VERSION, resource);
        }

        /// <summary>
        /// Sends an HTTP GET request and returns the reponse as a JSON string
        /// </summary>
        /// <param name="resource">The resource identifier for the resource to be called (after /api/<version>/)</param>
        /// <returns>The response content in the form of a JSON string</returns>
        /// <exception cref="SendwithusException">Thrown when the API response status code is not success</exception>
        public static async Task<string> SendGetRequestAsync(string resource)
        {   
            using (var client = new HttpClient())
            {
                // Send the GET request
                ConfigureHttpClient(client);
                var uri = BuildURI(resource);
                var response = await client.GetAsync(uri);

                // Convert the response to a string, validate it, and return it
                return await ExtractAndValidateResponseContentAsync(response);
            }
        }

        /// <summary>
        /// Sends an HTTP PUT request and returns the reponse as a JSON string
        /// </summary>
        /// <param name="resource">The resource identifier for the resource to be called (after /api/<version>/)</param>
        /// <param name="content">The object to be sent with the PUT request. Will be converted to JSON in this function</param>
        /// <returns>The response content in the form of a JSON string</returns>
        /// <exception cref="SendwithusException">Thrown when the API response status code is not success</exception>
        public static async Task<string> SendPutRequestAsync(string resource, object content)
        {
            using (var client = new HttpClient())
            {
                // Send the PUT request
                ConfigureHttpClient(client);
                var uri = BuildURI(resource);
                var httpContent = SerializeContent(content);
                var response = await client.PutAsync(uri, httpContent);

                // Convert the response to a string, validate it, and return it
                return await ExtractAndValidateResponseContentAsync(response);
            }
        }

        /// <summary>
        /// Sends an HTTP POST request and returns the reponse as a JSON string
        /// </summary>
        /// <param name="resource">The resource identifier for the resource to be called (after /api/<version></version>/)</param>
        /// <param name="content">The object to be sent with the POST request. Will be converted to JSON in this function</param>
        /// <returns>The response content in the form of a JSON string</returns>
        /// <exception cref="SendwithusException">Thrown when the API response status code is not success</exception>
        public static async Task<string> SendPostRequestAsync(string resource, object content)
        {
            using (var client = new HttpClient())
            {
                // Send the PUT request
                ConfigureHttpClient(client);
                var uri = BuildURI(resource);
                var httpContent = SerializeContent(content);
                var response = await client.PostAsync(uri, httpContent);

                // Convert the response to a string, validate it, and return it
                return await ExtractAndValidateResponseContentAsync(response);    
            }
        }

        /// <summary>
        /// Sends an HTTP DELETE request and returns the reponse as a JSON string
        /// </summary>
        /// <param name="resource">The resource identifier for the resource to be called (after /api/<version></version>/)</param>
        /// <returns>The response content in the form of a JSON string</returns>
        /// <exception cref="SendwithusException">Thrown when the API response status code is not success</exception>
        public static async Task<string> SendDeleteRequestAsync(string resource)
        {
            using (var client = new HttpClient())
            {
                // Send the DELETE request
                ConfigureHttpClient(client);
                var uri = BuildURI(resource);
                var response = await client.DeleteAsync(uri);

                // Convert the response to a string, validate it, and return it
                return await ExtractAndValidateResponseContentAsync(response);
            }
        }

        /// <summary>
        /// Converts the response content to a string and validates that the response was successfull
        /// </summary>
        /// <param name="response">The HTTP response</param>
        /// <returns>The response content as a string</returns>
        /// /// <exception cref="SendwithusException">Thrown when the API response status code is not success</exception>
        private static async Task<string> ExtractAndValidateResponseContentAsync(HttpResponseMessage response)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode == false)
            {
                throw new SendwithusException(response.StatusCode, responseContent);
            }
            return responseContent;
        }

        /// <summary>
        /// Sets the base address, Accept type, API key, and password for the HTTP Client
        /// </summary>
        /// <param name="client">The client to prepare to setup</param>
        private static void ConfigureHttpClient (HttpClient client)
        { 
            client.BaseAddress = GetBaseAddress();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add(Sendwithus.SWU_API_HEADER, Sendwithus.ApiKey);
            var clientStub = String.Format("{0}-{1}", Sendwithus.CLIENT_LANGUAGE, Sendwithus.CLIENT_VERSION);
            client.DefaultRequestHeaders.Add(Sendwithus.SWU_CLIENT_HEADER, clientStub);
        }

        /// <summary>
        /// JSON serializes the given object into an StringContent to send it in an HTTP request
        /// </summary>
        /// <param name="content">The content to serialize</param>
        /// <returns>A StringContent containing the JSON serialized object</returns>
        private static StringContent SerializeContent(object content)
        {
            var serializer = new JavaScriptSerializer();
            var contentString = serializer.Serialize(content);
            var stringContent = new StringContent(contentString);
            stringContent.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");
            return stringContent;
        }
    }
}
