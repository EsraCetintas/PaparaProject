using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaparaProject.Application.Dtos.FlatDtos;
using PaparaProject.Application.Interfaces.Services;
using PaparaProject.Application.Utilities.Results;
using System.Threading.Tasks;

namespace PaparaProject.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FlatsController : ControllerBase
    {
        private readonly IFlatService _service;

        public FlatsController(IFlatService service)
        {
            _service = service;
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResult))]
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllFlatDtosAsync();
            return Ok(result);
        }


        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResult))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(APIResult))]
        [HttpGet("getbyid")]
        public async Task<IActionResult> GetByIdAsync([FromQuery] int id)
        {
            var result = await _service.GetByIdFlatDtoAsync(id);
            if (result.Success)
                return Ok(result);
            else return NotFound(result);
        }



        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResult))]
        [HttpPost("add")]
        public async Task<IActionResult> Add(FlatCreateDto flatCreateDto)
        {
            var result = await _service.AddAsync(flatCreateDto);
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
        public async Task<IActionResult> Update([FromQuery] int id, FlatUpdateDto flatUpdateDto)
        {
            var result = await _service.UpdateAsync(id, flatUpdateDto);
            if (result.Success)
                return Ok(result);
            else return NotFound(result);
        }
    }
}
