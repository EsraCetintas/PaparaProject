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
using System.Net;
using System.Threading;
using PaparaProject.Application.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace PaparaProject.Infrastructure.MailService.Hangfire
{
    public class MailService : IMailService
    {

        public async Task SendMailAsync(List<string> mailAdress)
        {

            RecurringJob.AddOrUpdate(() => CreateMailAsync(mailAdress), Cron.Daily);
        }

        public void CreateMailAsync(List<string> mailAdress)
        {
            //foreach (var adress in mailAdress)
            //{
            //    SmtpClient sc = new SmtpClient();
            //    sc.Port = 587;
            //    sc.Host = "smtp.outlook.com";
            //    sc.EnableSsl = true;
            //    sc.Credentials = new NetworkCredential("esra.cetintas000@outlook.com", "12345Papara");

            //    MailMessage mail = new MailMessage();
            //    mail.From = new MailAddress("esra.cetintas000@outlook.com", "Esra Çetintaş");
            //    mail.To.Add(adress);
            //    mail.Subject = "Ödenmemiş Faturalar";
            //    mail.IsBodyHtml = true;
            //    mail.Body = "Ödenmemiş faturanız bulunmaktadır. Lütfen son ödeme tarihinden önce faturanızı ödeyiniz.";

            //    sc.Send(mail);

            //    Thread.Sleep(1000);
            //}

        }

    }
}
