using PaparaProject.Application.Dtos;
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
        Task<APIResult> GetAllAsync();
        Task<APIResult> GetByIdAsync(int id);
        Task<APIResult> AddAsync(FlatDto flatDto);
        Task<APIResult> DeleteAsync(int id);
        Task<APIResult> UpdateAsync(int id, FlatDto flatDto);

    }
}
