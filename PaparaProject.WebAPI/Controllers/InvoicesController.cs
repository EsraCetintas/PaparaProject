using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using PaparaProject.Application.Dtos.InvoiceDtos;
using PaparaProject.Application.Dtos.UserDtos;
using PaparaProject.Application.Interfaces.Infrastructure;
using PaparaProject.Application.Interfaces.Services;
using PaparaProject.Application.Utilities.IoC;
using PaparaProject.Application.Utilities.Results;
using PaparaProject.Infrastructure.MailService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PaparaProject.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {
        readonly IInvoiceService _service;

        public InvoicesController(IInvoiceService service)
        {
            _service = service;
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResult))]
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllInvoiceDtosAsync();
            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResult))]
        [HttpGet("getallbypayfilter")]
        public async Task<IActionResult> GetAllByPayFilterInvoicesAsync([FromQuery] bool isPaid)
        {
            
            var result = await _service.GetAllByPayFilterInvoicesAsync(isPaid);
            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResult))]
        [HttpGet("getallbyflatinvoice")]
        public async Task<IActionResult> GetAllByFlatInvoiceDtosAsync([FromQuery]int flatId, [FromQuery]bool isPaid)
        {

            var result = await _service.GetAllByFlatInvoiceDtosAsync(flatId, isPaid);
            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResult))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(APIResult))]
        [HttpGet("getbyid")]
        public async Task<IActionResult> GetByIdAsync([FromQuery] int id)
        {
            var result = await _service.GetInvoiceDtoByIdAsync(id);
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
        public async Task<IActionResult> Update([FromQuery] int id, InvoiceUpdateDto ınvoiceUpdateDto)
        {
            var result = await _service.UpdateAsync(id, ınvoiceUpdateDto);
            if (result.Success)
                return Ok(result);
            else return NotFound(result);
        }
    }
}
