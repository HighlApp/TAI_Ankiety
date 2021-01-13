using System;
using MediatR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Surveys.Infrastructure.DTO;
using System.Collections.Generic;
using Surveys.Infrastructure.Common;
using Surveys.Infrastructure.Requests.Invitations.InviteUsers;
using Surveys.Infrastructure.Requests.Invitations.GetInvitations;
using Surveys.Infrastructure.Requests.Invitations.DeleteInvitation;
using Surveys.Infrastructure.Requests.Invitations.GetUserInvitations;
using Surveys.Infrastructure.Requests.Invitations.GetUsersForInvitation;

namespace Surveys.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvitationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InvitationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //GET: api/invitations
        [HttpGet]
        public async Task<Response<IEnumerable<AdministratorInvitationDTO>>> GetInvitations()
            => await _mediator.Send(new GetInvitationsRequest());

        [HttpPost]
        //POST: api/invitations
        public async Task<Response<StatusResponseDTO>> InviteUsers(InviteUsersRequest request)
            => await _mediator.Send(request);

        [HttpGet]
        [Route("users/{surveyId}")]
        //GET: api/invitations/users/{surveyId}
        public async Task<Response<IEnumerable<UserDTO>>> GetUsersForInvitation(Guid surveyId)
            => await _mediator.Send(new GetUsersForInvitationRequest(surveyId));

        [HttpGet]
        [Route("user")]
        //GET: api/invitations/user
        public async Task<Response<IEnumerable<UserInvitationDTO>>> GetUserInvitations()
            => await _mediator.Send(new GetUserInvitationsRequest());

        [HttpDelete]
        [Route("{invitationId}")]
        //DELETE: api/invitations/{invitationId}
        public async Task<Response<StatusResponseDTO>> DeleteInvitation(DeleteInvitationRequest request)
            => await _mediator.Send(request);
    }
}
