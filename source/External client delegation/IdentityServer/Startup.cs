using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Services;
using Owin;

namespace IdentityServer
{
  internal class Startup
  {
    public void Configuration(IAppBuilder app)
    {
      IdentityServerServiceFactory factory = new IdentityServerServiceFactory()
          .UseInMemoryUsers(Users.Get())
          .UseInMemoryScopes(Scopes.Get())
          .UseInMemoryClients(Clients.Get());

      factory.CustomGrantValidators.Add(new Registration<ICustomGrantValidator>(typeof(ActAsGrantValidator)));

      var options = new IdentityServerOptions
      {
        Factory = factory,
        SigningCertificate = Certificate.Get(),
        RequireSsl = false
      };

      app.UseIdentityServer(options);
    }
  }
}