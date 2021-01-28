using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer
{
    public static class Config
    {
        private const string spaClientUrl = "https://localhost:5001";

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile(),
            };


        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("resourceApi", "My API")
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                // Angular Client
                new Client
                {
                    ClientId = "spaCodeClient",
                    ClientName = "SPA Code Client",
                    AccessTokenType = AccessTokenType.Jwt,
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireConsent = false,
                    RequireClientSecret = false,
                    AllowAccessTokensViaBrowser = true,
                    RequirePkce = false,
                    AccessTokenLifetime = 3600,
                    IdentityTokenLifetime = 30,

                    RedirectUris = new List<string>
                    {
                        $"{spaClientUrl}/admin/dashboard",
                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        $"{spaClientUrl}",
                    },
                    AllowedCorsOrigins = new List<string>
                    {
                        $"{spaClientUrl}",
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "resourceApi"
                    }
                }
            };
    }
}