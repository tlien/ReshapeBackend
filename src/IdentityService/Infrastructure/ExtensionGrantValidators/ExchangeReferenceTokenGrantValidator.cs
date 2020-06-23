using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Validation;

namespace Reshape.IdentityService.Infrastructure
{
    /// <summary>
    /// Handles requests from clients using the special grant for exchanging reference tokens to JWT tokens.
    /// </summary>
    public class ExchangeReferenceTokenGrantValidator : IExtensionGrantValidator
    {
        private readonly ITokenValidator _validator;

        public ExchangeReferenceTokenGrantValidator(ITokenValidator validator)
        {
            _validator = validator;
        }

        public string GrantType => "exchange_reference_token";

        public async Task ValidateAsync(ExtensionGrantValidationContext context)
        {
            // Get current reference token from request
            var userToken = context.Request.Raw.Get("reference_token");

            if (string.IsNullOrEmpty(userToken))
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant);
                return;
            }

            // Validate reference token
            var result = await _validator.ValidateAccessTokenAsync(userToken);
            if (result.IsError)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant);
                return;
            }

            // Generate the JWT as if it was for the reference token's client
            context.Request.Client = result.Client;
            context.Request.AccessTokenType = AccessTokenType.Jwt;

            var sub = result.Claims.FirstOrDefault(c => c.Type == "sub").Value;
            context.Result = new GrantValidationResult(sub, GrantType, result.Claims);
            return;
        }
    }
}