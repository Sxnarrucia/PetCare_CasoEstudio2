using APIPetCare.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

using HttpPostAttribute = System.Web.Http.HttpPostAttribute;
using RouteAttribute = System.Web.Http.RouteAttribute;
using System.IdentityModel.Tokens;
using SecurityTokenDescriptor = Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor;
using SigningCredentials = Microsoft.IdentityModel.Tokens.SigningCredentials;
using SymmetricSecurityKey = Microsoft.IdentityModel.Tokens.SymmetricSecurityKey;
using SecurityAlgorithms = Microsoft.IdentityModel.Tokens.SecurityAlgorithms;

namespace APIPetCare.Controllers
{
    public class AuthController : ApiController
    {
        private const string Apikey = "1234567890abcdefregegergreg5g5e5g5g5g555vv5wsgew4y554";

        [HttpPost]
        [Route("Login")]
        public IHttpActionResult Login([FromBody] LoginModel login)
        {
            
            if (login.Username == "admin" && login.Password == "123")
            {
                var Token = GenerateToken(login.Username);
                return Ok(new {token=Token});
            }

            return Unauthorized();
        }

        private string GenerateToken(string username)
        {
           var key = Encoding.UTF8.GetBytes(Apikey);
            
           var PreToken = new SecurityTokenDescriptor()
           {
               Subject = new ClaimsIdentity(new[] {new Claim(ClaimTypes.Name, username) }),
               Expires = DateTime.UtcNow.AddMinutes(30),
               SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
           };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(PreToken);

            return tokenHandler.WriteToken(token);
        }
    }
}