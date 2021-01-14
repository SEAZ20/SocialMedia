using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SocialMedia.Core.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TockenController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        //private readonly ISecurityService _securityService;
       // private readonly IPasswordService _passwordService;

        public TockenController(IConfiguration configuration /*ISecurityService securityService, IPasswordService passwordService*/)
        {
            _configuration = configuration;
           // _securityService = securityService;
            //_passwordService = passwordService;
        }

        [HttpPost]
        public IActionResult Authentication(UserLogin login)
        {
            //if it is a valid user
            //var validation = IsValidUser(login);
            if (IsValidUser(login))
            {
                var token = GenerateToken();
                return Ok(new { token });
            }

            return NotFound();
        }

        private /* async Task<(bool, Security)>*/ bool IsValidUser(UserLogin login)
        {
            //var user = await _securityService.GetLoginByCredentials(login);
            //var isValid = _passwordService.Check(user.Password, login.Password);
            return true; /*(isValid, user);*/
        }

        private string GenerateToken(/*Security security*/)
        {
            //Header
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:SecretKey"]));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(signingCredentials);

            //Claims
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, "Silvio Alcuvar"),
                new Claim(ClaimTypes.Email, "seaz@gmail.com"),
                new Claim(ClaimTypes.Role, "Administrador"),
            };

            //Payload
            var payload = new JwtPayload
            (
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                claims,
                DateTime.Now,
                DateTime.UtcNow.AddMinutes(10)
            );

            var token = new JwtSecurityToken(header, payload);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
