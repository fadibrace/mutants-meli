using Microsoft.AspNetCore.Mvc;
using MutantsAPI.Domains;
using MutantsAPI.Models;

namespace MutantsAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StatsController : Controller
    {
        private readonly IStatsService _statsService;

        public StatsController(IStatsService statsService)
        {
            _statsService = statsService;
        }

        [HttpGet]
        public ActionResult<StatsModel> GetGenomesStats()
        {
            return _statsService.GetStats();
        }
    }
}
