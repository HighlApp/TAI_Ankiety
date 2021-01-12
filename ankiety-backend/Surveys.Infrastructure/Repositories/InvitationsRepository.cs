using Surveys.Core.Entities;
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
    }
}
