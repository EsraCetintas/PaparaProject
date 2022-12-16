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

namespace PaparaProject.Infrastructure.Excel
{
    public class ExcelService : IExcelService
    {
        public async Task<APIResult> CreateInvoicesExcel(InvoiceDto invoice)
        {

            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            string paymentDate = invoice.PaymentDate.ToString();
            string deadline = invoice.Deadline.ToString();

            //Uygulama oluşturuyoruz.
            excel.Visible = true;
            //Uygulamayı başlatıyoruz.
            object Missing = Type.Missing;
            Workbook workbook = excel.Workbooks.Add(Missing);
            //Çalışma düzenimizi oluşturuyoruz.
            Worksheet sheet1 = (Worksheet)workbook.Sheets[1];
            int BaslangicKolon = 1;
            int BaslangicSatir = 1;
            //** Kolon Ekliyoruz **
            ((Range)sheet1.Cells[BaslangicSatir, BaslangicKolon + 0]).Value2 = "Amount Of İnvoice";
            ((Range)sheet1.Cells[BaslangicSatir, BaslangicKolon + 1]).Value2 = "Payment Date";
            ((Range)sheet1.Cells[BaslangicSatir, BaslangicKolon + 2]).Value2 = "Deadline";
            ((Range)sheet1.Cells[BaslangicSatir, BaslangicKolon + 3]).Value2 = "Invoice Type";
            ((Range)sheet1.Cells[BaslangicSatir, BaslangicKolon + 4]).Value2 = "Block No";
            ((Range)sheet1.Cells[BaslangicSatir, BaslangicKolon + 5]).Value2 = "Floor No";
            ((Range)sheet1.Cells[BaslangicSatir, BaslangicKolon + 6]).Value2 = "Flat No";

            //**                 **
            //** Satır Ekliyoruz **

            Range Kolon1Satir1 = (Range)sheet1.Cells[BaslangicSatir + 1, BaslangicKolon + 0];
            Kolon1Satir1.Value2 = invoice.AmountOfInvoice;
            Kolon1Satir1.Select();

            Range Kolon2Satir1 = (Range)sheet1.Cells[BaslangicSatir + 1, BaslangicKolon + 1];
            Kolon2Satir1.Value2 = paymentDate;
            Kolon2Satir1.Select();

            Range Kolon3Satir1 = (Range)sheet1.Cells[BaslangicSatir + 1, BaslangicKolon + 2];
            Kolon3Satir1.Value2 = deadline;
            Kolon3Satir1.Select();

            Range Kolon4Satir1 = (Range)sheet1.Cells[BaslangicSatir + 1, BaslangicKolon + 3];
            Kolon3Satir1.Value2 = invoice.InvoiceType;
            Kolon3Satir1.Select();

            Range Kolon5Satir1 = (Range)sheet1.Cells[BaslangicSatir + 1, BaslangicKolon + 4];
            Kolon3Satir1.Value2 = invoice.Flat.BlockNo;
            Kolon3Satir1.Select();

            Range Kolon6Satir1 = (Range)sheet1.Cells[BaslangicSatir + 1, BaslangicKolon + 5];
            Kolon3Satir1.Value2 = invoice.Flat.FloorNo;
            Kolon3Satir1.Select();

            Range Kolon7Satir1 = (Range)sheet1.Cells[BaslangicSatir + 1, BaslangicKolon + 6];
            Kolon3Satir1.Value2 = invoice.Flat.FlatNo;
            Kolon3Satir1.Select();

            return new APIResult { Success = true, Message = "Excel Created", Data = null };

        }

        public async Task<APIResult> CreateDuesExcel(DuesDto dues)
        {
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();

            //Uygulama oluşturuyoruz.
            excel.Visible = true;
            //Uygulamayı başlatıyoruz.
            object Missing = Type.Missing;
            Workbook workbook = excel.Workbooks.Add(Missing);
            //Çalışma düzenimizi oluşturuyoruz.
            Worksheet sheet1 = (Worksheet)workbook.Sheets[1];
            int BaslangicKolon = 1;
            int BaslangicSatir = 1;
            //** Kolon Ekliyoruz **
            ((Range)sheet1.Cells[BaslangicSatir, BaslangicKolon + 0]).Value2 = "Amount Of Dues";
            ((Range)sheet1.Cells[BaslangicSatir, BaslangicKolon + 1]).Value2 = "Payment Date";
            ((Range)sheet1.Cells[BaslangicSatir, BaslangicKolon + 2]).Value2 = "Deadline";
            ((Range)sheet1.Cells[BaslangicSatir, BaslangicKolon + 3]).Value2 = "Block No";
            ((Range)sheet1.Cells[BaslangicSatir, BaslangicKolon + 4]).Value2 = "Floor No";
            ((Range)sheet1.Cells[BaslangicSatir, BaslangicKolon + 5]).Value2 = "Flat No";


            //**                 **
            //** Satır Ekliyoruz **
            Range Kolon1Satir1 = (Range)sheet1.Cells[BaslangicSatir + 1, BaslangicKolon + 0];
            Kolon1Satir1.Value2 = dues.AmountOfDues;
            Kolon1Satir1.Select();

            Range Kolon2Satir1 = (Range)sheet1.Cells[BaslangicSatir + 1, BaslangicKolon + 1];
            Kolon2Satir1.Value2 = dues.PaymentDate;
            Kolon2Satir1.Select();

            Range Kolon3Satir1 = (Range)sheet1.Cells[BaslangicSatir + 1, BaslangicKolon + 2];
            Kolon3Satir1.Value2 = dues.Deadline;
            Kolon3Satir1.Select();

            Range Kolon4Satir1 = (Range)sheet1.Cells[BaslangicSatir + 1, BaslangicKolon + 3];
            Kolon1Satir1.Value2 = dues.Flat.BlockNo;
            Kolon1Satir1.Select();

            Range Kolon5Satir1 = (Range)sheet1.Cells[BaslangicSatir + 1, BaslangicKolon + 4];
            Kolon2Satir1.Value2 = dues.Flat.FloorNo;
            Kolon2Satir1.Select();

            Range Kolon6Satir1 = (Range)sheet1.Cells[BaslangicSatir + 1, BaslangicKolon + 5];
            Kolon3Satir1.Value2 = dues.Flat.FlatNo;
            Kolon3Satir1.Select();

            return new APIResult { Success = true, Message = "Excel Created", Data = null };
        }
    }
}
