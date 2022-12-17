using PaparaProject.Infrastructure.Excel;
using PaparaProject.Infrastructure.PaymentService.Dtos.Commons;
using PaparaProject.Infrastructure.PaymentService.Repositories.Interfaces;
using PaparaProject.Infrastructure.PaymentService.Services.Interfaces;
using PaparaProject.Infrastructure.PaymentService.Utilities.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Infrastructure.PaymentService.Services.Concrete
{
    public class DocumentService: IDocumentService
    {
        private readonly ICardService _cardService;
        private readonly ICardActivityService _cardActivityService;
        private readonly IExcelService _excelService;

        public DocumentService(ICardService cardService, ICardActivityService cardActivityService, IExcelService excelService)
        {
            _cardService = cardService;
            _cardActivityService = cardActivityService;
            _excelService = excelService;
        }

        public async Task<CardServiceResult> CreateExcel(CreditCardModel creditCardModel)
        {
            var result = await _cardService.FindByCreditCardParams(creditCardModel);

            if (result == null)
                return new CardServiceResult(false, "Kart Bulunamadı.");

            var cardActivities = await _cardActivityService.GetCardByIdAsync(result.Value);

            await _excelService.CreateCardActivitiesExcel(cardActivities);

            return new CardServiceResult(true, "");
        }


    }
}
