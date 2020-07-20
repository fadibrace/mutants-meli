using MutantsAPI.Models;

namespace MutantsAPI.Repositories
{
    public interface IStatsRepository
    {
        StatsModel GetGenomeStats();
        StatsModel RefreshGenomeStatsCache();
    }
}