using Duende.IdentityServer.Models;

namespace eShop.Identity;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope("catalogapi"),
            new ApiScope("basketapi"),
            new ApiScope("catalogapi.read"),
            new ApiScope("catalogapi.write"),
            new ApiScope("eshoppinggateway")
        };

    public static IEnumerable<ApiResource> ApiResources =>
        new ApiResource[]
        {
            new ApiResource("Catalog","Catalog.API")
            {
                Scopes ={ "catalogapi.read", "catalogapi.write" }
            },
            new ApiResource("Basket","Basket.API")
            {
                Scopes ={ "basketapi" }
            },
            new ApiResource("EShoppingGateway","Eshopping Gateway")
            {
                Scopes = { "eshoppinggateway", "basketapi" }
            }
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            // m2m client credentials flow client
            new Client
            {
                ClientName="Catalog API Client",
                ClientId="CatalogApiClient",
                ClientSecrets ={new Secret("49C1A7A9-1C79-4A89-A3D6-A37998FB86B0".Sha256()) } ,
                AllowedGrantTypes=GrantTypes.ClientCredentials,
                AllowedScopes ={ "catalogapi.read", "catalogapi.write" }

            },
            new Client
            {
                ClientName="Basket API Client",
                ClientId="BasketApiClient",
                ClientSecrets ={new Secret("49C1A7B8-1C79-4A89-A3D6-A37998FB86B0".Sha256()) } ,
                AllowedGrantTypes=GrantTypes.ClientCredentials,
                AllowedScopes ={"basketapi" }
            },
            new Client
            {
                ClientName="EShopping Gateway Client",
                ClientId="EShppingGatewayClient",
                ClientSecrets ={new Secret("49C1A7B8-1C79-4A70-A3C6-A37998FB86B0".Sha256()) } ,
                AllowedGrantTypes=GrantTypes.ClientCredentials,
                AllowedScopes ={ "eshoppinggateway", "basketapi" }
            }
        };
}
