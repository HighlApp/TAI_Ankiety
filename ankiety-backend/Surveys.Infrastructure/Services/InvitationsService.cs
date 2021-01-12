using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Surveys.Infrastructure.DTO;
using Surveys.Infrastructure.Common;
using Surveys.Infrastructure.Services.Interfaces;

namespace Surveys.Infrastructure.Services
{
    public class InvitationsService : IInvitationsService
    {
        public Task<Response<StatusResponseDTO>> DeleteInvitationAsync(int invitationId)
        {
            throw new NotImplementedException();
        }

        public Task<Response<IEnumerable<AdministratorInvitationDTO>>> GetAllInvitationsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Response<IEnumerable<UserInvitationDTO>>> GetUserInvitationsAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<Response<IEnumerable<UserDTO>>> GetUsersForInvitationAsync(int surveyId)
        {
            throw new NotImplementedException();
        }

        public Task<Response<StatusResponseDTO>> InviteUsersAsync(IEnumerable<Guid> UsersId)
        {
            throw new NotImplementedException();
        }
    }
}
