using System.Collections.Generic;
using System.Security.Claims;
using IdentityModel;
using IdentityServer4.Test;

namespace Reshape.IdentityService.Infrastructure
{
    /// <summary>
    /// These are test users. They are absolutely not for production.
    /// In production, users would come from an ASP.NET Identity (or similar) service holding similar information to the test users below albeit in a persistable, secure manner.
    /// </summary>
    public class TestUsers
    {
        public static List<TestUser> Users = new List<TestUser>
        {
            // Represents an account user
            new TestUser{SubjectId = "ccb76871-272c-44d3-ad20-8cf7af2ab4ad", Username = "alice", Password = "alice",
                Claims =
                {
                    new Claim(JwtClaimTypes.Name, "Alice Smith"),
                    new Claim(JwtClaimTypes.GivenName, "Alice"),
                    new Claim(JwtClaimTypes.FamilyName, "Smith"),
                    new Claim(JwtClaimTypes.Email, "AliceSmith@email.com"),
                    new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                    new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
                    new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json),
                    new Claim(JwtClaimTypes.Role, "account_user"),
                    new Claim("account", "74c20cbc-9e0c-4cef-8325-27b8a26a64b1"), // This is the account domain Account aggregate id to which this user belongs.
                }
            },
            // Represents an account admin.
            new TestUser{SubjectId = "1dd7832f-5884-43b3-a080-e2578c0993f3", Username = "bob", Password = "bob",
                Claims =
                {
                    new Claim(JwtClaimTypes.Name, "Bob Smith"),
                    new Claim(JwtClaimTypes.GivenName, "Bob"),
                    new Claim(JwtClaimTypes.FamilyName, "Smith"),
                    new Claim(JwtClaimTypes.Email, "BobSmith@email.com"),
                    new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                    new Claim(JwtClaimTypes.WebSite, "http://bob.com"),
                    new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json),
                    new Claim(JwtClaimTypes.Role, "account_admin"),
                    new Claim("account", "74c20cbc-9e0c-4cef-8325-27b8a26a64b1"), // This is the account domain Account aggregate id to which this user belongs.
                }
            },
            // Represents a different account user
            new TestUser{SubjectId = "759201e5-a5e4-4585-8967-d8061bdc63c7", Username = "smol", Password = "smol",
                Claims =
                {
                    new Claim(JwtClaimTypes.Name, "Smol Uanu"),
                    new Claim(JwtClaimTypes.GivenName, "Smol"),
                    new Claim(JwtClaimTypes.FamilyName, "Uanu"),
                    new Claim(JwtClaimTypes.Email, "Smol@email.com"),
                    new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                    new Claim(JwtClaimTypes.WebSite, "https://www.bay12games.com/dwarves/"),
                    new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json),
                    new Claim(JwtClaimTypes.Role, "account_user"),
                    new Claim("account", "bec823e4-aced-4b92-9442-70c2f32c65f9"), // This is the account domain Account aggregate id to which this user belongs.
                }
            },
            // Represents a different account admin.
            new TestUser{SubjectId = "3d5d4906-019a-4de8-b89b-d8940fa1d104", Username = "don", Password = "don",
                Claims =
                {
                    new Claim(JwtClaimTypes.Name, "Don Keigh"),
                    new Claim(JwtClaimTypes.GivenName, "Don"),
                    new Claim(JwtClaimTypes.FamilyName, "Keigh"),
                    new Claim(JwtClaimTypes.Email, "DonKeigh@email.com"),
                    new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                    new Claim(JwtClaimTypes.WebSite, "http://don.com"),
                    new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json),
                    new Claim(JwtClaimTypes.Role, "account_admin"),
                    new Claim("account", "bec823e4-aced-4b92-9442-70c2f32c65f9"), // This is the account domain Account aggregate id to which this user belongs.
                }
            },
            // Represents a Reshape employee. Not associated with an account.
            new TestUser{SubjectId = "021e186b-ad4d-48df-b8a0-c3e2fdb8ac91", Username = "anne", Password = "anne",
                Claims =
                {
                    new Claim(JwtClaimTypes.Name, "Anne Nahnumus"),
                    new Claim(JwtClaimTypes.GivenName, "Anne"),
                    new Claim(JwtClaimTypes.FamilyName, "Nahnumus"),
                    new Claim(JwtClaimTypes.Email, "AnneNahnumus@email.com"),
                    new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                    new Claim(JwtClaimTypes.WebSite, "http://annenahnumus.com"),
                    new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json),
                    new Claim(JwtClaimTypes.Role, "admin"),
                }
            }
        };
    }
}