using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServicesAbstraction;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentaion
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthController(ISerivcesManager servicesmanager):ControllerBase
    {
        [HttpPost("Login")]
        public async Task<IActionResult>Login(LoginDto loginDto)
        {
            var user = await servicesmanager.AuthServices.LoginAsync(loginDto);
            return Ok(user);
        }

        [HttpGet("currentUser")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUserAsync(string email)
        {
            var address = User.FindFirstValue(ClaimTypes.Email);
            var user = await servicesmanager.AuthServices.GetCurrentUserAsync(email);
            return Ok(user);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync(RegisterDto refisterDto)
        {
            var user = await servicesmanager.AuthServices.RegisterAsync(refisterDto);
            return Ok(user);
        }

        [Authorize]
        [HttpPut("update-profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateUserDto dto)
        {
            var phone = User.FindFirstValue(ClaimTypes.MobilePhone);
            if (phone == null) return Unauthorized("The phone number is not in the token");

            dto.Phone = phone;

            var result = await servicesmanager.AuthServices.UpdateProfileAsync(dto);
            if (!result) return BadRequest();
            return Ok();
        }

        [HttpPost("send-code")]
        public async Task<IActionResult> SendCode([FromBody] string phone)
        {
            var result = await servicesmanager.AuthServices.SendVerificationCodeAsync(phone);
            if (!result) return BadRequest("Code sending failed");
            return Ok("Code sent successfully");
        }

        [HttpPost("verify-code")]
        public async Task<IActionResult> VerifyCode(string phoneNumber, string code)
        {
            var result = await servicesmanager.AuthServices.VerifyCodeAsync(phoneNumber, code);
            if (!result) return BadRequest("Invalid or expired token");
            return Ok("Verification successful");
        }
    }
}
