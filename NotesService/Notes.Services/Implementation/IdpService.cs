using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using Notes.Services.Interfaces;
using Notes.Services.Model;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Notes.Services.Implementation
{
    /// <summary>
    ///  To handle IDP Management API calls
    /// </summary>
    public class IdpService : IIdpService
    {
        private readonly IdpServiceConfiguration configuration;

        public IdpService(IOptions<IdpServiceConfiguration> option)
        {
            this.configuration = option.Value;
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
            var client = new RestClient($"{configuration.UserServiceBaseUrl}oauth/token");
            client.UseNewtonsoftJson(new Newtonsoft.Json.JsonSerializerSettings() { ContractResolver = new DefaultContractResolver() { NamingStrategy = new CamelCaseNamingStrategy() } });
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", $"grant_type=client_credentials&client_id={configuration.UserServiceClientId}&client_secret={configuration.UserServiceClientSecret}&audience={configuration.UserServiceAudience}", ParameterType.RequestBody);
            var response = await client.ExecuteAsync<AccessToken>(request);
            if(response.IsSuccessful)
            {
                return response.Data;
            }

            throw new IdpWebException(response.StatusCode, response.Content);
        }

        private async Task<IdpResponse> SearchEmail(string email,AccessToken token)
        {
            var searchClient = new RestClient($"{configuration.UserServiceBaseUrl}api/v2/users-by-email?email={email}");
            searchClient.UseNewtonsoftJson(new Newtonsoft.Json.JsonSerializerSettings() { ContractResolver = new DefaultContractResolver() { NamingStrategy = new CamelCaseNamingStrategy() } });
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", $"Bearer {token.Access_token}");
            var response = await searchClient.ExecuteAsync<List<IdpResponse>>(request);
            if(response.IsSuccessful)
            {
                if (response.Data.Count > 0)
                    return response.Data[0];
                else
                    return null;
            }
            throw new IdpWebException(response.StatusCode, response.Content);
        }
        /// <summary>
        ///  Create user in IDP
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<IdpResponse> CreateUser(User user)
        {
            var idpUser = CreateIdpUser(user);
            var token = await GetAccessTokenAsync();

            //Check if email already exists in the IDP
            var result = await SearchEmail(user.Email,token);
            if(result == null)
            {
                var client = new RestClient($"{configuration.UserServiceBaseUrl}api/v2/users");
                client.UseNewtonsoftJson(new Newtonsoft.Json.JsonSerializerSettings() { ContractResolver = new DefaultContractResolver() { NamingStrategy = new CamelCaseNamingStrategy() } });
                var request = new RestRequest(Method.POST);
                request.AddHeader("Authorization", $"Bearer {token.Access_token}");
                request.AddJsonBody(idpUser);
                var response = await client.ExecuteAsync<IdpResponse>(request);

                if (response.IsSuccessful)
                {
                    return response.Data;
                }

                throw new IdpWebException(response.StatusCode, response.Content);
            }
            else
            {
                var valException = new ValidationException($"{user.Email} already registered.");
                valException.MetaData = result;
                throw valException;

            }

            

            
            
        }
    }
}
