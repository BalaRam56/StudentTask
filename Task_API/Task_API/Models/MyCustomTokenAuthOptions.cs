using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Task_API.Models
{
    public class MyCustomTokenAuthOptions : AuthenticationSchemeOptions
    {
        public const string DefaultScemeName = "MyCustomTokenAuthenticationScheme";
        public string TokenHeaderName { get; set; } = "X-CUSTOM-TOKEN";
    }
    public class MyCustomTokenAuthHandler : AuthenticationHandler<MyCustomTokenAuthOptions>
    {
        public MyCustomTokenAuthHandler(IOptionsMonitor<MyCustomTokenAuthOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
            : base(options, logger, encoder, clock) { }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey(Options.TokenHeaderName))
                return Task.FromResult(AuthenticateResult.Fail($"Missing Header For Token: {Options.TokenHeaderName}"));

            var token = Request.Headers[Options.TokenHeaderName];
            // get username from db or somewhere else accordining to this token
            var username = "Username-From-Somewhere-By-Token";
            var claims = new[] {
            new Claim(ClaimTypes.NameIdentifier, username),
            new Claim(ClaimTypes.Name, username),
            // add other claims/roles as you like
        };
            var id = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(id);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }
}
