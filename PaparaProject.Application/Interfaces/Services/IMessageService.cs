using PaparaProject.Application.Dtos;
using PaparaProject.Application.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Application.Interfaces.Services
{
    public interface IMessageService
    {
        Task<APIResult> GetAllAsync();
        Task<APIResult> GetAllByReadFilterAsync(bool isReaded);
        Task<APIResult> GetByIdAsync(int id);
        Task<APIResult> AddAsync(MessageDto messageDto);
        Task<APIResult> DeleteAsync(int id);
        Task<APIResult> UpdateAsync(int id, MessageDto messageDto);
    }
}
