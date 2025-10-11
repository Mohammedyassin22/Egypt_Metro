using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstraction
{
    public interface IAuthServices
    {
        Task<UserResultDto> LoginAsync(LoginDto loginDto);
        Task<UserResultDto> RegisterAsync(RegisterDto registerDto);
        Task<UserResultDto> GetCurrentUserAsync(string Phone);
        Task<bool> SendVerificationCodeAsync(string phoneNumber);
        Task<bool> VerifyCodeAsync(string phoneNumber, string code);
        Task<bool> UpdateProfileAsync(UpdateUserDto updateDto);
    }
}
