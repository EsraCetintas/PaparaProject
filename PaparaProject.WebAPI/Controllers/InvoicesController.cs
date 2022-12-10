using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using PaparaProject.Application.Dtos.InvoiceDtos;
using PaparaProject.Application.Interfaces.Infrastructure;
using PaparaProject.Application.Interfaces.Services;
using PaparaProject.Application.Utilities.IoC;
using PaparaProject.Application.Utilities.Results;
using PaparaProject.Infrastructure.MailService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PaparaProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {
        readonly IInvoiceService _service;
        readonly IMailService _mailService;
        public InvoicesController(IInvoiceService service, IMailService mailService)
        {
            _service = service;
            _mailService = mailService; 
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResult))]
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResult))]
        [HttpGet("getallbypayfilter")]
        public async Task<IActionResult> GetAllByPayFilterInvoicesAsync([FromQuery] bool isPaid)
        {
            
            var result = await _service.GetAllByPayFilterInvoicesAsync(isPaid);
            if(!isPaid)
            {
                List<string> mailAdress = new List<string>();
                
                foreach (var item in (List<InvoiceDto>)result.Data)
                {
                    mailAdress.Add(item.Flat.User.EMail);
                }
                _mailService.SendMailAsync(mailAdress);
            }
            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResult))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(APIResult))]
        [HttpGet("getbyid")]
        public async Task<IActionResult> GetByIdAsync([FromQuery] int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result.Success)
                return Ok(result);
            else return NotFound(result);
        }



        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResult))]
        [HttpPost("add")]
        public async Task<IActionResult> Add(InvoiceCreateDto invoiceCreateDto)
        {
            var result = await _service.AddAsync(invoiceCreateDto);
            return Ok(result);
        }


        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResult))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(APIResult))]
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            var result = await _service.DeleteAsync(id);
            if (result.Success)
                return Ok(result);
            else return NotFound(result);
        }


        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResult))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(APIResult))]
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromQuery] int id, InvoiceCreateDto invoiceCreateDto)
        {
            var result = await _service.UpdateAsync(id, invoiceCreateDto);
            if (result.Success)
                return Ok(result);
            else return NotFound(result);
        }
    }
}
