using System;
using System.Linq;
using System.Threading.Tasks;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using MutantsAPI.Models;

namespace MutantsAPI.Repositories
{
    public class GenomeRepository : IGenomeRepository
    {
        private readonly ReadGenomeContext _readContext;
        private readonly CommandGenomeContext _commandContext;
        private readonly int _BatchSize = 2500;

        public GenomeRepository(CommandGenomeContext commandContext, ReadGenomeContext readContext)
        {
            _readContext = readContext;
            _commandContext = commandContext;
        }

        /// <summary>
        /// Checks if the given Genome exists in the db, and stores it if it doesn't.
        /// Returns true if a Genome was inserted.
        /// </summary>
        public async Task<bool> CreateGenome(Genome genome)
        {
            var result = 0;

            // Using a hash function to improve query performance is efficient but not perfect.
            // Though very unlikely, collisions may still happen. For simplicity's sake we are
            // ignoring that possibility for now
            var genomes = _commandContext.Genomes.AsNoTracking().Where(x => x.DnaHash == genome.DnaHash);
            if (genomes.Any())
                return false;
            await _commandContext.Genomes.AddAsync(genome);
            result = await _commandContext.SaveChangesAsync();

            return result > 0;
        }

        /// <summary>
        /// Moves batches of Genomes entries from the local Command storage to the
        /// Read database. If a Genome with the same hash already exists in the Read db
        /// it will be updated with the current data (we are ignoring collisions for now)
        /// </summary>
        public async Task<bool> LoadGenomeReadDb()
        {
            try
            {
                var genomesToLoad = _commandContext.Genomes.Take(_BatchSize).AsNoTracking();
                if (genomesToLoad.Any())
                {
                    await _readContext.BulkInsertOrUpdateAsync(genomesToLoad.ToList());
                    _commandContext.RemoveRange(genomesToLoad);
                    await _commandContext.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                // should log the error here
                return false;
            }
            return true;
        }
    }
}
