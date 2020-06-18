using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using IdentityServer4.Models;
using IdentityModel;

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
                CustomClaims.Role,
            };

        public static IEnumerable<ApiResource> Apis(IConfigurationSection conf) =>
            new ApiResource[]
            {
                new ApiResource("acc", "Account Service API")
                {
                    ApiSecrets =
                    {
                        new Secret("!s3cr3t".Sha256())
                    },
                    UserClaims =
                    {
                        JwtClaimTypes.Name,
                        JwtClaimTypes.Email,
                        JwtClaimTypes.Role,
                        CustomClaims.Account.Name,
                        CustomClaims.Feature.Name,
                        CustomClaims.BusinessTier.Name,
                    },
                },
                new ApiResource("bm", "Business Management Service API")
                {
                    ApiSecrets =
                    {
                        new Secret("s3cr3t".Sha256())
                    },
                    UserClaims =
                    {
                        JwtClaimTypes.Role
                    },
                },
                new ApiResource("gateway", "Api gateway" )
                {
                    ApiSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                }
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

                    AllowedScopes = { "openid", "profile", "role", "acc", "bm", "gateway"},
                },
                new Client
                {
                    ClientName = "Reshape API Gateway",
                    ClientId = "rshp.gateway",
                    ClientSecrets = { new Secret("secret".Sha256()) },

                    AllowedGrantTypes = { "exchange_reference_token" },

                    AllowedScopes = {"openid", "profile", "role", "acc", "bm",}
                }
            };
        }
    }

    public static class CustomClaims
    {
        public static IdentityResource Role =>
            new IdentityResource(
                name: "role",
                displayName: "User Role",
                claimTypes: new[] { JwtClaimTypes.Role }
            );

        public static IdentityResource Account =>
            new IdentityResource(
                    name: "account",
                    displayName: "Company Account",
                    claimTypes: new[] { "account" }
                );

        public static IdentityResource Feature =>
            new IdentityResource(
                    name: "features",
                    displayName: "Account Features",
                    claimTypes: new[] { "features" }
                );

        public static IdentityResource BusinessTier =>
            new IdentityResource(
                    name: "businesstier",
                    displayName: "Account Business Tier",
                    claimTypes: new[] { "businesstier" }
                );
    }
}
