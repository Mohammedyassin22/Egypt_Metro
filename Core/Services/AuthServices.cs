using AutoMapper;
using Domain.Exceptions;
using Domain.Modules.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ServicesAbstraction;
using ServicesAbstraction.Twilio;
using Shared;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class Authservice(UserManager<AppUsers> userManager, IOptions<JWtOptions> options, IMapper mapper, SignInManager<AppUsers> signInManager, IConnectionMultiplexer _redis,ISmsService smsService,ILogger logger) : IAuthServices
    {

        public async Task<UserResultDto> GetCurrentUserAsync(string Phone)
        {
            var result = await userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber ==Phone);
            if (result is null)
            {
                throw new UserNotFoundException($"User with phone not found");
            }
            return new UserResultDto()
            {
                Username = result.DisplayName,
                PhoneNumber = result.PhoneNumber,
                Token = await GenerateToken(result)
            };
        }

        public async Task<UserResultDto> LoginAsync(LoginDto loginDto)
        {
            var user = await userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == loginDto.PhoneNumber);
            if (user is null)
                throw new UnAuthorizedException();
            return new UserResultDto()
            {
                Username = user.DisplayName,
                PhoneNumber = user.PhoneNumber,
                Token = await GenerateToken(user)
            };
        }

        public async Task<UserResultDto> RegisterAsync(RegisterDto refisterDto)
        {
            var user = new AppUsers()
            {
                DisplayName = refisterDto.DisplayName,
                PhoneNumber = refisterDto.Phone
            };
            var result = await userManager.CreateAsync(user, refisterDto.Phone);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                throw new ValidationException(errors);
            }
            return new UserResultDto()
            {
                Username = user.DisplayName,
                PhoneNumber = user.PhoneNumber,
                Token = await GenerateToken(user)
            };
        }

        public async Task<bool> SendVerificationCodeAsync(string phoneNumber)
        {
            try
            {
                var random = new Random();
                var code = random.Next(100000, 999999).ToString();

                var db = _redis.GetDatabase();
                await db.StringSetAsync($"verify:{phoneNumber}", code, TimeSpan.FromMinutes(1));

                var message = $"Your verification code is: {code} (Valid for one minute.)";

                await smsService.SendSmsAsync(phoneNumber, message);

                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to send the verification code to {phoneNumber}");
                return false;
            }
        }

        public async Task<bool> UpdateProfileAsync(UpdateUserDto updateDto)
        {
            var user = await userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == updateDto.Phone);
            if (user == null) return false;

            user.DisplayName = updateDto.DisplayName;
            user.Address.FirstName = updateDto.FirstName;
            user.Address.LastName = updateDto.LastName;
            user.Address.street = updateDto.Street;
            user.Address.city = updateDto.City;
            user.Address.Country = updateDto.Country;

            var result = await userManager.UpdateAsync(user);
            return result.Succeeded;
        }

        public async Task<bool> VerifyCodeAsync(string phoneNumber, string code)
        {
            var db = _redis.GetDatabase();
            var savedCode = await db.StringGetAsync($"verify:{phoneNumber}");

            if (savedCode.IsNullOrEmpty)
                return false;

            return savedCode == code;
        }

        private async Task<string> GenerateToken(AppUsers user)
        {
            var jwtoptions = options.Value;
            var authclaims = new List<Claim>
           {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.MobilePhone,user.PhoneNumber)
            };
            var roles = await userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                authclaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var secretkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtoptions.SecretKey));
            var token = new JwtSecurityToken(
                issuer: jwtoptions.Issuer,
                audience: jwtoptions.Audience,
                claims: authclaims,
                expires: DateTime.UtcNow.AddDays((jwtoptions.DurationInDays)),
                signingCredentials: new SigningCredentials(secretkey, SecurityAlgorithms.HmacSha256Signature)
             );
            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        
    }
}
