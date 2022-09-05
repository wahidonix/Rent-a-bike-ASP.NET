using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JWT;
using JWT.Data;
using JWT.Repositories;

namespace JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BikesController : ControllerBase
    {
        public static Bike bikes = new Bike();

        private readonly IConfiguration _configuration;

        private readonly IRepository<Bike, int> bikeRepository;

        public BikesController(IConfiguration configuration, IRepository<Bike, int> bikeRepository)
        {
            this.bikeRepository = bikeRepository;
            _configuration = configuration;
        }

        // GET: api/Bikes
        [HttpGet]
        public async Task<IActionResult> GetBikes()
        {
            return Ok(await bikeRepository.GetAll());
        }

        // GET: api/Bikes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBike(int id)
        {
            return Ok(await bikeRepository.GetById(id));
        }

        // PUT: api/Bikes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /*[HttpPut("{id}")]
        public async Task<IActionResult> PutBike(int id, Bike bike)
        {
            if (id != bike.Id)
            {
                return BadRequest();
            }

            _context.Entry(bike).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BikeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }*/

        // POST: api/Bikes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> insertBikes(Bike bike)
        {
            await bikeRepository.Insert(bike);
            await bikeRepository.Save();
            return Ok(bike);
        }

        // DELETE: api/Bikes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBike(int id)
        {
            await bikeRepository.Delete(id);
            await bikeRepository.Save();
            return Ok("Bike deleted");
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBike(int id, Bike bike)
        {
            var dbBike = await bikeRepository.GetById(id);
            dbBike.StationId = bike.StationId;
            dbBike.Repair = bike.Repair;
            dbBike.Code = bike.Code;
            bikeRepository.Save();
            return Ok(dbBike);
        }
    }
}
