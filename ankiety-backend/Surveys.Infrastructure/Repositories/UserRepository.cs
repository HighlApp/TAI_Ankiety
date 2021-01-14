using System.Linq;
using Surveys.Core.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Surveys.Infrastructure.Contexts;
using Surveys.Infrastructure.Repositories.Interfaces;

namespace Surveys.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SurveysContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserRepository(UserManager<User> userManager, 
            SignInManager<User> signInManager, SurveysContext context)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> AddAsync(User user, string password)
            => await _userManager.CreateAsync(user, password);

        public async Task AddUserToRoleAsync(User user, string role)
            => await _userManager.AddToRoleAsync(user, role);

        public async Task<IdentityResult> ChangePasswordAsync(
            User user, string currentPassword, string newPassword)
            => await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);

        public async Task<User> FindByEmailAsync(string email)
            => await _userManager.FindByEmailAsync(email);

        public async Task<User> FindByIdAsync(string id)
            => await _userManager.FindByIdAsync(id);

        public async Task<IEnumerable<User>> GetAllUsers()
            => await _context.Users
                .ToListAsync();

        public async Task<SignInResult> SignInAsync(string email, string password)
            => await _signInManager.PasswordSignInAsync(email, password, true, false);

        public async Task<IEnumerable<string>> GetUserIdByRoleId(string roleId)
            => await _context.UserRoles
                .Where(x => x.RoleId == roleId)
                .Select(x => x.UserId)
                .ToListAsync();
    }
}
