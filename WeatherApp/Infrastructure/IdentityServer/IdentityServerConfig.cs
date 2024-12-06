using IdentityServer4;
using IdentityServer4.Models;

namespace Infrastructure.IdentityServer;
internal class IdentityServerConfig
{
    public static IEnumerable<ApiScope> GetApiScopes()
    {
        return
        [
            new("doAll", "doAll"),
        ];
    }

    public static IEnumerable<ApiResource> GetApis()
    {
        return new List<ApiResource>
        {
            new("WeatherApi", "WeatherApi")
            {
                Scopes = { "doAll"}
            },
        };
    }

    public static IEnumerable<IdentityResource> GetIdentityResources()
    {
        return
        [
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        ];
    }

    public static IEnumerable<Client> GetClients()
    {
        return
        [
            new Client
            {
                ClientId = "WeatherUIClientId",
                ClientName = "WeatherUI",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                AllowedCorsOrigins =
                {
                    "http://localhost:7082"
                },
                RedirectUris = {
                    "http://localhost:7082/signin-oidc"
                },
                ClientSecrets =
                {
                    new Secret("WEATHERUISECRET".Sha256())
                },
                AllowedScopes =
                {
                    "doAll",
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile
                },
                AllowAccessTokensViaBrowser = true,
                AllowOfflineAccess = true,
            },
        ];
    }
}