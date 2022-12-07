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
    public class FlatService : IFlatService
    {
        readonly IFlatRepository _repository;
        readonly IMapper _mapper;

        public FlatService(IFlatRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<APIResult> AddAsync(FlatDto flatDto)
        {
            var flat = _mapper.Map<Flat>(flatDto);
            await _repository.AddAsync(flat);
            return new APIResult { Success = true, Message = "Flat Added", Data = flat };
        }

        public async Task<APIResult> DeleteAsync(int id)
        {
            var result = await GetByIdAsync(id);
            if (result.Success)
            {
                await _repository.DeleteAsync((Flat)result.Data);
                result.Data = null;
                result.Message = "Flat deleted";
                return result;
            }

            else return result;
        }

        public async Task<APIResult> GetAllAsync()
        {
            var flats = await _repository.GetAllAsync();
            var result = _mapper.Map<List<FlatDto>>(flats);
            return new APIResult { Success = true, Message = "Bringed", Data = result };
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
                var dues = _mapper.Map<FlatDto>(result);
                return new APIResult { Success = true, Message = "Found", Data = dues };
            }
        }

        public async Task<APIResult> UpdateAsync(int id, FlatDto flatDto)
        {
            var result = await GetByIdAsync(id);
           

            if (result.Success)
            {
                Flat flatToUpdate = (Flat)result.Data;
                var flat = _mapper.Map<Flat>(flatDto);
                flat.Id = flatToUpdate.Id;
                flat.LastUpdateAt = DateTime.Now;
                flat.IsDeleted = false;
                flat.CreatedDate = flatToUpdate.CreatedDate;
                await _repository.UpdateAsync(flat);
                result.Message = "Updated";
                result.Data = flat;

                return result;
            }

            else return result;
        }
    }
}
