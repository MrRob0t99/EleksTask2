using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using EleksTask.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace EleksTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApllicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<ApllicationUser> userManager,IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task LogInAsync([FromBody] LogInDto logInDto)
        {

            var key = _configuration.GetSection("SecretKey").Value;
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == logInDto.Email && u.EmailConfirmed);
            if (user != null && await _userManager.CheckPasswordAsync(user, logInDto.Password))
            {
                var claim = new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()), 
                };

                
                //var credentials = new EncryptingCredentials(new ECDsaSecurityKey(),"C";

                //var token = new JwtSecurityToken(
                //    issuer: "http://localhost:1111",
                //    audience: "http://localhost:1111",
                //    expires: DateTime.Now.AddHours(1),
                //    claims:claim,
                //    signingCredentials:new SigningCredentials()



                //);


            }
        }
    }
}