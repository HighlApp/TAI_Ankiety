using MediatR;
using Surveys.Infrastructure.Common;
using Surveys.Infrastructure.DTO;
using System;

namespace Surveys.Infrastructure.Requests.Users.UpdateUser
{
    public class UpdateUserRequest : IRequest<Response<UserDTO>>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }
    }
}
