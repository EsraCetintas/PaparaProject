using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaparaProject.Application.Dtos.CardDto;
using PaparaProject.Application.Interfaces.Services;
using PaparaProject.Application.Utilities.Results;
using System.Threading.Tasks;

namespace PaparaProject.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(APIResult))]
        [HttpPost("paydues")]
        public async Task<IActionResult> PayDues(int duesId,  CreditCardDto creditCardDto)
        {
            var result = await _paymentService.PayDuesAsync(duesId, creditCardDto);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(APIResult))]
        [HttpPost("payinvoice")]
        public async Task<IActionResult> PayInvoice(int invoiceId, CreditCardDto creditCardDto)
        {
            var result = await _paymentService.PayInvoiceAsync(invoiceId, creditCardDto);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }
    }
}
