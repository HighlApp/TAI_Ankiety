using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Surveys.Infrastructure.DTO;
using Surveys.Infrastructure.Common;

namespace Surveys.Infrastructure.Requests.Invitations.InviteUsers
{
    public class InviteUsersRequestHandler :
        IRequestHandler<InviteUsersRequest, Response<StatusResponseDTO>>
    {
        public Task<Response<StatusResponseDTO>> Handle(InviteUsersRequest request, 
            CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
