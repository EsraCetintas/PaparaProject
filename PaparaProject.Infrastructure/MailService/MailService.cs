using Hangfire;
using PaparaProject.Application.Interfaces.Infrastructure;
using PaparaProject.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using Microsoft.Extensions.Hosting;

namespace PaparaProject.Infrastructure.MailService
{
    public class MailService : IMailService
    {
        readonly IInvoiceService _service;

        public MailService(IInvoiceService service)
        {
            _service = service;
        }

        public async Task SendMailAsync()
        {
            RecurringJob.AddOrUpdate(() => CreateMailAsync(), Cron.Daily);
        }

        public async Task CreateMailAsync()
        {
            var unPaidInvoices = await _service.GetAllByPayFilterInvoicesAsync(false);

            //Mail mesajimi olusturabilmek için MailMessage sinifi türünden bir degisken olusturmamiz gerekmektedir.
            MailMessage ePosta = new MailMessage();
            //E-Posta'nin kimden gönderilecegi bilgisini tutar. MailAddress türünden bir degisken istemektedir.
            ePosta.From = new MailAddress("esracetoo58@gmail.com");
            //E-Postanin kime/kimlere gönderilecegi bilgisini tutar.
            ePosta.To.Add("esra.cetintass34@gmail.com");
            //E-Posta'nin konusu bilgisini tutar.
            ePosta.Subject = "Deneme";
            //E - Posta'nin içerik bilgisini tutar.
            ePosta.Body = "Deneme-Mail";
            // E-Posta'nin gönderilecegi SMTP sunucu ve gönderen kullanicinin bilgilerinin
            // yazilip, MailMessage türünde olusturulan mailin gönderildigi siniftir.
            SmtpClient smtp = new SmtpClient();
            //E - Posta'yi gönderen kullanicinin kimlik bilgilerini tutar.
            smtp.Credentials = new System.Net.NetworkCredential("esracetoo58@gmail.com", "sifre");

            smtp.Port = 25;

            smtp.Host = "smtp.esracetoo58@gmail.com";

            //E - Posta'yi asenkron olarak gönderir. Yani e-posta gönderilene kadar çalisan
            //thread kapanmaz, gönderme islemi tamamlandiktan sonra kapatilir.
            smtp.SendAsync(ePosta, (object)ePosta);

            smtp.Send(ePosta);

        }

    }
}
