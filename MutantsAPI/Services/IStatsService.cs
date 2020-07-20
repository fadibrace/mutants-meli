using MutantsAPI.Models;

namespace MutantsAPI.Domains
{
    public interface IStatsService
    {
         StatsModel GetStats();
    }
}