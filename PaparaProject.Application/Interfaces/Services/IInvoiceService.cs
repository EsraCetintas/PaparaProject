using PaparaProject.Application.Dtos.InvoiceDtos;
using PaparaProject.Application.Utilities.Results;
using PaparaProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Application.Interfaces.Services
{
    public interface IInvoiceService
    {
        Task<APIResult> GetAllInvoiceDtosAsync();
        Task<APIResult> GetAllByPayFilterInvoicesAsync(bool isPaid);
        Task<APIResult> GetAllByFlatInvoiceDtosAsync(int flatId, bool isPaid);
        Task<APIResult> GetInvoiceDtoByIdAsync(int id);
        Task<Invoice> GetInvoiceByIdAsync(int id);
        Task<APIResult> AddAsync(InvoiceCreateDto invoiceCreateDto);
        Task<APIResult> DeleteAsync(int id);
        Task<APIResult> UpdateAsync(int id, InvoiceUpdateDto invoiceUpdateDto);
        Task<APIResult> UpdateForPayAsync(int id);
    }
}
