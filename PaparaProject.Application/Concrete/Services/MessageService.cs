using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PaparaProject.Application.Aspects.Autofac.Security;
using PaparaProject.Application.Dtos.MessageDtos;
using PaparaProject.Application.Interfaces.Persistence.Repositories;
using PaparaProject.Application.Interfaces.Services;
using PaparaProject.Application.Utilities.Results;
using PaparaProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Application.Concrete.Services
{
    public class MessageService : IMessageService
    {
        readonly IMessageRepository _repository;
        readonly IMapper _mapper;

        public MessageService(IMessageRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        //[SecuredOperationAspect("Admin,User")]
        public async Task<APIResult> AddAsync(MessageCreateDto messageCreateDto)
        {
            Message message = new Message();
            message.Title = messageCreateDto.Title;
            message.Context = messageCreateDto.Context;
            message.UserId = messageCreateDto.UserId;
            message.CreatedDate = DateTime.Now;
            message.LastUpdateAt = DateTime.Now;
            message.IsNew = true;
            message.IsReaded = false;
            message.IsDeleted = false;

            await _repository.AddAsync(message);
            return new APIResult { Success = true, Message = "Message Added", Data = message };
        }

        [SecuredOperationAspect("Admin,User")]
        public async Task<APIResult> DeleteAsync(int id)
        {
            var messageDelete = await _repository.GetAsync(x => x.Id == id);
            if (messageDelete is null)
                return new APIResult { Success = false, Message = "Not Found", Data = null };
            else
            {
                await _repository.DeleteAsync(messageDelete);
                return new APIResult { Success = true, Message = "Deleted Message", Data = null };
            }
        }

        public  async Task<APIResult> GetAllByUserMessageDtosAsync(int userId, bool isReaded)
        {
            List<Message> messages = null;

            messages = await _repository.GetAllAsync(p => p.UserId == userId && p.IsReaded == isReaded, includes: x => x.Include(x => x.User));

            var result = _mapper.Map<List<MessageDto>>(messages);
            return new APIResult { Success = true, Message = "By Read Filter Messages Brought", Data = result };
        }

        //[SecuredOperationAspect("Admin")]
        public async Task<APIResult> GetAllMessageDtosAsync()
        {
            var messages = await _repository.GetAllAsync(includes: x => x.Include(x => x.User));
            var result = _mapper.Map<List<MessageDto>>(messages);
            
            foreach (var message in messages)
            {
                message.IsNew = false;
                int id = message.Id;
                var messageDto = _mapper.Map<MessageUpdateDto>(message);
                await UpdateAsync(id, messageDto);
            }

            return new APIResult { Success = true, Message = "All Messages Brought", Data = result };
        }

        [SecuredOperationAspect("Admin")]
        public async Task<APIResult> GetAllMessageDtosByReadFilterAsync(bool isReaded)
        {
            List<Message> messages = null;

            messages = await _repository.GetAllAsync(p => p.IsReaded == isReaded, includes: x => x.Include(x => x.User));
            
            var result = _mapper.Map<List<MessageDto>>(messages);
            return new APIResult { Success = true, Message = "By Read Filter Messages Brought", Data = result };
        }

        [SecuredOperationAspect("Admin,User")]
        public async Task<APIResult> GetMessageDtoByIdAsync(int id)
        {
            var result = await _repository.GetAsync(p => p.Id == id, includes: x => x.Include(x => x.User));
            if (result is null)
                return new APIResult { Success = false, Message = "Not Found", Data = null };
            else
            {
                var messageToUpdate = result;
                messageToUpdate.IsReaded = false;
                await UpdateAsync(messageToUpdate.Id, _mapper.Map<MessageUpdateDto>(messageToUpdate));
                var message = _mapper.Map<MessageDto>(result);
                var apiResult = new APIResult { Success = true, Message = "By Id Message Brought", Data = message };
                return apiResult;
            }
        }

        [SecuredOperationAspect("Admin,User")]
        public async Task<APIResult> UpdateAsync(int id, MessageUpdateForUserDto messageUpdateDto)
        {
            Message messageToUpdate = await _repository.GetAsync(x => x.Id == id);

            if (messageToUpdate is null)
                return new APIResult { Success = false, Message = "Message Not Found", Data = null };

            messageToUpdate.LastUpdateAt = DateTime.Now;
            messageToUpdate.Context = messageUpdateDto.Context;
            messageToUpdate.Title = messageUpdateDto.Title;
            await _repository.UpdateAsync(messageToUpdate);

            return new APIResult { Success = true, Message = "Updated Message", Data = null };
        }

        private async Task UpdateAsync(int id, MessageUpdateDto messageUpdateDto)
        {
            Message messageToUpdate = await _repository.GetAsync(x => x.Id == id);

            if (messageToUpdate is not null)
            {
                messageToUpdate.LastUpdateAt = DateTime.Now;
                messageToUpdate.Context = messageUpdateDto.Context;
                messageToUpdate.Title = messageUpdateDto.Title;
                messageToUpdate.IsNew = messageUpdateDto.IsNew;
                messageToUpdate.IsReaded = messageUpdateDto.IsReaded;  
                await _repository.UpdateAsync(messageToUpdate);
            }
        }
    }
}
