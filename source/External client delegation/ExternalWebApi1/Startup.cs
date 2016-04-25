﻿using System;
using System.Threading.Tasks;
using System.Web.Http;
using ExternalWebApi1;
using IdentityServer3.AccessTokenValidation;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace ExternalWebApi1
{
  public class Startup
  {
    public void Configuration(IAppBuilder app)
    {
      app.Use<GlobalExceptionMiddleware>();
      app.UseIdentityServerBearerTokenAuthentication(
        new IdentityServerBearerTokenAuthenticationOptions
        {
          Authority = "http://localhost:44333",
          ValidationMode = ValidationMode.ValidationEndpoint,
          RequiredScopes = new[] { "ExternalWebApi1" },
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

  public class GlobalExceptionMiddleware : OwinMiddleware
  {
    public GlobalExceptionMiddleware(OwinMiddleware next) : base(next)
    {
    }

    public override async Task Invoke(IOwinContext context)
    {
      try
      {
        await Next.Invoke(context);
      }
      catch (Exception ex)
      {
        // your handling logic
      }
    }
  }
}