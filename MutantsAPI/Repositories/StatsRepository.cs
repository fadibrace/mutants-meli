using System;
using System.Linq;
using MutantsAPI.Dtos;
using MutantsAPI.Models;

namespace MutantsAPI.Repositories
{
    public class StatsRepository : IStatsRepository
    {
        private readonly ReadGenomeContext _readContext;
        private readonly CommandGenomeContext _commandContext;
        public StatsRepository(ReadGenomeContext readContext, CommandGenomeContext commandContext)
        {
            _readContext = readContext;
            _commandContext = commandContext;
        }

        /// <summary>
        /// Returns a Stat object with the numbers of human genomes,
        /// mutant genomes, and the resulting mutant genome ratio.
        /// Results maybe cached up to 30 seconds
        /// </summary>
        public StatsModel GetGenomeStats()
        {
            var cachedStats = _commandContext.StatsModels.Find(1);
            if (cachedStats == null || cachedStats.Changed < DateTime.UtcNow.AddSeconds(-30))
                return RefreshGenomeStatsCache();
            return cachedStats;
        }

        /// <summary>
        /// Refresh (or creates) the GenomeStats cache with current data
        /// </summary>
        public StatsModel RefreshGenomeStatsCache()
        {
            var cachedStats = _commandContext.StatsModels.Find(1);
            if (cachedStats == null)
            {
                cachedStats = new StatsModel() { Id = 1};
                _commandContext.StatsModels.Add(cachedStats);
                _commandContext.SaveChanges();
            }
                
            var newStats = _readContext.Genomes.GroupBy(genome => genome.IsMutant)
                        .Select(group => new
                        {
                            IsMutant = group.Key,
                            Count = group.LongCount()
                        }).OrderBy(x => x.IsMutant).ToList();

            if (newStats.Any())
            {
                if (newStats.FirstOrDefault(x => x.IsMutant) != null)
                    cachedStats.MutantCount = newStats.First(x => x.IsMutant).Count;
                if (newStats.FirstOrDefault(x => !x.IsMutant) != null)
                    cachedStats.HumanCount = newStats.First(x => !x.IsMutant).Count;
                cachedStats.Ratio = (double)cachedStats.MutantCount / (cachedStats.HumanCount + cachedStats.MutantCount);
                cachedStats.Changed = DateTime.UtcNow;
            }

            _commandContext.StatsModels.Update(cachedStats);
            _commandContext.SaveChanges();
            return cachedStats;
        }
    }
}
