using PaparaProject.Application.Dtos.DuesDtos;
using PaparaProject.Application.Dtos.InvoiceDtos;
using PaparaProject.Application.Utilities.Results;
using PaparaProject.Infrastructure.PaymentService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Infrastructure.Excel
{
    public interface IExcelService
    {
        Task<APIResult> CreateCardActivitiesExcel(List<CardActivity> cardActivities);

    }
}
