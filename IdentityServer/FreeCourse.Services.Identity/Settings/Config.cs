using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace FreeCourse.Services.Identity.Settings
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.Email(),
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource(){Name = "roles" , DisplayName = "Roles" , Description = "User roles", UserClaims = new[]{ "role" } }
            };

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
                },
                new Client
                {
                    ClientId = "WebMvcClientforUser", // ASP.NET Core MVC kullanıcı girişleri için
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                  
                    AllowedScopes =
                    {
                        "catalog_fullpermission",
                        "photostock_fullpermission",
                        "gateway_fullpermission",
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        "roles"
                    },

                    AllowOfflineAccess = true, // Refresh token kullanımı için gerekli
                    AccessTokenLifetime = 3600, // saniye cinsinden: 1 saat
                    RefreshTokenExpiration = TokenExpiration.Absolute,
                    AbsoluteRefreshTokenLifetime = (int)(DateTime.Now.AddDays(60)-DateTime.Now).TotalSeconds,
                    RefreshTokenUsage = TokenUsage.ReUse
                }
            };
    }
}
