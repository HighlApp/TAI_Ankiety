using System;
using Surveys.Core.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Surveys.Infrastructure.Repositories.Interfaces
{
    public interface IInvitationsRepository : IRepositoryBase<Invitation>
    {
        Task<Invitation> GetInvitationWithUser(Guid invitationId);

        Task<Invitation> GetInvitationWithSurveyDetails(Guid invitation);

        Task<IEnumerable<Invitation>> GetInvitation();

        Task<IEnumerable<Invitation>> GetUserInvitations(Guid userId);

        Task<IEnumerable<string>> GetIdUsersWithSurveyInvitation(Guid surveyId);

        Task<IEnumerable<Invitation>> GetBySurveyId(Guid id);
    }
}
