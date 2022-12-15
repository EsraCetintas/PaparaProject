using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PaparaProject.Application.Dtos.InvoiceDtos;
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
    public class InvoiceService : IInvoiceService
    {
        readonly IInvoiceRepository _repository;
        readonly IMapper _mapper;

        public InvoiceService(IInvoiceRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<APIResult> AddAsync(InvoiceCreateDto invoiceCreateDto)
        {
            var invoice = _mapper.Map<Invoice>(invoiceCreateDto);
            invoice.CreatedDate = DateTime.Now;
            invoice.LastUpdateAt = DateTime.Now;
            invoice.IsDeleted = false;
            invoice.CreatedBy = 2;
            await _repository.AddAsync(invoice);
            return new APIResult { Success = true, Message = "Invoice Added", Data = invoice };
        }

        public async Task<APIResult> DeleteAsync(int id)
        {
            var invoiceDelete = await _repository.GetAsync(x => x.Id == id);
            if (invoiceDelete is null)
                return new APIResult { Success = false, Message = "Not Found", Data = null };
            else
            {
                await _repository.DeleteAsync(invoiceDelete);
                return new APIResult { Success = true, Message = "Deleted Invoice", Data = null };
            }
        }

        public async Task<APIResult> GetAllAsync()
        {
            var invoices = await _repository.GetAllAsync(includes: x => x.Include(x => x.Flat)
            .Include(x => x.InvoiceType));
            var result = _mapper.Map<List<InvoiceDto>>(invoices);
            return new APIResult { Success = true, Message = "By Pay Filter Invoices Brought", Data = result };
        }

        public async Task<APIResult> GetAllByPayFilterInvoicesAsync(bool isPaid)
        {
            List<Invoice> invoices = null;

            if (isPaid)
                invoices = await _repository.GetAllAsync(p => p.PaymentDate != null, includes: x => x.Include(x => x.Flat)
           .Include(x => x.InvoiceType));
            else
                invoices = await _repository.GetAllAsync(p => p.PaymentDate == null, includes: x => x.Include(x => x.Flat)
           .Include(x => x.InvoiceType));

            var result = _mapper.Map<List<InvoiceDto>>(invoices);
            return new APIResult { Success = true, Message = "By Pay Filter Invoices Brought", Data = result };
        }

        public async Task<List<InvoiceDto>> GetAllUnPaidInvoicesAsync()
        {
            var invoices = await _repository.GetAllAsync(p => p.PaymentDate == null,
                includes: x => x.Include(x => x.Flat)
                .Include(x => x.InvoiceType));
            var result = _mapper.Map<List<InvoiceDto>>(invoices);
            return result;
        }

        public async Task<APIResult> GetByIdAsync(int id)
        {
            var result = await _repository.GetAsync(p => p.Id == id, includes: x => x.Include(x => x.Flat)
            .Include(x => x.InvoiceType));
            if (result is null)
                return new APIResult { Success = false, Message = "Not Found", Data = null };
            else
            {
                var invoice = _mapper.Map<InvoiceDto>(result);
                return new APIResult { Success = true, Message = "By Id Invoice Brought", Data = invoice };
            }
        }

        public async Task<APIResult> UpdateAsync(int id, InvoiceCreateDto invoiceCreateDto)
        {
            Invoice invoceUpdate = await _repository.GetAsync(x => x.Id == id);

            if (invoceUpdate is null)
                return new APIResult { Success = false, Message = "Not Found", Data = null };

            invoceUpdate.LastUpdateAt = DateTime.Now;
            invoceUpdate.IsDeleted = false;
            await _repository.UpdateAsync(invoceUpdate);

            return new APIResult { Success = true, Message = "Updated Invoice", Data = null };
        }
    }
}
