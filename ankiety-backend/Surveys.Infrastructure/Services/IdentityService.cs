using System;
using System.Linq;
using Surveys.Core.Entities;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Surveys.Infrastructure.DTO;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Surveys.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Surveys.Infrastructure.Extensions;
using Surveys.Infrastructure.Services.Interfaces;
using Surveys.Infrastructure.Repositories.Interfaces;

namespace Surveys.Infrastructure.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly SignInManager<User> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IdentityService(IHttpContextAccessor httpContextAccessor,
            IJwtTokenService jwtTokenService, IUserRepository userRepository,
            SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
            _userRepository = userRepository;
            _jwtTokenService = jwtTokenService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Response<StatusResponseDTO>> ChangePasswordAsync(
            string currentPassword, string newPassword)
        {
            string userId = _httpContextAccessor.GetUserId();
            User user = await _userRepository.FindByIdAsync(userId);

            if (user == null)
            {
                return new Response<StatusResponseDTO>(
                    new StatusResponseDTO(true));
            }

            IdentityResult result = await _userRepository.ChangePasswordAsync(
                user, currentPassword, newPassword);

            if (!result.Succeeded)
            {
                IEnumerable<string> errors = result.Errors.Select(x => x.Code);

                if (errors.Contains("PasswordMismatch"))
                {
                    return await Task.FromResult(new Response<StatusResponseDTO>(
                        "Passwords does not match"));
                }

                return await Task.FromResult(new Response<StatusResponseDTO>(
                    "Password does not meet the password policy"));
            }

            await _jwtTokenService.InvalidateUserRefreshTokensAsync(user.Id);

            return await Task.FromResult(new Response<StatusResponseDTO>(
                new StatusResponseDTO(true)));
        }

        public async Task<Response<AuthResponseDTO>> SignInAsync(
            string email, string password)
        {
            User user = await _userRepository.FindByEmailAsync(email);

            if (user == null)
            {
                return await Task.FromResult(new Response<AuthResponseDTO>(
                    "Invalid email address or password"));
            }

            SignInResult result = await _userRepository.SignInAsync(email, password);

            if (!result.Succeeded)
            {
                return await Task.FromResult(new Response<AuthResponseDTO>(
                    "Invalid email address or password"));
            }

            AuthResponseDTO response = await _jwtTokenService.
                GenerateJwtTokenWithRefreshToken(user);

            return await Task.FromResult(new Response<AuthResponseDTO>(response));
        }

        public async Task<Response<StatusResponseDTO>> SignUpAsync(
            string name, string surname, string email, string password)
        {
            User user = new User()
            {
                Name = name,
                Surname = surname,
                Email = email,
                UserName = email
            };

            IdentityResult result = await _userRepository.AddAsync(user, password);

            if(!result.Succeeded)
            {
                return await Task.FromResult(new Response<StatusResponseDTO>(
                    "Invalid username or password. Try again."));
            }

            await _userRepository.AddUserToRoleAsync(user, UserRoles.User);

            return await Task.FromResult(new Response<StatusResponseDTO>(
                new StatusResponseDTO(true)));
        }
    }
}
