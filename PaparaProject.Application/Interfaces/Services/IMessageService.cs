using PaparaProject.Application.Dtos.MessageDtos;
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
        Task<APIResult> GetAllMessageDtosAsync();
        Task<APIResult> GetAllByUserMessageDtosAsync(int userId, bool isReaded);
        Task<APIResult> GetAllMessageDtosByReadFilterAsync(bool isReaded);
        Task<APIResult> GetMessageDtoByIdAsync(int id);
        Task<APIResult> AddAsync(MessageCreateDto messageCreateDto);
        Task<APIResult> DeleteAsync(int id);
        Task<APIResult> UpdateAsync(int id, MessageUpdateForUserDto messageUpdateDto);
    }
}
