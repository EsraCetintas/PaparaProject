using PaparaProject.Application.Dtos.UserDtos;
using PaparaProject.Application.Utilities.Results;
using PaparaProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<APIResult> GetAllUserDtosAsync();
        Task<List<User>> GetAllUsersAsync();
        Task<APIResult> AddAsync(User user);
        Task<APIResult> DeleteAsync(int id);
        Task<APIResult> UpdateAsync(int id, UserUpdateDto userUpdateDto);
        Task<APIResult> GetUserDtoByIdAsync(int id);
        Task<List<OperationClaim>> GetClaims(User user);
        Task<User> GetByMailAsync(string email);
    }
}
