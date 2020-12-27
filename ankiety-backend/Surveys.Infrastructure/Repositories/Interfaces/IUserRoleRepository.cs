using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Surveys.Infrastructure.Repositories.Interfaces
{
    public interface IUserRoleRepository
    {
        Task<IEnumerable<IdentityRole>> GetAllRolesAsync();

        Task<IdentityRole> GetRoleByNameAsync(string name);
    }
}
