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
    public class FlatTypesController : ControllerBase
    {
        readonly IFlatTypeService _service;

        public FlatTypesController(IFlatTypeService service)
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
        public async Task<IActionResult> Add(FlatTypeDto flatTypeDto)
        {
            var result = await _service.AddAsync(flatTypeDto);
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
        public async Task<IActionResult> Update([FromQuery] int id, FlatTypeDto flatTypeDto)
        {
            var result = await _service.UpdateAsync(id, flatTypeDto);
            if (result.Success)
                return Ok(result);
            else return NotFound(result);
        }
    }
}
