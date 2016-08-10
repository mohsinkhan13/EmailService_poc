﻿using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using IdentityServer3.AccessTokenValidation;
using System.Security.Claims;
using System.Linq;
using System.Web.Http;

[assembly: OwinStartup(typeof(AccountsTransactions.Api.App_Start.Startup))]

namespace AccountsTransactions.Api.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // token validation
            app.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
            {
                Authority = "https://services.oneclickuk.de/authority",
                RequiredScopes = new[] { "oneclick" }
            });

            // add app local claims per request
            app.UseClaimsTransformation(incoming =>
            {
                // either add claims to incoming, or create new principal
                var appPrincipal = new ClaimsPrincipal(incoming);
                incoming.Identities.First().AddClaim(new Claim("appSpecific", "some_value"));

                return Task.FromResult(appPrincipal);
            });

            // web api configuration
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();

            app.UseWebApi(config);
        }
    }
}