using IdentityModel;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Json;
using IdentityServer4;

namespace IdentityServerHost.Quickstart.UI
{
    public class DefaultUsers
    {
        public static List<TestUser> Users
        {
            get
            {
                var address = new
                {
                    street_address = "One Hacker Way",
                    locality = "Hackerland",
                    postal_code = 007007,
                    country = "Laplandia"
                };

                return new List<TestUser>
                {
                    new TestUser
                    {
                        SubjectId = "007",
                        Username = "admin",
                        Password = "admin",
                        Claims =
                        {
                            new Claim(JwtClaimTypes.Name, "James Bond"),
                            new Claim(JwtClaimTypes.GivenName, "James"),
                            new Claim(JwtClaimTypes.FamilyName, "Bond"),
                            new Claim(JwtClaimTypes.Email, "laplandia@wonder.country"),
                            new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                            new Claim(JwtClaimTypes.WebSite, "http://mr-bond-from-laplandia.country"),
                            new Claim(JwtClaimTypes.Address, JsonSerializer.Serialize(address), IdentityServerConstants.ClaimValueTypes.Json)
                        }
                    },
                };
            }
        }
    }
}