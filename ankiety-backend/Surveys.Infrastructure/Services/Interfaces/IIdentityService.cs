using System.Threading.Tasks;
using Surveys.Infrastructure.DTO;
using Surveys.Infrastructure.Common;
using System.Collections.Generic;

namespace Surveys.Infrastructure.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<Response<StatusResponseDTO>> SignUpAsync(
            string name, string surname, string email, string password);

        Task<Response<AuthResponseDTO>> SignInAsync(
            string email, string password);

        Task<Response<StatusResponseDTO>> ChangePasswordAsync(
            string password, string newPassword);

    }
}
