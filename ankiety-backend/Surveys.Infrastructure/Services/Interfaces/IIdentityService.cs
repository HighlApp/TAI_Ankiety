using System;
using System.Threading.Tasks;
using Surveys.Infrastructure.DTO;
using Surveys.Infrastructure.Common;

namespace Surveys.Infrastructure.Services.Interfaces
{
    public interface IIdentityService
    {
        Task SignUpAsync(string name, string surname, string email,
            string password, string role = UserRoles.User);

        Task<AuthResponseDTO> SignInAsync(string email, string password);

        Task ChangePasswordAsync(string password, string newPassword);

        Task<AuthResponseDTO> RefreshTokenAsync(string token, string refreshToken);
    }
}
