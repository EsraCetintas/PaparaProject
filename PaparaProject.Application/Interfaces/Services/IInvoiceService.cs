using PaparaProject.Application.Dtos.InvoiceDtos;
using PaparaProject.Application.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Application.Interfaces.Services
{
    public interface IInvoiceService
    {
        Task<APIResult> GetAllAsync();
        Task<APIResult> GetAllByPayFilterInvoicesAsync(bool isPaid);
        Task<List<InvoiceDto>> GetAllUnPaidInvoicesAsync();
        Task<APIResult> GetByIdAsync(int id);
        Task<APIResult> AddAsync(InvoiceCreateDto invoiceCreateDto);
        Task<APIResult> DeleteAsync(int id);
        Task<APIResult> UpdateAsync(int id, InvoiceCreateDto invoiceCreateDto);
    }
}
