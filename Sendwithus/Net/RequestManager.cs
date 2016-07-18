using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Sendwithus
{
    /// <summary>
    /// sendwithus RequestManager class.
    /// Handles the HTTP calls
    /// </summary>
    public static class RequestManager
    {
        /// <summary>
        /// The base address for a sendiwthus API call (ex. https://api.sendwithus.com:443)
        /// </summary>
        public static Uri BaseAddress
        {
            get
            { 
                var uriString = String.Format(CultureInfo.InvariantCulture, @"{0}://{1}:{2}", SendwithusClient.API_PROTO,
                        SendwithusClient.API_HOST, SendwithusClient.API_PORT);
                return new Uri(uriString);
            }
        }

        /// <summary>
        /// Builds the full resource string for a sendwithus API call.
        /// Adds the "/api/{version}/" prefix to the given resource.
        /// </summary>
        /// <param name="resource">The resource to call</param>
        /// <returns>The full resource string</returns>
        public static string BuildFullResourceString(string resource)
        {
            return String.Format(CultureInfo.InvariantCulture, "/api/{0}/{1}", SendwithusClient.API_VERSION, resource);            
        }

        /// <summary>
        /// Sends an HTTP GET request and returns the reponse as a JSON string
        /// </summary>
        /// <param name="resource">The resource identifier for the resource to be called (after /api/<version>/)</param>
        /// <param name="queryParameters">The query parameters to use with the API call</param>
        /// <returns>The response content in the form of a JSON string</returns>
        /// <exception cref="AggregateException">Thrown when the API response status code is not success or when the API call times out</exception>
        /// <exception cref="InvalidOperationException">Thrown when making a Batch API Request that has already reached the maxmimum API calls per batch request</exception>
        public static async Task<string> SendGetRequestAsync(string resource, Dictionary<string, object> queryParameters)
        {
            using (var client = new HttpClient())
            {
                // Prepare the GET request
                ConfigureHttpClient(client);
                var queryString = ConvertQueryParametersToQueryString(queryParameters);
                var uri = String.Format("{0}{1}", BuildFullResourceString(resource), queryString);

                // If batch mode is enabled then add the request to the current batch of requests to send later on
                if (BatchApiRequest.IsBatchApiModeEnabled() == true)
                {
                    BatchApiRequest.AddApiRequest(new BatchApiRequest(uri, "GET"));
                    return String.Empty;
                }
                // Otherwise, send the GET request
                else
                {
                    return await RunWithRetries(() => client.GetAsync(uri));
                }
            }
        }

        /// <summary>
        /// Sends an HTTP GET request and returns the reponse as a JSON string
        /// </summary>
        /// <param name="resource">The resource identifier for the resource to be called (after /api/<version>/)</param>
        /// <returns>The response content in the form of a JSON string</returns>
        /// <exception cref="AggregateException">Thrown when the API response status code is not success or when the API call times out</exception>
        /// <exception cref="InvalidOperationException">Thrown when making a Batch API Request that has already reached the maxmimum API calls per batch request</exception>
        public static async Task<string> SendGetRequestAsync(string resource)
        {
            return await SendGetRequestAsync(resource, null);
        }

        /// <summary>
        /// Sends an HTTP PUT request and returns the reponse as a JSON string
        /// </summary>
        /// <param name="resource">The resource identifier for the resource to be called (after /api/<version>/)</param>
        /// <param name="content">The object to be sent with the PUT request. Will be converted to JSON in this function</param>
        /// <returns>The response content in the form of a JSON string</returns>
        /// <exception cref="AggregateException">Thrown when the API response status code is not success or when the API call times out</exception>
        /// <exception cref="InvalidOperationException">Thrown when making a Batch API Request that has already reached the maxmimum API calls per batch request</exception>
        public static async Task<string> SendPutRequestAsync(string resource, object content)
        {
            using (var client = new HttpClient())
            {
                // Prepare the PUT request
                ConfigureHttpClient(client);
                var uri = BuildFullResourceString(resource);
                var httpContent = ConvertObjectToJsonHttpContent(content);

                // If batch mode is enabled then add the request to the current batch of requests to send later on
                if (BatchApiRequest.IsBatchApiModeEnabled() == true)
                {
                    BatchApiRequest.AddApiRequest(new BatchApiRequest(uri, "PUT", content));
                    return String.Empty;
                }
                // Otherwise, send the PUT request
                else
                {
                    return await RunWithRetries(() => client.PutAsync(uri, httpContent));
                }
            }
        }

        /// <summary>
        /// Sends an HTTP POST request and returns the reponse as a JSON string
        /// </summary>
        /// <param name="resource">The resource identifier for the resource to be called (after /api/<version></version>/)</param>
        /// <param name="content">The object to be sent with the POST request. Will be converted to JSON in this function</param>
        /// <returns>The response content in the form of a JSON string</returns>
        /// <exception cref="AggregateException">Thrown when the API response status code is not success or when the API call times out</exception>
        /// <exception cref="InvalidOperationException">Thrown when making a Batch API Request that has already reached the maxmimum API calls per batch request</exception>
        public static async Task<string> SendPostRequestAsync(string resource, object content)
        {
            using (var client = new HttpClient())
            {
                // Build the POST request
                ConfigureHttpClient(client);
                var uri = BuildFullResourceString(resource);
                var httpContent = ConvertObjectToJsonHttpContent(content);

                // If batch mode is enabled then add the request to the current batch of requests to send later on
                if (BatchApiRequest.IsBatchApiModeEnabled() == true)
                {
                    BatchApiRequest.AddApiRequest(new BatchApiRequest(uri, "POST", content));
                    return String.Empty;
                }
                // Otherwise, send the POST request
                else
                {
                    return await RunWithRetries(() => client.PostAsync(uri, httpContent));
                }
            }
        }

        /// <summary>
        /// Sends an HTTP POST request and returns the reponse as a JSON string
        /// </summary>
        /// <param name="resource">The resource identifier for the resource to be called (after /api/<version></version>/)</param>
        /// <returns>The response content in the form of a JSON string</returns>
        /// <exception cref="AggregateException">Thrown when the API response status code is not success or when the API call times out</exception>
        /// <exception cref="InvalidOperationException">Thrown when making a Batch API Request that has already reached the maxmimum API calls per batch request</exception>
        public static async Task<string> SendPostRequestAsync(string resource)
        {
            return await SendPostRequestAsync(resource, null);
        }

        /// <summary>
        /// Sends an HTTP DELETE request and returns the reponse as a JSON string
        /// </summary>
        /// <param name="resource">The resource identifier for the resource to be called (after /api/<version></version>/)</param>
        /// <returns>The response content in the form of a JSON string</returns>
        /// <exception cref="AggregateException">Thrown when the API response status code is not success or when the API call times out</exception>
        /// <exception cref="InvalidOperationException">Thrown when the call is made in batch mode but the current batch has no more room for additional API calls</exception>
        public static async Task<string> SendDeleteRequestAsync(string resource)
        {
            using (var client = new HttpClient())
            {
                // Prepare the DELETE request
                ConfigureHttpClient(client);
                var uri = BuildFullResourceString(resource);

                // If batch mode is enabled then add the request to the current batch of requests to send later on
                if (BatchApiRequest.IsBatchApiModeEnabled() == true)
                {
                    BatchApiRequest.AddApiRequest(new BatchApiRequest(uri, "DELETE"));
                    return String.Empty;
                }
                // Otherwise, send the DELETE request
                else
                {
                    return await RunWithRetries(() => client.DeleteAsync(uri));
                }
            }
        }

        /// <summary>
        /// Converts the response content to a string and validates that the response was successfull
        /// </summary>
        /// <param name="response">The HTTP response</param>
        /// <returns>The response content as a string</returns>
        /// <exception cref="SendwithusException">Thrown when the API response status code is not success</exception>
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
        /// Sets the base address, Accept type, API key, client info, and timeout for the HTTP Client
        /// </summary>
        /// <param name="client">The client to prepare to setup</param>
        private static void ConfigureHttpClient (HttpClient client)
        {
            client.BaseAddress = RequestManager.BaseAddress;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add(SendwithusClient.SWU_API_HEADER, SendwithusClient.ApiKey);
            var clientStub = String.Format(CultureInfo.InvariantCulture, "{0}-{1}", SendwithusClient.CLIENT_LANGUAGE, SendwithusClient.CLIENT_VERSION);
            client.DefaultRequestHeaders.Add(SendwithusClient.SWU_CLIENT_HEADER, clientStub);
            client.Timeout = SendwithusClient.GetTimeout();
        }

        /// <summary>
        /// Serializes the given object into a JSON string then stores it in a 
        /// StringContent object to send it in an HTTP request.
        /// </summary>
        /// <param name="content">The content to convert to HttpContent</param>
        /// <returns>A StringContent (a subclass of HttpContent) representation of the object</returns>
        private static StringContent ConvertObjectToJsonHttpContent(object content)
        {
            var serializer = new JavaScriptSerializer();
            var contentString = serializer.Serialize(content);
            var stringContent = new StringContent(contentString);
            stringContent.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");
            return stringContent;
        }

        /// <summary>
        /// Converts the given object into a query parameter string
        /// </summary>
        /// <param name="parameters">A list of parameters to convert to a query string.
        /// All objects must be of types that are supported by the JsonQueryStringConverter class.</param>
        /// <returns>The query string</returns>
        private static string ConvertQueryParametersToQueryString(Dictionary<string, object> parameters)
        {
            if (parameters == null)
            {
                return String.Empty;
            }

            // Build the query parameter string
            var converter = new JsonQueryStringConverter();
            var queryString = new StringBuilder();
            bool isFirstItem = true;
            foreach (KeyValuePair<string, object> parameter in parameters)
            {
                if (isFirstItem == true)
                {
                    queryString.Append("?");
                    isFirstItem = false;
                }
                else
                {
                    queryString.Append("&");
                }
                queryString.Append(parameter.Key);
                queryString.Append("=");

                // Handle the "esp_type" parameter separately as it must be sent without quotations "" around the value
                if (parameter.Key == "esp_type")
                {
                    queryString.Append(parameter.Value.ToString());
                }
                // Otherwise, convert the value into a JSON string
                else if (converter.CanConvert(parameter.GetType()))
                { 
                    queryString.Append(converter.ConvertValueToString(parameter.Value, parameter.GetType()));
                }
            }
            return queryString.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <param name="retryInterval"></param>
        /// <param name="retryCount"></param>
        /// <returns>The response to the API call as a JSON string</returns>
        /// <exception cref="AggregateException">Thrown when the API response status code is not success or when the API call times out</exception>
        private static async Task<string> RunWithRetries(
            Func<Task<HttpResponseMessage>> apiCall)
        {
            var exceptions = new List<Exception>();
            var retryCount = SendwithusClient.RetryCount;
            var retryInterval = SendwithusClient.RetryIntervalMilliseconds;

            for (int retry = 0; retry < retryCount; retry++)
            {
                try
                {
                    // Wait for the retry interval (after the first call)
                    if (retry > 0)
                    {
                        Thread.Sleep(retryInterval);
                    }

                    // Make the API call
                    var response = await apiCall();

                    // Convert the response to a string, validate it, and return it
                    return await ExtractAndValidateResponseContentAsync(response);
                }
                catch (Exception ex)
                {
                    // Store the exception so that we can return all the exceptions if all the retries fail
                    exceptions.Add(ex);

                    // Check if the exception is one that warrants a retry (ex. a server error)
                    // If it isn't, then re-throw the exception(s)
                    if (ExceptionIsRetriable(ex) == false)
                    {
                        throw new AggregateException(exceptions);
                    }
                }
            }
            throw new AggregateException(exceptions);
        }

        /// <summary>
        /// Checks if the exception might be transient and warrants a retry of the API call
        /// </summary>
        /// <param name="ex">The exception that was thrown by the most recent API call attempt</param>
        /// <returns>True if the exception might be transient and warrants a retry, false otherwise</returns>
        private static bool ExceptionIsRetriable(Exception ex)
        {
            // Check if the exception was a caused by a potentially transient server error
            var sendwithusException = ex as SendwithusException;
            if (sendwithusException != null)
            {
                switch(sendwithusException.StatusCode)
                {
                    case HttpStatusCode.BadGateway:
                    case HttpStatusCode.ServiceUnavailable:
                    case HttpStatusCode.GatewayTimeout:
                        return true;
                }
            }
            // Check if the exception was caused by the API call timeout
            else if (ex is TaskCanceledException)
            {
                return true;
            }
            
            // Don't retry the API call if it failed for any other reason
            return false;
        }
    }
}
