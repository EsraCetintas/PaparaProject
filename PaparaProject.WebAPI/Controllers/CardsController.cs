using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaparaProject.Application.Dtos.CardDto;
using PaparaProject.Application.Interfaces.Services;
using PaparaProject.Application.Utilities.Results;
using PaparaProject.Infrastructure.PaymentService.Utilities.Result;
using System.Threading.Tasks;

namespace PaparaProject.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly ICardService _cardService;
        public CardsController(ICardService cardService)
        {
            _cardService = cardService;
        }

        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(APIResult))]
        [HttpPost("add")]
        public async Task<IActionResult> Add(CardCreateDto cardCreateDto)
        {
            var result = await _cardService.SendCardAddRequest(cardCreateDto);

            if (!result.Success)
                return BadRequest(result);
            
            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CardServiceResult))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(CardServiceResult))]
        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromQuery] string cardNo)
        {
            var result = await _cardService.DeleteAsync(cardNo);
            if (result.Success)
                return Ok(result);
            else return NotFound(result);
        }
    }
}
