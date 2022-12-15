using AutoMapper;
using Newtonsoft.Json;
using PaparaProject.Application.Dtos.CardDto;
using PaparaProject.Application.Dtos.DuesDtos;
using PaparaProject.Application.Interfaces.Services;
using PaparaProject.Application.Utilities.Results;
using PaparaProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PaparaProject.Application.Concrete.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IInvoiceService _invoiceService;
        private readonly IDuesService _duesService;
        public PaymentService(IInvoiceService invoiceService, IDuesService duesService)
        {
            _invoiceService = invoiceService;
            _duesService = duesService;
        }

        public async Task<APIResult> PayDuesAsync(int duesId, CardDto cardto)
        {
            var dues = await _duesService.GetDuesByIdAsync(duesId);

            if (dues is null)
            {
                return new APIResult { Success= false, Data = null, Message = "Not Found" };
            }
            else
            {
              
                DuesCreateDto duesCreateDto = new DuesCreateDto();
                duesCreateDto.AmountOfDues = dues.AmountOfDues;
                duesCreateDto.Deadline = dues.Deadline;
                duesCreateDto.CreatedBy = dues.CreatedBy;
                duesCreateDto.FlatId = dues.FlatId;
                cardto.PaymentAmount = dues.AmountOfDues;
                var result = await PostAsync(cardto);
                if (result.Success)
                {
                    duesCreateDto.PaymentDate = DateTime.Now;
                    var updatedDues = await _duesService.UpdateAsync(duesId, duesCreateDto);
                    return result;
                }
                else return result;

            }
        }

        public Task<APIResult> PayInvoiceAsync(int invoiceId, CardDto cardDto)
        {
            throw new NotImplementedException();
        }

        private async Task<APIResult> PostAsync(CardDto cardDto)
        {
            HttpClient httpClient = new HttpClient();
            var body = new StringContent(JsonConvert.SerializeObject(cardDto).ToString(), Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("https://localhost:44327/api/Payments/pay", body);

            var responseContent = await response.Content.ReadAsStringAsync();

            return new APIResult { Success = true, Message = responseContent.ToString(), Data = null };
        }
    }
}
