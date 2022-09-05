using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Repositories
{
    public class StationRepository : IRepository<Station, int>
    {
        private readonly DataContext context;

        public StationRepository(DataContext context) => this.context = context;

        public async Task Delete(int id)
        {
            var station = await context.Stations.FindAsync(id);
            if (station != null)
            {
                context.Remove(station);
            }
        }

        public async Task<IEnumerable<Station>> GetAll()
        {
            return await context.Stations.ToListAsync();
        }

        public async Task<Station> GetById(int id)
        {
            return await context.Stations.FindAsync(id);
        }

        public async Task<Station> Insert(Station entity)
        {
            await context.Stations.AddAsync(entity);
            return entity;
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }
    }
}
