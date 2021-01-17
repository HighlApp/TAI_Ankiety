using MediatR;
using Surveys.Infrastructure.Common;
using Surveys.Infrastructure.DTO;
using System;

namespace Surveys.Infrastructure.Requests.Users.DeleteUser
{
    public class DeleteUserRequest : IRequest<Response<UserDTO>>
    {
        public DeleteUserRequest(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
