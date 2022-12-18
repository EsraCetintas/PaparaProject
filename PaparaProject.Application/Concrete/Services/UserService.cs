using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PaparaProject.Application.Aspects.Autofac.Caching;
using PaparaProject.Application.Aspects.Autofac.Security;
using PaparaProject.Application.Aspects.Autofac.Validation;
using PaparaProject.Application.Dtos.UserDtos;
using PaparaProject.Application.Interfaces.Persistence.Repositories;
using PaparaProject.Application.Interfaces.Services;
using PaparaProject.Application.Utilities.Results;
using PaparaProject.Application.ValidationRules.FluentValidation;
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
       private readonly IUserRepository _repository;
       private readonly IMapper _mapper;

        public UserService(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [SecuredOperationAspect("Admin")]
        [ValidationAspect(typeof(UserValidator))]
        [CacheRemoveAspect]
        public async Task<APIResult> AddAsync(User user)
        {
            user.CreatedDate = DateTime.Now;
            user.LastUpdateAt = DateTime.Now;
            user.IsDeleted = false;
            await _repository.AddAsync(user);
            return new APIResult { Success = true, Message = "User Added", Data = user };
        }

        [SecuredOperationAspect("Admin")]
        [CacheRemoveAspect]
        public async Task<APIResult> DeleteAsync(int id)
        {
            var userDelete = await _repository.GetAsync(x => x.Id == id);
            if (userDelete is null)
                return new APIResult { Success = false, Message = "User Not Found", Data = null };
            else
            {
                await _repository.DeleteAsync(userDelete);
                return new APIResult { Success = true, Message = "Deleted User", Data = null };
            }
        }

        [SecuredOperationAspect("Admin")]
        [CacheAspect]

        public async Task<List<User>> GetAllUsersAsync()
        {
            var users = await _repository.GetAllAsync();
            return users;
        }

        [SecuredOperationAspect("Admin")]
        [CacheAspect]
        public async Task<APIResult> GetAllUserDtosAsync()
        {
            var users = await _repository.GetAllAsync(includes: x => x.Include(x => x.Flat)
             .ThenInclude(x => x.FlatType));
            var result = _mapper.Map<List<UserDto>>(users);
            return new APIResult { Success = true, Message = "All Users Brought", Data = result };
        }

        [SecuredOperationAspect("Admin,User")]
        public async Task<APIResult> GetUserDtoByIdAsync(int id)
        {
            var result = await _repository.GetAsync(p => p.Id == id, includes: x => x.Include(x => x.Flat)
            .ThenInclude(x=>x.FlatType));
            if (result is null)
                return new APIResult { Success = false, Message = "User Not Found", Data = null };
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

        [SecuredOperationAspect("Admin,User")]
        [ValidationAspect(typeof(UserValidator))]
        [CacheRemoveAspect]
        public async Task<APIResult> UpdateAsync(int id, UserUpdateDto userUpdateDto)
        {
            User userUpdate = await _repository.GetAsync(x => x.Id == id);

            if (userUpdate is null)
                return new APIResult { Success = false, Message = "User Not Found", Data = null };

            userUpdate.LastUpdateAt = DateTime.Now;
            userUpdate.IsDeleted = false;
            userUpdate.NumberPlate = userUpdateDto.NumberPlate;
            userUpdate.PhoneNumber = userUpdateDto.PhoneNumber;
            userUpdate.SurName = userUpdateDto.SurName;
            userUpdate.Name = userUpdateDto.Name;
            userUpdate.IdentityNo = userUpdateDto.IdentityNo;
            userUpdate.EMail = userUpdateDto.EMail;
            userUpdate.FlatId= userUpdateDto.FlatId;

            await _repository.UpdateAsync(userUpdate);

            return new APIResult { Success = true, Message = "Updated User", Data = null };
        }
    }
}
