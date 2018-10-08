using System;
using System.Threading.Tasks;
using EleksTask.Dto;
using EleksTask.Interface;
using EleksTask.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EleksTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IAuth _auth;

        public AuthController(IAuth auth)
        {
            _auth = auth;
        }

        [HttpPost("token")]
        public async Task<IActionResult> LogInAsync([FromBody] LogInRequestDto logInDto)
        {
            var response = await _auth.LogInAsync(logInDto);
            if (response.Error != null)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }


        [HttpPost("registration")]
        public async Task<IActionResult> Registration([FromBody] RegistrationDto registrationDto)
        {
            var response = await _auth.Registration(registrationDto);
            if (response.Error != null)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("confirmEmail")]
        public async Task<ActionResult> ConfirmEmail([FromQuery]Guid token, [FromQuery] string userId)
        {
            var response = await _auth.ConfirmEmail(token, userId);
            if (response.Error != null)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }

}
