using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaparaProject.Application.Dtos.CardDto;
using PaparaProject.Application.Interfaces.Services;
using PaparaProject.Infrastructure.PaymentService.Dtos.PaymentDtos;
using PaparaProject.Infrastructure.PaymentService.Utilities.Result;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace PaparaProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaysController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaysController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CardServiceResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CardServiceResult))]
        [HttpPost("pay")]
        public async Task<IActionResult> PayDues(int duesId,CardDto cardDto)
        {
            var result = await _paymentService.PayDuesAsync(duesId, cardDto);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }
    }
}
