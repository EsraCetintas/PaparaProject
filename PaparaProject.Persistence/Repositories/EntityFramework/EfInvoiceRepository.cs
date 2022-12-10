using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PaparaProject.Application.Dtos.InvoiceDtos;
using PaparaProject.Application.Interfaces.Persistence.Repositories;
using PaparaProject.Domain.Entities;
using PaparaProject.Persistence.Context.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Persistence.Repositories.EntityFramework
{
    public class EfInvoiceRepository : RepositoryBase<Invoice, AppDbContext>, IInvoiceRepository

    {
        
    }
}
