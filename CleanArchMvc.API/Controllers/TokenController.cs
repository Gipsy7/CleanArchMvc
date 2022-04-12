using CleanArchMvc.API.Models;
using CleanArchMvc.Domain.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IAuthenticate _authentication;
        private readonly IConfiguration _configuration;

        public TokenController(IAuthenticate authentication, IConfiguration configuration)
        {
            _authentication = authentication;
            _configuration = configuration;
        }

        [HttpPost("CreateUser")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult<UserToken>> CreateUser([FromBody] RegisterModel userInfo)
        {
            var result = await _authentication.RegisterUserAsync(userInfo.Email, userInfo.Password);

            if (result)
            {
                return Ok($"User {userInfo.Email} was create successfully");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid Login attempt");
                return BadRequest(ModelState);
            }
        }
        [AllowAnonymous]
        [HttpPost("LoginUser")]
        public async Task<ActionResult<UserToken>> Login([FromBody] LoginModel userInfo)
        {
            var result = await _authentication.AuthenticateAsync(userInfo.Email, userInfo.Password);

            if (result)
            {
                return GenerateToken(userInfo);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid Login attempt");
                return BadRequest(ModelState);
            }
        }

        private UserToken GenerateToken(LoginModel userInfo)
        {
            //User statements
            var claims = new[]
            {
                new Claim("email", userInfo.Email),
                new Claim("meuvalor", "oque voce quiser"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            //Generate private key to sign the token
            var privateKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));

            //Generate digital signature
            var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);

            //Set expiration time
            var expiration = DateTime.UtcNow.AddMinutes(10);

            //Generate token
            JwtSecurityToken token = new JwtSecurityToken(issuer: _configuration["Jwt:Issuer"],
                                                          audience: _configuration["Jwt:Audience"],
                                                          claims: claims,
                                                          expires: expiration,
                                                          signingCredentials: credentials);

            return new UserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
    }
}
