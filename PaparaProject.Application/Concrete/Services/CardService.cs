using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PaparaProject.Application.Dtos.CardDto;
using PaparaProject.Application.Interfaces.Services;
using PaparaProject.Application.Utilities.Results;
using PaparaProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Application.Concrete.Services
{
    public class CardService : ICardService
    {
        public Task<APIResult> SendCardAddRequest(CardCreateDto cardCreateDto)
        {
            var result = this.AddPostAsync(cardCreateDto);
            return result;
        }

        public Task<APIResult> DeleteAsync(string cardNo)
        {
            var result = this.DeletePostAsync(cardNo);
            return result;
        }

        private async Task<APIResult> AddPostAsync(CardCreateDto cardCreateDto)
        {
            HttpClient httpClient = new HttpClient();
            var body = new StringContent(JsonConvert.SerializeObject(cardCreateDto).ToString(), Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("https://localhost:44327/api/cards/add", body);

            var responseContent = await response.Content.ReadAsStringAsync();

            if(response.StatusCode != System.Net.HttpStatusCode.OK)
                return new APIResult { Success = false, Message = responseContent.ToString(), Data = null };

            return new APIResult { Success = true, Message = responseContent.ToString(), Data = null };
        }

        private async Task<APIResult> DeletePostAsync(string cardNo)
        {
            HttpClient httpClient = new HttpClient();

            var response = await httpClient.DeleteAsync($"https://localhost:44327/api/cards/delete?cardNo={cardNo}");

            JObject responseContent = (JObject)JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());
            var b = (string)responseContent["Message"];
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                return new APIResult { Success = false, Message = (string)responseContent["Message"], Data = null };

            return new APIResult { Success = true, Message = (string)responseContent["Message"], Data = null };
        }
    }
}
