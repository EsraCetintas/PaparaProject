using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PaparaProject.Application.Dtos.DuesDtos;
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
            var flatToDelete = await _repository.GetAsync(x => x.Id == id);
            if (flatToDelete is null)
                return new APIResult { Success = false, Message = "Not Found", Data = null };
            else
            {
                await _repository.DeleteAsync(flatToDelete);
                return new APIResult { Success = true, Message = "Deleted Flat", Data = null };
            }
        }

        public async Task<APIResult> GetAllFlatDtosAsync()
        {
            var flats = await _repository.GetAllAsync(includes: x => x.Include(x => x.FlatType));
            var result = _mapper.Map<List<FlatDto>>(flats);
            return new APIResult { Success = true, Message = "All Flats Brought", Data = result };
        }

        public async Task<APIResult> GetByIdFlatDtoAsync(int id)
        {
            var result = await _repository.GetAsync(p => p.Id == id, includes: x => x.Include(x => x.FlatType));
            if (result is null)
                return new APIResult { Success = false, Message = "Not Found", Data = null };
            else
            {
                var dues = _mapper.Map<FlatDto>(result);
                return new APIResult { Success = true, Message = "By Id Flat Brought", Data = dues };
            }
        }

        public async Task<APIResult> UpdateAsync(int id, FlatUpdateDto flatUpdateDto)
        {
            Flat flatToUpdate = await _repository.GetAsync(x => x.Id == id);

            if (flatToUpdate == null)
                return new APIResult { Success = false, Message = "Not Found", Data = null };

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
