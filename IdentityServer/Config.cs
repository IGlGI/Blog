using BlogApp.Common.Extensions;
using BogApp.Entities;
using Flurl;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;
using Constants = BlogApp.Common.Constants.IdentityServerConstants;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources()
        {
            yield return new IdentityResources.OpenId();
            yield return new IdentityResources.Profile();
            yield return new IdentityResources.Email();
        }

        public static IEnumerable<ApiResource> ApiResources()
        {
            yield return new ApiResource(Constants.Clients.AppApiName);
        }

        public static IEnumerable<ApiScope> ApiScopes()
        {
            yield return new ApiScope(Constants.Clients.AppApiName, "Blog application swagger API");
        }

        public static IEnumerable<Client> Clients(AppSettings settings) =>
            new List<Client>
            {
                // Blog app Api
                new Client
                {
                    ClientId = Constants.Clients.AppApiId ,
                    ClientSecrets = {new Secret(Constants.Clients.AppApiSecret.Base64Decode().ToSha256())},
                    AllowedCorsOrigins = settings.AllowedCors,
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AllowedScopes =
                    {
                        Constants.Clients.AppApiName,
                        Constants.OpenId,
                        Constants.Profile
                    }
                },

                // Angular Client
                new Client
                {
                    ClientId = Constants.Clients.AppClientId,
                    ClientName = Constants.Clients.AppClientName,
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    AllowedCorsOrigins = settings.AllowedCors,

                    RedirectUris = new List<string>
                    {
                        settings.AppConnectionString.AppendPathSegment(Constants.Clients.AppClientRedirectUri),
                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        settings.AppConnectionString,
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        Constants.Clients.AppApiName
                    }
                }

            };
    }
}