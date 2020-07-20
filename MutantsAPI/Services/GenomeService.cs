using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MutantsAPI.Dtos;
using MutantsAPI.Models;
using MutantsAPI.Repositories;

namespace MutantsAPI.Domains
{
    public class GenomeService : IGenomeService
    {
        private readonly IGenomeRepository _genomeRepository;

        public GenomeService()
        {
        }

        public GenomeService(IGenomeRepository genomeRepository)
        {
            _genomeRepository = genomeRepository;
        }

        /// <summary>
        /// Handles the logic for the 'PostMutant' endpoint.
        /// Checks if the provided DNA is mutant, creates
        /// the corresponding genome, and returns true if it's mutant
        /// </summary>
        public async Task<bool> ProcessDna(Dna dna)
        {

            var isMutant = IsMutant(dna.DnaMatrix);
            var genome = new Genome(dna.DnaMatrix, isMutant);

            //_genomeRepository.CreateGenomeEnqueued(genome);
            await CreateGenome(genome);
            return isMutant;
        }

        /// <summary>
        /// Checks if a given DNA matrix contains more than one 4 equal letters sequence,
        /// in vertical, horizontal, or diagonal direction
        /// </summary>
        public bool IsMutant(string[] dna)
        {
            var sequencesFound = 0;
            List<Func<string[], int, int, int>> sequenceChecksList = new List<Func<string[], int, int, int>>
            {
                CheckHorizontalSequence,
                CheckVerticalSequence,
                CheckLRDiagonalSequence,
                CheckRLDiagonalSequence
            };

            for (var i = 0; i < dna.Length; i++)
            {
                for (var j = 0; j < dna.Length; j++)
                {
                    foreach(var checkSequence in sequenceChecksList)
                    {
                        sequencesFound += checkSequence(dna, i, j);
                        if (sequencesFound > 1) return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Tries to store a new Genome in the db by calling the GenomeRepository
        /// </summary>
        public async Task<Genome> CreateGenome(Genome genome)
        {
            var success = await _genomeRepository.CreateGenome(genome);
            if (success)
                return genome;
            else
                return null;

        }

        // Helper functions to check if a given position in a matrix is a 4 same letters sequence 
        private int CheckHorizontalSequence(string[] dna, int i, int j)
        {
            if (j + 3 < dna.Length)
                if (dna[i][j] == dna[i][j + 1] && dna[i][j] == dna[i][j + 2] && dna[i][j] == dna[i][j + 3])
                    return 1;
            return 0;
        }
        private int CheckVerticalSequence(string[] dna, int i, int j)
        {
            if (i + 3 < dna.Length)
                if (dna[i][j] == dna[i + 1][j] && dna[i][j] == dna[i + 2][j] && dna[i][j] == dna[i + 3][j])
                    return 1;
            return 0;
        }
        private int CheckLRDiagonalSequence(string[] dna, int i, int j)
        {
            if (i + 3 < dna.Length && j + 3 < dna.Length)
                if (dna[i][j] == dna[i + 1][j + 1] && dna[i][j] == dna[i + 2][j + 2] && dna[i][j] == dna[i + 3][j + 3])
                    return 1;
            return 0;
        }
        private int CheckRLDiagonalSequence(string[] dna, int i, int j)
        {
            if (i - 3 >= 0 && j + 3 < dna.Length)
                if (dna[i][j] == dna[i - 1][j + 1] && dna[i][j] == dna[i - 2][j + 2] && dna[i][j] == dna[i - 3][j + 3])
                    return 1;
            return 0;
        }
    }
}
