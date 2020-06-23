using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using IdentityModel.Client;

namespace Reshape.ApiGateway
{
    /// <summary>
    /// Handler for exchanging a reference token to a JWT token specced for the downstream client.
    /// If token can't be exchanged, the original reference token will be sent downstream, likely causing an authentication failure.
    ///
    /// Currently there isn't any caching behaviour implemented, so the JWT token will be refetched for each request.
    /// The reason to use JWT tokens for downstream services is to loosen dependency on the IdentityServer for getting claims from reference tokens.
    /// Downstream services are only exposed through the API gateway anyway, so arguably there should be no need to authenticate inside each downstream service.
    /// </summary>
    public class AddJWTHandler : DelegatingHandler
    {
        private readonly ILogger _logger;

        public AddJWTHandler(ILogger<AddJWTHandler> logger) : base()
        {
            _logger = logger;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Set the client details and the idsvc token endpoint address
            TokenClientOptions tokenOptions = new TokenClientOptions
            {
                ClientId = "rshp.gateway",
                ClientSecret = "secret", // DEV: for development purposes. Use proper secret in prod.
                Address = "http://identity.svc/connect/token"
            };

            var client = new TokenClient(new HttpClient(), tokenOptions);

            // Get currently attached token (reference) from downstream-bound request
            var accessToken = request.Headers.Authorization.Parameter;
            _logger.LogDebug("Exchanging reference token to JWT - reference token: {0} ", accessToken);

            // Set desired scope and attach current reference token
            var opt = new Dictionary<string, string>
            {
                { "scope", "bm acc" },
                { "token", accessToken }
            };

            // Send request for token using custom grant type
            var tokenResponse = await client.RequestTokenAsync("exchange_reference_token", opt);
            _logger.LogDebug("Token exchange result: {0}", tokenResponse?.AccessToken);

            // Modify request with new JWT token if it returned successfully
            if (!tokenResponse.IsError)
                request.SetBearerToken(tokenResponse.AccessToken);
            else
            {
                _logger.LogError("RequestTokenAsync() Failed! {0}, {1} \t Exception: {2}", tokenResponse.Error, tokenResponse.ErrorDescription, tokenResponse.Exception.ToString());
            }

            _logger.LogDebug("Final token result: {0}", request.Headers.Authorization.Parameter);
            return await base.SendAsync(request, cancellationToken);
        }
    }
}