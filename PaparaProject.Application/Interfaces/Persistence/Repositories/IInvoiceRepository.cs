using PaparaProject.Application.Dtos.InvoiceDtos;
using PaparaProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Application.Interfaces.Persistence.Repositories
{
    public interface IInvoiceRepository : IEntityRepository<Invoice>
    {
        
    }
}
