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
    public class StationsController : ControllerBase
    {
        public static Station bikes = new Station();

        private readonly IConfiguration _configuration;

        private readonly IRepository<Station, int> stationRepository;

        public StationsController(IConfiguration configuration, IRepository<Station, int> stationRepository)
        {
            this.stationRepository = stationRepository;
            _configuration = configuration;
        }

        // GET: api/Stations
        [HttpGet]
        public async Task<IActionResult> GetStations()
        {
          return Ok(await stationRepository.GetAll());
        }

        // GET: api/Stations/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStation(int id)
        {
            return Ok(await stationRepository.GetById(id));
        }

        // PUT: api/Stations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBike(int id, Station station)
        {
            var dbStation = await stationRepository.GetById(id);
            dbStation.Location = station.Location;
            stationRepository.Save();
            return Ok(dbStation);
        }

        // POST: api/Stations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> insertStation(Station station)
        {
            await stationRepository.Insert(station);
            await stationRepository.Save();
            return Ok(station);
        }

        // DELETE: api/Bikes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStation(int id)
        {
            await stationRepository.Delete(id);
            await stationRepository.Save();
            return Ok("Station deleted");
        }

    }
}
