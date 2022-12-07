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
    public class DuesService : IDuesService
    {
        readonly IDuesRepository _repository;
        readonly IMapper _mapper;
        public DuesService(IDuesRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<APIResult> AddAsync(DuesDto duesDto)
        {
            var dues = _mapper.Map<Dues>(duesDto);
            await _repository.AddAsync(dues);
            return new APIResult { Success= true, Message= "Dues Added", Data=dues };
        }

        public async Task<APIResult> DeleteAsync(int id)
        {
            var result = await GetByIdAsync(id);
            if (result.Success)
            {
                await _repository.DeleteAsync((Dues)result.Data);
                result.Data = null;
                result.Message = "Dues deleted";
                return result;
            }

            else return result;

        }

        public async Task<APIResult> GetAllAsync()
        {
            var dues = await _repository.GetAllAsync();
            var result = _mapper.Map<List<DuesDto>>(dues);
            return new APIResult { Success = true, Message = "Bringed", Data = result };
        }

        public async Task<APIResult> GetAllByPayFilterInvoicesAsync(bool isPaid)
        {
            if(isPaid)
            {
                var paidDues = await _repository.GetAllAsync(p => p.PaymentDate != null);
                var result = _mapper.Map<List<DuesDto>>(paidDues);
                return new APIResult { Success = true, Message = "Bringed", Data = result };
            }

            else
            {
                var unPaidDues = await _repository.GetAllAsync(p => p.PaymentDate == null);
                var result = _mapper.Map<List<DuesDto>>(unPaidDues);
                return new APIResult { Success = true, Message = "Bringed", Data = result };
            }
           
        }

        public async Task<APIResult> GetByIdAsync(int id)
        {
            var result = await _repository.GetAsync(p=>p.Id== id);
            if (result is null)
            {
                return new APIResult { Success = false, Message = "Not Found", Data = null };
            }
            else
            {
                var dues = _mapper.Map<DuesDto>(result);
                return new APIResult { Success = true, Message = "Found", Data = dues };
            }
        }

        public async Task<APIResult> UpdateAsync(int id, DuesDto duesDto)
        {
            var result = await GetByIdAsync(id);

            if (result.Success)
            {
                Dues duesToUpdate = (Dues)result.Data;
                var dues = _mapper.Map<Dues>(duesDto);
                dues.Id = duesToUpdate.Id;
                dues.LastUpdateAt = DateTime.Now;
                dues.IsDeleted = false;
                dues.CreatedDate = duesToUpdate.CreatedDate;
                await _repository.UpdateAsync(dues);
                result.Message = "Updated";
                result.Data = dues;

                return result;
            }

            else return result;
        }
    }
}
