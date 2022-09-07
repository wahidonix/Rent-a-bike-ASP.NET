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
    public class HistoriesController : ControllerBase
    {
        public static History history = new History();

        private readonly IConfiguration _configuration;

        private readonly IRepository<History, int> historyRepository;

        public HistoriesController(IConfiguration configuration, IRepository<History, int> historyRepository)
        {
            this.historyRepository = historyRepository;
            _configuration = configuration;
        }

        // GET: api/Histories
        [HttpGet]
        public async Task<IActionResult> GetHistory()
        {
            return Ok(await historyRepository.GetAll());
        }

        // GET: api/Histories/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetHistory(int id)
        {
            return Ok(await historyRepository.GetById(id));
        }
        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetHistoryByUserId(int id)
        {
            var histories = await historyRepository.GetAll();
            var userHistory = histories.Where(x => x.UserId == id).ToList();
            return Ok(userHistory);
        }

        // PUT: api/Histories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHistory(int id, History history)
        {
            var historyDb = await historyRepository.GetById(id);
            historyDb.Returned = history.Returned;
            historyRepository.Save();
            return Ok(history);
        }

        // POST: api/Histories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> insertHistory(History history)
        {
            await historyRepository.Insert(history);
            await historyRepository.Save();
            return Ok(history);
        }

        // DELETE: api/Histories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHistory(int id)
        {
            await historyRepository.Delete(id);
            await historyRepository.Save();
            return Ok("Entry deleted");
        }

    }
}
