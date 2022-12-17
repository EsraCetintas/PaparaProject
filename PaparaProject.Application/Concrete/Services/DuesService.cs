using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
            Dues dues = new Dues();
            dues.FlatId = duesCreateDto.FlatId;
            dues.AmountOfDues = duesCreateDto.AmountOfDues;
            dues.Deadline = duesCreateDto.Deadline;
            dues.CreatedDate = DateTime.Now;
            dues.LastUpdateAt = DateTime.Now;
            dues.IsDeleted = false;

            await _repository.AddAsync(dues);
            return new APIResult { Success = true, Message = "Dues Added", Data = null };
        }

        [SecuredOperationAspect("Admin")]
        public async Task<APIResult> DeleteAsync(int id)
        {
            var duesToDelete = await _repository.GetAsync(x => x.Id == id);
            if (duesToDelete is null)
                return new APIResult { Success = false, Message = "Not Found", Data = null };
            else
            {
                await _repository.DeleteAsync(duesToDelete);
                return new APIResult { Success = true, Message = "Deleted Dues", Data = null };
            }
        }

        [SecuredOperationAspect("Admin")]
        public async Task<APIResult> GetAllDuesDtosAsync()
        {
            var dues = await _repository.GetAllAsync(includes: x => x.Include(x => x.Flat));
            var result = _mapper.Map<List<DuesDto>>(dues);
            return new APIResult { Success = true, Message = "All Dues Brought", Data = result };
        }

        [SecuredOperationAspect("Admin")]
        public async Task<APIResult> GetAllDuesDtosByPayFilterAsync(bool isPaid)
        {
            List<Dues> dues = null;

            dues = await _repository.GetAllAsync(p => isPaid ? p.PaymentDate != null : p.PaymentDate == null,
                includes: x => x.Include(x => x.Flat));

            var result = _mapper.Map<List<DuesDto>>(dues);
            return new APIResult { Success = true, Message = "By Pay Filter Dues Brought", Data = result };

        }

        [SecuredOperationAspect("Admin,User")]
        public async Task<APIResult> GetDuesDtoByIdAsync(int id)
        {
            var result = await _repository.GetAsync(p => p.Id == id, includes: x => x.Include(x => x.Flat));
            if (result is null)
                return new APIResult { Success = false, Message = "Not Found", Data = null };
            else
            {
                var dues = _mapper.Map<DuesDto>(result);
                return new APIResult { Success = true, Message = "By Id Dues Brought", Data = dues };
            }
        }

        public async Task<Dues> GetDuesByIdAsync(int id)
        {
            var result = await _repository.GetAsync(p => p.Id == id, includes: x => x.Include(x => x.Flat));
            return result;
        }

        [SecuredOperationAspect("Admin")]
        public async Task<APIResult> UpdateAsync(int id, DuesUpdateDto duesUpdateDto)
        {
            Dues duesToUpdate = await _repository.GetAsync(x => x.Id == id);

            if (duesToUpdate == null)
                return new APIResult { Success = false, Message = "Dues Not Found", Data = null };

            duesToUpdate.LastUpdateAt = DateTime.Now;
            duesToUpdate.PaymentDate = duesUpdateDto.PaymentDate;
            duesToUpdate.Deadline= duesUpdateDto.Deadline;
            duesToUpdate.AmountOfDues = duesUpdateDto.AmountOfDues;
            duesToUpdate.FlatId= duesUpdateDto.FlatId;
            await _repository.UpdateAsync(duesToUpdate);

            return new APIResult { Success = true, Message = "Updated Dues", Data = null };
        }

        [SecuredOperationAspect("Admin, User")]
        public async Task<APIResult> GetAllDuesDtosByFlatAsync(int flatId, bool isPaid)
        {
            List<Dues> dues = null;

            dues = await _repository.GetAllAsync(p => p.FlatId == flatId && isPaid ? p.PaymentDate != null : p.PaymentDate == null,
                includes: x => x.Include(x => x.Flat));

            var result = _mapper.Map<List<DuesDto>>(dues);
            return new APIResult { Success = true, Message = "By Pay Filter Dues Brought", Data = result };
        }

        [SecuredOperationAspect("Admin, User")]
        public async Task<APIResult> UpdateForPayAsync(int id)
        {
            Dues duesToUpdate = await _repository.GetAsync(x => x.Id == id);

            if (duesToUpdate == null)
                return new APIResult { Success = false, Message = "Dues Not Found", Data = null };

            duesToUpdate.LastUpdateAt = DateTime.Now;
            duesToUpdate.PaymentDate = DateTime.Now;
            await _repository.UpdateAsync(duesToUpdate);

            return new APIResult { Success = true, Message = "Updated Dues", Data = null };
        }
    }
}
