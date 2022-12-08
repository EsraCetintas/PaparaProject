using Hangfire;
using PaparaProject.Application.Interfaces.Infrastructure;
using PaparaProject.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            RecurringJob.AddOrUpdate(() => SetMailAsync(), Cron.Daily);
        }

        public async Task SetMailAsync()
        {
            var unPaidInvoices = await _service.GetAllByPayFilterInvoicesAsync(false);
            Trace.TraceInformation("Hi");
        }

    }
}
