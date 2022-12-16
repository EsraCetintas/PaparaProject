using PaparaProject.Application.Dtos.FlatDtos;
using PaparaProject.Application.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Application.Interfaces.Services
{
    public interface IFlatService
    {
        Task<APIResult> GetAllFlatDtosAsync();
        Task<APIResult> GetByIdFlatDtoAsync(int id);
        Task<APIResult> AddAsync(FlatCreateDto flatCreateDto);
        Task<APIResult> DeleteAsync(int id);
        Task<APIResult> UpdateAsync(int id, FlatUpdateDto flatUpdateDto);

    }
}
