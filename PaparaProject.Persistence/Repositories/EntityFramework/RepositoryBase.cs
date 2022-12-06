using Microsoft.EntityFrameworkCore;
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
    public class RepositoryBase<T, TContext> : IEntityRepository<T>
       where T : class
       where TContext : DbContext, new()
    {
        //public readonly IUnitOfWork _unitOfWork;
        //public EfEntityRepository(IUnitOfWork unitOfWork)
        //{
        //    _unitOfWork = unitOfWork;
        //}

        public async Task AddAsync(T entity)
        {
            using (TContext context = new TContext())
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;
                //await _unitOfWork.Commit();
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(T entity)
        {
            using (TContext context = new TContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null)
        {
            using (TContext context = new TContext())
            {
                return filter == null
                    ? await context.Set<T>().ToListAsync()
                    : await context.Set<T>().Where(filter).ToListAsync();
            }
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter)
        {
            using (TContext context = new TContext())
            {
                return await context.Set<T>().SingleOrDefaultAsync(filter);
            }
        }

        public async Task UpdateAsync(T entity)
        {
            using (TContext context = new TContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
        }
    }
}
