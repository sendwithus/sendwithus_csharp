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
    /// sendwithus EspAccount class
    /// </summary>
    public class EspAccount
    {
        public string Object { get; set; }
        public string id { get; set; }
        public Int64 created { get; set; }
        public string esp_type { get; set; }
        public string name { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public EspAccount()
        {
            Object = String.Empty;
            id = String.Empty;
            created = 0;
            esp_type = String.Empty;
            name = String.Empty;
        }

        /// <summary>
        /// Gets all the ESP accounts
        /// </summary>
        /// <param name="queryParameters">The query parameters.  Options include:
        /// esp_type (optional) – Filter response to only return ESP accounts of a certain type</param>
        /// <returns>A list of all the ESP accounts</returns>
        public static async Task<List<EspAccount>> GetAccountsAsync(Dictionary<string, object> queryParameters)
        {
            // Send the GET request
            var resource = "esp_accounts";
            var jsonResponse = await RequestManager.SendGetRequestAsync(resource, queryParameters);

            // Convert the JSON result into an object
            var serializer = new JavaScriptSerializer();
            return serializer.Deserialize<List<EspAccount>>(jsonResponse);
        }

        /// <summary>
        /// Gets all the ESP accounts without any query parametesr
        /// </summary>
        /// <returns>A list of all the ESP accounts</returns>
        public static async Task<List<EspAccount>> GetAccountsAsync()
        {
            return await GetAccountsAsync(null);
        }

        /// <summary>
        /// Add a new ESP account.
        /// POST /esp_accounts
        /// </summary>
        /// <param name="addRequest">The parameters for the new ESP Account to add</param>
        /// <returns>A response containing the API call status and the new ESP account</returns>
        public static async Task<EspAccountResponse> AddAccountAsync(EspAccountAddAccountRequest addRequest)
        {
            // Send the GET request
            var resource = "esp_accounts";
            var jsonResponse = await RequestManager.SendPostRequestAsync(resource, addRequest);

            // Convert the JSON result into an object
            var serializer = new JavaScriptSerializer();
            return serializer.Deserialize<EspAccountResponse>(jsonResponse);
        }

        /// <summary>
        /// Set a given ESP Account as the default for sending emails.
        /// PUT /esp_accounts/set_default
        /// </summary>
        /// /// <param name="espId">The ID of the ESP to set as the default</param>
        /// <returns>A response containing the API call status and the ESP account</returns>
        public static async Task<EspAccountResponse> SetDefaultEspAccountAsync(string espId)
        {
            // Send the GET request
            var resource = "esp_accounts/set_default";
            var espIdDict = new Dictionary<string, string>();
            espIdDict.Add("esp_id", espId);
            var jsonResponse = await RequestManager.SendPutRequestAsync(resource, espIdDict);

            // Convert the JSON result into an object
            var serializer = new JavaScriptSerializer();
            return serializer.Deserialize<EspAccountResponse>(jsonResponse);
        }
    }
}
