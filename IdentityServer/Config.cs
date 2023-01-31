using Duende.IdentityServer.Models;

namespace IdentityServer
{
    public static class Config
    {
        /*
         * Scopes are a feature that allows to express the extent of what an user can access.
         */
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope(name: "accessAPI", displayName: "API")
            };

        //test client
        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = {new Secret("secret".Sha256())},
                    AllowedScopes = {"accessAPI"}
                }
            };
    }
}