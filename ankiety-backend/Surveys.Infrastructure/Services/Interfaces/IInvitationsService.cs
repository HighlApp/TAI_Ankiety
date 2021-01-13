using System;
using System.Threading.Tasks;
using Surveys.Infrastructure.DTO;
using System.Collections.Generic;
using Surveys.Infrastructure.Common;

namespace Surveys.Infrastructure.Services.Interfaces
{
    public interface IInvitationsService
    {
        Task<Response<IEnumerable<AdministratorInvitationDTO>>> GetAllInvitationsAsync();

        Task<Response<StatusResponseDTO>> InviteUsersAsync(IEnumerable<Guid> usersId, InvitationDetailsDTO details);

        Task<Response<IEnumerable<UserDTO>>> GetUsersForInvitationAsync(Guid surveyId);

        Task<Response<IEnumerable<UserInvitationDTO>>> GetUserInvitationsAsync();

        Task<Response<StatusResponseDTO>> DeleteInvitationAsync(Guid invitationId);
    }
}
