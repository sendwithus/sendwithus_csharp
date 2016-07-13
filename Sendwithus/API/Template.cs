using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

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
        public Collection<TemplateVersion> versions { get; }
        public Collection<string> tags { get; }

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
        /// /// <exception cref="SendwithusException">Thrown when the API response status code is not success</exception>
        public static async Task<Collection<Template>> GetTemplatesAsync()
        {
            // Send the GET request
            var resource = "templates";
            var jsonResponse = await RequestManager.SendGetRequestAsync(resource);
            
            // Convert the JSON result into an object
            var serializer = new JavaScriptSerializer();
            var templateList = serializer.Deserialize<Collection<Template>>(jsonResponse);
            return templateList;
        }

        /// <summary>
        /// Get a template by ID.
        /// GET /templates/(:template_id)
        /// </summary>
        /// <param name="templateID">The ID of the template</param>
        /// <returns>The template with the given ID</returns>
        /// <exception cref="SendwithusException">Thrown when the API response status code is not success</exception>
        public static async Task<Template> GetTemplateAsync(string templateID)
        {
            // Send the GET request
            var resource = String.Format("templates/{0}", templateID);
            var jsonResponse = await RequestManager.SendGetRequestAsync(resource);

            // Convert the JSON result into an object
            var serializer = new JavaScriptSerializer();
            var template = serializer.Deserialize<Template>(jsonResponse);
            return template;
        }

        /// <summary>
        /// Get a template by ID and locale.
        /// GET /templates/(:template_id)/locales/(:locale)
        /// </summary>
        /// <param name="templateID">The ID of the template</param>
        /// <param name="locale"></param>
        /// <returns>The template with the given ID and locale</returns>
        /// <exception cref="SendwithusException">Thrown when the API response status code is not success</exception>
        public static async Task<Template> GetTemplateAsync(string templateID, string locale)
        {
            // Send the GET request
            var resource = String.Format("templates/{0}/locales/{1}", templateID, locale);
            var jsonResponse = await RequestManager.SendGetRequestAsync(resource);

            // Convert the JSON result into an object
            var serializer = new JavaScriptSerializer();
            var template = serializer.Deserialize<Template>(jsonResponse);
            return template;
        }

        /// <summary>
        /// Get a list of template versions (with HTML/text).
        /// GET /templates/(:template_id)/versions
        /// </summary>
        /// <param name="templateID">The ID of the template</param>
        /// <returns>The template versions associated with the given ID</returns>
        /// <exception cref="SendwithusException">Thrown when the API response status code is not success</exception>
        public static async Task<Collection<TemplateVersion>> GetTemplateVersionsAsync(string templateID)
        {
            // Send the GET request
            var resource = String.Format("templates/{0}/versions", templateID);
            var jsonResponse = await RequestManager.SendGetRequestAsync(resource);

            // Convert the JSON result into an object
            var serializer = new JavaScriptSerializer();
            var versions = serializer.Deserialize<Collection<TemplateVersion>>(jsonResponse);
            return versions;
        }

        /// <summary>
        /// Get a list of template versions (with HTML/text).
        /// GET /templates/(:template_id)/locales/(:locale)/versions
        /// </summary>
        /// <param name="templateID">The ID of the template</param>
        /// <param name="locale">The locale of the template</param>
        /// <returns>The template versions associated with the given ID and locale</returns>
        /// <exception cref="SendwithusException">Thrown when the API response status code is not success</exception>
        public static async Task<Collection<TemplateVersion>> GetTemplateVersionsAsync(string templateID, string locale)
        {
            // Send the GET request
            var resource = String.Format("templates/{0}/locales/{1}/versions", templateID, locale);
            var jsonResponse = await RequestManager.SendGetRequestAsync(resource);

            // Convert the JSON result into an object
            var serializer = new JavaScriptSerializer();
            var versions = serializer.Deserialize<Collection<TemplateVersion>>(jsonResponse);
            return versions;
        }

        /// <summary>
        /// Get a specific version (with HTML/text).
        /// GET /templates/(:template_id)/versions/(:version_id)
        /// </summary>
        /// <param name="templateID">The ID of the template</param>
        /// <param name="versionID">The ID of the version</param>
        /// <returns>The template version associated with the given ID</returns>
        /// <exception cref="SendwithusException">Thrown when the API response status code is not success</exception>
        public static async Task<TemplateVersion> GetTemplateVersionAsync(string templateID, string versionID)
        {
            // Send the GET request
            var resource = String.Format("templates/{0}/versions/{1}", templateID, versionID);
            var jsonResponse = await RequestManager.SendGetRequestAsync(resource);

            // Convert the JSON result into an object
            var serializer = new JavaScriptSerializer();
            var response = serializer.Deserialize<TemplateVersion>(jsonResponse);
            return response;
        }

        /// <summary>
        /// Get a specific version (with HTML/text).
        /// GET /templates/(:template_id)/locales/(:locale)/versions/(:version_id)
        /// </summary>
        /// <param name="templateID">The ID of the template</param>
        /// <param name="locale">The locale of the template</param>
        /// <param name="versionID">The ID of the version</param>
        /// <returns>The template version associated with the given ID</returns>
        /// <exception cref="SendwithusException">Thrown when the API response status code is not success</exception>
        public static async Task<TemplateVersion> GetTemplateVersionAsync(string templateID, string locale, string versionID)
        {
            // Send the GET request
            var resource = String.Format("templates/{0}/locales/{1}/versions/{2}", templateID, locale, versionID);
            var jsonResponse = await RequestManager.SendGetRequestAsync(resource);

            // Convert the JSON result into an object
            var serializer = new JavaScriptSerializer();
            var response = serializer.Deserialize<TemplateVersion>(jsonResponse);
            return response;
        }

        /// <summary>
        /// Update a Template Version.
        /// PUT /templates/(:template_id)/versions/(:version_id)
        /// NOTE - At least one of html or text must be specified in the TemplateVersion
        /// </summary>
        /// <param name="templateID">The ID of the template</param>
        /// <param name="versionID">The ID of the version</param>
        /// <returns>The template version associated with the given ID</returns>
        /// <exception cref="SendwithusException">Thrown when the API response status code is not success</exception>
        public static async Task<TemplateVersion> UpdateTemplateVersionAsync(string templateID, string versionID, TemplateVersion updatedTemplateVersion)
        {
            // Send the PUT request
            var resource = String.Format("templates/{0}/versions/{1}", templateID, versionID);
            var jsonResponse = await RequestManager.SendPutRequestAsync(resource, updatedTemplateVersion);

            // Convert the JSON result into an object
            var serializer = new JavaScriptSerializer();
            var response = serializer.Deserialize<TemplateVersion>(jsonResponse);
            return response;
        }

        /// <summary>
        /// Update a Template Version.
        /// PUT /templates/(:template_id)/locales/(:locale)/versions/(:version_id)
        /// </summary>
        /// <param name="templateID">The ID of the template</param>
        /// <param name="locale">The locale of the template</param>
        /// <param name="versionID">The ID of the version</param>
        /// <param name="updatedTemplateVersion">The updated template version</param>
        /// <returns>The template version associated with the given ID</returns>
        /// <exception cref="SendwithusException">Thrown when the API response status code is not success</exception>
        public static async Task<TemplateVersion> UpdateTemplateVersionAsync(string templateID, string locale, string versionID, TemplateVersion updatedTemplateVersion)
        {
            // Send the PUT request
            var resource = String.Format("templates/{0}/locales/{1}/versions/{2}", templateID, locale, versionID);
            var jsonResponse = await RequestManager.SendPutRequestAsync(resource, updatedTemplateVersion);

            // Convert the JSON result into an object
            var serializer = new JavaScriptSerializer();
            var response = serializer.Deserialize<TemplateVersion>(jsonResponse);
            return response;
        }

        /// <summary>
        /// Creates a new template
        /// POST /templates
        /// </summary>
        /// <param name="templateID">The ID of the template</param>
        /// <param name="locale">The locale of the template</param>
        /// <param name="versionID">The ID of the version</param>
        /// <param name="updatedTemplateVersion">The updated template version</param>
        /// <returns>The new template</returns>
        /// <exception cref="SendwithusException">Thrown when the API response status code is not success</exception>
        public static async Task<Template> CreateTemplateAsync(TemplateVersion newTemplateVersion)
        {
            // Send the POST request
            var resource = "templates";
            var jsonResponse = await RequestManager.SendPostRequestAsync(resource, newTemplateVersion);

            // Convert the JSON result into an object
            var serializer = new JavaScriptSerializer();
            var response = serializer.Deserialize<Template>(jsonResponse);
            return response;
        }

        /// <summary>
        /// Add Locale to Existing Template
        /// POST /templates/(:template_id)/locales
        /// </summary>
        /// <param name="templateID">The ID of the template to add the locale to</param>
        /// <param name="locale">The locale to add</param>
        /// <param name="templateVersion">The template version</param>
        /// <returns>The template with the updated locale</returns>
        /// <exception cref="SendwithusException">Thrown when the API response status code is not success</exception>
        public static async Task<Template> AddLocaleToTemplate(string templateID, string locale, TemplateVersion templateVersion)
        {
            templateVersion.locale = locale;
            // Send the POST request
            var resource = String.Format("templates/{0}/locales", templateID);
            var jsonResponse = await RequestManager.SendPostRequestAsync(resource, templateVersion);

            // Convert the JSON result into an object
            var serializer = new JavaScriptSerializer();
            var response = serializer.Deserialize<Template>(jsonResponse);
            return response;
        }

        /// <summary>
        /// Create a New Template Version
        /// POST /templates/(:template_id)/versions
        /// </summary>
        /// <param name="templateID">The ID of the template to add the version to</param>
        /// <param name="templateVersion">The new template verison to add</param>
        /// <returns>The newly created template version</returns>
        /// <exception cref="SendwithusException">Thrown when the API response status code is not success</exception>
        public static async Task<TemplateVersion> CreateTemplateVersion(string templateID, TemplateVersion templateVersion)
        {
            // Send the POST request
            var resource = String.Format("templates/{0}/versions", templateID);
            var jsonResponse = await RequestManager.SendPostRequestAsync(resource, templateVersion);

            // Convert the JSON result into an object
            var serializer = new JavaScriptSerializer();
            var response = serializer.Deserialize<TemplateVersion>(jsonResponse);
            return response;
        }

        /// <summary>
        /// Create a New Template Version
        /// POST /templates/(:template_id)/locales/(:locale)/versions
        /// </summary>
        /// <param name="templateID">The ID of the template to add the version to</param>
        /// <param name="templateVersion">The new template verison to add</param>
        /// <returns>The newly created template version</returns>
        /// <exception cref="SendwithusException">Thrown when the API response status code is not success</exception>
        public static async Task<TemplateVersion> CreateTemplateVersion(string templateID, string locale, TemplateVersion templateVersion)
        {
            // Send the POST request
            var resource = String.Format("templates/{0}/locales/{1}/versions", templateID, locale);
            var jsonResponse = await RequestManager.SendPostRequestAsync(resource, templateVersion);

            // Convert the JSON result into an object
            var serializer = new JavaScriptSerializer();
            var response = serializer.Deserialize<TemplateVersion>(jsonResponse);
            return response;
        }

        /// <summary>
        /// Delete a specific template.
        /// DELETE /templates/(:template_id)
        /// </summary>
        /// <param name="templateID">The ID of the template to delete</param>
        /// <returns>The status of the api call</returns>
        /// <exception cref="SendwithusException">Thrown when the API response status code is not success</exception>
        public static async Task<GenericApiCallStatus> DeleteTemplate(string templateID)
        {
            // Send the POST request
            var resource = String.Format("templates/{0}", templateID);
            var jsonResponse = await RequestManager.SendDeleteRequestAsync(resource);

            // Convert the JSON result into an object
            var serializer = new JavaScriptSerializer();
            var response = serializer.Deserialize<GenericApiCallStatus>(jsonResponse);
            return response;
        }

        /// <summary>
        /// Delete a specific template with a given locale.
        /// DELETE /templates/(:template_id)/locales/(:locale)
        /// </summary>
        /// <param name="templateID">The ID of the template to delete</param>
        /// <param name="locale">The locale of the template to delete</param>
        /// <returns>The status of the api call</returns>
        /// <exception cref="SendwithusException">Thrown when the API response status code is not success</exception>
        public static async Task<GenericApiCallStatus> DeleteTemplate(string templateID, string locale)
        {
            // Send the POST request
            var resource = String.Format("templates/{0}/locales/{1}", templateID, locale);
            var jsonResponse = await RequestManager.SendDeleteRequestAsync(resource);

            // Convert the JSON result into an object
            var serializer = new JavaScriptSerializer();
            var response = serializer.Deserialize<GenericApiCallStatus>(jsonResponse);
            return response;
        }
    }
}
