using PaparaProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Application.Interfaces.Persistence.Repositories
{
    public interface IUserRepository : IEntityRepository<User>
    {
        Task<List<OperationClaim>> GetClaims(User user);
    }
}
