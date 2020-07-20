using System;
using System.Threading.Tasks;
using Hangfire;
using MutantsAPI.Repositories;

namespace MutantsAPI.Utils
{
    public class HousekeeperJob : IHousekeeperJob
    {
        private static IGenomeRepository _genomeRepository;
        public HousekeeperJob(IGenomeRepository genomeRepository)
        {
            _genomeRepository = genomeRepository;
        }

        public async Task LoadReadDbJob()
        {
            await _genomeRepository.LoadGenomeReadDb();
            BackgroundJob.Schedule(() => LoadReadDbJob(), TimeSpan.FromSeconds(new Random().Next(5, 11)));
        }
    }
}
