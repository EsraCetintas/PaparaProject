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
    public class EfUserRepository : RepositoryBase<User, AppDbContext>, IUserRepository
    {
        public async Task<List<OperationClaim>> GetClaims(User user)
        {
            using (var context = new AppDbContext())
            {
                var result = from operationClaim in context.OperationClaims
                             join userOperationClaim in context.UserOperationClaims
                                 on operationClaim.Id equals userOperationClaim.OperationClaimId
                             where userOperationClaim.UserId == user.Id
                             select new OperationClaim { Id = operationClaim.Id, RoleName = operationClaim.RoleName };
                return result.ToList();

            }
        }
    }
}
