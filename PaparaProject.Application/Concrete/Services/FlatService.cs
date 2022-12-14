using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PaparaProject.Application.Aspects.Autofac.Caching;
using PaparaProject.Application.Aspects.Autofac.Security;
using PaparaProject.Application.Aspects.Autofac.Validation;
using PaparaProject.Application.Dtos.DuesDtos;
using PaparaProject.Application.Dtos.FlatDtos;
using PaparaProject.Application.Interfaces.Persistence.Repositories;
using PaparaProject.Application.Interfaces.Services;
using PaparaProject.Application.Utilities.Results;
using PaparaProject.Application.ValidationRules.FluentValidation;
using PaparaProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Application.Concrete.Services
{
    public class FlatService : IFlatService
    {
        private readonly IFlatRepository _repository;
        private readonly IMapper _mapper;

        public FlatService(IFlatRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [SecuredOperationAspect("Admin")]
        [ValidationAspect(typeof(FlatValidator))]
        [CacheRemoveAspect]
        public async Task<APIResult> AddAsync(FlatCreateDto flatCreateDto)
        {
            Flat flat = new Flat();
            flat.FlatTypeId= flatCreateDto.FlatTypeId;
            flat.FlatState= flatCreateDto.FlatState;
            flat.FloorNo= flatCreateDto.FloorNo;
            flat.BlockNo= flatCreateDto.BlockNo;
            flat.FlatNo= flatCreateDto.FlatNo;
            flat.CreatedDate = DateTime.Now;
            flat.LastUpdateAt = DateTime.Now;
            flat.IsDeleted = false;

            await _repository.AddAsync(flat);
            return new APIResult { Success = true, Message = "Flat Added", Data = flat };
        }

        [SecuredOperationAspect("Admin")]
        [CacheRemoveAspect]
        public async Task<APIResult> DeleteAsync(int id)
        {
            var flatToDelete = await _repository.GetAsync(x => x.Id == id);
            if (flatToDelete is null)
                return new APIResult { Success = false, Message = "Flat Not Found", Data = null };
            else
            {
                await _repository.DeleteAsync(flatToDelete);
                return new APIResult { Success = true, Message = "Deleted Flat", Data = null };
            }
        }

        [SecuredOperationAspect("Admin")]
        [CacheAspect]
        public async Task<APIResult> GetAllFlatDtosAsync()
        {
            var flats = await _repository.GetAllAsync(includes: x => x.Include(x => x.FlatType));
            var result = _mapper.Map<List<FlatDto>>(flats);
            return new APIResult { Success = true, Message = "All Flats Brought", Data = result };
        }

        [SecuredOperationAspect("Admin, User")]
        [CacheAspect]
        public async Task<APIResult> GetByIdFlatDtoAsync(int id)
        {
            var result = await _repository.GetAsync(p => p.Id == id, includes: x => x.Include(x => x.FlatType));
            if (result is null)
                return new APIResult { Success = false, Message = "Flat Not Found", Data = null };
            else
            {
                var dues = _mapper.Map<FlatDto>(result);
                return new APIResult { Success = true, Message = "By Id Flat Brought", Data = dues };
            }
        }

        [SecuredOperationAspect("Admin")]
        [ValidationAspect(typeof(FlatValidator))]
        [CacheRemoveAspect]
        public async Task<APIResult> UpdateAsync(int id, FlatUpdateDto flatUpdateDto)
        {
            Flat flatToUpdate = await _repository.GetAsync(x => x.Id == id);

            if (flatToUpdate == null)
                return new APIResult { Success = false, Message = "Flat Not Found", Data = null };

            flatToUpdate.LastUpdateAt = DateTime.Now;
            flatToUpdate.FlatState = flatUpdateDto.FlatState;
            flatToUpdate.FlatNo = flatUpdateDto.FlatNo;
            flatToUpdate.BlockNo = flatUpdateDto.BlockNo;
            flatToUpdate.FloorNo = flatUpdateDto.FloorNo;
            flatToUpdate.FlatTypeId= flatUpdateDto.FlatTypeId;
            await _repository.UpdateAsync(flatToUpdate);

            return new APIResult { Success = true, Message = "Updated Flat", Data = null };
        }
    }
}
