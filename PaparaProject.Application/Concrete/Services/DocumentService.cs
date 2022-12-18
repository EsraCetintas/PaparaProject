using Newtonsoft.Json;
using PaparaProject.Application.Aspects.Autofac.Security;
using PaparaProject.Application.Dtos.CardDto;
using PaparaProject.Application.Interfaces.Services;
using PaparaProject.Application.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Application.Concrete.Services
{
    public class DocumentService : IDocumentService
    {
        [SecuredOperationAspect("Admin,User")]
        public async Task<APIResult> CreateExcel(CreditCardDto creditCardDto)
        {
            var result =await PostAsync(creditCardDto);
            return result;
        }

        private async Task<APIResult> PostAsync(CreditCardDto creditCardDto)
        {
            HttpClient httpClient = new HttpClient();
            var body = new StringContent(JsonConvert.SerializeObject(creditCardDto).ToString(), Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("https://localhost:44327/api/documents/getexceldocument", body);

            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                return new APIResult { Success = false, Message = responseContent.ToString(), Data = null };

            return new APIResult { Success = true, Message = responseContent.ToString(), Data = null };
        }
    }
}
