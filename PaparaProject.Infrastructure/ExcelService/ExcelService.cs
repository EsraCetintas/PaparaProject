using Microsoft.Office.Interop.Excel;
using PaparaProject.Application.Utilities.Results;
using PaparaProject.Domain.Entities;
using System;
using System.Threading.Tasks;
using Range = Microsoft.Office.Interop.Excel.Range;
using OfficeOpenXml;
using System.Linq;
using System.Runtime.InteropServices;
using System.IO;
using System.Collections.Generic;
using PaparaProject.Application.Dtos.InvoiceDtos;
using PaparaProject.Application.Dtos.DuesDtos;
using PaparaProject.Infrastructure.PaymentService.Model;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace PaparaProject.Infrastructure.Excel
{
    public class ExcelService : IExcelService
    {
        public async Task<APIResult> CreateCardActivitiesExcel(List<CardActivity> cardActivities)
        {
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();

            excel.Visible = true;
            object Missing = Type.Missing;
            Workbook workbook = excel.Workbooks.Add(Missing);

            Worksheet sheet1 = (Worksheet)workbook.Sheets[1];
            int startingColumn = 1;
            int startingRow = 1;

            ((Range)sheet1.Cells[startingRow, startingColumn + 0]).Value2 = "Old Balance";
            ((Range)sheet1.Cells[startingRow, startingColumn + 1]).Value2 = "New Balance";
            ((Range)sheet1.Cells[startingRow, startingColumn + 2]).Value2 = "Activity Date";

            foreach (var item in cardActivities.Select((value, i) => new { i, value }))
            {
                int row = item.i + 1;

                Range column1Row1 = (Range)sheet1.Cells[startingRow + row, startingColumn + 0];
                column1Row1.Value2 = item.value.OldBalance.ToString();
                column1Row1.Select();

                Range column2Row2 = (Range)sheet1.Cells[startingRow + row, startingColumn + 1];
                column2Row2.Value2 = item.value.NewBalance.ToString(); ;
                column2Row2.Select();

                Range column3Row3 = (Range)sheet1.Cells[startingRow + row, startingColumn + 2];
                column3Row3.Value2 = item.value.ActivityDate.ToString(); ;
                column3Row3.Select();

            }
            return new APIResult { Success = true, Message = "Excel Created", Data = null };
        }
    }
}
