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
    public class InvoiceTypeService : IInvoiceTypeService
    {
        readonly IInvoiceTypeRepository _repository;
        readonly IMapper _mapper;

        public InvoiceTypeService(IInvoiceTypeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<APIResult> AddAsync(InvoiceTypeDto invoiceTypeDto)
        {
            var invoiceType = _mapper.Map<InvoiceType>(invoiceTypeDto);
            invoiceType.CreatedDate = DateTime.Now;
            invoiceType.LastUpdateAt = DateTime.Now;
            invoiceType.IsDeleted = false;
            invoiceType.CreatedBy = 1;
            await _repository.AddAsync(invoiceType);
            return new APIResult { Success = true, Message = "Invoice Added", Data = invoiceType };
        }

        public async Task<APIResult> DeleteAsync(int id)
        {
            var result = await GetByIdAsync(id);
            if (result.Success)
            {
                await _repository.DeleteAsync((InvoiceType)result.Data);
                result.Data = null;
                result.Message = "InvoiceType Deleted";
                return result;
            }

            else return result;
        }

        public async Task<APIResult> GetAllAsync()
        {
            var invoiceTypes = await _repository.GetAllAsync();
            var result = _mapper.Map<List<InvoiceTypeDto>>(invoiceTypes);
            return new APIResult { Success = true, Message = "All Invoice Types Brought", Data = result };
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
                var invoiceTypes = _mapper.Map<InvoiceTypeDto>(result);
                return new APIResult { Success = true, Message = "By Id Invoice Brought", Data = invoiceTypes };
            }
        }

        public async Task<APIResult> UpdateAsync(int id, InvoiceTypeDto invoiceTypeDto)
        {
            var result = await GetByIdAsync(id);

            if (result.Success)
            {
                InvoiceType invoiceToUpdate = (InvoiceType)result.Data;
                var invoiceType = _mapper.Map<InvoiceType>(invoiceTypeDto);
                invoiceType.Id = invoiceToUpdate.Id;
                invoiceType.LastUpdateAt = DateTime.Now;
                invoiceType.IsDeleted = invoiceToUpdate.IsDeleted;
                invoiceType.CreatedDate = invoiceToUpdate.CreatedDate;
                await _repository.UpdateAsync(invoiceType);
                result.Message = "Invoice Type Updated";
                result.Data = invoiceType;

                return result;
            }

            else return result;
        }
    }
}
