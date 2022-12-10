using AutoMapper;
using PaparaProject.Application.Dtos.FlatDtos;
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
    public class FlatService : IFlatService
    {
        readonly IFlatRepository _repository;
        readonly IMapper _mapper;

        public FlatService(IFlatRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<APIResult> AddAsync(FlatCreateDto flatCreateDto)
        {
            var flat = _mapper.Map<Flat>(flatCreateDto);
            flat.CreatedDate = DateTime.Now;
            flat.LastUpdateAt = DateTime.Now;
            flat.IsDeleted = false;
            flat.CreatedBy = 1;
            await _repository.AddAsync(flat);
            return new APIResult { Success = true, Message = "Flat Added", Data = flat };
        }

        public async Task<APIResult> DeleteAsync(int id)
        {
            var result = await GetByIdAsync(id);
            if (result.Success)
            {
                Flat flatToDelete = _mapper.Map<Flat>(result.Data);
                flatToDelete.Id = id;
                await _repository.DeleteAsync(flatToDelete);
                result.Data = null;
                result.Message = "Flat Deleted";
                return result;
            }

            else return result;
        }

        public async Task<APIResult> GetAllAsync()
        {
            var flats = await _repository.GetAllAsync();
            var result = _mapper.Map<List<FlatDto>>(flats);
            return new APIResult { Success = true, Message = "All Flats Brought", Data = result };
        }

        public async Task<APIResult> GetByIdAsync(int id)
        {
            var result = await _repository.GetAsync(p => p.Id == id);
            if (result is null)
                return new APIResult { Success = false, Message = "Not Found", Data = null };
            else
            {
                var dues = _mapper.Map<FlatDto>(result);
                return new APIResult { Success = true, Message = "By Id Flat Brought", Data = dues };
            }
        }

        public async Task<APIResult> UpdateAsync(int id, FlatCreateDto flatCreateDto)
        {
            var result = await GetByIdAsync(id);
           

            if (result.Success)
            {
                Flat flatToUpdate = (Flat)result.Data;
                var flat = _mapper.Map<Flat>(flatCreateDto);
                flat.Id = flatToUpdate.Id;
                flat.LastUpdateAt = DateTime.Now;
                flat.IsDeleted = flatToUpdate.IsDeleted;
                flat.CreatedDate = flatToUpdate.CreatedDate;
                await _repository.UpdateAsync(flat);
                result.Message = "Flat Updated";
                result.Data = flat;

                return result;
            }

            else return result;
        }
    }
}
