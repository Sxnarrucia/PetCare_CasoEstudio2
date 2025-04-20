using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace APIPetCare.Filtros
{
    public class JWTValidateFilter : AuthorizationFilterAttribute
    {
        private const string Apikey = "1234567890abcdefregegergreg5g5e5g5g5g555vv5wsgew4y554";
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var autHeader = actionContext.Request.Headers.Authorization;

            if (autHeader == null || autHeader.Scheme != "Bearer")
            {
                actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
                return;
            }

            var token = autHeader.Parameter;

            try
            {
                var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
                var key = System.Text.Encoding.UTF8.GetBytes(Apikey);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (System.IdentityModel.Tokens.Jwt.JwtSecurityToken)validatedToken;
                var usuario = jwtToken.Claims.First(x => x.Type == "unique_name").Value;

                var identity = new GenericIdentity(usuario);
                actionContext.RequestContext.Principal = new GenericPrincipal(identity, null);
            }
            catch (Exception)
            {
                actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
                return;
            }
        }
    }
}