using AutoMapper;
using PaparaProject.Application.Dtos;
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

        public async Task<APIResult> AddAsync(MessageDto messageDto)
        {
            var message = _mapper.Map<Message>(messageDto);
            message.CreatedDate = DateTime.Now;
            message.LastUpdateAt = DateTime.Now;
            message.IsDeleted = false;
            message.CreatedBy = 1;
            await _repository.AddAsync(message);
            return new APIResult { Success = true, Message = "Message Added", Data = message };
        }

        public async Task<APIResult> DeleteAsync(int id)
        {
            var result = await GetByIdAsync(id);
            if (result.Success)
            {
                await _repository.DeleteAsync((Message)result.Data);
                result.Data = null;
                result.Message = "Message Deleted";
                return result;
            }

            else return result;
        }

        public async Task<APIResult> GetAllAsync()
        {
            var messages = await _repository.GetAllAsync();
            var result = _mapper.Map<List<MessageDto>>(messages);

            foreach (var message in messages)
            {
                message.IsNew = false;
                int id = message.Id;
                var messageDto = _mapper.Map<MessageDto>(message);
                await UpdateAsync(id, messageDto);
            }

            return new APIResult { Success = true, Message = "All Messages Brought", Data = result };
        }

        public async Task<APIResult> GetAllByReadFilterAsync(bool isReaded)
        {
            List<Message> messages = null;

            if (isReaded)
                messages = await _repository.GetAllAsync(p => p.IsReaded == isReaded);
            else
                messages = await _repository.GetAllAsync(p => p.IsReaded == isReaded);

            var result = _mapper.Map<List<MessageDto>>(messages);
            return new APIResult { Success = true, Message = "By Read Filter Messages Brought", Data = result };
        }

        //Okundu burda olucak.
        public async Task<APIResult> GetByIdAsync(int id)
        {
            var result = await _repository.GetAsync(p => p.Id == id);
            if (result is null)
                return new APIResult { Success = false, Message = "Not Found", Data = null };
            else
            {
                var message = _mapper.Map<MessageDto>(result);
                var apiResult = new APIResult { Success = true, Message = "By Id Message Brought", Data = message };
                message.IsNew = false;
                return apiResult;
            }
        }

        public async Task<APIResult> UpdateAsync(int id, MessageDto messageDto)
        {
            var result = await GetByIdAsync(id);

            if (result.Success)
            {
                Message messageToUpdate = (Message)result.Data;
                var message = _mapper.Map<Message>(messageDto);
                message.Id = messageToUpdate.Id;
                message.LastUpdateAt = DateTime.Now;
                message.IsDeleted = messageToUpdate.IsDeleted;
                message.CreatedDate = messageToUpdate.CreatedDate;
                await _repository.UpdateAsync(message);
                result.Message = "Message Updated";
                result.Data = message;

                return result;
            }

            else return result;
        }
    }
}
