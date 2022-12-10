using AutoMapper;
using PaparaProject.Application.Interfaces.Persistence.Repositories;
using PaparaProject.Domain.Entities;
using PaparaProject.Persistence.Context.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Persistence.Repositories.EntityFramework
{
    public class EfFlatTypeRepository : RepositoryBase<FlatType, AppDbContext>, IFlatTypeRepository
    {
       
    }
}
