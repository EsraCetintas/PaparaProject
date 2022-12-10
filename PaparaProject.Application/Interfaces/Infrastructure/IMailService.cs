using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Application.Interfaces.Infrastructure
{
    public interface IMailService
    {
        Task SendMailAsync(List<string> mailAdress);
    }
}
