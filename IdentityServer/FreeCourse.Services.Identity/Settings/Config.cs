using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace FreeCourse.Services.Identity.Settings
{
    public static class Config
    {
        // 1. API Resources
        public static IEnumerable<ApiResource> ApiResources => new[]
        {
            new ApiResource("resource_catalog", "Catalog Resource")
            {
                Scopes = { "catalog_fullpermission" }
            },
            new ApiResource("resource_photo_stock", "PhotoStock Resource")
            {
                Scopes = { "photostock_fullpermission" }
            },
            new ApiResource("resource_basket", "Basket Resource")
            {
                Scopes = { "basket_fullpermission" }
            },
            new ApiResource("resource_discount", "Discount Resource")
            {
                Scopes = { "discount_fullpermission" }
            },
            new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
        };

        // 2. Identity Resources (OpenId, Email, Profile, Roles)
        public static IEnumerable<IdentityResource> IdentityResources => new IdentityResource[]
        {
            new IdentityResources.Email(),
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResource
            {
                Name = "roles",
                DisplayName = "Roles",
                Description = "User roles",
                UserClaims = new[] { "role" }
            }
        };

        // 3. API Scopes
        public static IEnumerable<ApiScope> ApiScopes => new[]
        {
            new ApiScope("catalog_fullpermission", "Catalog API full access"),
            new ApiScope("photostock_fullpermission", "PhotoStock API full access"),
            new ApiScope("basket_fullpermission", "Basket API full access"),
            new ApiScope("discount_fullpermission", "Discount API full access"),
            new ApiScope("gateway_fullpermission", "Gateway API full access"),
            new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
        };

        // 4. Clients (client_credentials & resource_owner_password)
        public static IEnumerable<Client> Clients => new[]
        {
            // Microservice internal access (client_credentials)
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
                ClientId = "WbMvcClientUser",
                ClientSecrets = { new Secret("secret".Sha256()) },
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes = { "catalog_fullpermission", "photostock_fullpermission", IdentityServerConstants.LocalApi.ScopeName ,"gateway_fullpermission" }
            },

            // MVC/WebApp/User Login (resource_owner_password)
            new Client
            {
                ClientId = "WebMvcClientforUser",
                ClientSecrets = { new Secret("secret".Sha256()) },
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                AllowedScopes =
                {
                    "catalog_fullpermission",
                    "photostock_fullpermission",
                    "basket_fullpermission",
                    "discount_fullpermission",
                    "gateway_fullpermission",
                    IdentityServerConstants.StandardScopes.Email,
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.OfflineAccess,
                    "roles"
                },

                AllowOfflineAccess = true, // Refresh Token alabilmek için
                AccessTokenLifetime = 3600, // 1 saat
                RefreshTokenExpiration = TokenExpiration.Absolute,
                AbsoluteRefreshTokenLifetime = (int)(TimeSpan.FromDays(60).TotalSeconds),
                RefreshTokenUsage = TokenUsage.ReUse
            }
        };
    }
}
