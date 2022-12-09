using PaparaProject.Application.Dtos.InvoiceTypeDtos;
using PaparaProject.Application.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Application.Interfaces.Services
{
    public interface IInvoiceTypeService
    {
        Task<APIResult> GetAllAsync();
        Task<APIResult> GetByIdAsync(int id);
        Task<APIResult> AddAsync(InvoiceTypeDto invoiceTypeDto);
        Task<APIResult> DeleteAsync(int id);
        Task<APIResult> UpdateAsync(int id, InvoiceTypeDto invoiceTypeDto);
    }
}
