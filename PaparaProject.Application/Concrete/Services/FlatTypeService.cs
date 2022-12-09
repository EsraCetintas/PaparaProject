using AutoMapper;
using PaparaProject.Application.Dtos.FlatTypeDtos;
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
    public class FlatTypeService : IFlatTypeService
    {
        readonly IFlatTypeRepository _repository;
        readonly IMapper _mapper;

        public FlatTypeService(IFlatTypeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<APIResult> AddAsync(FlatTypeDto flatTypeDto)
        {
            var flatType = _mapper.Map<FlatType>(flatTypeDto);
            flatType.CreatedDate = DateTime.Now;
            flatType.LastUpdateAt = DateTime.Now;
            flatType.IsDeleted = false;
            flatType.CreatedBy = 1;
            await _repository.AddAsync(flatType);
            return new APIResult { Success = true, Message = "FlatType Added", Data = flatType };
        }

        public async Task<APIResult> DeleteAsync(int id)
        {
            var result = await GetByIdAsync(id);
            if (result.Success)
            {
                await _repository.DeleteAsync((FlatType)result.Data);
                result.Data = null;
                result.Message = "FlatType Deleted";
                return result;
            }

            else return result;
        }

        public async Task<APIResult> GetAllAsync()
        {
            var flatTypes = await _repository.GetAllAsync();
            var result = _mapper.Map<List<FlatTypeDto>>(flatTypes);
            return new APIResult { Success = true, Message = "All Flat Types Brought", Data = result };
        }


        public async Task<APIResult> GetByIdAsync(int id)
        {
            var result = await _repository.GetAsync(p => p.Id == id);
            if (result is null)
                return new APIResult { Success = false, Message = "Not Found", Data = null };
            else
            {
                var flatType = _mapper.Map<FlatTypeDto>(result);
                return new APIResult { Success = true, Message = "By Pay Filter Flat Type Brought", Data = flatType };
            }
        }

        public async Task<APIResult> UpdateAsync(int id, FlatTypeDto flatTypeDto)
        {
            var result = await GetByIdAsync(id);

            if (result.Success)
            {
                FlatType flatTypeToUpdate = (FlatType)result.Data;
                var flatType = _mapper.Map<FlatType>(flatTypeDto);
                flatType.Id = flatTypeToUpdate.Id;
                flatType.LastUpdateAt = DateTime.Now;
                flatType.IsDeleted = flatTypeToUpdate.IsDeleted;
                flatType.CreatedDate = flatTypeToUpdate.CreatedDate;
                await _repository.UpdateAsync(flatType);
                result.Message = "Flat Updated";
                result.Data = flatType;

                return result;
            }

            else return result;
        }
    }
}
