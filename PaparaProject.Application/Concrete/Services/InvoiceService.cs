using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PaparaProject.Application.Aspects.Autofac.Security;
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

        [SecuredOperationAspect("Admin")]
        public async Task<APIResult> AddAsync(InvoiceCreateDto invoiceCreateDto)
        {
            Invoice invoice = new Invoice();
            invoice.FlatId= invoiceCreateDto.FlatId;
            invoice.InvoiceTypeId= invoiceCreateDto.InvoiceTypeId;
            invoice.AmountOfInvoice= invoiceCreateDto.AmountOfInvoice;
            invoice.Deadline= invoiceCreateDto.Deadline;
            invoice.CreatedDate = DateTime.Now;
            invoice.LastUpdateAt = DateTime.Now;
            invoice.IsDeleted = false;
            await _repository.AddAsync(invoice);
            return new APIResult { Success = true, Message = "Invoice Added", Data = invoice };
        }

        [SecuredOperationAspect("Admin")]
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

        [SecuredOperationAspect("Admin")]
        public async Task<APIResult> GetAllInvoiceDtosAsync()
        {
            var invoices = await _repository.GetAllAsync(includes: x => x.Include(x => x.Flat)
            .Include(x => x.InvoiceType));
            var result = _mapper.Map<List<InvoiceDto>>(invoices);
            return new APIResult { Success = true, Message = "By Pay Filter Invoices Brought", Data = result };
        }

        [SecuredOperationAspect("Admin")]
        public async Task<APIResult> GetAllByPayFilterInvoicesAsync(bool isPaid)
        {
            List<Invoice> invoices = null;

            invoices = await _repository.GetAllAsync(p => isPaid ? p.PaymentDate != null : p.PaymentDate == null, includes: x => x.Include(x => x.Flat)
           .Include(x => x.InvoiceType));

            if(!isPaid)
                await this.SendUnPaidInvoiceMail(invoices);

            var result = _mapper.Map<List<InvoiceDto>>(invoices);
            return new APIResult { Success = true, Message = "By Pay Filter Invoices Brought", Data = result };
        }

        [SecuredOperationAspect("User")]
        public async Task<APIResult> GetAllByFlatInvoiceDtosAsync(int flatId, bool isPaid)
        {
            var invoices = await _repository.GetAllAsync(p => p.FlatId == flatId && isPaid ? p.PaymentDate != null : p.PaymentDate == null,
                includes: x => x.Include(x => x.Flat)
                .Include(x => x.InvoiceType));           

            var result = _mapper.Map<List<InvoiceDto>>(invoices);
            return new APIResult { Success = true, Message = "By Flat Invoices Brought and Sent Mail", Data = result };
        }

        [SecuredOperationAspect("Admin, User")]
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

        [SecuredOperationAspect("Admin")]
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

        private async Task SendUnPaidInvoiceMail(List<Invoice> invoices)
        {
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
        }

        [SecuredOperationAspect("Admin, User")]
        public async Task<APIResult> UpdateForPayAsync(int id)
        {
            Invoice invoceToUpdate = await _repository.GetAsync(x => x.Id == id);

            if (invoceToUpdate is null)
                return new APIResult { Success = false, Message = "Not Found", Data = null };

            invoceToUpdate.LastUpdateAt= DateTime.Now;
            invoceToUpdate.PaymentDate = DateTime.Now;
            await _repository.UpdateAsync(invoceToUpdate);

            return new APIResult { Success = true, Message = "Updated Invoice", Data = null };
        }
    }
}
