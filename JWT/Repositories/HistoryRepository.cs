using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Repositories
{
    public class HistoryRepository : IRepository<History, int>
    {
        private readonly DataContext context;
        public HistoryRepository(DataContext context) => this.context = context;
        public async Task Delete(int id)
        {
            var history = await context.Histories.FindAsync(id);
            if (history != null)
            {
                context.Remove(history);
            }
        }

        public async Task<IEnumerable<History>> GetAll()
        {
            return await context.Histories.ToListAsync();
        }

        public async Task<History> GetById(int id)
        {
            return await context.Histories.FindAsync(id);
        }

        public async Task<History> Insert(History entity)
        {
            await context.Histories.AddAsync(entity);
            return entity;
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }
    }
}
