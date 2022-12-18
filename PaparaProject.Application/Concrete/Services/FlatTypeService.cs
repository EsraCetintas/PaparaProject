using AutoMapper;
using PaparaProject.Application.Aspects.Autofac.Caching;
using PaparaProject.Application.Aspects.Autofac.Security;
using PaparaProject.Application.Aspects.Autofac.Validation;
using PaparaProject.Application.Dtos.FlatTypeDtos;
using PaparaProject.Application.Interfaces.Persistence.Repositories;
using PaparaProject.Application.Interfaces.Services;
using PaparaProject.Application.Utilities.Results;
using PaparaProject.Application.ValidationRules.FluentValidation;
using PaparaProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Application.Concrete.Services
{
    public class FlatTypeService : IFlatTypeService
    {
        private readonly IFlatTypeRepository _repository;
        private readonly IMapper _mapper;

        public FlatTypeService(IFlatTypeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [SecuredOperationAspect("Admin")]
        [CacheRemoveAspect]
        [ValidationAspect(typeof(FlatTypeValidator))]
        public async Task<APIResult> AddAsync(FlatTypeDto flatTypeDto)
        {
            FlatType flatType = new FlatType();
            flatType.FlatTypeName = flatTypeDto.FlatTypeName;
            flatType.CreatedDate = DateTime.Now;
            flatType.LastUpdateAt = DateTime.Now;
            flatType.IsDeleted = false;
            await _repository.AddAsync(flatType);
            return new APIResult { Success = true, Message = "FlatType Added", Data = null };
        }

        [SecuredOperationAspect("Admin")]
        [CacheRemoveAspect]
        public async Task<APIResult> DeleteAsync(int id)
        {
            var flatTypeToDelete = await _repository.GetAsync(x => x.Id == id);
            if (flatTypeToDelete is null)
                return new APIResult { Success = false, Message = "Flat Type Not Found", Data = null };
            else
            {
                await _repository.DeleteAsync(flatTypeToDelete);
                return new APIResult { Success = true, Message = "Deleted Flat Type", Data = null };
            }
        }

        [SecuredOperationAspect("Admin")]
        [CacheAspect]
        public async Task<APIResult> GetAllFlatTypeDtosAsync()
        {
            var flatTypes = await _repository.GetAllAsync();
            var result = _mapper.Map<List<FlatTypeDto>>(flatTypes);
            return new APIResult { Success = true, Message = "All Flat Types Brought", Data = result };
        }

        [SecuredOperationAspect("Admin")]
        public async Task<APIResult> GetByIdFlatTypeDtoAsync(int id)
        {
            var result = await _repository.GetAsync(p => p.Id == id);
            if (result is null)
                return new APIResult { Success = false, Message = "Flat Type Not Found", Data = null };
            else
            {
                var flatType = _mapper.Map<FlatTypeDto>(result);
                return new APIResult { Success = true, Message = "By Pay Filter Flat Type Brought", Data = flatType };
            }
        }

        [SecuredOperationAspect("Admin")]
        [ValidationAspect(typeof(FlatTypeValidator))]
        [CacheRemoveAspect]
        public async Task<APIResult> UpdateAsync(int id, FlatTypeDto flatTypeDto)
        {
            FlatType flatTypeToUpdate = await _repository.GetAsync(x => x.Id == id);

            if (flatTypeToUpdate == null)
                return new APIResult { Success = false, Message = "Flat Type Not Found", Data = null };

            flatTypeToUpdate.LastUpdateAt = DateTime.Now;
            flatTypeToUpdate.FlatTypeName = flatTypeDto.FlatTypeName;
            await _repository.UpdateAsync(flatTypeToUpdate);

            return new APIResult { Success = true, Message = "Updated Flat Type", Data = null };
        }
    }
}
