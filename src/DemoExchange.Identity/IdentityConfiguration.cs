using System.Collections.Generic;
using DemoExchange.Interface;
using IdentityServer4.Models;

namespace DemoExchange.Identity {
  public static class IdentityConfiguration {
    public static IEnumerable<ApiScope> ApiScopes =>
      new List<ApiScope> {
        new ApiScope(Constants.Identity.API_SCOPE, Constants.Identity.API_NAME)
      };

    public static IEnumerable<Client> Clients =>
      new List<Client> {
        new Client {
        ClientId = Constants.Identity.CLIENT_ID,

        // no interactive user, use the clientid/secret for authentication
        AllowedGrantTypes = GrantTypes.ClientCredentials,

        // secret for authentication
        ClientSecrets = {
        new Secret(Constants.Identity.SECRET.Sha256())
        },

        // scopes that client has access to
        AllowedScopes = { Constants.Identity.API_SCOPE }
        }
      };
  }
}
