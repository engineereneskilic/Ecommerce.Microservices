using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace FreeCourse.Services.Identity.Settings
{
    public static class Config
    {
        public static IEnumerable<ApiScope> ApiScopes =>
         new List<ApiScope>
         {
            new ApiScope("catalog_fullpermission", "Catalog API full access"),
            new ApiScope("photostock_fullpermission", "PhotoStock API full access"),
            new ApiScope("gateway_fullpermission", "Gateway API full access")
         };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "CatalogServiceClient",
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "catalog_fullpermission", "gateway_fullpermission" }
                },
                new Client
                {
                    ClientId = "PhotoStockServiceClient",
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "photostock_fullpermission", "gateway_fullpermission" }
                }
            };
    }
}
