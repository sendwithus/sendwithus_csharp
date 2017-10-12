using Newtonsoft.Json;
using Sendwithus.Net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Sendwithus
{
    /// <summary>
    /// sendwithus Template class
    /// </summary>
    public class Template
    {
        public const string DEFAULT_LOCALE = "en-US";

        // all lowercase to match expected JSON format (case-sensitive on server side)
        public string id { get; set; }
        public string name { get; set; }
        public string locale { get; set; }
        public string created { get; set; }
        public Collection<TemplateVersion> versions { get; set; }
        public Collection<string> tags { get; set; }

        /// <summary>
        /// Create an empty Template
        /// </summary>
        public Template() {
            locale = DEFAULT_LOCALE;
        }

        /// <summary>
        /// Get all the templates associated with the account.
        /// GET /templates
        /// </summary>
        /// <returns>A list of all the templates associated with the account</returns>
        /// <exception cref="AggregateException">Thrown when the API response status code is not success or when the API call times out</exception>
        /// <exception cref="InvalidOperationException">Thrown when making a Batch API Request that has already reached the maxmimum API calls per batch request</exception>
        public static async Task<List<Template>> GetTemplatesAsync()
        {
            // Send the GET request
            var resource = "templates";
            var jsonResponse = await RequestManager.SendGetRequestAsync(resource);
            
            // Convert the JSON result into an object
            return JsonConvert.DeserializeObject<List<Template>>(jsonResponse);
        }

        /// <summary>
        /// Get a template by ID.
        /// GET /templates/(:template_id)
        /// </summary>
        /// <param name="templateId">The ID of the template</param>
        /// <returns>The template with the given ID</returns>
        /// <exception cref="AggregateException">Thrown when the API response status code is not success or when the API call times out</exception>
        /// <exception cref="InvalidOperationException">Thrown when making a Batch API Request that has already reached the maxmimum API calls per batch request</exception>
        public static async Task<Template> GetTemplateAsync(string templateId)
        {
            // Send the GET request
            var resource = String.Format("templates/{0}", templateId);
            var jsonResponse = await RequestManager.SendGetRequestAsync(resource);

            // Convert the JSON result into an object
            return JsonConvert.DeserializeObject<Template>(jsonResponse);
        }

        /// <summary>
        /// Get a template by ID and locale.
        /// GET /templates/(:template_id)/locales/(:locale)
        /// </summary>
        /// <param name="templateId">The ID of the template</param>
        /// <param name="locale"></param>
        /// <returns>The template with the given ID and locale</returns>
        /// <exception cref="AggregateException">Thrown when the API response status code is not success or when the API call times out</exception>
        /// <exception cref="InvalidOperationException">Thrown when making a Batch API Request that has already reached the maxmimum API calls per batch request</exception>
        public static async Task<Template> GetTemplateAsync(string templateId, string locale)
        {
            // Send the GET request
            var resource = String.Format("templates/{0}/locales/{1}", templateId, locale);
            var jsonResponse = await RequestManager.SendGetRequestAsync(resource);

            // Convert the JSON result into an object
            return JsonConvert.DeserializeObject<Template>(jsonResponse);
        }

        /// <summary>
        /// Get a list of template versions (with HTML/text).
        /// GET /templates/(:template_id)/versions
        /// </summary>
        /// <param name="templateId">The ID of the template</param>
        /// <returns>The template versions associated with the given ID</returns>
        /// <exception cref="AggregateException">Thrown when the API response status code is not success or when the API call times out</exception>
        /// <exception cref="InvalidOperationException">Thrown when making a Batch API Request that has already reached the maxmimum API calls per batch request</exception>
        public static async Task<List<TemplateVersion>> GetTemplateVersionsAsync(string templateId)
        {
            // Send the GET request
            var resource = String.Format("templates/{0}/versions", templateId);
            var jsonResponse = await RequestManager.SendGetRequestAsync(resource);

            // Convert the JSON result into an object
            return JsonConvert.DeserializeObject<List<TemplateVersion>>(jsonResponse);
        }

        /// <summary>
        /// Get a list of template versions (with HTML/text).
        /// GET /templates/(:template_id)/locales/(:locale)/versions
        /// </summary>
        /// <param name="templateId">The ID of the template</param>
        /// <param name="locale">The locale of the template</param>
        /// <returns>The template versions associated with the given ID and locale</returns>
        /// <exception cref="AggregateException">Thrown when the API response status code is not success or when the API call times out</exception>
        /// <exception cref="InvalidOperationException">Thrown when making a Batch API Request that has already reached the maxmimum API calls per batch request</exception>
        public static async Task<List<TemplateVersion>> GetTemplateVersionsAsync(string templateId, string locale)
        {
            // Send the GET request
            var resource = String.Format("templates/{0}/locales/{1}/versions", templateId, locale);
            var jsonResponse = await RequestManager.SendGetRequestAsync(resource);

            // Convert the JSON result into an object
            return JsonConvert.DeserializeObject<List<TemplateVersion>>(jsonResponse);
        }

        /// <summary>
        /// Get a specific version (with HTML/text).
        /// GET /templates/(:template_id)/versions/(:version_id)
        /// </summary>
        /// <param name="templateId">The ID of the template</param>
        /// <param name="versionId">The ID of the version</param>
        /// <returns>The template version associated with the given ID</returns>
        /// <exception cref="AggregateException">Thrown when the API response status code is not success or when the API call times out</exception>
        /// <exception cref="InvalidOperationException">Thrown when making a Batch API Request that has already reached the maxmimum API calls per batch request</exception>
        public static async Task<TemplateVersion> GetTemplateVersionAsync(string templateId, string versionId)
        {
            // Send the GET request
            var resource = String.Format("templates/{0}/versions/{1}", templateId, versionId);
            var jsonResponse = await RequestManager.SendGetRequestAsync(resource);

            // Convert the JSON result into an object
            return JsonConvert.DeserializeObject<TemplateVersion>(jsonResponse);
        }

        /// <summary>
        /// Get a specific version (with HTML/text).
        /// GET /templates/(:template_id)/locales/(:locale)/versions/(:version_id)
        /// </summary>
        /// <param name="templateId">The ID of the template</param>
        /// <param name="locale">The locale of the template</param>
        /// <param name="versionId">The ID of the version</param>
        /// <returns>The template version associated with the given ID</returns>
        /// <exception cref="AggregateException">Thrown when the API response status code is not success or when the API call times out</exception>
        /// <exception cref="InvalidOperationException">Thrown when making a Batch API Request that has already reached the maxmimum API calls per batch request</exception>
        public static async Task<TemplateVersion> GetTemplateVersionAsync(string templateId, string locale, string versionId)
        {
            // Send the GET request
            var resource = String.Format("templates/{0}/locales/{1}/versions/{2}", templateId, locale, versionId);
            var jsonResponse = await RequestManager.SendGetRequestAsync(resource);

            // Convert the JSON result into an object
            return JsonConvert.DeserializeObject<TemplateVersion>(jsonResponse);
        }

        /// <summary>
        /// Update a Template Version.
        /// PUT /templates/(:template_id)/versions/(:version_id)
        /// NOTE - At least one of html or text must be specified in the TemplateVersion
        /// </summary>
        /// <param name="templateId">The ID of the template</param>
        /// <param name="versionId">The ID of the version</param>
        /// <returns>The template version associated with the given ID</returns>
        /// <exception cref="AggregateException">Thrown when the API response status code is not success or when the API call times out</exception>
        /// <exception cref="InvalidOperationException">Thrown when making a Batch API Request that has already reached the maxmimum API calls per batch request</exception>
        public static async Task<TemplateVersion> UpdateTemplateVersionAsync(string templateId, string versionId, TemplateVersion updatedTemplateVersion)
        {
            // Send the PUT request
            var resource = String.Format("templates/{0}/versions/{1}", templateId, versionId);
            var jsonResponse = await RequestManager.SendPutRequestAsync(resource, updatedTemplateVersion);

            // Convert the JSON result into an object
            return JsonConvert.DeserializeObject<TemplateVersion>(jsonResponse);
        }

        /// <summary>
        /// Update a Template Version.
        /// PUT /templates/(:template_id)/locales/(:locale)/versions/(:version_id)
        /// </summary>
        /// <param name="templateId">The ID of the template</param>
        /// <param name="locale">The locale of the template</param>
        /// <param name="versionId">The ID of the version</param>
        /// <param name="updatedTemplateVersion">The updated template version</param>
        /// <returns>The template version associated with the given ID</returns>
        /// <exception cref="AggregateException">Thrown when the API response status code is not success or when the API call times out</exception>
        /// <exception cref="InvalidOperationException">Thrown when making a Batch API Request that has already reached the maxmimum API calls per batch request</exception>
        public static async Task<TemplateVersion> UpdateTemplateVersionAsync(string templateId, string locale, string versionId, TemplateVersion updatedTemplateVersion)
        {
            // Send the PUT request
            var resource = String.Format("templates/{0}/locales/{1}/versions/{2}", templateId, locale, versionId);
            var jsonResponse = await RequestManager.SendPutRequestAsync(resource, updatedTemplateVersion);

            // Convert the JSON result into an object
            return JsonConvert.DeserializeObject<TemplateVersion>(jsonResponse);
        }

        /// <summary>
        /// Creates a new template.
        /// POST /templates
        /// </summary>
        /// <param name="newTemplateVersion">The new version for the template</param>
        /// <returns>The new template</returns>
        /// <exception cref="AggregateException">Thrown when the API response status code is not success or when the API call times out</exception>
        /// <exception cref="InvalidOperationException">Thrown when making a Batch API Request that has already reached the maxmimum API calls per batch request</exception>
        public static async Task<Template> CreateTemplateAsync(TemplateVersion newTemplateVersion)
        {
            // Send the POST request
            var resource = "templates";
            var jsonResponse = await RequestManager.SendPostRequestAsync(resource, newTemplateVersion);

            // Convert the JSON result into an object
            return JsonConvert.DeserializeObject<Template>(jsonResponse);
        }

        /// <summary>
        /// Add Locale to Existing Template.
        /// POST /templates/(:template_id)/locales
        /// </summary>
        /// <param name="templateId">The ID of the template to add the locale to</param>
        /// <param name="locale">The locale to add</param>
        /// <param name="templateVersion">The template version</param>
        /// <returns>The template with the updated locale</returns>
        /// <exception cref="AggregateException">Thrown when the API response status code is not success or when the API call times out</exception>
        /// <exception cref="InvalidOperationException">Thrown when making a Batch API Request that has already reached the maxmimum API calls per batch request</exception>
        public static async Task<Template> AddLocaleToTemplate(string templateId, string locale, TemplateVersion templateVersion)
        {
            templateVersion.locale = locale;
            // Send the POST request
            var resource = String.Format("templates/{0}/locales", templateId);
            var jsonResponse = await RequestManager.SendPostRequestAsync(resource, templateVersion);

            // Convert the JSON result into an object
            return JsonConvert.DeserializeObject<Template>(jsonResponse);
        }

        /// <summary>
        /// Create a New Template Version.
        /// POST /templates/(:template_id)/versions
        /// </summary>
        /// <param name="templateId">The ID of the template to add the version to</param>
        /// <param name="templateVersion">The new template verison to add</param>
        /// <returns>The newly created template version</returns>
        /// <exception cref="AggregateException">Thrown when the API response status code is not success or when the API call times out</exception>
        /// <exception cref="InvalidOperationException">Thrown when making a Batch API Request that has already reached the maxmimum API calls per batch request</exception>
        public static async Task<TemplateVersion> CreateTemplateVersion(string templateId, TemplateVersion templateVersion)
        {
            // Send the POST request
            var resource = String.Format("templates/{0}/versions", templateId);
            var jsonResponse = await RequestManager.SendPostRequestAsync(resource, templateVersion);

            // Convert the JSON result into an object
            return JsonConvert.DeserializeObject<TemplateVersion>(jsonResponse);
        }

        /// <summary>
        /// Create a New Template Version.
        /// POST /templates/(:template_id)/locales/(:locale)/versions
        /// </summary>
        /// <param name="templateId">The ID of the template to add the version to</param>
        /// <param name="locale">The locale of the template to add the version to</param>
        /// /// <param name="templateVersion">The new verison to add to the template</param>
        /// <returns>The newly created template version</returns>
        /// <exception cref="AggregateException">Thrown when the API response status code is not success or when the API call times out</exception>
        /// <exception cref="InvalidOperationException">Thrown when making a Batch API Request that has already reached the maxmimum API calls per batch request</exception>
        public static async Task<TemplateVersion> CreateTemplateVersion(string templateId, string locale, TemplateVersion templateVersion)
        {
            // Send the POST request
            var resource = String.Format("templates/{0}/locales/{1}/versions", templateId, locale);
            var jsonResponse = await RequestManager.SendPostRequestAsync(resource, templateVersion);

            // Convert the JSON result into an object
            return JsonConvert.DeserializeObject<TemplateVersion>(jsonResponse);
        }

        /// <summary>
        /// Delete a specific template.
        /// DELETE /templates/(:template_id)
        /// </summary>
        /// <param name="templateId">The ID of the template to delete</param>
        /// <returns>The status of the api call</returns>
        /// <exception cref="AggregateException">Thrown when the API response status code is not success or when the API call times out</exception>
        /// <exception cref="InvalidOperationException">Thrown when making a Batch API Request that has already reached the maxmimum API calls per batch request</exception>
        public static async Task<GenericApiCallStatus> DeleteTemplate(string templateId)
        {
            // Send the POST request
            var resource = String.Format("templates/{0}", templateId);
            var jsonResponse = await RequestManager.SendDeleteRequestAsync(resource);

            // Convert the JSON result into an object
            return JsonConvert.DeserializeObject<GenericApiCallStatus>(jsonResponse);
        }

        /// <summary>
        /// Delete a specific template with a given locale.
        /// DELETE /templates/(:template_id)/locales/(:locale)
        /// </summary>
        /// <param name="templateId">The ID of the template to delete</param>
        /// <param name="locale">The locale of the template to delete</param>
        /// <returns>The status of the api call</returns>
        /// <exception cref="AggregateException">Thrown when the API response status code is not success or when the API call times out</exception>
        /// <exception cref="InvalidOperationException">Thrown when making a Batch API Request that has already reached the maxmimum API calls per batch request</exception>
        public static async Task<GenericApiCallStatus> DeleteTemplate(string templateId, string locale)
        {
            // Send the POST request
            var resource = String.Format("templates/{0}/locales/{1}", templateId, locale);
            var jsonResponse = await RequestManager.SendDeleteRequestAsync(resource);

            // Convert the JSON result into an object
            return JsonConvert.DeserializeObject<GenericApiCallStatus>(jsonResponse);
        }
    }
}
