using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using PaparaProject.Infrastructure.PaymentService.Dtos.PaymentDtos;
using PaparaProject.Infrastructure.PaymentService.Model;
using PaparaProject.Infrastructure.PaymentService.Services.Interfaces;
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

        [HttpPost("pay")]
        public async Task<IActionResult> Pay(PaymentPayDto paymentPayDto)
        {
            var result = await _paymentService.Pay(paymentPayDto);

            if(!result.Result)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpGet]
        public IActionResult Deneme()
        {
            var settings = MongoClientSettings.FromConnectionString("mongodb+srv://EsraCetintas:135790@paymentcluster.ri9rjjy.mongodb.net/?retryWrites=true&w=majority");
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("PaymentDb");
            var collection = database.GetCollection<Card>("Cards");
            return Ok();
        }
    }
}
