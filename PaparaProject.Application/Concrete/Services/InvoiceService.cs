using AutoMapper;
using PaparaProject.Application.Dtos;
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

        public async Task<APIResult> AddAsync(InvoiceDto invoiceDto)
        {
            var invoice = _mapper.Map<Invoice>(invoiceDto);
            await _repository.AddAsync(invoice);
            return new APIResult { Success = true, Message = "Invoice Added", Data = invoice };
        }

        public async Task<APIResult> DeleteAsync(int id)
        {
            var result = await GetByIdAsync(id);
            if (result.Success)
            {
                await _repository.DeleteAsync((Invoice)result.Data);
                result.Data = null;
                result.Message = "Invoice deleted";
                return result;
            }

            else return result;
        }

        public async Task<APIResult> GetAllAsync()
        {
            var invoices = await _repository.GetAllAsync();
            var result = _mapper.Map<List<InvoiceDto>>(invoices);
            return new APIResult { Success = true, Message = "Bringed", Data = result };
        }

        public async Task<APIResult> GetAllByPayFilterInvoicesAsync(bool isPaid)
        {
            if(isPaid)
            {
                var paidInvoices = await _repository.GetAllAsync(p => p.PaymentDate != null);
                var result = _mapper.Map<List<InvoiceDto>>(paidInvoices);
                return new APIResult { Success = true, Message = "Bringed", Data = result };
            }

            else
            {
                var unPaidInvoices = await _repository.GetAllAsync(p => p.PaymentDate == null);
                var result = _mapper.Map<List<InvoiceDto>>(unPaidInvoices);
                return new APIResult { Success = true, Message = "Bringed", Data = result };
            }
        }

        public async Task<APIResult> GetByIdAsync(int id)
        {
            var result = await _repository.GetAsync(p => p.Id == id);
            if (result is null)
            {
                return new APIResult { Success = false, Message = "Not Found", Data = null };
            }
            else
            {
                var invoices = _mapper.Map<InvoiceDto>(result);
                return new APIResult { Success = true, Message = "Found", Data = invoices };
            }
        }

        public async Task<APIResult> UpdateAsync(int id, InvoiceDto invoiceDto)
        {
            var result = await GetByIdAsync(id);
            

            if (result.Success)
            {
                Invoice invoiceToUpdate = (Invoice)result.Data;
                var invoice = _mapper.Map<Invoice>(invoiceDto);
                invoice.Id = invoiceToUpdate.Id;
                invoice.LastUpdateAt = DateTime.Now;
                invoice.IsDeleted = false;
                invoice.CreatedDate = invoiceToUpdate.CreatedDate;
                await _repository.UpdateAsync(invoice);
                result.Message = "Updated";
                result.Data = invoice;

                return result;
            }

            else return result;
        }
    }
}
