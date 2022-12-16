using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PaparaProject.Application.Dtos.InvoiceDtos;
using PaparaProject.Application.Interfaces.Infrastructure;
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
        readonly IUserService _userService;
        readonly IMailService _mailService;

        public InvoiceService(IInvoiceRepository repository, IMapper mapper, IUserService userService, IMailService mailService)
        {
            _repository = repository;
            _mapper = mapper;
            _userService = userService;
            _mailService = mailService;
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

        public async Task<APIResult> GetAllInvoiceDtosAsync()
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

        public async Task<APIResult> GetAllUnPaidInvoiceDtosAsync()
        {
            var invoices = await _repository.GetAllAsync(p => p.PaymentDate == null,
                includes: x => x.Include(x => x.Flat)
                .Include(x => x.InvoiceType));

            List<string> mailAdress = new List<string>();
            var users = await _userService.GetAllUsersAsync();
            foreach (var user in users)
            {
                foreach (var invoice in invoices)
                {
                    if (user.FlatId == invoice.FlatId)
                    {
                        mailAdress.Add(user.EMail);
                    }
                }
                await _mailService.SendMailAsync(mailAdress);
            }
            var result = _mapper.Map<List<InvoiceDto>>(invoices);
            return new APIResult { Success = true, Message = "By UnPaid Filter Invoices Brought and Sent Mail", Data = result };
        }

        public async Task<APIResult> GetInvoiceDtoByIdAsync(int id)
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

        public async Task<Invoice> GetInvoiceByIdAsync(int id)
        {
            var result = await _repository.GetAsync(p => p.Id == id, includes: x => x.Include(x => x.InvoiceType));
            return result;
        }

        public async Task<APIResult> UpdateAsync(int id, InvoiceUpdateDto invoiceUpdateDto)
        {
            Invoice invoceToUpdate = await _repository.GetAsync(x => x.Id == id);

            if (invoceToUpdate is null)
                return new APIResult { Success = false, Message = "Not Found", Data = null };

            invoceToUpdate.LastUpdateAt = DateTime.Now;
            invoceToUpdate.FlatId = invoiceUpdateDto.FlatId;
            invoceToUpdate.AmountOfInvoice = invoiceUpdateDto.AmountOfInvoice;
            invoceToUpdate.PaymentDate = invoiceUpdateDto.PaymentDate;
            invoceToUpdate.Deadline = invoiceUpdateDto.Deadline;
            invoceToUpdate.InvoiceTypeId = invoiceUpdateDto.InvoiceTypeId;
            await _repository.UpdateAsync(invoceToUpdate);

            return new APIResult { Success = true, Message = "Updated Invoice", Data = null };
        }
    }
}
