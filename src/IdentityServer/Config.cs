// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityMicroservice
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
         new List<IdentityResource>
         {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
         };

        public static IEnumerable<ApiScope> ApiScopes =>
       new List<ApiScope>
       {
            new ApiScope("websitesMs.all", "All crud operations for the website micro service"),
            new ApiScope("websiteCategoryMs.all", "All crud operations for the category website micro service"),
            new ApiScope("productsMs.all", "All crud operations for the products micro service"),
            new ApiScope("categoryProductsMs.all", "All crud operations for the category products micro service"),
            new ApiScope("reviewsMs.all", "All crud operations for the reviews micro service"),
            new ApiScope("shoppingCartMs.all", "All crud operations for the shopping cart micro service"),
            new ApiScope("savedWebsitesMs.all", "All crud operations for the saved websites micro service"),
            new ApiScope("statisticsMs.all", "All crud operations for the statistics micro service"),
            new ApiScope("searchWebsiteMs.all", "All crud operations for the search website micro service"),
            new ApiScope("purchaseMs.all", "All crud operations for the search website micro service"),
            new ApiScope("role", "The user roles", new List<string>{ "role"})
       };

        public static IEnumerable<ApiResource> ApiResources =>
            new List<ApiResource>
            {
            };

        public static IEnumerable<Client> Clients =>
        new List<Client>
        {
            new Client
            {
                ClientId = "ShoppingWebsitesMVC",

                ClientSecrets = { new Secret("ShoppingWebsitesMVCSecret".Sha256()) },

                AllowedGrantTypes = GrantTypes.Code,
                
                RequirePkce = true,

                // where to redirect to after login
                RedirectUris = { "https://localhost:9000/signin-oidc" },

                // where to redirect to after logout
                PostLogoutRedirectUris = { "https://localhost:9000/signout-callback-oidc" },

                AllowOfflineAccess = true,


                // scopes that client has access to
                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "role",
                    "websitesMs.all",
                    "websiteCategoryMs.all",
                    "productsMs.all",
                    "categoryProductsMs.all",
                    "reviewsMs.all",
                    "shoppingCartMs.all",
                    "savedWebsitesMs.all",
                    "statisticsMs.all",
                    "searchWebsiteMs.all"
                }
            }
        };
    }
}