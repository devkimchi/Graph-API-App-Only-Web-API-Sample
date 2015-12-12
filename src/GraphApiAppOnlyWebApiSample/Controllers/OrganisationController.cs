using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using GraphApiAppOnlyWebApiSample.Models;

using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.OptionsModel;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace GraphApiAppOnlyWebApiSample.Controllers
{
    /// <summary>
    /// This represents a controller entity for organisation.
    /// </summary>
    [Route("api/[controller]")]
    public class OrganisationController : Controller
    {
        private const string Organisation = "organization";

        private readonly GraphApp _graphApp;

        private AuthenticationContext _authContext;
        private ClientCredential _credential;
        private string _graphUrl;
        private string _version;

        private JsonSerializerSettings _settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrganisationController"/> class.
        /// </summary>
        public OrganisationController(IOptions<GraphApp> graphApp)
        {
            this._graphApp = graphApp.Value;
            this.Init();
        }

        /// <summary>
        /// Gets the organisation details.
        /// </summary>
        /// <returns>Returns the organisation details.</returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            AuthenticationResult authResult;
            try
            {
                authResult = await this._authContext.AcquireTokenAsync(this._graphUrl, this._credential);
            }
            catch (Exception ex)
            {
                var result = new JsonResult(ex);
                return result;
            }

            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authResult.AccessToken);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var url = $"{this._graphUrl}/{this._version}/{Organisation}";

                    var resultAsString = await client.GetStringAsync(url);

                    var organisation = JsonConvert.DeserializeObject<Organisation>(resultAsString, this._settings);
                    var result = new JsonResult(organisation);
                    return result;
                }
            }
            catch (Exception ex)
            {
                var result = new JsonResult(ex);
                return result;
            }
        }

        private void Init()
        {
            var tenant = this._graphApp.Tenant;
            var authUrl = this._graphApp.AuthUrl;
            var authority = string.Format(authUrl, tenant);

            this._authContext = new AuthenticationContext(authority);

            var clientId = this._graphApp.ClientId;
            var clientSecret = this._graphApp.ClientSecret;

            this._credential = new ClientCredential(clientId, clientSecret);

            this._graphUrl = this._graphApp.GraphUrl;
            this._version = this._graphApp.Version;

            this._settings = new JsonSerializerSettings
                                 {
                                     ContractResolver = new CamelCasePropertyNamesContractResolver(),
                                     NullValueHandling = NullValueHandling.Ignore,
                                     MissingMemberHandling = MissingMemberHandling.Ignore
                                 };
        }
    }
}