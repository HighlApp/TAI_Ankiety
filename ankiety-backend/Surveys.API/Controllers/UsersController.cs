using MediatR;
using Microsoft.AspNetCore.Mvc;
using Surveys.Infrastructure.Common;
using Surveys.Infrastructure.DTO;
using Surveys.Infrastructure.Requests.Users.DeleteUser;
using Surveys.Infrastructure.Requests.Users.GetUsers;
using Surveys.Infrastructure.Requests.Users.UpdateUser;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Surveys.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<Response<IEnumerable<UserDTO>>> GetAllUsers()
            => await _mediator.Send(new GetUsersRequest());

        [HttpDelete("{id}")]
        public async Task<Response<UserDTO>> DeleteUser(Guid id)
            => await _mediator.Send(new DeleteUserRequest(id));

        [HttpPut]
        public async Task<Response<UserDTO>> UpdateUser(UpdateUserRequest request)
            => await _mediator.Send(request);
    }
}
