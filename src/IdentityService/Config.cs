using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using IdentityServer4.Models;

namespace Reshape.IdentityService
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> Ids =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
            };

        public static IEnumerable<ApiResource> Apis(IConfigurationSection conf) =>
            new ApiResource[]
            {
                new ApiResource("acc", "Account Service API"),
                new ApiResource("bm", "Business Management Service API")
            };

        public static IEnumerable<Client> Clients(IConfigurationSection conf)
        {
            var frontendUrl = conf["FrontendUrl"];
            return new Client[]
            {
                // Reshape frontend
                new Client
                {
                    ClientName = "Reshape Frontend",
                    ClientId = "ReshapeFrontend",

                    RequireConsent = false,

                    // https://identityserver4.readthedocs.io/en/latest/topics/grant_types.html#grant-types
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    // ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },

                    // Specifies the allowed URIs to return tokens or authorization codes to
                    RedirectUris = { frontendUrl, $"{frontendUrl}/callback.html", $"{frontendUrl}/silent-renew.html" },
                    PostLogoutRedirectUris = { $"{frontendUrl}/", $"{frontendUrl}" },
                    AllowedCorsOrigins = { $"{frontendUrl}" },

                    AlwaysIncludeUserClaimsInIdToken = true,

                    // https://identityserver4.readthedocs.io/en/latest/topics/refresh_tokens.html#refresh-tokens
                    AllowOfflineAccess = true, // allow refresh tokens
                    AccessTokenType = AccessTokenType.Reference, // set token type to reference

                    AllowedScopes = { "openid", "profile", "acc", "bm" },
                }
            };
        }
    }
}
