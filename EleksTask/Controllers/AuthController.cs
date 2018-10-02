using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using EleksTask.Dto;
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
        private readonly UserManager<ApplicationUser> _userManager;
        public readonly IConfiguration _configuration;
        private RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationContext _context;

        public AuthController(UserManager<ApplicationUser> userManager, IConfiguration configuration, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _configuration = configuration;
            _roleManager = roleManager;
        }

        [HttpPost("token")]
        public async Task<IActionResult> LogInAsync([FromBody] LogInDto logInDto)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == logInDto.UserName && u.EmailConfirmed);//
            if (user != null && await _userManager.CheckPasswordAsync(user, logInDto.Password))
            {
                var roles = await _userManager.GetRolesAsync(user);
                var claim = new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                    new Claim("Roles",roles[0]),
                };

                var token = new JwtSecurityToken(
                    expires: DateTime.Now.AddHours(1),
                    claims: claim,
                    signingCredentials: new SigningCredentials(
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("SecretKey").Value)),
                            SecurityAlgorithms.HmacSha256)
                );
                var handler = new JwtSecurityTokenHandler();

                return Ok(new
                {
                    token = handler.WriteToken(token),
                    exparion = token.ValidTo
                });
        }

            return BadRequest("Data is not valid");
    }

    [HttpPost("registration")]
    public async Task<IActionResult> Registration([FromBody] RegistrationDto registrationDto)
    {
        var user = new ApplicationUser()
        {
            Email = registrationDto.Email,
            FirstName = registrationDto.FirstName,
            LastName = registrationDto.LastName,
            UserName = registrationDto.UserName
        };

        if (!await _roleManager.RoleExistsAsync(registrationDto.Role.ToString()))
        {
            await _roleManager.CreateAsync(new IdentityRole(registrationDto.Role.ToString()));
        }

        var result = await _userManager.CreateAsync(user, registrationDto.Password);
        if (result.Errors != null && result.Errors.Any())
        {
            return BadRequest(result.Errors.Select(e => e.Description).Aggregate((a, b) => a + b));
        }
        await _userManager.AddToRoleAsync(user, registrationDto.Role.ToString());
        return Ok();
    }
}

}
