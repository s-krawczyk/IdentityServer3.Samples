using System.Collections.Generic;
using IdentityServer3.Core.Models;

namespace IdentityServer
{
  internal static class Scopes
  {
    public static List<Scope> Get()
    {
      var scopes = new List<Scope>
            {
                StandardScopes.OpenId,
                StandardScopes.Profile,
                StandardScopes.Email,
                StandardScopes.Address,
                StandardScopes.OfflineAccess,
                StandardScopes.RolesAlwaysInclude,
                StandardScopes.AllClaims,

                new Scope()
                {
                  Name = "ExternalWebApi1",
                  Type = ScopeType.Resource,
                  Claims = new List<ScopeClaim>()
                  {
                    new ScopeClaim("name", true),
                    new ScopeClaim("internal_security_data", true)
                  }
                },
                new Scope
                {
                    Name = "InternalApis",
                    Type = ScopeType.Resource,
                    Claims = new List<ScopeClaim>
                    {
                        new ScopeClaim("name", true),
                    new ScopeClaim("internal_security_data", true)
                    }
                },
                new Scope
                {
                    Name = "WebApi2",
                    Type = ScopeType.Resource,
                    Claims = new List<ScopeClaim>
                    {
                        new ScopeClaim("name", true)
                    }
                }
            };

      return scopes;
    }
  }
}