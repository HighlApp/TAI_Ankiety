using System;
using Surveys.Core.Entities;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Surveys.Infrastructure.Contexts;
using Surveys.Infrastructure.Repositories.Interfaces;

namespace Surveys.Infrastructure.Repositories
{
    public class SurveysRepository : RepositoryBase<Survey>, ISurveysRepository
    {
        private readonly SurveysContext _context;

        public SurveysRepository(SurveysContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Survey> GetByIdWithQuestionsAndAnswerOptionsAsync(Guid id)
            => await _context.Set<Survey>()
                .Include(x => x.Questions)
                    .ThenInclude(x => x.Options)
                .FirstOrDefaultAsync(x => x.Id == id);
    }
}
