using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PaparaProject.Application.Dtos.UserDtos;
using PaparaProject.Application.Interfaces.Persistence.Repositories;
using PaparaProject.Application.Interfaces.Services;
using PaparaProject.Application.Utilities.Results;
using PaparaProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Application.Concrete.Services
{
    public class UserService : IUserService
    {
        readonly IUserRepository _repository;
        readonly IMapper _mapper;

        public UserService(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<APIResult> AddAsync(User user)
        {
            user.CreatedDate = DateTime.Now;
            user.LastUpdateAt = DateTime.Now;
            user.IsDeleted = false;
            user.CreatedBy = 1;
            await _repository.AddAsync(user);
            return new APIResult { Success = true, Message = "User Added", Data = user };
        }

        public async Task<APIResult> DeleteAsync(int id)
        {
            var userDelete = await _repository.GetAsync(x => x.Id == id);
            if (userDelete is null)
                return new APIResult { Success = false, Message = "Not Found", Data = null };
            else
            {
                await _repository.DeleteAsync(userDelete);
                return new APIResult { Success = true, Message = "Deleted User", Data = null };
            }
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            var users = await _repository.GetAllAsync();
            return users;
        }

        public async Task<APIResult> GetAllUserDtosAsync()
        {
            var users = await _repository.GetAllAsync();
            var result = _mapper.Map<List<UserDto>>(users);
            return new APIResult { Success = true, Message = "All Users Brought", Data = result };
        }

        public async Task<APIResult> GetUserDtoByIdAsync(int id)
        {
            var result = await _repository.GetAsync(p => p.Id == id);
            if (result is null)
                return new APIResult { Success = false, Message = "Not Found", Data = null };
            else
            {
                var user = _mapper.Map<UserDto>(result);
                return new APIResult { Success = true, Message = "By Id User Brought", Data = user };
            }
        }

        public async Task<User> GetByMailAsync(string email)
        {
            var result = await _repository.GetAsync(u => u.EMail == email);
            return result;
        }

        public async Task<List<OperationClaim>> GetClaims(User user)
        {
            var result = await _repository.GetClaims(user);

            return result;
        }

        public async Task<APIResult> UpdateAsync(int id, UserUpdateDto userUpdateDto)
        {
            User userUpdate = await _repository.GetAsync(x => x.Id == id);

            if (userUpdate is null)
                return new APIResult { Success = false, Message = "User Not Found", Data = null };

            userUpdate.LastUpdateAt = DateTime.Now;
            userUpdate.IsDeleted = false;
            await _repository.UpdateAsync(userUpdate);

            return new APIResult { Success = true, Message = "Updated User", Data = null };
        }
    }
}
