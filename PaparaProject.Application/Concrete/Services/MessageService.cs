using AutoMapper;
using PaparaProject.Application.Dtos;
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
            await _repository.AddAsync(message);
            return new APIResult { Success = true, Message = "Invoice Added", Data = message };
        }

        public async Task<APIResult> DeleteAsync(int id)
        {
            var result = await GetByIdAsync(id);
            if (result.Success)
            {
                await _repository.DeleteAsync((Message)result.Data);
                result.Data = null;
                result.Message = "Message deleted";
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
                message.IsReaded = true;
                int id = message.Id;
                var messageDto = _mapper.Map<MessageDto>(message);
               await UpdateAsync(id, messageDto);
            }

            return new APIResult { Success = true, Message = "Bringed", Data = result };
        }

        public async Task<APIResult> GetAllByReadFilterAsync(bool isReaded)
        {
            if(isReaded)
            {
                var readMessages = await _repository.GetAllAsync(p => p.IsReaded == isReaded);
                var result = _mapper.Map<List<MessageDto>>(readMessages);
                return new APIResult { Success = true, Message = "Bringed", Data = result };
            }
            else
            {
                var unReadMessages = await _repository.GetAllAsync(p => p.IsReaded == isReaded);
                var result = _mapper.Map<List<MessageDto>>(unReadMessages);
                return new APIResult { Success = true, Message = "Bringed", Data = result };
            }
        }

        public async Task<APIResult> GetByIdAsync(int id)
        {
            var result = await _repository.GetAsync(p => p.Id == id);
            if (result is null)
            {
                return new APIResult { Success = false, Message = "Not Found", Data = null };
            }
            else
            {
                var message = _mapper.Map<MessageDto>(result);
                return new APIResult { Success = true, Message = "Found", Data = message };
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
                message.IsDeleted = false;
                message.CreatedDate = messageToUpdate.CreatedDate;
                await _repository.UpdateAsync(message);
                result.Message = "Updated";
                result.Data = message;

                return result;
            }

            else return result;
        }
    }
}
