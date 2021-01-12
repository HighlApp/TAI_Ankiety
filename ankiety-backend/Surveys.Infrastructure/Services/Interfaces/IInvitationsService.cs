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

        Task<Response<StatusResponseDTO>> InviteUsersAsync(IEnumerable<Guid> UsersId);

        Task<Response<IEnumerable<UserDTO>>> GetUsersForInvitationAsync(int surveyId);

        Task<Response<IEnumerable<UserInvitationDTO>>> GetUserInvitationsAsync(Guid userId);

        Task<Response<StatusResponseDTO>> DeleteInvitationAsync(int invitationId);
    }
}
