using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using EleksTask.Dto;
using EleksTask.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TourServer;
using TourServer.Models;

namespace EleksTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationContext _context;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<ApplicationUser> userManager, IEmailService emailService,IConfiguration configuration,
            RoleManager<IdentityRole> roleManager, ApplicationContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _emailService = emailService;
            _configuration = configuration;
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

            var token = new EmailToken()
            {
                UserId = user.Id,
                Token = Guid.NewGuid()
            };

            await _context.EmailTokens.AddAsync(token);
            await _context.SaveChangesAsync();
            var apiPath = "https://localhost:1111/api/Auth/confirmEmail?token=" + token.Token + "&userId=" + user.Id;
            var link = "<a href='" + apiPath + "'>link</a>";
            await _emailService.SendEmailAsync(user.Email, "Confirm Email", link);
            return Ok(user.Id);
        }

        [HttpGet("confirmEmail")]
        public async Task<ActionResult> ConfirmEmail([FromQuery]Guid token, [FromQuery] string userId)
        {
            var tok = await _context.EmailTokens.AsNoTracking().FirstOrDefaultAsync(t => t.UserId == userId && t.Token == token);
            if (tok == null)
            {
                return NotFound("Not Found");
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            user.EmailConfirmed = true;
            await _context.SaveChangesAsync();
            return Ok();
        }
    }

}
