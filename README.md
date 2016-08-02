sendwithus_sharp
================

sendwithus C# Client

## status
[![Build status](https://ci.appveyor.com/api/projects/status/2f6ljyg5a6y1umba/branch/master?svg=true)](https://ci.appveyor.com/project/bvanvugt/sendwithus-csharp/branch/master)

## requirements
none

## .NET Framework Support
Target framework is 4.5.2
    
## API Coverage
With one exception, this client covers all of the sendwithus API calls documented at: https://www.sendwithus.com/docs/api#overview

The exception is the Multi-Langauge API calls; those API calls are not supported in this client.  If you require support for the Multi-Language API, please contact sendwithus: https://www.sendwithus.com/contact

Install via NuGet Package Manager
---------------------------------

If you are using Visual Studio 2015:
* In the Solution Explorer window, right click on your solution and select "Manage NuGet Packages for Solution..."
* Search for "SendwithusClient"
  * Be sure to use the one that's by sendwithus.  There's another one called SendWithUs.Client by Mimeo, but that is not supported by sendwithus
* Select the "SendwithusClient" package and choose "Install" for your solution/project.

## Getting started

```csharp
using sendwithus;

// Set the API Key
SendwithusClient.ApiKey = <your_api_key> // Test or Production

// Configure API settings (optional)
SendwithusClient.SetTimeoutInMilliseconds(your_preferred_timeout); // Default is 30000 (30s).  This is the timeout for an individual API call attempt
SendwithusClient.RetryCount = your_preferred_retryCount; // Default is 3.  This is the number of times that each API call will be retried if it fails due to a potentially transient event
SendwithusClient.RetryIntervalMilliseconds = your_preferred_retryInterval; // Default is 100ms.  This is the amount of time to wait between retry attempts.

```

### Notes on Retries and Exceptions
The API will perform a retry in the following cases:

| Retry Causes | Exception Thrown |
| ------------ | ---------------- |
| API Call Timeout | TaskCanceledException |
| Received HTTP Status 502:  BadGateway | SendwithusException |
| Received HTTP Status 503: ServiceUnavailable | SendwithusException |
| Received HTTP Status 505: GatewayTimeout | SendwithusException |

A SendwithusException is a regular Exception with a StatusCode property added

All exceptions will be part of an AggregateException, regardless of whether a retry was attempted or not.

Some examples: 
* An API call that fails with a status code 403: Forbidden (not a retriable status code) will throw an AggregateException with one InnerException of type SendwithusException
* An API call that repeatedly times out will throw an AggregateException with InnerExceptions of type TaskCanceledException
* An API call that times out once, then fails with a status code of 503: Service Unavailable, and then fails again with a status code 400: Bad Request will throw an AggregateException with InnerExceptions, in order, of: { TaskCanceledException, SendwithusException, SendwithusException}

#### Other Exceptions
The API Client will also throw the following other exceptions:
* An InvalidOperationException will be thrown when adding a new API call to a queue of Batched API calls when the limit of maximum API calls per batch has already been met

# API Calls
## Templates
### Get a list of templates
#### GET /templates
```csharp
try
{
    var templates = await Template.GetTemplatesAsync();
}
catch (AggregateException exception)
{
    // Exception handling
}
```
### Get a specific template (all versions)
#### GET /templates/(:template_id)
```csharp
var templateId = "tem_SxZKpxJSHPbYDWRSQnAQUR";
try
{
    var response = await Template.GetTemplateAsync(templateId);
}
catch (AggregateException exception)
{
    // Exception handling
}
```
#### GET /templates/(:template_id)/versions
```csharp
var templateId = "tem_SxZKpxJSHPbYDWRSQnAQUR";
try
{
    var template = await Template.GetTemplateVersionsAsync(templateId);
}
catch (AggregateException exception)
{
    // Exception handling
}
```
### Get a list of template versions (with HTML/text)
#### GET /templates/(:template_id)/versions
```csharp
var templateId = "tem_SxZKpxJSHPbYDWRSQnAQUR";
var locale = "en-US";
try
{
    var template = await Template.GetTemplateAsync(templateId, locale);
}
catch (AggregateException exception)
{
    // Exception handling
}
```
#### GET /templates/(:template_id)/locales/(:locale)/versions
```csharp
var templateId = "tem_SxZKpxJSHPbYDWRSQnAQUR";
var locale = "en-US";
try
{
    var templateVersions = await Template.GetTemplateVersionsAsync(templateId, locale);
}
catch (AggregateException exception)
{
    // Exception handling
}
```
### Get a specific version (with HTML/text)
#### GET /templates/(:template_id)/versions/(:version_id)
```csharp
var templateId = "tem_SxZKpxJSHPbYDWRSQnAQUR";
var versionId = "ver_ET3j2snkKhqsjRjtK6bXJE";
try
{
    var templateVersion = await Template.GetTemplateVersionAsync(templateId, versionId);
}
catch (AggregateException exception)
{
    // Exception handling
}
```
#### GET /templates/(:template_id)/locales/(:locale)/versions/(:version_id)
```csharp
var templateId = "tem_SxZKpxJSHPbYDWRSQnAQUR";
var locale = "en-US";
var versionId = "ver_ET3j2snkKhqsjRjtK6bXJE";
try
{
    var templateVersion = await Template.GetTemplateVersionAsync(templateId, locale, versionId);
}
catch (AggregateException exception)
{
    // Exception handling
}
```
### Update a template version
Note: at least one of "html" or "text" must be specified in the updated template version object
#### PUT /templates/(:template_id)/versions/(:version_id)
```csharp
var templateId = "tem_SxZKpxJSHPbYDWRSQnAQUR";
var versionId = "ver_ET3j2snkKhqsjRjtK6bXJE";
var updatedTemplateVersion = new TemplateVersion();
var templateVersionName = "New Version";
var templateSubject = "edited!";
var updatedTemplateVersion = new TemplateVersion(templateVersionName, templateSubject);
updatedTemplateVersion.html = "<html><head></head><body><h1>UPDATE</h1></body></html>"; // optional
updatedTemplateVersion.text = "sometext"; // optional
try
{
    var templateVersion = await Template.UpdateTemplateVersionAsync(templateId, versionId, updatedTemplateVersion);
}
catch (AggregateException exception)
{
    // Exception handling
}
```
#### PUT /templates/(:template_id)/locales/(:locale)/versions/(:version_id)
```csharp
var templateId = "tem_SxZKpxJSHPbYDWRSQnAQUR";
var locale = "en-US"
var versionId = "ver_ET3j2snkKhqsjRjtK6bXJE";
var updatedTemplateVersion = new TemplateVersion();
var templateVersionName = "New Version";
var templateSubject = "edited!";
var updatedTemplateVersion = new TemplateVersion(templateVersionName, templateSubject);
updatedTemplateVersion.html = "<html><head></head><body><h1>UPDATE</h1></body></html>"; // optional
updatedTemplateVersion.text = "sometext"; // optional
try
{
    var templateVersion = await Template.UpdateTemplateVersionAsync(templateId, versionId, updatedTemplateVersion);
}
catch (AggregateException exception)
{
    // Exception handling
}
```
### Create a New Template
#### POST /templates
```csharp
var templateVersionName = "New Template Version";
var templateSubject = "New Version!";
var updatedTemplateVersion = new TemplateVersion(templateVersionName, templateSubject);
updatedTemplateVersion.html = "<html><head></head><body><h1>NEW TEMPLATE VERSION</h1></body></html>"; // optional
updatedTemplateVersion.text = "some text"; // optional
updatedTemplateVersion.locale = "en-US"; // optional
try
{
    var template = await Template.CreateTemplateAsync(updatedTemplateVersion);
}
catch (AggregateException exception)
{
    // Exception handling
}
```
### Add locale to existing template
#### POST /templates/(:template_id)/locales
```csharp
var templateId = "tem_SxZKpxJSHPbYDWRSQnAQUR";
var locale = "fr-FR";
var templateVersionName = "Published French Version";
var templateSubject = "Ce est un nouveau modèle!";
var updatedTemplateVersion = new TemplateVersion(templateVersionName, templateSubject);
updatedTemplateVersion.html = "<html><head></head><body><h1>Nouveau modèle!</h1></body></html>"; // optional
updatedTemplateVersion.text = "un texte"; // optional
try
{
    var template = await Template.AddLocaleToTemplate(templateId, locale, updatedTemplateVersion);
}
catch (AggregateException exception)
{
    // Exception handling
}
```
### Create a new template version
#### POST /templates/(:template_id)/versions
*NOTE* – At least one of html or text must be specified
```csharp
var templateId = "tem_SxZKpxJSHPbYDWRSQnAQUR";
var templateVersionName = "New Template Version";
var templateSubject = "New Version!";
var updatedTemplateVersion = new TemplateVersion(templateVersionName, templateSubject);
updatedTemplateVersion.html = "<html><head></head><body><h1>NEW TEMPLATE VERSION</h1></body></html>"; // optional
updatedTemplateVersion.text = "some text"; // optional
try
{
    var templateVersion = await Template.CreateTemplateVersion(templateId, updatedTemplateVersion);
}
catch (AggregateException exception)
{
    // Exception handling
}
```
#### POST /templates/(:template_id)/locales/(:locale)/versions
*NOTE* – At least one of html or text must be specified
```csharp
var templateId = "tem_SxZKpxJSHPbYDWRSQnAQUR";
var locale = "en-US";
var templateVersionName = "New Template Version";
var templateSubject = "New Version!";
var updatedTemplateVersion = new TemplateVersion(templateVersionName, templateSubject);
updatedTemplateVersion.html = "<html><head></head><body><h1>NEW TEMPLATE VERSION</h1></body></html>"; // optional
updatedTemplateVersion.text = "some text"; // optional
try
{
    var templateVersion = await Template.CreateTemplateVersion(templateId, locale, updatedTemplateVersion);
}
catch (AggregateException exception)
{
    // Exception handling
}
```
### Delete a specific template
#### DELETE /templates/(:template_id)
```csharp
var templateId = "tem_SxZKpxJSHPbYDWRSQnAQUR";
try
{
    var genericApiCallStatus = await Template.DeleteTemplate(templateId);
}
catch (AggregateException exception)
{
    // Exception handling
}
```
#### DELETE /templates/(:template_id)/locales/(:locale)
```csharp
var templateId = "tem_SxZKpxJSHPbYDWRSQnAQUR";
var locale = "en-US";
try
{
    var genericApiCallStatus = await Template.DeleteTemplate(templateId, locale);
}
catch (AggregateException exception)
{
    // Exception handling
}
```
## Sending Emails

### Send an email
_We validate all HTML content_
#### POST /send
Example with only the required parameters:
```csharp
var templateId = "tem_SxZKpxJSHPbYDWRSQnAQUR";

// Construct the template data
// The content of the template data is all optional and is based on the template being used
var templateData = new Dictionary<string, object>();
templateData.Add("first_name", "Chuck");
templateData.Add("last_name", "Norris");
templateData.Add("img", "http://placekitten.com/50/60");
var link = new Dictionary<string, string>();
link.Add("url", "https://www.sendwithus.com");
link.Add("text", "sendwithus!");
templateData.Add("link", link);

// Construct the recipient
var recipient = new EmailRecipient(DEFAULT_RECIPIENT_EMAIL_ADDRESS);

// Construct the email object
var email = new Email(templateId, templateData, recipient);

// Send the email
try
{
    var emailResponse = await email.Send();
}
catch (AggregateException exception)
{
    // Exception handling
}
```
Example with all parameters:
```csharp
var templateId = "tem_SxZKpxJSHPbYDWRSQnAQUR";

// Construct the template data
// The content of the template data is all optional and is based on the template being used
var templateData = new Dictionary<string, object>();
templateData.Add("first_name", "Chuck");
templateData.Add("last_name", "Norris");
templateData.Add("img", "http://placekitten.com/50/60");
var link = new Dictionary<string, string>();
link.Add("url", "https://www.sendwithus.com");
link.Add("text", "sendwithus!");
templateData.Add("link", link);

// Construct the recipient
var recipient = new EmailRecipient(DEFAULT_RECIPIENT_EMAIL_ADDRESS);

// Construct the email object
var email = new Email(templateId, templateData, recipient);
email.cc.Add(new EmailRecipient("cc_one@email.com", "CC One"));
email.cc.Add(new EmailRecipient("cc_two@email.com", "CC Two"));
email.bcc.Add(new EmailRecipient("bcc_one@email.com", "BCC One"));
email.bcc.Add(new EmailRecipient("bcc_two@email.com", "BCC Two"));
email.sender.address = "company@company.com";
email.sender.reply_to = "info@company.com";
email.sender.name = "Company";
email.tags.Add("tag1");
email.tags.Add("tag2");
email.tags.Add("tag3");
email.headers.Add("X-HEADER-ONE", "header-value");
email.inline.id = "cat.png";
email.inline.data = "{BASE_64_ENCODED_FILE_DATA}";
email.files.Add(new EmailFileData("doc.txt", "{BASE_64_ENCODED_FILE_DATA}"));
email.files.Add(new EmailFileData("stuff.zip", "{BASE_64_ENCODED_FILE_DATA}"));
email.version_name = "this version";
email.locale = "en-US";
email.esp_account = "esp_EsgkbqQdDg7F3ncbz9EHW7";

// Send the email
try
{
    var emailResponse = await email.Send();
}
catch (AggregateException exception)
{
    // Exception handling
}
```
## Logs
### Get a list of logs
#### GET /logs
Example with no filter parameters:
```csharp
try
{
    var logs = await Log.GetLogsAsync();
}
catch (AggregateException exception)
{
    // Exception handling
}
```
Example with all the filter parameters:
```csharp
// Build the query parameters (each is optional)
Dictionary<string, object> queryParameters = new Dictionary<string, object>();
queryParameters.Add("count", DEFAULT_COUNT);
queryParameters.Add("offset", DEFAULT_OFFSET);
queryParameters.Add("created_gt", LOG_CREATED_AFTER_TIME);
queryParameters.Add("created_gte", LOG_CREATED_AFTER_TIME);
queryParameters.Add("created_lt", LOG_CREATED_BEFORE_TIME);
queryParameters.Add("created_lte", LOG_CREATED_BEFORE_TIME);

// Make the API call
try
{
    var logs = await Log.GetLogsAsync(queryParameters);
}
catch (AggregateException exception)
{
    // Exception handling
}
```
### Get a specific log + metadata
#### GET /logs/(:log_id)
```csharp
var logId = "log_88be2c0f8b5c6d3933dd578b6a0f13e5";
try
{
    var log = await Log.GetLogAsync(logId);
}
catch (AggregateException exception)
{
    // Exception handling
}
```
### Retrieve events for a specific log_id
#### GET /logs/(:log_id)/events
```csharp
var logId = "log_88be2c0f8b5c6d3933dd578b6a0f13e5";
try
{
    var logEvents = await Log.GetLogEventsAsync(logId);
}
catch (AggregateException exception)
{
    // Exception handling
}
```
### Resend an existing Log
#### POST /resend
```csharp
var logId = "log_88be2c0f8b5c6d3933dd578b6a0f13e5";
try
{
    var logResendResponse = await Log.ResendLogAsync(logId);
}
catch (AggregateException exception)
{
    // Exception handling
}
```
## Snippets
### Get all snippets
#### GET /snippets
```csharp
try
{
    var snippets = await Snippet.GetSnippetsAsync();
}
catch (AggregateException exception)
{
    // Exception handling
}
```
### Get specific snippet
#### GET /snippets/(:id)
```csharp
var snippetId = "snp_bn8c87iXuFWdtYLGJrBAWW";
try
{
    var snippets = await Snippet.GetSnippetAsync(snippetId);
}
catch (AggregateException exception)
{
    // Exception handling
}
```
### Creating a new snippet
#### POST /snippets
```csharp
var snippetName = "My First Snippet";
var snippetBody = "<h1>Welcome!</h1>";
try
{
    var snippetResponse = await Snippet.CreateSnippetAsync(snippetName, snippetBody);
}
catch (AggregateException exception)
{
    // Exception handling
}
```
### Update an existing snippet
#### PUT /snippets/(:id)
```csharp
var snippetId = "snp_bn8c87iXuFWdtYLGJrBAWW";
var snippetName = "Updated Snippet";
var snippetBody = "<h1>Welcome Again!</h1>";
try
{
    var response = await Snippet.UpdateSnippetAsync(snippetId, snippetName, snippetBody);
}
catch (AggregateException exception)
{
    // Exception handling
}
```
### Delete an existing snippet
#### DELETE /snippets/(:id)
```csharp
var snippetId = "snp_bn8c87iXuFWdtYLGJrBAWW";
try
{
    var genericApiCallStatus = await Snippet.DeleteSnippetAsync(snippetId);
}
catch (AggregateException exception)
{
    // Exception handling
}
```
## Render templates
### Render a template with data
#### POST /render
```csharp
var templateId = "tem_SxZKpxJSHPbYDWRSQnAQUR";

// Create the template data
var templateData = new Dictionary<string, object>();
templateData.Add("amount", "$12.00");

// Create the render object
var renderTemplate = new Render(templateId, templateData);
renderTemplate.version_id = "ver_ET3j2snkKhqsjRjtK6bXJE"; // optional.  Can use either version_id or version_name to specify a version, but not both
renderTemplate.locale = "en-US"; // optional
renderTemplate.strict = true; // optional.  Strict defaults to false if not set

try
{
    var renderTemplateResponse = await renderTemplate.RenderTemplateAsync();
}
catch (AggregateException exception)
{
    // Exception handling
}
```
## ESP Account
### List all ESP accounts
#### GET /esp_accounts
Gets all ESP accounts:
```csharp
try
{
    var espAccounts = await EspAccount.GetAccountsAsync();
}
catch (AggregateException exception)
{
    // Exception handling
}
```
Gets all ESP accounts of a given account type:
```csharp
// Build the query parameters
var queryParameters = new Dictionary<string, object>();
queryParameters.Add("esp_type", DEFAULT_ESP_ACCOUNT_TYPE);

// Make the API call
try
{
    var espAccounts = await EspAccount.GetAccountsAsync(queryParameters);
}
catch (AggregateException exception)
{
    // Exception handling
}
```
Possible esp_account.esp_type values:
* sendgrid
* mailgun
* mandrill
* postmark
* ses
* mailjet
* dyn
* sparkpost
* smtp
### Add a new ESP account
#### POST /esp_accounts
SendGrid example:
```csharp
var credentials = new EspAccountCredientialsSendgrid("mysendgridusername", "password123");
var addAccountRequest = new EspAccountAddAccountRequest("My SendGrid Account", "sendgrid", credentials);
try
{
    var espAccountResponse = await EspAccount.AddAccountAsync(addAccountRequest);
}
catch (AggregateException exception)
{
    // Exception handling
}
```
Mailgun example:
```csharp
var credentials = new EspAccountCredientialsMailgun("key-mymailgunapikey", "my.mailgun.domain.com");
var addAccountRequest = new EspAccountAddAccountRequest("My Mailgun Account", "mailgun", credentials);
try
{
    var espAccountResponse = await EspAccount.AddAccountAsync(addAccountRequest);
}
catch (AggregateException exception)
{
    // Exception handling
}
```
Mandrill example:
```csharp
var credentials = new EspAccountCredientialsMandrill("mymandrillapikey");
var addAccountRequest = new EspAccountAddAccountRequest("My Mandrill Account", "mandrill", credentials);
try
{
    var espAccountResponse = await EspAccount.AddAccountAsync(addAccountRequest);
}
catch (AggregateException exception)
{
    // Exception handling
}
```
Postmark example:
```csharp
var credentials = new EspAccountCredientialsPostmark("my-postmark-api-key"); // Note: use your Postmark "Server API Token" as the api key for this call
var addAccountRequest = new EspAccountAddAccountRequest("My Postmark Account", "postmark", credentials);
try
{
    var espAccountResponse = await EspAccount.AddAccountAsync(addAccountRequest);
}
catch (AggregateException exception)
{
    // Exception handling
}
```
SES example:
```csharp
var credentials = new EspAccountCredientialsSes("mysesaccesskeyid", "mysessecretaccesskey", "us-east-1");
var addAccountRequest = new EspAccountAddAccountRequest("My SES Account", "ses", credentials);
try
{
    var espAccountResponse = await EspAccount.AddAccountAsync(addAccountRequest);
}
catch (AggregateException exception)
{
    // Exception handling
}
```
Mailjet example:
```csharp
var credentials = new EspAccountCredientialsMailjet("mymailjetapikey", "mymailjetsecretkey");
var addAccountRequest = new EspAccountAddAccountRequest("My Mailjet Account", "mailjet", credentials);
try
{
    var espAccountResponse = await EspAccount.AddAccountAsync(addAccountRequest);
}
catch (AggregateException exception)
{
    // Exception handling
}
```
DYN example:
```csharp
var credentials = new EspAccountCredientialsDyn("mydynapikey");
var addAccountRequest = new EspAccountAddAccountRequest("My DYN Account", "dyn", credentials);
try
{
    var espAccountResponse = await EspAccount.AddAccountAsync(addAccountRequest);
}
catch (AggregateException exception)
{
    // Exception handling
}
```
SMTP example:
```csharp
var credentials = new EspAccountCredientialsSmtp("smtp.example.com", 25, "myusername", "mypassword", true);
var addAccountRequest = new EspAccountAddAccountRequest("My SMTP Account", "smtp", credentials);
try
{
    var espAccountResponse = await EspAccount.AddAccountAsync(addAccountRequest);
}
catch (AggregateException exception)
{
    // Exception handling
}
```
### Set a given ESP Account as the default for sending emails
#### PUT /esp_accounts/set_default
```csharp
var espAccountId = "esp_e3ut7pFtWttcN4HNoQ8Vgm";
try
{
    var espAccountResponse = await EspAccount.SetDefaultEspAccountAsync(DEFAULT_ESP_ACCOUNT_ID);
}
catch (AggregateException exception)
{
    // Exception handling
}
```
## Customers
### Get a specific customer
#### GET /customers/customer@example.com
```csharp
var customerEmailAddress = "customer@example.com";
try
{
    var customerResponse = await Customer.GetCustomerAsync(customerEmailAddress);
}
catch (AggregateException exception)
{
    // Exception handling
}
```
### Creating/updating a new customer
#### POST /customers
If a Customer already exists with the specified email address, then a data merge is performed. Merge operations will:
 * replace existing attributes with new values
 * add any new attributes to the Customer
Merge operations will never remove attributes from a Customer. Note that customer data can only be simple data types like strings and integers.
```csharp
// Build the customer
var customer = new Customer("customer@example.com");
customer.data.Add("first_name", "Matt"); // optional
customer.data.Add("city", "San Francisco"); // optional
customer.locale = "en-US"; // optional
customer.groups.Add("grp_7zpRYpExEBPpd6dGvyAfcT"); // optional

// Make the API call
try
{
    var genericApiCallStatus = await Customer.CreateOrUpdateCustomerAsync(customer);
}
catch (AggregateException exception)
{
    // Exception handling
}
```
### Delete a customer
#### DELETE /customers/(:email)
```csharp
var customerEmailAddress = "customer@example.com";
try
{
    var genericApiCallStatus = await Customer.DeleteCustomerAsync(customerEmailAddress);
}
catch (AggregateException exception)
{
    // Exception handling
}
```
### Get email logs for a customer
#### GET /customers/matt@sendwithus.com/logs?count={count}&created_lt={timestamp}&created_gt={timestamp}
With no query parameters to filter the logs:
```csharp
var customerEmailAddress = "customer@example.com";
try
{
    var customerEmailLogsResponse = await Customer.GetCustomerEmailLogsAsync(customerEmailAddress);
}
catch (AggregateException exception)
{
    // Exception handling
}
```
With all query parameters to filter the logs:
```csharp
var customerEmailAddress = "customer@example.com";
var queryStartTime = 123456789;
var queryEndTime = 987654321;

// Build the query parameters.  All of these are optional
var queryParameters = new Dictionary<string, object>();
queryParameters.Add("count", 2);
queryParameters.Add("created_gt", queryStartTime);
queryParameters.Add("created_lt", queryEndTime);

// Make the API call
try
{
    var customerEmailLogsResponse = await Customer.GetCustomerEmailLogsAsync(customerEmailAddress, queryParameters);
}
catch (AggregateException exception)
{
    // Exception handling
}
```
### Add customer to a group
#### POST /customers/(:email)/groups/(:group_id)
```csharp
var customerEmailAddress = "customer@example.com";
var groupId = "grp_7zpRYpExEBPpd6dGvyAfcT";
try
{
    var genericApiCallStatus = await Customer.AddCustomerToGroupAsync(customerEmailAddress, groupId);
}
catch (AggregateException exception)
{
    // Exception handling
}
```
### Remove customer from a group
#### DELETE /customers/(:email)/groups/(:group_id)
```csharp
var customerEmailAddress = "customer@example.com";
var groupId = "grp_7zpRYpExEBPpd6dGvyAfcT";
try
{
    var genericApiCallStatus = await Customer.RemoveCustomerFromGroupAsync(DEFAULT_CUSTOMER_EMAIL_ADDRESS, DEFAULT_GROUP_ID);
}
catch (AggregateException exception)
{
    // Exception handling
}
```
## Conversions
*WARNING: This API is currently in beta.*
### Add conversion to customer
This will add a new conversion to a specific customer in your sendwithus account
#### POST /customers/[EMAIL_ADDRESS]/conversions
Add conversion without any parameters:
```csharp
var customerEmailAddress = "customer@example.com";

// Build the conversion object
var conversion = new Conversion();

// Make the API call
try
{
    var genericApiCallStatus = await conversion.AddAsync(customerEmailAddress);
}
catch (AggregateException exception)
{
    // Exception handling
}
```
Add conversion with all parameters (revenue and timestamp):
```csharp
var customerEmailAddress = "customer@example.com";
var revenue = 1999;
var timestamp = 1417321700;

// Build the conversion object
var conversion = new Conversion(revenue, timestamp);

// Make the API call
try
{
    var response = await conversion.AddAsync(DEFAULT_EMAIL_ADDRESS);
}
catch (AggregateException exception)
{
    // Exception handling
}
```
## Drip Campaigns
### Activate campaign for a customer
This will add the specified customer to the first step of the specified drip campaign. If the first step has a delay on it, then it will send the first email once that delay has elapsed.
#### POST /drip_campaigns/(drip_campaign_id)/activate
```csharp
var dripCampaignId = "dc_VXKGx85NmwHnRv9FZv88TW";

// Build the drip campaign object
var recipient = new EmailRecipient("user@email.com", "John");  // The email name is optional
var dripCampaign = new DripCampaign(recipient);
dripCampaign.cc.Add(new EmailRecipient("cc_one@email.com", "Suzy Smith")); // Optional
dripCampaign.cc.Add(new EmailRecipient("cc_two@email.com", "Joe")); // Optional
dripCampaign.bcc.Add(new EmailRecipient("bcc_one@email.com", "Fake Name")); // Optional
dripCampaign.bcc.Add(new EmailRecipient("bcc_one@email.com", "Matt Damon")); // Optional
dripCampaign.sender.address = "company@company.com"; // Optional
dripCampaign.sender.name = "Company"; // Optional
dripCampaign.sender.reply_to = "info@company.com"; // Optional
dripCampaign.tags.Add("tag1"); // Optional
dripCampaign.tags.Add("tag2"); // Optional
dripCampaign.tags.Add("tag3"); // Optional
dripCampaign.locale = "en-US"; // Optional
dripCampaign.esp_account = "esp_1a2b3c4d5e"; // Optional
dripCampaign.email_data.Add("amount", "$12.00"); // Optional

// Make the API call
try
{ 
    var response = await dripCampaign.ActivateAsync(dripCampaignId);
}
catch (AggregateException exception)
{
    // Exception handling
}
```
### Deactivate a campaign for customer
#### POST /drip_campaigns/(drip_campaign_id)/deactivate
```csharp
var dripCampaignId = "dc_VXKGx85NmwHnRv9FZv88TW";
var customerEmailAddress = "user@email.com";
try
{ 
    var dripCampaignResponse = await DripCampaign.DeactivateAsync(dripCampaignId, customerEmailAddress);
}
catch (AggregateException exception)
{
    // Exception handling
}
```
### Deactivate a customer from all campaigns
If a user unsubscribes, changes email addresses, or cancels, call this endpoint to remove the specified email address from all active drip campaigns.
#### POST /drip_campaigns/deactivate
```csharp
var customerEmailAddress = "user@email.com";
try
{ 
    var dripCampaignDeactivateAllResponse = await DripCampaign.DeactivateFromAllCampaignsAsync(customerEmailAddress);
}
catch (AggregateException exception)
{
    // Exception handling
}
```
### Get a list of campaigns
#### GET /drip_campaigns
```csharp
try
{ 
    var dripCampaignDetails = await DripCampaign.GetDripCampaignsAsync();
}
catch (AggregateException exception)
{
    // Exception handling
}
```
### Get the details on a specific drip campaign
#### GET /drip_campaigns/(drip_campaign_id)
```csharp
var dripCampaignId = "dc_VXKGx85NmwHnRv9FZv88TW";
try
{ 
    var dripCampaignDetails = await DripCampaign.GetDripCampaignAsync(dripCampaignId);
}
catch (AggregateException exception)
{
    // Exception handling
}
```
## Segments
### List all segments
#### GET /segments
```csharp
try
{ 
    var segments = await Segment.GetSegmentsAsync();
}
catch (AggregateException exception)
{
    // Exception handling
}
```
### Send to a segment
#### POST /segments/(:segment_id)/send
```csharp
// Build the segment content
var segmentContent = new SegmentContent(DEFAULT_TEMPLATE_ID);

// Build and add the email data (optional)
var contentItemOne = new Dictionary<string, string>();
contentItemOne.Add("url", "http://www.example.com/1");
contentItemOne.Add("image", "http://www.example.com/image1.jpg");
contentItemOne.Add("text", "Check this sweet thing out!");
var contentItemTwo = new Dictionary<string, string>();
contentItemTwo.Add("url", "http://www.example.com/2");
contentItemTwo.Add("image", "http://www.example.com/image2.jpg");
contentItemTwo.Add("text", "Check this other sweet thing out!");
var contentList = new List<Dictionary<string, string>>();
contentList.Add(contentItemOne);
contentList.Add(contentItemTwo);
segmentContent.email_data.Add("Weekly_Newsletter", contentList);

// Add the ESP account (optional)
segmentContent.esp_account = DEFAULT_ESP_ACCOUNT_ID;

// Make the API call
try
{ 
    var genericApiCallStatus = await Segment.SendToSegmentAsync(DEFAULT_SEGMENT_ID, segmentContent);
}
catch (AggregateException exception)
{
    // Exception handling
}
```
## Customer Groups
### Get all customer groups
#### GET /groups
```csharp
try
{ 
    var customerGroupResponseMultipleGroups = await CustomerGroup.GetCustomeGroupsAsync();
}
catch (AggregateException exception)
{
    // Exception handling
}
```
### Create a customer group
#### POST /groups
```csharp
var groupName = "New Group";
try
{ 
    var customerGroupResponse = await CustomerGroup.CreateCustomerGroupAsync(groupName);
}
catch (AggregateException exception)
{
    // Exception handling
}
```
### Update a customer group
#### PUT /groups/(:group_id)
Update a customer group name:
```csharp
var groupId = "grp_7zpRYpExEBPpd6dGvyAfcT";
var groupName = "New Group";
try
{ 
    var customerGroupResponse = await CustomerGroup.UpdateCustomerGroupNameAsync(groupId, groupName);
}
catch (AggregateException exception)
{
    // Exception handling
}
```
Update a customer group description:
```csharp
var groupId = "grp_7zpRYpExEBPpd6dGvyAfcT";
var groupDescription = "New group description";
try
{ 
    var customerGroupResponse = await CustomerGroup.UpdateCustomerGroupDescriptionAsync(groupId, groupDescription);
}
catch (AggregateException exception)
{
    // Exception handling
}
```
Update a customer group name and description:
```csharp
var groupId = "grp_7zpRYpExEBPpd6dGvyAfcT";
var groupName = "New Group";
var groupDescription = "New group description";
try
{ 
    var customerGroupResponse = await CustomerGroup.UpdateCustomerGroupNameAndDescriptionAsync(groupId, groupName, groupDescription);
}
catch (AggregateException exception)
{
    // Exception handling
}
```
### Delete a customer group
#### DELETE /groups/(:group_id)
```csharp
var groupId = "grp_7zpRYpExEBPpd6dGvyAfcT";
try
{ 
    var genericApiCallStatus = await CustomerGroup.DeleteCustomerGroupAsync(groupId);
}
catch (AggregateException exception)
{
    // Exception handling
}
```
## Batch API Calls
The sendwithus batch endpoint enables multiple API calls to be made in a single HTTP request.
*NOTE* – Batch sizes over 10 requests are not recommended.
* By default, batch API calls are limited to 10 per batch.
* Any additional calls beyond 10 will throw an InvalidOperationException.
* This limit can be increased by calling BatchApiRequest.OverrideMaximumBatchRequests(int newMaximum)

The process to making a batch API call is:

1. Call BatchApiRequest.StartNewBatchRequest()
  * After making this call, all subsequent API calls will queued up in a new batch request.
2. Make all the API calls as you normally would.  Instead of being sent, these calls will be queued.
3. Call BatchApiRequest.SendBatchApiRequest().  This will send the batch request with all of the queued requests, clear the queue, return the response (which will contain the response for each API call in the queue), and exit batch mode.
  * After this call, all subsequent API calls will be sent out as soon as they are called, instead of being queued.
4. Access the response to each batch command by calling 

Additional Features:
* PauseBatchRequest() and ResumeBatchRequest(): Respectively exist and then re-enter batch mode without clearing the list of batched calls.  Allows other API calls to be sent immediately (not batched) while building a list of batched API calls.
* AbortBatchRequest(): Allows a batch API calls can be aborted without sending any of the batched calls.  This will exit batch mode and clear the list of batched API calls.


### Send batch request
#### POST /batch
Example using 5 commands:
```csharp
// Start the batch request
BatchApiRequest.StartNewBatchRequest();

// Make the API calls to be batched
var espAccountId = "esp_e3ut7pFtWttcN4HNoQ8Vgm";
var customerEmailAddress = "customer@example.com";
try
{
    // Discard the response to the API calls as it will just be an empty object (since the requests aren't actually sent yet)
    await Snippet.GetSnippetsAsync();
    await Log.GetLogsAsync();
    await EspAccount.SetDefaultEspAccountAsync(espAccountId);
    await DripCampaign.GetDripCampaignsAsync();
    await Customer.DeleteCustomerAsync(customerEmailAddress);

    // Make the batch API Request
    var batchResponses = await BatchApiRequest.SendBatchApiRequest();

    // Get the response to the individual API calls
    var snippets = response[0].GetBody<List<Snippet>>();
    var logs = response[1].GetBody<List<Log>>();
    var espAccountResponse = response[2].GetBody<EspAccountResponse>();
    var dripCampaignDetails = response[3].GetBody<List<DripCampaignDetails>>();
    var genericApiCallStatus = response[4].GetBody<GenericApiCallStatus>();
}
catch (AggregateException exception)
{
    // Exception handling
}
catch (InvalidOperationException exception)
{
    // Exception handling
}
```
Example sending 11 commands without overriding the limit (to show when the exception is thrown):
```csharp
var espAccountId = "esp_e3ut7pFtWttcN4HNoQ8Vgm";
var customerEmailAddress = "customer@example.com";

// Start the batch request
BatchApiRequest.StartNewBatchRequest();

// Make the API calls to be batched
try
{
    // Make the first 10 API calls
    // Discard the response to the API calls as it will just be an empty object (since the requests aren't actually sent yet)
    await Snippet.GetSnippetsAsync();
    await Log.GetLogsAsync();
    await EspAccount.SetDefaultEspAccountAsync(espAccountId);
    await DripCampaign.GetDripCampaignsAsync();
    await Customer.DeleteCustomerAsync(customerEmailAddress);
    await Snippet.GetSnippetsAsync();
    await Log.GetLogsAsync();
    await EspAccount.SetDefaultEspAccountAsync(espAccountId);
    await DripCampaign.GetDripCampaignsAsync();
    await Customer.DeleteCustomerAsync(customerEmailAddress);
    
    // Make the 11th API call.  This is when the InvalidOperationException will be thrown
    await Snippet.GetSnippetsAsync();

    // Make the batch API Request.  This won't be reached.
    var batchResponses = await BatchApiRequest.SendBatchApiRequest();
    
    // Handle the responses
}
catch (AggregateException exception)
{
    // Exception handling
}
catch (InvalidOperationException exception)
{
    // Exception handling
}
```
Example sending 12 commands after overriding the limit:
```csharp
var espAccountId = "esp_e3ut7pFtWttcN4HNoQ8Vgm";
var customerEmailAddress = "customer@example.com";

// Start the batch request
BatchApiRequest.StartNewBatchRequest();

// Override the maximum number of API calls that can be included in this batch
BatchApiRequest.OverrideMaximumBatchRequests(12);

// Make the API calls to be batched
try
{
    // Make the first 10 API calls
    // Discard the response to the API calls as it will just be an empty object (since the requests aren't actually sent yet)
    await Snippet.GetSnippetsAsync();
    await Log.GetLogsAsync();
    await EspAccount.SetDefaultEspAccountAsync(espAccountId);
    await DripCampaign.GetDripCampaignsAsync();
    await Customer.DeleteCustomerAsync(customerEmailAddress);
    await Snippet.GetSnippetsAsync();
    await Log.GetLogsAsync();
    await EspAccount.SetDefaultEspAccountAsync(espAccountId);
    await DripCampaign.GetDripCampaignsAsync();
    await Customer.DeleteCustomerAsync(customerEmailAddress);
    
    // Make the 11th and 12th API calls.  This is when the InvalidOperationException will be thrown
    await Snippet.GetSnippetsAsync();
    await Log.GetLogsAsync();

    // Make the batch API Request.
    var batchResponses = await BatchApiRequest.SendBatchApiRequest();
    
    // Handle the responses
}
catch (AggregateException exception)
{
    // Exception handling
}
catch (InvalidOperationException exception)
{
    // Exception handling
}
finally
{
    // Return the max batch request limit to its default value (optional)
    BatchApiRequest.SetMaximumBatchRequestsToDefault();
}
```
Example pausing and resuming a batch request
```csharp
// Start the batch request
BatchApiRequest.StartNewBatchRequest();

try
{
    // Make the API call to be batched
    await Template.GetTemplatesAsync();

    // Pause the batch request
    BatchApiRequest.PauseBatchRequest();

    // Make another API call which will be sent immediately
    var snippets = await Snippet.GetSnippetsAsync();

    // Resume the batch request and add another API call to it
    BatchApiRequest.ResumeBatchRequest();
    await Segment.GetSegmentsAsync();

    // Make the final batch request, containing the Get Templates and Get Segments API calls
    var batchResponses = await BatchApiRequest.SendBatchApiRequest();
}
catch (AggregateException exception)
{
    Assert.Fail(exception.ToString());
}
catch (InvalidOperationException exception)
{
    // Exception handling
}
```
Example aborting a batch request
```csharp
// Start the batch request
BatchApiRequest.StartNewBatchRequest();

try
{
    // Make the API call to be batched
    await Template.GetTemplatesAsync();

    // Abort the batch request
    BatchApiRequest.AbortBatchRequest();

    // Make another API call which will be sent immediately
    var snippets = await Snippet.GetSnippetsAsync();

    // For demonstration, make the aborted batch API Reqeust anyways
    // This will be an empty request as the list of batched API calls was cleared by the Abort call
    var batchResponses = await BatchApiRequest.SendBatchApiRequest();
}
catch (AggregateException exception)
{
    Assert.Fail(exception.ToString());
}
catch (InvalidOperationException exception)
{
    // Exception handling
}
```

## Managing the API
### Versioning
The build version is set by the Sendwithus_csharp/appveyor.yml file.
It is set so that the 3rd digit in the build number is automatically incremented whenever a new build is made.
To change the major or minor build revision, simply edit the line at the top of the file accordingly:
```
# version format
version: 1.0.{build}
```
The way the version settings for the final package are set is:
* "version" variable set in appveyor.yml file, as shown above
* appveyor.yml sets the C# project's "assembly_version", "assembly_file_version", and "assembly_informational_version" to this "version"
* the sendwithus_csharp/Sendwithus/SendwithusClient.nuspec sets the NuGet version to the assembly version
### Continuous Integration
This project uses AppVeyor for its CI.  AppVeyor is configured to automatically run all of the unit tests whenever a new commit is pushed to GitHub, regardless of the branch.
All of the AppVeyor settings are configured in the Sendwithus_csharp/appveyor.yml file.
All unused settings are simply commented out, so all available settings can be identified in that file.
### Continuous Deployment
The appveyor.yml file is configured to automatically deploy new commits to NuGet.
It is configured so that only new commits on the master branch will be deployed.  Therefore, please develop all features on a feature branch and only integrate with master when ready for deployment.
This setting can be changed under the "build:" settings in the appveyor.yml file.
It is also possible to manually build and deploy the solution from www.appveyor.com.

Settings for the NuGet deployment can be edited in the sendwithus_csharp/Sendwithus/SendwithusClient.nuspec file.
## CLS Compliance - Using this Client in Other .NET Languages
The project is CLS compliant, so the package can be used in any .NET language that supports CLS compliant packages.
For example, this includes VB.NET and might also include F# and Visual C++.  The lattter two haven't been tested, but support for VB.NET has been.
To add the package to a solution in a different language, simply follow the same procedure that you would for C#:
* In the Solution Explorer window, right click on your solution and select "Manage NuGet Packages for Solution..."
* Search for "SendwithusClient"
  * Be sure to use the one that's by sendwithus.  There's another one called SendWithUs.Client by Mimeo, but that is not supported by sendwithus and might not be CLS compliant.
* Select the "SendwithusClient" package and choose "Install" for your solution/project.

## Tests

### Running Unit Tests

The tests are run using Visual Studio's standard unit-testing libaries and built in test runner.  Simply select "Test->Run->All Tests" to run the unit tests.

## Response Ranges

Sendwithus' API typically sends responses back in these ranges:

-   2xx – Successful Request
-   4xx – Failed Request (Client error)
-   5xx – Failed Request (Server error)

If you're receiving an error in the 400 response range follow these steps:

-   Double check the data and ID's getting passed to sendwithus
-   Ensure your API key is correct
-   Make sure there's no extraneous spaces in the id's getting passed

## Gmail Delivery Issues

Sendwithus recommends using one of our [supported ESPs](https://support.sendwithus.com/esp_accounts/esp_compatability/ "ESP Compatibility Chart"). For some hints on getting Gmail working check [here](https://support.sendwithus.com/esp_accounts/can_i_use_gmail/ "Can I use Gmail?").
