using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaparaProject.Infrastructure.Excel;
using PaparaProject.Infrastructure.PaymentService.Dtos.Commons;
using PaparaProject.Infrastructure.PaymentService.Services.Interfaces;

namespace PaparaProject.PaymentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _documentService;

        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        [HttpPost("getexceldocument")]
        public IActionResult GetExcelDocument(CreditCardModel creditCardModel)
        {
            _documentService.CreateExcel(creditCardModel);

            return Ok();
        }
    }
}
