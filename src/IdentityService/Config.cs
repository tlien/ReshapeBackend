using System.Collections.Generic;
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

        public static IEnumerable<ApiResource> Apis =>
            new ApiResource[]
            {
                new ApiResource("acc", "Account Service API"),
                new ApiResource("bm", "Business Management Service API")
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                // Reshape frontend
                new Client
                {
                    ClientId = "ReshapeFrontend",
                    ClientName = "Reshape Frontend",

                    RequireConsent = false,

                    // https://identityserver4.readthedocs.io/en/latest/topics/grant_types.html#grant-types
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    // RequireClientSecret = false,
                    // ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },

                    // Specifies the allowed URIs to return tokens or authorization codes to
                    // RedirectUris = { "<FRONTEND_URL>, <GATEWAY_URL>, <OTHER???>" },

                    // FrontChannelLogoutUri = "http://localhost:5003/signout-oidc",
                    // PostLogoutRedirectUris = { "http://localhost:5003/signout-callback-oidc" },

                    AlwaysIncludeUserClaimsInIdToken = true,

                    // https://identityserver4.readthedocs.io/en/latest/topics/refresh_tokens.html#refresh-tokens
                    AllowOfflineAccess = true, // allow refresh tokens
                    AccessTokenType = AccessTokenType.Reference, // set token type to reference

                    AllowedScopes = { "openId", "profile", "acc", "bm" },
                }
            };
    }
}
