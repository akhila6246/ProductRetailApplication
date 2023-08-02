using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductRetailApp.Core.Interfaces;

namespace ProductRetailApp.Infrastructure.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly DbContextClass _dbContext;

        protected GenericRepository(DbContextClass context)
        {
            _dbContext = context;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }
    }
}
