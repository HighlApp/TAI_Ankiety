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
    public class InvitationsRepository : RepositoryBase<Invitation>, IInvitationsRepository
    {
        private readonly SurveysContext _context;

        public InvitationsRepository(SurveysContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Invitation>> GetInvitation()
            => await _context.Invitations
                .Include(x => x.User)
                .Include(x => x.Survey)
                .ToListAsync();

        public async Task<IEnumerable<Invitation>> GetUserInvitations(Guid userId)
            => await _context.Invitations
                .Include(x => x.Survey)
                .Where(x => x.UserId == userId.ToString() && x.FilledDate == null 
                    && (x.ExpirationDate == null || x.ExpirationDate > DateTime.Now))
                .ToListAsync();

        public async Task<IEnumerable<string>> GetIdUsersWithSurveyInvitation(Guid surveyId)
            => await _context.Invitations.Where(
                    x => (x.SurveyId == surveyId && x.FilledDate == null && x.ExpirationDate == null)
                        || (x.SurveyId == surveyId && x.FilledDate == null && x.ExpirationDate > DateTime.Now))
                    .Select(x => x.UserId)
                    .ToListAsync();

        public async Task<Invitation> GetInvitationWithUser(Guid invitationId)
            => await _context.Invitations
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == invitationId);

        public async Task<Invitation> GetInvitationWithSurveyDetails(Guid invitationId)
            => await _context.Invitations
                .Include(x => x.Survey)
                    .ThenInclude(x => x.Questions)
                        .ThenInclude(x => x.Options)
                .Where(x => x.Id == invitationId)
                .SingleOrDefaultAsync();
                
    }
}
