using PaparaProject.Application.Dtos.DuesDtos;
using PaparaProject.Application.Utilities.Results;
using PaparaProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Application.Interfaces.Services
{
    public interface IDuesService
    {
        Task<APIResult> GetAllAsync();
        Task<APIResult> GetAllByPayFilterDuesAsync(bool isPaid);
        Task<APIResult> GetByIdAsync(int id);
        Task<Dues> GetDuesByIdAsync(int id);
        Task<APIResult> AddAsync(DuesCreateDto duesCreateDto);
        Task<APIResult> DeleteAsync(int id);
        Task<APIResult> UpdateAsync(int id, DuesCreateDto DuesCreateDto);

    }
}
