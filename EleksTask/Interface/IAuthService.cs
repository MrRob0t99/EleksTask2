using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EleksTask.Dto;
using EleksTask.Models;
using Microsoft.AspNetCore.Mvc;

namespace EleksTask.Interface
{
    public interface IAuth
    {
        Task<Response<LogInResponseDto>> LogInAsync([FromBody] LogInRequestDto logInDto);

        Task<Response<string>> Registration([FromBody] RegistrationDto registrationDto);

        Task<Response<bool>> ConfirmEmail([FromQuery] Guid token, [FromQuery] string userId);

    }
}
