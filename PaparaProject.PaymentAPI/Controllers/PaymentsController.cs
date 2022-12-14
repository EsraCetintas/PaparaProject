using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using PaparaProject.Application.Utilities.Results;
using PaparaProject.Infrastructure.PaymentService.Dtos.PaymentDtos;
using PaparaProject.Infrastructure.PaymentService.Model;
using PaparaProject.Infrastructure.PaymentService.Services.Interfaces;
using PaparaProject.Infrastructure.PaymentService.Utilities.Result;
using System.Threading.Tasks;

namespace PaparaProject.PaymentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private IPaymentService _paymentService;
        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;    
        }
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CardServiceResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CardServiceResult))]
        [HttpPost("pay")]
        public async Task<IActionResult> Pay(PaymentPayDto paymentPayDto)
        {
            var result = await _paymentService.Pay(paymentPayDto);

            if(!result.Result)
                return BadRequest(result.Message);

            return Ok(result);
        }
    }
}
