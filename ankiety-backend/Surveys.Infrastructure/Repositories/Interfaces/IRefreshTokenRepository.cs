using Surveys.Core.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Surveys.Infrastructure.Repositories.Interfaces
{
    public interface IRefreshTokenRepository : IRepositoryBase<RefreshToken>
    {
        Task<IEnumerable<RefreshToken>> GetUserRefreshTokensAsync(string userId);
    }
}
