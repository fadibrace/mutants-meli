using System;
using System.Threading.Tasks;
using MutantsAPI.Dtos;
using MutantsAPI.Models;

namespace MutantsAPI.Repositories
{
    public interface IGenomeRepository
    {
        Task<bool> CreateGenome(Genome genome);
        Task<bool> LoadGenomeReadDb();
    }
}
