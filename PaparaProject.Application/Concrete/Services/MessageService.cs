using AutoMapper;
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

        public async Task<APIResult> AddAsync(MessageCreateDto messageCreateDto)
        {
            var message = _mapper.Map<Message>(messageCreateDto);
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

                Message messageToDelete = _mapper.Map<Message>(result.Data);
                messageToDelete.Id = id;
                await _repository.DeleteAsync(messageToDelete);
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
                var messageDto = _mapper.Map<MessageCreateDto>(message);
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
                var messageToUpdate = result;
                messageToUpdate.IsNew = false;
                await UpdateAsync(messageToUpdate.Id, _mapper.Map<MessageCreateDto>(messageToUpdate));
                var message = _mapper.Map<MessageDto>(result);
                var apiResult = new APIResult { Success = true, Message = "By Id Message Brought", Data = message };
                return apiResult;
            }
        }

        public async Task<APIResult> UpdateAsync(int id, MessageCreateDto messageCreateDto)
        {
            var result = await GetByIdAsync(id);

            if (result.Success)
            {
                Message messageToUpdate = (Message)result.Data;
                var message = _mapper.Map<Message>(messageCreateDto);
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
