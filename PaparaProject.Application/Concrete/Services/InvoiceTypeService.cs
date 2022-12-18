using AutoMapper;
using PaparaProject.Application.Aspects.Autofac.Caching;
using PaparaProject.Application.Aspects.Autofac.Security;
using PaparaProject.Application.Aspects.Autofac.Validation;
using PaparaProject.Application.Dtos.InvoiceTypeDtos;
using PaparaProject.Application.Interfaces.Persistence.Repositories;
using PaparaProject.Application.Interfaces.Services;
using PaparaProject.Application.Utilities.Results;
using PaparaProject.Application.ValidationRules.FluentValidation;
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
       private readonly IInvoiceTypeRepository _repository;
       private readonly IMapper _mapper;

        public InvoiceTypeService(IInvoiceTypeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [SecuredOperationAspect("Admin")]
        [ValidationAspect(typeof(InvoiceTypeValidator))]
        [CacheRemoveAspect]
        public async Task<APIResult> AddAsync(InvoiceTypeDto invoiceTypeDto)
        {
            InvoiceType invoiceType = new InvoiceType();
            invoiceType.InvoiceTypeName = invoiceTypeDto.InvoiceTypeName;
            invoiceType.CreatedDate = DateTime.Now;
            invoiceType.LastUpdateAt = DateTime.Now;
            invoiceType.IsDeleted = false;
            await _repository.AddAsync(invoiceType);

            return new APIResult { Success = true, Message = "Invoice Type Added", Data = invoiceType };
        }

        [SecuredOperationAspect("Admin")]
        [CacheRemoveAspect]
        public async Task<APIResult> DeleteAsync(int id)
        {
            var invoiceTypeDelete = await _repository.GetAsync(x => x.Id == id);
            if (invoiceTypeDelete is null)
                return new APIResult { Success = false, Message = "Invoice Type Not Found", Data = null };
            else
            {
                await _repository.DeleteAsync(invoiceTypeDelete);
                return new APIResult { Success = true, Message = "Deleted Invoice Type", Data = null };
            }
        }

        [SecuredOperationAspect("Admin")]
        [CacheAspect]
        public async Task<APIResult> GetAllInvoiceTypeDtosAsync()
        {
            var invoiceTypes = await _repository.GetAllAsync();
            var result = _mapper.Map<List<InvoiceTypeDto>>(invoiceTypes);
            return new APIResult { Success = true, Message = "All Invoice Types Brought", Data = result };
        }

        [SecuredOperationAspect("Admin")]
        public async Task<APIResult> GetInvoiceTypeDtoByIdAsync(int id)
        {
            var result = await _repository.GetAsync(p => p.Id == id);
            if (result is null)
                return new APIResult { Success = false, Message = "Invoice Type Not Found", Data = null };
            else
            {
                var invoiceTypes = _mapper.Map<InvoiceTypeDto>(result);
                return new APIResult { Success = true, Message = "By Id Invoice Type Brought", Data = invoiceTypes };
            }
        }

        [SecuredOperationAspect("Admin")]
        [ValidationAspect(typeof(InvoiceTypeValidator))]
        [CacheRemoveAspect]
        public async Task<APIResult> UpdateAsync(int id, InvoiceTypeDto invoiceTypeDto)
        {
            InvoiceType invoceTypeUpdate = await _repository.GetAsync(x => x.Id == id);

            if (invoceTypeUpdate is null)
                return new APIResult { Success = false, Message = "Invoice Type Not Found", Data = null };

            invoceTypeUpdate.LastUpdateAt = DateTime.Now;
            invoceTypeUpdate.IsDeleted = false;
            await _repository.UpdateAsync(invoceTypeUpdate);

            return new APIResult { Success = true, Message = "Updated Invoice Type", Data = null };
        }
    }
}
