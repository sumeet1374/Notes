using Notes.Services.Interfaces;
using Notes.Services.Model;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using System.Threading.Tasks;

namespace Notes.Services.Implementation
{
    /// <summary>
    ///  To handle IDP Management API calls
    /// </summary>
    public class IdpService : IIdpService
    {
        private readonly IdpServiceConfiguration configuration;

        public IdpService(IdpServiceConfiguration configuration)
        {
            this.configuration = configuration;
        }
        /// <summary>
        ///  Change User object to User expected by IDP 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private IdpUser CreateIdpUser(User user)
        {
            var idpUser = new IdpUser();
            idpUser.Given_name = user.FirstName;
            idpUser.Family_name = user.LastName;
            idpUser.Name = $"{user.FirstName} {user.LastName}";
            idpUser.Email = user.Email;
            idpUser.Password = user.Password;
            return idpUser;
        }
        /// <summary>
        ///  Get Access token
        /// </summary>
        /// <returns></returns>
        private async Task<AccessToken> GetAccessTokenAsync()
        {
            var client = new RestClient($"{configuration.IdpServiceBaseUrl}/oauth/token");
            client.UseNewtonsoftJson();
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", $"grant_type=client_credentials&client_id={configuration.ClientId}&client_secret={configuration.ClientSecret}&audience={configuration.Audience}", ParameterType.RequestBody);
            var response = await client.ExecuteAsync<AccessToken>(request);
            if(response.IsSuccessful)
            {
                return response.Data;
            }

            throw new IdpWebException(response.StatusCode, response.Content);
        }
        /// <summary>
        ///  Create user in IDP
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task CreateUser(User user)
        {
            var idpUser = CreateIdpUser(user);
            var token = await GetAccessTokenAsync();

            var client = new RestClient($"{configuration.IdpServiceBaseUrl}/api/v2/users");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", $"Bearer {token.Access_token}");
            request.AddJsonBody(idpUser);
            var response = await client.ExecuteAsync(request);

            if (!response.IsSuccessful)
                throw new IdpWebException(response.StatusCode, response.Content);
        }
    }
}
