using AutoMapper;
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

        public async Task<APIResult> AddAsync(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            user.CreatedDate = DateTime.Now;
            user.LastUpdateAt = DateTime.Now;
            user.IsDeleted = false;
            user.CreatedBy = 1;
            await _repository.AddAsync(user);
            return new APIResult { Success = true, Message = "User Added", Data = user };
        }

        public async Task<APIResult> DeleteAsync(int id)
        {
            var result = await GetByIdAsync(id);
            if (result.Success)
            {
                User userDelete = _mapper.Map<User>(result.Data);
                userDelete.Id = id;
                await _repository.DeleteAsync(userDelete);
                result.Data = null;
                result.Message = "User Deleted";
                return result;
            }

            else return result;
        }

        public async Task<APIResult> GetAllAsync()
        {
            var users = await _repository.GetAllAsync();
            var result = _mapper.Map<List<UserDto>>(users);
            return new APIResult { Success = true, Message = "All Users Brought", Data = result };
        }

        public async Task<APIResult> GetByIdAsync(int id)
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

        public async Task<APIResult> GetByMailAsync(string email)
        {
           var result = await _repository.GetAsync(u => u.EMail == email);
            if (result is null)
                return new APIResult { Success = false, Message = "Not Found", Data = null };

            else return new APIResult { Success = true, Message = "By Mail User Brought", Data = result };
        }

        public async Task<APIResult> UpdateAsync(int id, UserDto userDto)
        {
            var result = await GetByIdAsync(id);

            if (result.Success)
            {
                User userToUpdate = (User)result.Data;
                var user = _mapper.Map<User>(userDto);
                user.Id = userToUpdate.Id;
                user.LastUpdateAt = DateTime.Now;
                user.IsDeleted = userToUpdate.IsDeleted;
                user.CreatedDate = userToUpdate.CreatedDate;
                await _repository.UpdateAsync(user);
                result.Message = "User Updated";
                result.Data = user;

                return result;
            }

            else return result;
        }
    }
}
