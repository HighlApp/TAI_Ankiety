using System;
using System.Linq;
using Surveys.Core.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Surveys.Infrastructure.Contexts;
using Surveys.Infrastructure.Repositories.Interfaces;

namespace Surveys.Infrastructure.Repositories
{
    public class AnswersRepository : RepositoryBase<Answer>, IAnswersRepository
    {
        private readonly SurveysContext _context;

        public AnswersRepository(SurveysContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Answer>> GetInvitationAnswers(Guid invitationId)
            => await _context.Answers
                .Where(x => x.InvitationId == invitationId)
                .ToListAsync();
    }
}
