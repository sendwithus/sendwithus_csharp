using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;


namespace SendWithUs
{
    public abstract class RequestManager
    {
        public static Uri GetBaseAddress()
        {
            var uriString = String.Format(@"{0}://{1}:{2}", SendWithUs.API_PROTO,
                    SendWithUs.API_HOST, SendWithUs.API_PORT);
            return new Uri(uriString);
        }

        public static string BuildURI(string resource)
        {
            return String.Format("/api/{0}/{1}", SendWithUs.API_VERSION, resource);
        }

        /// <summary>
        /// Sends an HTTP GET request and returns the reponse as a JSON string
        /// </summary>
        /// <param name="resource">The resource identifier for the resource to be called (after /api/<version>/)</param>
        /// <returns>The response content in the form of a JSON string</returns>
        /// <exception cref="SendWithUsException">Thrown when the API response status code is not success</exception>
        public static async Task<string> SendGetRequestAsync(string resource)
        {   
            using (var client = new HttpClient())
            {
                // Send the GET request
                client.BaseAddress = GetBaseAddress();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add(SendWithUs.SWU_API_HEADER, SendWithUs.ApiKey);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
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
        /// <exception cref="SendWithUsException">Thrown when the API response status code is not success</exception>
        public static async Task<string> SendPutRequestAsync(string resource, object content)
        {
            using (var client = new HttpClient())
            {
                // Send the PUT request
                client.BaseAddress = GetBaseAddress();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add(SendWithUs.SWU_API_HEADER, SendWithUs.ApiKey);
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var uri = BuildURI(resource);
                var serializer = new JavaScriptSerializer();
                var contentString = serializer.Serialize(content);
                var httpContent = new StringContent(contentString);
                httpContent.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");
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
        /// <exception cref="SendWithUsException">Thrown when the API response status code is not success</exception>
        public static async Task<string> SendPostRequestAsync(string resource, object content)
        {
            using (var client = new HttpClient())
            {
                // Send the PUT request
                client.BaseAddress = GetBaseAddress();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add(SendWithUs.SWU_API_HEADER, SendWithUs.ApiKey);
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var uri = BuildURI(resource);
                var serializer = new JavaScriptSerializer();
                var contentString = serializer.Serialize(content);
                var httpContent = new StringContent(contentString);
                httpContent.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");
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
        /// <exception cref="SendWithUsException">Thrown when the API response status code is not success</exception>
        public static async Task<string> SendDeleteRequestAsync(string resource)
        {
            using (var client = new HttpClient())
            {
                // Send the DELETE request
                client.BaseAddress = GetBaseAddress();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add(SendWithUs.SWU_API_HEADER, SendWithUs.ApiKey);
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
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
        /// /// <exception cref="SendWithUsException">Thrown when the API response status code is not success</exception>
        private static async Task<string> ExtractAndValidateResponseContentAsync(HttpResponseMessage response)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode == false)
            {
                throw new SendWithUsException(response.StatusCode, responseContent);
            }
            return responseContent;
        }
    }
}
