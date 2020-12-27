using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Surveys.Infrastructure.Contexts;
using Surveys.Infrastructure.Repositories.Interfaces;

namespace Surveys.Infrastructure.Repositories
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly SurveysContext _context;

        public UserRoleRepository(SurveysContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<IdentityRole>> GetAllRolesAsync()
            => await _context.Roles.ToListAsync();

        public async Task<IdentityRole> GetRoleByNameAsync(string name)
            => await _context.Roles.SingleOrDefaultAsync(r => r.Name == name);
    }
}
