using Surveys.Core.Entities;
using Surveys.Core.Exceptions;
using Surveys.Infrastructure.Common;
using Surveys.Infrastructure.DTO;
using Surveys.Infrastructure.Repositories.Interfaces;
using Surveys.Infrastructure.Requests.Users.UpdateUser;
using Surveys.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Surveys.Infrastructure.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUserRepository _userRepository;

        public UsersService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Response<UserDTO>> DeleteAsync(Guid id)
        {
            User user = await _userRepository.FindByIdAsync(id.ToString());

            if (user == null)
                throw new NotFoundException("User not found.");

            await _userRepository.DeleteUser(user);

            return new Response<UserDTO>(MapToUserDTO(user));
        }

        public async Task<Response<IEnumerable<UserDTO>>> GetUsersAsync()
        {
            IEnumerable<User> users = await _userRepository.GetAllUsers();
            IEnumerable<UserDTO> response = users
                .Select(x => new UserDTO()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Surname = x.Surname,
                    Email = x.Email
                }).ToList();

            return new Response<IEnumerable<UserDTO>>(response);
        }

        public async Task<Response<UserDTO>> UpdateAsync(UpdateUserRequest request)
        {
            User user = await _userRepository.FindByIdAsync(request.Id.ToString());

            if (user == null)
                throw new NotFoundException("User not found.");

            user.Name = request.Name;
            user.Surname = request.Surname;
            user.Email = request.Email;

            await _userRepository.UpdateUser(user);

            return new Response<UserDTO>(MapToUserDTO(user));
        }

        private UserDTO MapToUserDTO(User user)
        {
            return new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                Surname = user.Surname,
                Name = user.Name
            };
        }
    }
}
