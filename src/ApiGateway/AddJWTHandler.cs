using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using IdentityModel.Client;
using Serilog;

namespace Reshape.ApiGateway
{
    public class AddJWTHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Set the client details and the idsvc token endpoint address
            TokenClientOptions tokenOptions = new TokenClientOptions
            {
                ClientId = "rshp.gateway",
                ClientSecret = "secret", // for development purposes. Use proper secret in prod.
                Address = "http://identity.svc/connect/token"
            };

            var client = new TokenClient(new HttpClient(), tokenOptions);

            // Get currently attached token (reference) from downstream-bound request
            var accessToken = request.Headers.Authorization.Parameter;
            Log.Debug("Exchanging reference token to JWT - reference token: {0} ", accessToken);

            // Set desired scope and attach current reference token
            var opt = new Dictionary<string, string>
            {
                { "scope", "bm acc" },
                { "token", accessToken }
            };

            // Send request for token using custom grant type
            var tokenResponse = await client.RequestTokenAsync("exchange_reference_token", opt);
            Log.Debug("Token exchange result: {0}", tokenResponse?.AccessToken);

            // Modify request with new JWT token if it returned successfully
            if (!tokenResponse.IsError)
                request.SetBearerToken(tokenResponse.AccessToken);
            else
            {
                Log.Error("RequestTokenAsync() Failed! {0}, {1} \t Exception: {2}", tokenResponse.Error, tokenResponse.ErrorDescription, tokenResponse.Exception.ToString());
            }

            Log.Debug("Final token result: {0}", request.Headers.Authorization.Parameter);
            return await base.SendAsync(request, cancellationToken);
        }
    }
}