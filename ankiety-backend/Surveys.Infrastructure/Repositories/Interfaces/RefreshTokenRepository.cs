using System;
using System.Linq;
using Surveys.Core.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Surveys.Infrastructure.Contexts;

namespace Surveys.Infrastructure.Repositories.Interfaces
{
    public class RefreshTokenRepository : 
        RepositoryBase<RefreshToken>, IRefreshTokenRepository
    {
        private readonly SurveysContext _context;

        public RefreshTokenRepository(SurveysContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RefreshToken>> GetUserRefreshTokensAsync(string userId)
        {
            return await _context.RefreshTokens
                    .Where(x => x.UserId == userId && !x.Used &&
                    !x.Invalidated && x.ExpirationDate > DateTime.UtcNow)
                    .ToListAsync();
        }
    }
}
