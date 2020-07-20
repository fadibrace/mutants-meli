using MutantsAPI.Models;
using MutantsAPI.Repositories;

namespace MutantsAPI.Domains
{
    public class StatsService : IStatsService
    {
        private readonly IStatsRepository _statsRepository;

        public StatsService(IStatsRepository statsRepository)
        {
            _statsRepository = statsRepository;
        }

        public StatsModel GetStats()
        {
            var results = _statsRepository.GetGenomeStats();
            return results;
        }
    }
}
