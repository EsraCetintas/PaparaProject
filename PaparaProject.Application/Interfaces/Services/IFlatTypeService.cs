using PaparaProject.Application.Dtos.FlatTypeDtos;
using PaparaProject.Application.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Application.Interfaces.Services
{
    public interface IFlatTypeService
    {
        Task<APIResult> GetAllFlatTypeDtosAsync();
        Task<APIResult> GetByIdFlatTypeDtoAsync(int id);
        Task<APIResult> AddAsync(FlatTypeDto flatTypeDto);
        Task<APIResult> DeleteAsync(int id);
        Task<APIResult> UpdateAsync(int id, FlatTypeDto flatTypeDto);
    }
}
