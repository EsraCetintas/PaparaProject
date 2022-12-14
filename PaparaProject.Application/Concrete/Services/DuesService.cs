using AutoMapper;
using PaparaProject.Application.Aspects.Autofac.Security;
using PaparaProject.Application.Dtos.DuesDtos;
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

        [SecuredOperationAspect("Admin")]
        public async Task<APIResult> AddAsync(DuesCreateDto duesCreateDto)
        {
            var dues = _mapper.Map<Dues>(duesCreateDto);
            dues.CreatedDate = DateTime.Now;
            dues.LastUpdateAt = DateTime.Now;
            dues.IsDeleted = false;
            dues.CreatedBy = 2;
            await _repository.AddAsync(dues);
            return new APIResult { Success= true, Message= "Dues Added", Data=dues };
        }

        [SecuredOperationAspect("Admin")]
        public async Task<APIResult> DeleteAsync(int id)
        {
            var result = await GetByIdAsync(id);
            if (result.Success)
            {
                Dues duesToDelete = _mapper.Map<Dues>(result.Data);
                duesToDelete.Id = id;
                await _repository.DeleteAsync(duesToDelete);
                result.Data = null;
                result.Message = "Dues Deleted";
                return result;
            }

            else return result;

        }

        //[SecuredOperationAspect("Admin,User")]
        public async Task<APIResult> GetAllAsync()
        {
            var dues = await _repository.GetAllAsync();
            var result = _mapper.Map<List<DuesDto>>(dues);
            return new APIResult { Success = true, Message = "All Dues Brought", Data = result };
        }

        [SecuredOperationAspect("User")]
        public async Task<APIResult> GetAllByPayFilterDuesAsync(bool isPaid)
        {
            List<Dues> dues = null;

            if(isPaid)
                dues = await _repository.GetAllAsync(p => p.PaymentDate != null);

            else
                dues = await _repository.GetAllAsync(p => p.PaymentDate == null);

            var result = _mapper.Map<List<DuesDto>>(dues);
            return new APIResult { Success = true, Message = "By Pay Filter Dues Brought", Data = result };

        }

        [SecuredOperationAspect("Admin,User")]
        public async Task<APIResult> GetByIdAsync(int id)
        {
            var result = await _repository.GetAsync(p=>p.Id== id);
            if (result is null)
                return new APIResult { Success = false, Message = "Not Found", Data = null };
            else
            {
                var dues = _mapper.Map<DuesDto>(result);
                return new APIResult { Success = true, Message = "By Id Dues Brought", Data = dues };
            }
        }

        [SecuredOperationAspect("Admin")]
        public async Task<APIResult> UpdateAsync(int id, DuesCreateDto duesCreateDto)
        {
            var result = await GetByIdAsync(id);

            if (result.Success)
            {
                Dues duesToUpdate = (Dues)result.Data;
                var dues = _mapper.Map<Dues>(duesCreateDto);
                dues.Id = duesToUpdate.Id;
                dues.LastUpdateAt = DateTime.Now;
                dues.IsDeleted = duesToUpdate.IsDeleted;
                dues.CreatedDate = duesToUpdate.CreatedDate;
                await _repository.UpdateAsync(dues);
                result.Message = "Dues Updated";
                result.Data = dues;

                return result;
            }

            else return result;
        }
    }
}
