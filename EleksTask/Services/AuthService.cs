using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using EleksTask.Dto;
using EleksTask.Interface;
using EleksTask.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TourServer.Models;

namespace EleksTask.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationContext _context;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<ApplicationUser> userManager, IEmailService emailService, IConfiguration configuration,
            RoleManager<IdentityRole> roleManager, ApplicationContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _emailService = emailService;
            _configuration = configuration;
        }

        public async Task<Response<LogInResponseDto>> LogInAsync(LogInRequestDto logInDto)
        {
            var response = new Response<LogInResponseDto>();
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == logInDto.UserName );//&& u.EmailConfirmed
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
                var data = new LogInResponseDto()
                {
                    Token = handler.WriteToken(token),
                    Exparion = token.ValidTo
                };
                response.Data = data;

                return response;
            }

            response.Error = new Error("Data not valid");
            return response;
        }

        public async Task<Response<string>> Registration(RegistrationDto registrationDto)
        {
            var response = new Response<string>();
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
                response.Error = new Error(result.Errors.Select(e => e.Description).Aggregate((a, b) => a + b));
                return response;
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
            //await _emailService.SendEmailAsync(user.Email, "Confirm Email", link);
            response.Data = user.Id;
            return response;
        }

        public async Task<Response<bool>> ConfirmEmail(Guid token, string userId)
        {
            var response = new Response<bool>();
            var tok = await _context.EmailTokens.AsNoTracking().FirstOrDefaultAsync(t => t.UserId == userId && t.Token == token);
            if (tok == null)
            {
                response.Error = new Error("User not found");
                return response;
            }
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            user.EmailConfirmed = true;
            await _context.SaveChangesAsync();
            response.Data = true;
            return response;
        }
    }
}
