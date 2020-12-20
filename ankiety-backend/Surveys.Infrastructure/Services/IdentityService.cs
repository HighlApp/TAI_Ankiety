using System;
using System.Threading.Tasks;
using Surveys.Infrastructure.DTO;
using Surveys.Infrastructure.Common;
using Surveys.Infrastructure.Services.Interfaces;

namespace Surveys.Infrastructure.Services
{
    public class IdentityService : IIdentityService
    {
        public Task ChangePasswordAsync(string currentPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public Task<AuthResponseDTO> RefreshTokenAsync(string token, 
            string refreshToken)
        {
            throw new NotImplementedException();
        }

        public Task<AuthResponseDTO> SignInAsync(string email, string password)
        {
            throw new NotImplementedException();
        }

        public Task SignUpAsync(string name, string surname, 
            string email, string password, string role = UserRoles.User)
        {
            throw new NotImplementedException();
        }
    }
}
