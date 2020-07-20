using System.Threading.Tasks;
using MutantsAPI.Dtos;
using MutantsAPI.Models;

namespace MutantsAPI.Domains
{
    public interface IGenomeService
    {
        Task<bool> ProcessDna(Dna dna);
        bool IsMutant(string[] dna);
        Task<Genome> CreateGenome(Genome genome);
    }
}
