using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Repositories
{
    public class BikeRepository : IRepository<Bike,int>
    {
        private readonly DataContext context;

        public BikeRepository(DataContext context) => this.context = context;
        public async Task<IEnumerable<Bike>> GetAll()
        {
            return await context.Bikes.ToListAsync();
        }
        public async Task<Bike> GetById(int id)
        {
            return await context.Bikes.FindAsync(id);
        }
        public async Task<Bike> Insert(Bike entity)
        {
            await context.Bikes.AddAsync(entity);
            return entity;
        }
        public async Task Delete(int id)
        {
            var bike = await context.Bikes.FindAsync(id);
            if (bike != null)
            {
                context.Remove(bike);
            }
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }
    }
}
