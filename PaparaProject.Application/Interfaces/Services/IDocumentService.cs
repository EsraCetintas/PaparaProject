using PaparaProject.Application.Dtos.CardDto;
using PaparaProject.Application.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Application.Interfaces.Services
{
    public interface IDocumentService
    {
        Task<APIResult> CreateExcel(CreditCardDto creditCardDto);
    }
}
