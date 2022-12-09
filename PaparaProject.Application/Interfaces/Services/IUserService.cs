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
        Task<APIResult> GetAllAsync();
        Task<APIResult> AddAsync(UserDto userDto);
        Task<APIResult> DeleteAsync(int id);
        Task<APIResult> UpdateAsync(int id, UserDto userDto);
        Task<APIResult> GetByIdAsync(int id);
        Task<APIResult> GetByMailAsync(string email);
    }
}
