using Surveys.Core.Entities;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Surveys.Infrastructure.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IdentityResult> AddAsync(User user, string password);

        Task AddUserToRoleAsync(User user, string role);

        Task<User> FindByIdAsync(string id);

        Task<User> FindByEmailAsync(string email);

        Task<IdentityResult> ChangePasswordAsync(
            User user, string currentPassword, string newPassword);

        Task<SignInResult> SignInAsync(string email, string password);
    }
}
