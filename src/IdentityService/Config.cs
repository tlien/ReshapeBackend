using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using IdentityServer4.Models;
using IdentityModel;

namespace Reshape.IdentityService
{
    /// <summary>
    /// Holds static configuration data defininig Clients; API and Identity Resources.
    /// </summary>
    public static class Config
    {
        /// <summary>
        /// These represent data about the user that a client can request and use for validation purposes.
        /// </summary>
        public static IEnumerable<IdentityResource> Ids =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                CustomClaims.Role,
            };

        /// <summary>
        /// These represent data about by the various APIs and what user data they require to do authorization.
        /// </summary>
        /// <param name="conf">Configuration holding the API secrets.</param>
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

        /// <summary>
        /// These represent data about the clients that a user might try to get authenticated with.
        /// It also defines what types of user data a client can access in order to authorize the users for access to its various API endpoints.
        /// This is also where the login/logout redirect urls, requested token type, lifetime of the tokens etc. are defined.
        /// </summary>
        /// <param name="conf">Configuration holding the client secrets and client base urls.</param>
        public static IEnumerable<Client> Clients(IConfigurationSection conf)
        {
            var frontendUrl = conf["FrontendUrl"];
            var apigwUrl = conf["ApigwUrl"];
            var bmUrl = conf["BmUrl"];
            var accUrl = conf["AccUrl"];

            return new Client[]
            {
                #region Applications
                // This is the frontend for the system that a user would use to interact with the system.
                // It is setup to use the 'authorization code' flow and deals out reference tokens in order to not expose more user data to the frontend than absolutely required.
                new Client
                {
                    ClientName = "Reshape Frontend",
                    ClientId = "rshp.frontend",

                    RequireClientSecret = false, // The oidc-client does not support client secrets.

                    RequireConsent = false, // Requiring user consent makes no sense in this case. The system is for professional use and stores no user data beyond a name, email, phone, (address is currenly added only for show).

                    // https://identityserver4.readthedocs.io/en/latest/topics/grant_types.html#grant-types
                    AllowedGrantTypes = GrantTypes.Code, // Use 'Authorization code' flow
                    RequirePkce = true, // Use Pkce code challenge to mitigate code substition attacks (check link above for more info)

                    // Specifies the allowed URIs to return tokens or authorization codes to
                    RedirectUris = { frontendUrl, $"{frontendUrl}/callback.html", $"{frontendUrl}/silent-renew.html" },
                    PostLogoutRedirectUris = { $"{frontendUrl}/", $"{frontendUrl}" },

                    AllowedCorsOrigins = { $"{frontendUrl}" }, // Once CORS is properly setup up, this will ensure the client can still connect from the docker hostname.

                    // AlwaysIncludeUserClaimsInIdToken = true, // This only here to show the user info in the frontend while developing.

                    // https://identityserver4.readthedocs.io/en/latest/topics/refresh_tokens.html#refresh-tokens
                    AllowOfflineAccess = true, // Use Refresh tokens if setup on the frontend (whether this should be enabled or not is still up in the air and will be determined by Reshape eventually)
                    AccessTokenType = AccessTokenType.Reference, // Use a reference token which is just a string that can be exchanged for the actual access token data when called for using the introspection endpoint of the identity server.

                    AllowedScopes = { "openid", "profile", "role", "gateway"}, // The scopes that this client has access to, these should not include the actual APIs, only the gateway.
                },
                #endregion

                #region Middletier API
                // The API gateway is currently set up to act as a middle tier API kinda like in this example https://identityserver4.readthedocs.io/en/latest/topics/extension_grants.html
                // It is setup to translate a reference token from the frontend user to a JWT token that can in theory be validated internally by each downstream microservice (this is currently not set up).
                // This serves to eliminate introspection calls to the identity server from the various microservices, as these are not an exposed attack surface (this is the case for production, it makes no sense for development).
                new Client
                {
                    ClientName = "Reshape API Gateway",
                    ClientId = "rshp.gateway",
                    ClientSecrets = { new Secret("secret".Sha256()) },

                    AllowedGrantTypes = { "exchange_reference_token" }, // The custom grant type allowing the gateway to exchange a reference token to a JWT token.

                    AllowedScopes = { "openid", "profile", "role", CustomClaims.Account.Name, "acc", "bm" } // The middletier has access to all scopes.
                },
                #endregion

                #region Swagger Clients
                // The swagger UI is technically a client since it presents a simple frontend for calling the API it is associated with.
                // These will be disabled in production.
                new Client
                {
                    ClientName = "Reshape Account API Swagger",
                    ClientId = "rshp.acc.swagger",
                    ClientSecrets = { new Secret("!s3cr3t".Sha256()) },

                    RequireConsent = false,
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,

                    RedirectUris = { accUrl, $"{accUrl}/swagger/oauth2-redirect.html", $"{apigwUrl}/swagger/oauth2-redirect.html" },
                    PostLogoutRedirectUris = { $"{accUrl}/swagger" },
                    AllowedCorsOrigins = { accUrl },

                    AccessTokenType = AccessTokenType.Jwt, // This token type was the easiest to set up with swagger.

                    AllowedScopes = { "openid", "profile", "role", CustomClaims.Account.Name, "acc" }
                },
                new Client
                {
                    ClientName = "Reshape Business Management API Swagger",
                    ClientId = "rshp.bm.swagger",
                    ClientSecrets = { new Secret("!s3cr3t".Sha256()) },

                    RequireConsent = false,
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,

                    RedirectUris = { bmUrl, $"{bmUrl}/swagger/oauth2-redirect.html", $"{apigwUrl}/swagger/oauth2-redirect.html" },
                    PostLogoutRedirectUris = { $"{bmUrl}/swagger" },
                    AllowedCorsOrigins = { bmUrl },

                    AccessTokenType = AccessTokenType.Jwt,

                    AllowedScopes = { "openid", "profile", "role", "bm" }
                }
                #endregion
            };
        }
    }

    /// <summary>
    /// Custom claims that give access to resources we define such as the account id of a user.
    /// These are then added to the actual user (see the TestUsers class).
    /// </summary>
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

        // public static IdentityResource Feature =>
        //     new IdentityResource(
        //         name: "features",
        //         displayName: "Account Features",
        //         claimTypes: new[] { "features" }
        //     );

        // public static IdentityResource BusinessTier =>
        //     new IdentityResource(
        //         name: "businesstier",
        //         displayName: "Account Business Tier",
        //         claimTypes: new[] { "businesstier" }
        //     );
    }
}
