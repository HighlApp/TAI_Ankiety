using System;
using Surveys.Core.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Surveys.Infrastructure.Repositories.Interfaces
{
    public interface IInvitationsRepository : IRepositoryBase<Invitation>
    {
        Task<IEnumerable<Invitation>> GetInvitationWithUsers();

        Task<IEnumerable<Invitation>> GetUserInvitations(Guid userId);

        Task<IEnumerable<string>> GetIdUsersWithSurveyInvitation(Guid surveyId);
    }
}
