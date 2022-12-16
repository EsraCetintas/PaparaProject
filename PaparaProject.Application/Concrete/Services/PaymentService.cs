using AutoMapper;
using Newtonsoft.Json;
using PaparaProject.Application.Dtos.CardDto;
using PaparaProject.Application.Dtos.DuesDtos;
using PaparaProject.Application.Dtos.InvoiceDtos;
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

        public async Task<APIResult> PayDuesAsync(int duesId, CreditCardDto creditCardDto)
        {
            var dues = await _duesService.GetDuesByIdAsync(duesId);

            if (dues is null)
            {
                return new APIResult { Success = false, Data = null, Message = "Dues Not Found" };
            }
            else
            {

                DuesUpdateDto duesUpdateDto = new DuesUpdateDto();
                duesUpdateDto.AmountOfDues = dues.AmountOfDues;
                duesUpdateDto.Deadline = dues.Deadline;
                duesUpdateDto.FlatId = dues.FlatId;
                CardDto cardDto = new CardDto();
                cardDto.CreditCard.FullName = creditCardDto.FullName;
                cardDto.CreditCard.ExpirationDateMonth = creditCardDto.ExpirationDateMonth;
                cardDto.CreditCard.ExpirationDateYear = creditCardDto.ExpirationDateYear;
                cardDto.CreditCard.CardNo = creditCardDto.CardNo;
                cardDto.CreditCard.CVV = creditCardDto.CVV;
                cardDto.PaymentAmount = dues.AmountOfDues;
                var result = await PostAsync(cardDto);
                if (result.Success)
                {
                    duesUpdateDto.PaymentDate = DateTime.Now;
                    await _duesService.UpdateAsync(duesId, duesUpdateDto);
                    return result;
                }
                else return result;

            }
        }

        public async Task<APIResult> PayInvoiceAsync(int invoiceId, CreditCardDto creditCardDto)
        {
            var invoice = await _invoiceService.GetInvoiceByIdAsync(invoiceId);

            if (invoice is null)
            {
                return new APIResult { Success = false, Data = null, Message = "Invoice Not Found" };
            }
            else
            {

                InvoiceUpdateDto invoiceUpdateDto = new InvoiceUpdateDto();
                invoiceUpdateDto.AmountOfInvoice = invoice.AmountOfInvoice;
                invoiceUpdateDto.Deadline = invoice.Deadline;
                invoiceUpdateDto.FlatId = invoice.FlatId;
                CardDto cardDto = new CardDto();
                cardDto.CreditCard.FullName = creditCardDto.FullName;
                cardDto.CreditCard.ExpirationDateMonth = creditCardDto.ExpirationDateMonth;
                cardDto.CreditCard.ExpirationDateYear = creditCardDto.ExpirationDateYear;
                cardDto.CreditCard.CardNo = creditCardDto.CardNo;
                cardDto.CreditCard.CVV = creditCardDto.CVV;
                cardDto.PaymentAmount = invoice.AmountOfInvoice;
                var result = await PostAsync(cardDto);
                if (result.Success)
                {
                    invoiceUpdateDto.PaymentDate = DateTime.Now;
                    await _invoiceService.UpdateAsync(invoiceId, invoiceUpdateDto);
                    return result;
                }
                else return result;

            }
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
