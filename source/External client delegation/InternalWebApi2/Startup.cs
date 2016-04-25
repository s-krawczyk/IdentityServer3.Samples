using System.Web.Http;
using IdentityServer3.AccessTokenValidation;
using InternalWebApi2;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace InternalWebApi2
{
  public class Startup
    {
    public void Configuration(IAppBuilder app)
        {
            app.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
            {
                Authority = "http://localhost:44333",
                ValidationMode = ValidationMode.ValidationEndpoint,
                RequiredScopes = new[] { "WebApi1" },
                PreserveAccessToken = true
            });

            // configure web api
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();

            // require authentication for all controllers
            config.Filters.Add(new AuthorizeAttribute());

            app.UseWebApi(config);
        }
    }
}