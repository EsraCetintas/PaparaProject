using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaparaProject.Infrastructure.PaymentService.Dtos.CardDtos;
using PaparaProject.Infrastructure.PaymentService.Dtos.PaymentDtos;
using PaparaProject.Infrastructure.PaymentService.Model;
using PaparaProject.Infrastructure.PaymentService.Repositories.Interfaces;
using PaparaProject.Infrastructure.PaymentService.Services.Interfaces;
using PaparaProject.Infrastructure.PaymentService.Utilities.Result;
using System.Threading.Tasks;

namespace PaparaProject.PaymentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        readonly ICardService _cardService;

        public CardsController(ICardService cardService)
        {
            _cardService = cardService;
        }

        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CardServiceResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CardServiceResult))]
        [HttpPost("add")]
        public async Task<IActionResult> Add(CardCreateDto cardCreateDto)
        {
           var result = await _cardService.AddAsync(cardCreateDto);
            if (result.Result)
                return Ok(result);
            else return BadRequest(result);
        }

       
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CardServiceResult))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(CardServiceResult))]
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromQuery] string cardNo)
        {
            var result = await _cardService.DeleteAsync(cardNo);
            if (result.Result)
                return Ok(result);
            else return NotFound(result);
        }
        // TODO: Sil
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Card))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("findbycardno")]
        public async Task<IActionResult> FindByCardNo([FromQuery] string cardNo)
        {
            var result = await _cardService.FindByCardNoAsync(cardNo);
            if (result is null)
                return BadRequest(result);
            else return Ok(result);
        }


    }
}
