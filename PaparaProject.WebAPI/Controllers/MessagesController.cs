using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaparaProject.Application.Dtos;
using PaparaProject.Application.Interfaces.Services;
using PaparaProject.Application.Utilities.Results;
using System.Threading.Tasks;

namespace PaparaProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        readonly IMessageService _service;

        public MessagesController(IMessageService service)
        {
            _service = service;
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResult))]
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResult))]
        [HttpGet("getallbyreadfilter")]
        public async Task<IActionResult> GetAllByReadFilterAsync([FromQuery] bool isReaded)
        {
            var result = await _service.GetAllByReadFilterAsync(isReaded);
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
        public async Task<IActionResult> Add(MessageDto messageDto)
        {
            var result = await _service.AddAsync(messageDto);
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
        public async Task<IActionResult> Update([FromQuery] int id, MessageDto messageDto)
        {
            var result = await _service.UpdateAsync(id, messageDto);
            if (result.Success)
                return Ok(result);
            else return NotFound(result);
        }
    }
}
