using Surveys.Infrastructure.Common;
using Surveys.Infrastructure.DTO;
using Surveys.Infrastructure.Requests.Users.UpdateUser;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Surveys.Infrastructure.Services.Interfaces
{
    public interface IUsersService
    {
        Task<Response<IEnumerable<UserDTO>>> GetUsersAsync();

        Task<Response<UserDTO>> DeleteAsync(Guid id);

        Task<Response<UserDTO>> UpdateAsync(UpdateUserRequest request);
    }
}
