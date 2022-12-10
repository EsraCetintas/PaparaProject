using AutoMapper;
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
            invoice.CreatedBy = 1;
            await _repository.AddAsync(invoice);
            return new APIResult { Success = true, Message = "Invoice Added", Data = invoice };
        }

        public async Task<APIResult> DeleteAsync(int id)
        {
            var result = await GetByIdAsync(id);
            if (result.Success)
            {
                Invoice invoiceToDelete = _mapper.Map<Invoice>(result.Data);
                invoiceToDelete.Id = id;
                await _repository.DeleteAsync(invoiceToDelete);
                result.Data = null;
                result.Message = "Invoice Deleted";
                return result;
            }

            else return result;
        }

        public async Task<APIResult> GetAllAsync()
        {
            var invoices = await _repository.GetAllAsync();
            var result = _mapper.Map<List<InvoiceDto>>(invoices);
            return new APIResult { Success = true, Message = "All Invoices Brought", Data = result };
        }

        public async Task<APIResult> GetAllByPayFilterInvoicesAsync(bool isPaid)
        {
            List<Invoice> invoices = null;

            if (isPaid)
                 invoices = await _repository.GetAllAsync(p => p.PaymentDate != null);
            else
                 invoices = await _repository.GetAllAsync(p => p.PaymentDate == null);

            var result = _mapper.Map<List<InvoiceDto>>(invoices);
            return new APIResult { Success = true, Message = "By Pay Filter Invoices Brought", Data = result };
        }

        public async Task<APIResult> GetByIdAsync(int id)
        {
            var result = await _repository.GetAsync(p => p.Id == id);
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
            var result = await GetByIdAsync(id);

            if (result.Success)
            {
                Invoice invoiceToUpdate = (Invoice)result.Data;
                var invoice = _mapper.Map<Invoice>(invoiceCreateDto);
                invoice.Id = invoiceToUpdate.Id;
                invoice.LastUpdateAt = DateTime.Now;
                invoice.IsDeleted = invoiceToUpdate.IsDeleted;
                invoice.CreatedDate = invoiceToUpdate.CreatedDate;
                await _repository.UpdateAsync(invoice);
                result.Message = "Invoice Updated";
                result.Data = invoice;
                return result;
            }

            else return result;
        }
    }
}
